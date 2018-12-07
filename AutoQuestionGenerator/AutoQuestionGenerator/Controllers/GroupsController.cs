using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return RedirectToAction("Close", "Home");
        }
    }
}