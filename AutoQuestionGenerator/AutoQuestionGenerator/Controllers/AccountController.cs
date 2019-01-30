using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class AccountController : Controller
    {
        IHubContext<FileHub> HubContext;
        IdentityModels _context;
        PythonInterpreter pythonInterpreter;
        private IHostingEnvironment _env;
        public AccountController(IHubContext<FileHub> hubcontext, IHostingEnvironment env)
        {
            _context = new IdentityModels();
            HubContext = hubcontext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

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
                Catagories = _context.catagories.ToArray()
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

                    var catagory = _context.catagories.FirstOrDefault(x => x.CatagoryName == item.Catagory);
                    if (catagory == null)
                    {
                        catagory = new Catagory()
                        {
                            CatagoryName = item.Catagory,
                            CatagoryType = _context.CatagoryTypes.FirstOrDefault(x => x.CatTypeName.ToLower() == "unknown").CatTypeID
                        };
                        _context.catagories.Add(catagory);
                        _context.SaveChanges();
                        setTypes = true;
                        new_catagories.Add(catagory);
                    }
                    var questiontype = new QuestionTypes()
                    {
                        Class = filename,
                        Type_Name = item.Name,
                        Catagory = catagory.CatagoryID
                    };
                    _context.questionTypes.Add(questiontype);
                }
            }
            _context.SaveChanges();
            if (setTypes) return View(new UploadCompleteViewModel()
            {
                Catagories = new_catagories.ToArray(),
                Types = _context.CatagoryTypes.Where(x => x.CatTypeName.ToLower() != "unknown").ToArray()
            });

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult SetCatagories(UploadCompleteViewModel model)
        {
            foreach (var item in model.Catagories)
            {
                var cat = _context.catagories.FirstOrDefault(x => x.CatagoryID == item.CatagoryID);
                cat.CatagoryType = item.CatagoryType;
            }
            _context.SaveChanges();
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