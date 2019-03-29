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

        public QuestionController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}