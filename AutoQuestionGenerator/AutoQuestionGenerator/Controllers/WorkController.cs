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
using AutoQuestionGenerator.Models;

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
            var user = UserHelper.GetUserId(HttpContext.Session);

            var groups = _context.groupUsers.Where(x => x.UserID == user).Select(x => x.GroupID).ToArray();

            List<WorksetViewModel> viewModels = new List<WorksetViewModel>();
            foreach(var group in groups)
            {
                var sets = _context.worksets.Where(x => x.GroupID == group);

                foreach (var set in sets)
                {
                    if(_context.questionSets.FirstOrDefault(x => x.WorkSetID == set.WorksetID && x.UserID == user) == null)
                    {
                        var setBy = _context.users.FirstOrDefault(x => x.UserID == set.SetBy);
                        viewModels.Add(new WorksetViewModel()
                        {
                            WorksetID = set.WorksetID,
                            WorksetName = set.WorksetName,
                            Time_Allowed = set.Time_Allowed,
                            Date_Due = set.Date_Due,
                            Date_Set = set.Date_Set,
                            SetBy = setBy.First_Name + " " + setBy.Last_Name,
                            Group_Name = _context.groups.FirstOrDefault(x => x.GroupID == group).Group_Name,
                            SetType = _context.worktype.FirstOrDefault(x => x.SetType_ID == set.SetType).SetType_Name,
                            RandomOrdering = set.RandomOrdering,
                            ExamStyle = set.ExamStyle
                        });
                    }
                }
            }

            return View(viewModels.ToArray());
        }

        public IActionResult Set()
        {
            var sets = _context.worksets.Where(x => x.SetBy == UserHelper.GetUserId(HttpContext.Session)).ToList();
            List<WorksetViewModel> workset = new List<WorksetViewModel>();
            foreach (var set in sets)
            {
                workset.Add(new WorksetViewModel()
                {
                    WorksetID = set.WorksetID,
                    WorksetName = set.WorksetName,
                    Time_Allowed = set.Time_Allowed,
                    SetType = _context.worktype.FirstOrDefault(x => x.SetType_ID == set.SetType).SetType_Name,
                    Date_Set = set.Date_Set,
                    Date_Due = set.Date_Due,
                    Group_Name = _context.groups.FirstOrDefault(x => x.GroupID == set.GroupID).Group_Name,
                    ExamStyle = set.ExamStyle,
                    RandomOrdering = set.RandomOrdering
                });
            }

            return View(workset.ToArray());
        }

        public IActionResult Workset(int id)
        {
            if (UserHelper.UserHasBasicAccess(UserHelper.GetUserId(HttpContext.Session), id, _context))
            {
                List<WorkViewModel> viewModel = new List<WorkViewModel>();
                var work = _context.work.Where(x => x.WorkSetID == id).ToList();

                foreach (var piece in work)
                {
                    viewModel.Add(new WorkViewModel()
                    {
                        WorkID = piece.WorkID,
                        WorkSetName = _context.worksets.FirstOrDefault(x => x.WorksetID == id).WorksetName,
                        Seed = piece.Seed,
                        QuestionType = _context.questionTypes.FirstOrDefault(x => x.TypeID == piece.QuestionType).Type_Name
                    });
                }

                return View(viewModel.ToArray());
            }
            return Unauthorized();
        }
    }
}