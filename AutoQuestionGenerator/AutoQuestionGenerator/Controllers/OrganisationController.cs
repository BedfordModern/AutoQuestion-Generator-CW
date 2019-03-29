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
    /// <summary>
    /// This controller deals with all the organisation information
    /// and many of the admin tasks to do with this.
    /// </summary>
    public class OrganisationController : Controller
    {
        IHostingEnvironment _env;

        public OrganisationController(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// This gerts most of the organisation details and returns them to the user
        /// so they can see the name and username of the organisation they are in
        /// More useful for the admins who can see details about the organisations subscription.
        /// </summary>
        /// <returns>The index page of the site</returns>
        public IActionResult Index()
        {
            return View(OrganisationHelper.GetOrganisation(HttpContext.Session));
        }

        /// <summary>
        /// Returns the organisation LoginPage
        /// 
        /// Now DEPRECATED as I moved to 1 page.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Returns information about the organistions users
        /// to an admin within that organisation.
        /// </summary>
        /// <returns>Users pages with information about the users</returns>
        public IActionResult Users()
        {
            var uid = UserHelper.GetUserId(HttpContext.Session);
            if (UserHelper.UserInRole(uid, UserHelper.ROLE_ADMIN))
            {
                return View(DatabaseConnector.GetUsers(UserHelper.GetUser(uid).OrganisationID));
            }
            return Unauthorized();
        }

        /// <summary>
        /// Lets the admin create a single new user
        /// this just loads the page.
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateUser()
        {
            //Checks if the user trying to access this is an admin.
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN))
                return View();

            //They are not an admin give them an error.
            return Unauthorized();
        }

        /// <summary>
        /// This method take in the post information from the 
        /// website and uses it to create a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateUser(Users user)
        {
            //Check to see if the user trying to do this is an admin
            var uid = UserHelper.GetUserId(HttpContext.Session);
            if (UserHelper.UserInRole(uid, UserHelper.ROLE_ADMIN))
            {
                //Get the id of the organisation
                var organisation = UserHelper.GetUser(uid).OrganisationID;
                user.OrganisationID = organisation;

                //Check that user doesn't aready exist in this organisation
                var dbuser = UserHelper.GetUser(user.Username, organisation);
                if (!ModelState.IsValid || dbuser != null)
                    return View(user);

                //Create the new user if this is ppossible
                UserHelper.CreateNewUser(user.Username, organisation, user.Password, user.First_Name, user.Last_Name);
                return RedirectToAction("Users");
            }

            // The user is not authorised to do this so error.
            return Unauthorized();
        }

        /// <summary>
        /// Retuns the get view of the page which allows users to be uploaded to the website using a CSV
        /// This page is then returned by the user with the documents they with to upload.
        /// </summary>
        /// <returns></returns>
        public IActionResult UploadUsers()
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN))
            {
                return View();
            }
            return Unauthorized();
        }

        /// <summary>
        /// The post form for uploading users
        /// this will then covert the files and save them, it will then parse
        /// them using some code to convert the csv into databas users.
        /// </summary>
        /// <param name="files">files from the upload</param>
        /// <returns>redirect to errors</returns>
        [HttpPost]
        public async Task<IActionResult> UploadUsers(List<IFormFile> files)
        {
            // Check if the user it authorised to make this action.
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN))
            {
                // Makes sure the upload directory exists
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "lib/Excel/"));
                long size = files.Sum(f => f.Length);

                //Initialises the dictionaries we need to kepp track of the files.
                Dictionary<int, string> diction = new Dictionary<int, string>();
                Dictionary<int, FileInfo> file = new Dictionary<int, FileInfo>();

                int i = 0;
                //Iterates through each file.
                foreach (var formFile in files)
                {
                    //Checks the file is not length 0
                    if (formFile.Length > 0)
                    {
                        //Creates file info for the file
                        var f = new FileInfo(formFile.FileName);

                        //Gets the path too and generates a tempeorary file.
                        var filePath = Path.GetTempFileName() + f.Extension;
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            //Copies in the file information.
                            await formFile.CopyToAsync(stream);
                        }
                        // Adds the file information to the dictionaries
                        diction.Add(i, formFile.FileName);
                        file.Add(i, new FileInfo(filePath));
                        // Adds 1 to the nu
                        i++;
                    }
                }

                // Parses the files and saves to the DB
                List<string> ret = UserFiles(file);

                // Returns information about the parse.
                return View("NewUsers", ret.ToArray());
            }
            return Unauthorized();
        }

        /// <summary>
        /// This method iterates through each of the possible files
        /// and parses them one by one.
        /// </summary>
        /// <param name="files">The files with are being parsed</param>
        /// <returns>the information about the parse</returns>
        private List<string> UserFiles(Dictionary<int, FileInfo> files)
        {
            List<string> output = new List<string>();
            //Iterate through each file.
            foreach (var item in files)
            {
                //Test and setup that file.
                output.AddRange(ParseFile(item));
            }
            return output;
        }

        /// <summary>
        /// This method parses either a CSV
        /// or a Excel(xls(x)) file into the database and is able to add
        /// new users based on the information within those files.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private List<string> ParseFile(KeyValuePair<int, FileInfo> item)
        {
            // Checks whether the gile is an Excel file
            if (item.Value.Extension.ToLower().Contains("xls"))
            {
                try
                {
                    //Using Excel convert and save the file as a csv
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.ApplicationClass();
                    Microsoft.Office.Interop.Excel.Workbook wbWorkbook = app.Workbooks.Open(item.Value.FullName);
                    wbWorkbook.SaveAs(Path.Combine(_env.WebRootPath, "lib/Excel/" + item.Value.Name) + ".csv", Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV);
                    wbWorkbook.Close();

                    // Save the CSV and replace the file info object.
                    item = new KeyValuePair<int, FileInfo>(item.Key, new FileInfo(Path.Combine(_env.WebRootPath, "lib/Excel/" + item.Value.Name) + ".csv"));
                }
                // Error would occur if the file was in an ureadable format or did not upload correctly.
                catch (Exception)
                {
                    // Tell the user what went wrong with this file.
                    return new List<string>()
                    {
                        "Failed to convert workbook into CSV"
                    };
                }
            }

            // Check if the item is listed as a CSV
            if (item.Value.Extension.ToLower().Contains("csv"))
            {
                // Setup a list of possible error or information
                List<string> Information = new List<string>();

                //Open the file in steam.
                using (var stream = new StreamReader(new FileStream(item.Value.FullName, FileMode.Open)))
                {
                    int userID = UserHelper.GetUserId(HttpContext.Session);
                    string line = "";
                    // Read the fill until you get to the first line;
                    while (string.IsNullOrEmpty(line = stream.ReadLine())) { }
                    // find the positions of the headers by spliting.
                    string[] headers = line.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);

                    // Find the positions of the headers in the list using my own extention method.
                    int uname = headers.PositionOf("username"),
                        password = headers.PositionOf("password"),
                        fname = headers.PositionOf("firstname"),
                        lname = headers.PositionOf("lastname");

                    // Make sure that all the required headers exist.
                    bool error = false;
                    var errortxt = "Could not find column: ";
                    if (uname == -1)
                    {
                        errortxt += "username, ";
                        error = true;
                    }
                    if (password == -1)
                    {
                        errortxt += "password, ";
                        error = true;
                    }
                    if (fname == -1)
                    {
                        errortxt += "fname, ";
                        error = true;
                    }
                    if (lname == -1)
                    {
                        errortxt += "lastname, ";
                        error = true;
                    }

                    // Return if their was an error
                    if (error)
                    {
                        Information.Add(errortxt);
                        return Information;
                    }

                    // Find the possible positions of the multiple headers.
                    var roles = headers.PositionsOf("role");
                    var groups = headers.PositionsOf("group");
                    int i = 1;
                    // Read each line to the end of the file.
                    while (!stream.EndOfStream)
                    {
                        // Get the new line and split it into components.
                        line = stream.ReadLine();
                        var items = line.Split(',');
                        try
                        {
                            // Try creating the user with the detials entered above.
                            string add = "";
                            var usr = UserHelper.CreateNewUser(UserHelper.GetUserId(HttpContext.Session), items[uname], items[password], items[fname], items[lname]);
                            if (usr.Username == UserHelper.USER_ERROR)
                            {
                                // The the user that the user failed to be added.
                                Information.Add("Line: " + i + " User: " + items[uname] + " Failed to add user. Username probably not unique!");
                            }
                            else
                            {
                                // the user was added, tell them.
                                add = $"Added user: {items[fname]} {items[lname]}, Username: {items[uname]}";

                                // Go through each possible position of roles in this file.
                                foreach (int role in roles)
                                {
                                    try
                                    {
                                        // Check to see their is a role their
                                        if (!string.IsNullOrEmpty(items[role]))
                                        {
                                            // Try to give the user the requested role
                                            try
                                            {
                                                UserHelper.GiveRole(usr.UserID, items[role]);
                                                add += $" role: {items[role]}";
                                            }
                                            // The role was not found if this errors.
                                            catch (KeyNotFoundException)
                                            {
                                                Information.Add("Failed: Unable to find role of name: " + items[role]
                                                    + ". Please make sure the user is in one of the following roles - Teacher, Student, Admin - and that it is spelt correctly");
                                            }
                                            DatabaseConnector.PushChanges();
                                        }
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        // Couuld not file that role if there is an error
                                        Information.Add("Line: " + i + " User: " + items[uname] + " Failed to find role at position: " + role + ".");
                                    }
                                }
                                // go through all the groups that the user was requested to be added to.
                                foreach (var group in groups)
                                {
                                    // Check a group exists at that location.
                                    if (!string.IsNullOrEmpty(items[group]))
                                    {
                                        // Attempt to get the groupID of that group
                                        int GroupID = DatabaseConnector.GetGroup(items[group], OrganisationHelper.GetOrganisationID(HttpContext.Session));
                                        //If the group doesn't exist create a new one
                                        if (GroupID == -1)
                                        {
                                            // Creates the new groups which is accessible by everyone
                                            // and was created by the admin uploading the users.
                                            Groups newGroup = new Groups()
                                            {
                                                AccessType = 1,
                                                CreatedBy = UserHelper.GetUserId(HttpContext.Session),
                                                Group_Name = items[group]
                                            };
                                            // Get the new ID of the group that the database returns
                                            newGroup.GroupID = DatabaseConnector.AddGroup(newGroup);
                                            // Push the group and user changes.
                                            DatabaseConnector.PushChanges();

                                            // Tell the user that you created a new group.
                                            Information.Add("Line: " + i + " User: " + items[uname] + "created new group: " + items[group] + ".");

                                            // Then add the user to the group you just created.
                                            GroupUsers usrAdd = new GroupUsers()
                                            {
                                                UserID = usr.UserID,
                                                GroupID = newGroup.GroupID
                                            };
                                            DatabaseConnector.AddGroupUser(usrAdd);
                                        }
                                        // If the group already exists
                                        else
                                        {
                                            // Again add the user too that group
                                            GroupUsers usrAdd = new GroupUsers()
                                            {
                                                UserID = usr.UserID,
                                                GroupID = GroupID
                                            };
                                            // add this to the database and tell the parser.
                                            DatabaseConnector.AddGroupUser(usrAdd);
                                            Information.Add("Line: " + i + " User: " + items[uname] + " added user to group: " + items[group] + ".");
                                        }
                                    }
                                    else
                                    {
                                        Information.Add("Line: " + i + " User: " + items[uname] + " group column blank: " + group + ".");
                                    }
                                }
                            }
                            Information.Add(add);
                        }
                        // Catches the eror that you cannot find the item. I.E. the person who created the CSV formatted it wrong.
                        catch (IndexOutOfRangeException)
                        {
                            Information.Add("Line: " + i + " Failed to find index of item");
                        }
                        i++;
                    }
                }
                // Return the information from the parse
                return Information;
            }
            // Return if you were unable to read the inputted files as a CSV.
            return new List<string>
            {
                "Could not be read as a CSV file"
            };
        }

        /// <summary>
        /// Logins in the organisatoin part of the system
        /// No longer used as we have moved to a single login page
        /// </summary>
        /// <param name="org">organistion</param>
        /// <param name="returnUrl">URL to go to once done.</param>
        /// <returns>Either to home or to the return URL</returns>
        [HttpPost]
        [Obsolete]
        public IActionResult Login(Organisations org, string returnUrl)
        {
            var Organisation = OrganisationHelper.GetOrganisation(org.Organisation_Username);

            if (!ModelState.IsValid || Organisation == null) return View(org);

            if (Hasher.ValidatePassword(org.Organisation_Password, Organisation.Organisation_Password))
            {
                HttpContext.Session.Set("OrgId", Encoding.UTF8.GetBytes(Organisation.OrganisationID.ToString()));
                HttpContext.Session.Set("Org_Uname", Encoding.UTF8.GetBytes(Organisation.Organisation_Username));
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

        /// <summary>
        /// Allows an admin to reset the organisations
        /// password if they need to.
        /// </summary>
        /// <returns></returns>
        public IActionResult Reset()
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN))
            {
                return View();
            }
            return LocalRedirect("~/");
        }

        /// <summary>
        /// The post form which will change an organisations pssword
        /// this allows the user to change the password of their organisation
        /// </summary>
        /// <param name="org">The organistion info</param>
        /// <returns>Login for the organistion</returns>
        [HttpPost]
        public IActionResult Reset(UpdateUser org)
        {
            if (!ModelState.IsValid || !org.SamePass)
            {
                return View(org);
            }
            var organisation = DatabaseConnector.Get<Organisations>().FirstOrDefault(x => x.Organisation_Username == org.Username);
            organisation.Organisation_Password = Hasher.Hash(org.Password);
            DatabaseConnector.Update(organisation);

            DatabaseConnector.PushChanges();

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Allows an admin to rest a users password for them
        /// this means that the organisation has more control over their users
        /// </summary>
        /// <returns></returns>
        public IActionResult Lost()
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN))
            {
                return View();
            }
            return LocalRedirect("~/");
        }

        /// <summary>
        /// The post form where the users password will be upated.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Lost(UpdateUser model)
        {
            // Gets the user from the database
            
            var user = UserHelper.GetUser(model.Username, OrganisationHelper.GetOrganisationID(HttpContext.Session));

            // Get the userID of the person making this request
            var userid = UserHelper.GetUserId(HttpContext.Session);

            // Check he has permission to be this
            if (user.OrganisationID == UserHelper.GetUser(userid).OrganisationID && UserHelper.UserInRole(userid, UserHelper.ROLE_ADMIN))
            {
                //Update the password and pass it to the database.
                user.Password = Hasher.Hash(model.Password);
                DatabaseConnector.Update(model.Password);
                DatabaseConnector.PushChanges();
            }
            return Unauthorized();
        }
    }
}