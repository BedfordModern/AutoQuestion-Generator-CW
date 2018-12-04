﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace AutoQuestionGenerator.Controllers
{
    [Authorized]
    public class WorkController : Controller
    {
        IdentityModels _context;

        public WorkController()
        {
            _context = new IdentityModels();
        }

        public IActionResult Index()
        {
            return View(_context.worksets.Where(x => x.SetBy == UserHelper.GetUserId(HttpContext.Session)).ToList());
        }

        public IActionResult Workset(int id)
        {
            return View(_context.work.Where(x => x.WorkSetID == id).ToList());
        }
    }
}