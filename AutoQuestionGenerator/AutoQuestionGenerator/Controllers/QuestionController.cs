using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Accounts;

namespace AutoQuestionGenerator.Controllers
{
    [Authorized]
    public class QuestionController : Controller
    {
        IdentityModels _context;

        public QuestionController()
        {
            _context = new IdentityModels();
        }

        public IActionResult Index()
        {
            return View(
                _context.users.ToList()
                );
        }
    }
}