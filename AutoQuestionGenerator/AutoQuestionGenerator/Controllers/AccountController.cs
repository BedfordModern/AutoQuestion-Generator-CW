using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoQuestionGenerator.Accounts;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Models;
using AutoQuestionGenerator.Models.Hubs;
using AutoQuestionGenerator.QuestionModels.Interpreter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AutoQuestionGenerator.Controllers
{
    /// <summary>
    /// This controller holds the methods for updating
    /// viewing and controlling the infomration of the user.
    /// </summary>
    [Authorized]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        // Setup class wide variables for the program.
        IHubContext<FileHub> HubContext;
        PythonInterpreter pythonInterpreter;
        private IHostingEnvironment _env;
        public AccountController(IHubContext<FileHub> hubcontext, IHostingEnvironment env)
        {
            // Use the variables that are passed into the controllers to setup my information.
            HubContext = hubcontext;
            _env = env;
        }

        /// <summary>
        /// Gets the information about the logged in user and returns
        /// it to them for them to view.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(UserHelper.GetUser(UserHelper.GetUserId(HttpContext.Session)));
        }

        /// <summary>
        /// If the user wants to update details about their accounts
        /// we need them to validate that they are actully the user
        /// before they can update their details.
        /// </summary>
        /// <param name="user">The User information from the page</param>
        /// <returns>Page to revalidate the user</returns>
        [HttpPost]
        public IActionResult Index(Users user)
        {
            var model = new RevalidateModel()
            {
                id = user.UserID,
                AspAction = "Revalidate",
                AspController = "Account"
            };
            return View("Revalidate", model);
        }

        /// <summary>
        /// After the user has attempted to revalidate
        /// we need to check it they are who they say
        /// that they are. If they are not we log them out
        /// and if they are send them tto the update page.
        /// </summary>
        /// <param name="model">The revalidation information</param>
        /// <returns>A page or redirect depending on the password</returns>
        [HttpPost]
        public IActionResult Revalidate(RevalidateModel model)
        {
            var user = UserHelper.GetUser(model.id);

            // Uses the hasher to compare the user's password and the
            // password that they just entered.
            if (Hasher.ValidatePassword(model.Password, user.Password))
            {
                // Let them update their details if they got it right.
                UpdateUser ret = new UpdateUser()
                {
                    UserID = user.UserID,
                    Username = user.Username,
                    Firstname = user.First_Name,
                    Lastname = user.Last_Name
                };
                return View("Update", ret);
            }
            // If the user got the password wrong
            // they probably aren't the user
            // so log them out.
            UserHelper.LogOut(HttpContext.Session);
            OrganisationHelper.LogOut(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// Updates the users accounts details if they
        /// have changed any of them.
        /// It also allows them to
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(UpdateUser model)
        {
            // Check if the user has enetered correct details
            if (!ModelState.IsValid)
                return View(model);

            // Check that the user hasn't passed a XSS attack
            // or tried to update form data.
            if (UserHelper.GetUserId(HttpContext.Session) == model.UserID)
            {
                var user = UserHelper.GetUser(model.UserID);

                // Checks if the user has updated their password
                if (!string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrWhiteSpace(model.VerifyPassword))
                {
                    // If the password and verification are not the same
                    // return the model with an error.
                    if(model.Password != model.VerifyPassword)
                    {
                        return View(model);
                    }
                    // if they are update the users password.
                    user.Password = Hasher.Hash(model.Password);
                }
                // Update their names to things that have changed.
                user.First_Name = model.Firstname;
                user.Last_Name = model.Lastname;
                user.Username = model.Username;

                // Update the database
                DatabaseConnector.Update(user);

                return View("Index", user);
            }
            // If their is an XSS attempt log the user out.
            UserHelper.LogOut(HttpContext.Session);
            OrganisationHelper.LogOut(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }

        
        /// <summary>
        /// Add the ability to upload new questions to the database if you are an Admin
        /// </summary>
        /// <returns></returns>
        public IActionResult AddQuestions()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPython(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            Dictionary<int, string> diction = new Dictionary<int, string>(),
                file = new Dictionary<int, string>();

            int i = 0;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    diction.Add(i, formFile.FileName);
                    file.Add(i, filePath);
                    HttpContext.Session.SetString("file-" + i, filePath);
                    i++;
                }
            }
            var model = new UploadViewModel()
            {
                identifier = Guid.NewGuid(),
                documents = diction,
                Catagories = DatabaseConnector.GetCatagories()
            };
            TestPython(new UploadViewModel()
            {
                identifier = model.identifier,
                documents = file
            });

            return View(model);
        }

        [HttpPost]
        public IActionResult UploadComplete(UploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool setTypes = false;
            List<Catagory> new_catagories = new List<Catagory>();
            var webRoot = _env.WebRootPath;
            for (int i = 0; i < model.Items.Length; i++)
            {
                var item = model.Items[i];
                if (item.Name.ToLower() != "error")
                {
                    var filepath = HttpContext.Session.GetString("file-" + i);
                    var filename = string.Format(@"{0}.py", DateTime.Now.Ticks);
                    var file = Path.Combine(webRoot, "lib/Python/" + filename);
                    using (var temp = new FileStream(filepath, FileMode.Open))
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        temp.CopyToAsync(stream);
                    }

                    var catagory = DatabaseConnector.GetCatagories().FirstOrDefault(x => x.CatagoryName == item.Catagory);
                    if (catagory == null)
                    {
                        catagory = new Catagory()
                        {
                            CatagoryName = item.Catagory,
                            CatagoryType = DatabaseConnector.GetCatagoryTypes().FirstOrDefault(x => x.CatTypeName.ToLower() == "unknown").CatTypeID
                        };
                        catagory.CatagoryID = DatabaseConnector.AddCatagory(catagory);
                        DatabaseConnector.PushChanges();
                        setTypes = true;
                        new_catagories.Add(catagory);
                    }
                    var questiontype = new QuestionTypes()
                    {
                        Class = filename,
                        Type_Name = item.Name,
                        Catagory = catagory.CatagoryID
                    };
                    questiontype.TypeID = DatabaseConnector.AddQesutionType(questiontype);
                }
            }
            DatabaseConnector.PushChanges();
            if (setTypes) return View(new UploadCompleteViewModel()
            {
                Catagories = new_catagories.ToArray(),
                Types = DatabaseConnector.GetCatagoryTypes().Where(x => x.CatTypeName.ToLower() != "unknown").ToArray()
            });

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult SetCatagories(UploadCompleteViewModel model)
        {
            foreach (var item in model.Catagories)
            {
                var cat = DatabaseConnector.GetCatagories().FirstOrDefault(x => x.CatagoryID == item.CatagoryID);
                cat.CatagoryType = item.CatagoryType;
                DatabaseConnector.Update(cat);
            }
            DatabaseConnector.PushChanges();
            return RedirectToAction("index");
        }

        public async Task TestPython(UploadViewModel model)
        {
            await Task.Delay(500);
            pythonInterpreter = new PythonInterpreter();
            var user = await FileHub.WaitForUserID(model.identifier.ToString());

            foreach (var item in model.documents)
            {
                TestFile(user, item);
            }
        }

        public async Task TestFile(string id, KeyValuePair<int, string> pair)
        {
            var error = await pythonInterpreter.CheckQuestion(pair.Value);
            if (error == "None")
            {
                SendMessage(id, pair.Key, "success", "No Error");
            }
            else
            {
                SendMessage(id, pair.Key, "fail", error);
            }
        }

        public async Task SendMessage(string UserID, int item, string message, string error)
        {
            await HubContext.Clients.Client(UserID).SendAsync("ReceiveMessage", item, message, error);
        }
    }
}