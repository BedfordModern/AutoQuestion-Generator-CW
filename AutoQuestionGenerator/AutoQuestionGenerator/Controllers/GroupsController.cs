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
    public class GroupsController : Controller
    {
        IdentityModels _context;

        public GroupsController()
        {
            _context = new IdentityModels();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(bool closeAfter)
        {
            var model = new CreateGroupViewModel();
            model.AccessTypes = _context.accessTypes.ToArray();
            model.Users = _context.users.ToArray();
            model.CloseAfter = closeAfter;

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AccessTypes = _context.accessTypes.ToArray();
                model.Users = _context.users.ToArray();
                return View(model);
            }

            Groups newGroup = new Groups()
            {
                Group_Name = model.GroupName,
                CreatedBy = UserHelper.GetUserId(HttpContext.Session),
                AccessType = model.AccessType
            };

            _context.groups.Add(newGroup);
            _context.SaveChanges();

            if (model.GroupUsers != null)
            {
                foreach (var usr in model.GroupUsers)
                {
                    GroupUsers user = new GroupUsers()
                    {
                        GroupID = newGroup.GroupID,
                        UserID = usr
                    };
                    _context.groupUsers.Add(user);
                }
                _context.SaveChanges();
            }

            if (model.CloseAfter)
            {
                return new JsonResult(newGroup.GroupID + ";" + newGroup.Group_Name);
            }

            return RedirectToAction("Index", "Groups");
        }
    }
}