using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoQuestionGenerator.Models;
using AutoQuestionGenerator.Questions.Interpreter;

namespace AutoQuestionGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            PythonInterpreter.GetParam(4000);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
