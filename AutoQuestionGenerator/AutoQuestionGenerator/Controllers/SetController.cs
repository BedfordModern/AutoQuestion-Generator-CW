using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoQuestionGenerator.DatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoQuestionGenerator.Controllers
{
    public class SetController : Controller
    {
        IdentityModels _context;

        public SetController()
        {
            _context = new IdentityModels();
        }

        public IActionResult Index(int WorkSetID)
        {
            var set = _context.worksets.FirstOrDefault(x => x.WorksetID == WorkSetID);

            if(set == null)
            {
                return View();
            }

            return View();
        }
    }
}