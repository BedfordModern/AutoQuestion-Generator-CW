using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoQuestionGenerator.Accounts;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Helper;
using AutoQuestionGenerator.Models;
using AutoQuestionGenerator.Models.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoQuestionGenerator.Controllers
{
    public class OrganisationController : Controller
    {
        IdentityModels _context;
        IHostingEnvironment _env;

        public OrganisationController(IHostingEnvironment env)
        {
            _context = new IdentityModels();
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult UploadUsers()
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN, _context))
            {
                return View();
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> UploadUsers(List<IFormFile> files)
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN, _context))
            {
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "lib/Excel/"));
                long size = files.Sum(f => f.Length);

                Dictionary<int, string> diction = new Dictionary<int, string>();
                Dictionary<int, FileInfo> file = new Dictionary<int, FileInfo>();

                int i = 0;
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var f = new FileInfo(formFile.FileName);
                        var filePath = Path.GetTempFileName() + f.Extension;
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        diction.Add(i, formFile.FileName);
                        file.Add(i, new FileInfo(filePath));
                        HttpContext.Session.SetString("users-" + i, filePath);
                        i++;
                    }
                }
                var model = new UploadViewModel()
                {
                    identifier = Guid.NewGuid(),
                    documents = diction,
                    Catagories = _context.catagories.ToArray()
                };

                List<string> ret = UserFiles(model.identifier, file).Result;

                return View("NewUsers", ret.ToArray());
            }
            return Unauthorized();
        }

        private async Task<List<string>> UserFiles(Guid identifier, Dictionary<int, FileInfo> files)
        {
            List<string> output = new List<string>();
            List<Task<List<string>>> tasks = new List<Task<List<string>>>();
            foreach (var item in files)
            {
                tasks.Add(TestFile(item));
            }
            foreach (var task in tasks)
            {
                output.AddRange(await task);
            }
            return output;
        }

        private async Task<List<string>> TestFile(KeyValuePair<int, FileInfo> item)
        {
            if (item.Value.Extension.ToLower().Contains("xls"))
            {
                //try
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.ApplicationClass();
                    Microsoft.Office.Interop.Excel.Workbook wbWorkbook = app.Workbooks.Open(item.Value.FullName);
                    wbWorkbook.SaveAs(Path.Combine(_env.WebRootPath, "lib/Excel/" + item.Value.Name) + ".csv", Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV);
                    wbWorkbook.Close();
                    item = new KeyValuePair<int, FileInfo>(item.Key, new FileInfo(Path.Combine(_env.WebRootPath, "lib/Excel/" + item.Value.Name) + ".csv"));
                }
                /*catch (Exception)
                {

                    return new List<string>()
                    {
                        "Failed to convert workbook into CSV"
                    };
                }*/
            }

            if (item.Value.Extension.ToLower().Contains("csv"))
            {
                List<string> Failed = new List<string>();
                using (var stream = new StreamReader(new FileStream(item.Value.FullName, FileMode.Open)))
                {
                    string line = "";
                    while(string.IsNullOrEmpty(line = stream.ReadLine())) { }
                    string[] headers = line.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);
                    int uname = headers.PositionOf("username"),
                        password = headers.PositionOf("password"),
                        fname = headers.PositionOf("firstname"),
                        lname = headers.PositionOf("lastname");
                    var roles = headers.PositionsOf("role");
                    int i = 1;
                    while (!stream.EndOfStream)
                    {
                        line = stream.ReadLine();
                        var items = line.Split(',');
                        try
                        {
                            string add = "";
                            var usr = UserHelper.CreateNewUser(UserHelper.GetUserId(HttpContext.Session), items[uname], items[password], items[fname], items[lname], _context);
                            if (usr.Username == UserHelper.USER_ERROR)
                            {
                                Failed.Add("Line: " + i + "User: " + items[uname] + "Failed to add user. Username probably not unique!");
                            }
                            else
                            {
                                add = $"Added user: {items[fname]} {items[lname]}, Username: {items[uname]}";

                                foreach (int role in roles)
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(items[role]))
                                        {
                                            try
                                            {
                                                UserHelper.GiveRole(usr.UserID, items[role], _context);
                                                add += $" role: {items[role]}";
                                            }
                                            catch (KeyNotFoundException)
                                            {
                                                Failed.Add("Unable to find role of name: " + items[role]
                                                    + ". Please make sure the user is in one of the following roles - Teacher, Student, Admin - and that it is spelt correctly");
                                            }
                                            _context.SaveChanges();
                                        }
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        Failed.Add("Line: " + i + "User: " + items[uname] + "Unable to find role at position: " + role + ".");
                                    }
                                }
                            }
                            Failed.Add(add);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Failed.Add("Line: " + i + "Failed to find index of item");
                        }
                        i++;
                    }
                }
                return Failed;
            }
            return new List<string>
            {
                "Could not be read as a CSV file"
            };
        }

        [HttpPost]
        public IActionResult Login(Organisations org, string returnUrl)
        {
            var Organisation = OrganisationHelper.getOrganisation(org.Organisation_Username, _context);

            if (!ModelState.IsValid || Organisation == null) return View(org);

            if (Hasher.ValidatePassword(org.Organisation_Password, Organisation.Organisation_Password))
            {
                HttpContext.Session.Set("OrgId", Encoding.ASCII.GetBytes(Organisation.OrganisationID.ToString()));
                HttpContext.Session.Set("Org_Uname", Encoding.ASCII.GetBytes(Organisation.Organisation_Username));
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(org);
            }
        }

        public IActionResult Reset()
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN, _context))
            {
                return View();
            }
            return LocalRedirect("~/");
        }

        [HttpPost]
        public IActionResult Reset(UpdateUser org)
        {
            if (!ModelState.IsValid || !org.SamePass)
            {
                return View(org);
            }
            _context.organisations.FirstOrDefault(x => x.Organisation_Username == org.Username).Organisation_Password = Hasher.Hash(org.Password);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Lost()
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN, _context))
            {
                return View();
            }
            return LocalRedirect("~/");
        }

        [HttpPost]
        public IActionResult Lost(UpdateUser model)
        {
            var user = _context.users.FirstOrDefault(x => x.Username == model.Username);
            var userid = UserHelper.GetUserId(HttpContext.Session);
            if (user.OrganisationID == UserHelper.getUser(userid, _context).OrganisationID && UserHelper.UserInRole(userid, UserHelper.ROLE_ADMIN, _context))
            {
                user.Password = Hasher.Hash(model.Password);
            }
            return Unauthorized();
        }
    }
}