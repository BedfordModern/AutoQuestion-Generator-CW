﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Models;
using AutoQuestionGenerator.QuestionModels;
using AutoQuestionGenerator.QuestionModels.Interpreter;
using Microsoft.AspNetCore.Mvc;

namespace AutoQuestionGenerator.Controllers
{
    [Authorized]
    public class SetController : Controller
    {
        IdentityModels _context;
        PythonInterpreter Interpreter;

        public SetController()
        {
            _context = new IdentityModels();
            Interpreter = new PythonInterpreter();
        }

        public IActionResult Index(int WorkSetID)
        {
            var set = _context.worksets.FirstOrDefault(x => x.WorksetID == WorkSetID);

            if (set == null)
            {
                return RedirectToAction("Index", "Work");
            }

            var work = _context.work.Where(x => x.WorkSetID == set.WorksetID).ToList();

            byte[] uidArr;
            HttpContext.Session.TryGetValue("UId", out uidArr);
            string idStr = Encoding.ASCII.GetString(uidArr);
            int uid = Convert.ToInt32(idStr);

            QuestionSets qSet = new QuestionSets()
            {
                UserID = uid,
                Date_Asked = DateTime.Today
            };

            _context.questionSets.Add(qSet);
            _context.SaveChanges();

            List<QuestionSetViewModel> Qusts = new List<QuestionSetViewModel>();

            foreach (var piece in work)
            {
                var seed = StoredQuestion.GenerateSeed(piece.Seed);
                Questions qust = new Questions()
                {
                    QuestionSetID = qSet.QuestionSetID,
                    Question_Type = piece.QuestionType,
                    Difficulty = (int)piece.Difficulty,
                    Seed = seed,
                    AnswerCorrect = 0
                };
                _context.questions.Add(qust);
                _context.SaveChanges();
                var questionSet = new QuestionSetViewModel()
                {
                    questionID = qust.QuestionID,
                    question = Interpreter.GenerateQuestion(@"C:\Users\Daniel Ledger 9CLW\Source\repos\GitCW\DanL\AutoQuestionGenerator\AutoQuestionGenerator\wwwroot\lib\Python\" + _context.questionTypes.SingleOrDefault(x => x.TypeID == qust.Question_Type).Class, seed),
                    answer = "",
                    correct = 0,
                    PerQuestion = true
                };
                Qusts.Add(questionSet);
                HttpContext.Session.Set("Q" + qust.QuestionID, Encoding.ASCII.GetBytes(questionSet.question.GetAnswer().ToString()));
            }
            return View(Qusts);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        #region Ajax Callbacks


        public IActionResult Checkquestion(QuestionSetViewModel model)
        {
            byte[] vals;
            HttpContext.Session.TryGetValue("Q" + model.questionID, out vals);

            string answer = Encoding.ASCII.GetString(vals);
            var question = _context.questions.FirstOrDefault(x => x.QuestionID == model.questionID);

            if (answer.EndsWith(model.answer.Trim()))
            {
                question.AnswerCorrect = (int)Math.Abs(question.AnswerCorrect ) + 1;
            }
            else if(question.AnswerCorrect <= 0)
            {
                question.AnswerCorrect -= 1;
            }
            model.correct = question.AnswerCorrect;
            _context.SaveChanges();

            return PartialView("_Question", model);
        }

        public IActionResult Checkworkset(List<QuestionSetViewModel> modelSet)
        {
            foreach(var model in modelSet)
            {

                byte[] vals;
                HttpContext.Session.TryGetValue("Q" + model.questionID, out vals);

                string answer = Encoding.ASCII.GetString(vals);
                var question = _context.questions.FirstOrDefault(x => x.QuestionID == model.questionID);

                if (answer.EndsWith(model.answer.Trim()))
                {
                    question.AnswerCorrect = (int)Math.Abs(question.AnswerCorrect) + 1;
                }
                else if (question.AnswerCorrect <= 0)
                {
                    question.AnswerCorrect -= 1;
                }
                model.correct = question.AnswerCorrect;
            }

            _context.SaveChanges();

            return View();
        }
        #endregion
    }
}