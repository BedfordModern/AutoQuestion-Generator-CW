using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoQuestionGenerator.Accounts;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoQuestionGenerator.Controllers
{
    /// <summary>
    /// This controller is used to create and control groups
    /// mainly it is used to crate and show the groups
    /// </summary>
    public class GroupsController : Controller
    {
        /// <summary>
        /// Gets the groups and shows them to the gerson requesting them
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(DatabaseConnector.GetAdminGroups());
        }

        /// <summary>
        /// Used to create a group for the user.
        /// </summary>
        /// <param name="closeAfter">If the page should close after we are done.</param>
        /// <returns></returns>
        public IActionResult Create(bool closeAfter)
        {
            var model = new CreateGroupViewModel();
            model.AccessTypes = DatabaseConnector.Get<AccessTypes>();
            model.Users = DatabaseConnector.GetWhere<Users>("OrganisationID="+OrganisationHelper.GetOrganisationID(HttpContext.Session));
            model.CloseAfter = closeAfter;

            return View(model);
        }

        /// <summary>
        /// Post form for the creation of a group
        /// Will then crate a group using the information provided
        /// </summary>
        /// <param name="model">the group information</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CreateGroupViewModel model)
        {
            // Checks that the model is valid
            if (!ModelState.IsValid)
            {
                model.AccessTypes = DatabaseConnector.Get<AccessTypes>();
                model.Users = DatabaseConnector.GetWhere<Users>("OrganisationID=" + OrganisationHelper.GetOrganisationID(HttpContext.Session));
                return View(model);
            }

            // Creates the new group with the info
            Groups newGroup = new Groups()
            {
                Group_Name = model.GroupName,
                CreatedBy = UserHelper.GetUserId(HttpContext.Session),
                AccessType = model.AccessType
            };

            // Saves the group to the database and gets it id.
            newGroup.GroupID = DatabaseConnector.AddGroup(newGroup);

            // Adds all the wanted users to the group.
            if (model.GroupUsers != null)
            {
                foreach (var usr in model.GroupUsers)
                {
                    // Now using check boxes we need to make sure they are selected.
                    if (usr.Selected)
                    {
                        GroupUsers user = new GroupUsers()
                        {
                            GroupID = newGroup.GroupID,
                            UserID = usr.UserID
                        };
                        DatabaseConnector.AddGroupUser(user);
                    }
                }
                DatabaseConnector.PushChanges();
            }

            // Returns a serilaised object if the page is going to closed
            if (model.CloseAfter)
            {
                return new JsonResult(newGroup.GroupID + ";" + newGroup.Group_Name);
            }

            // Redirects to the list of groups.
            return RedirectToAction("Index", "Groups");
        }
    }
}