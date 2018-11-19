using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Models;
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
                Questions qust = new Questions()
                {
                    QuestionSetID = qSet.QuestionSetID,
                    Question_Type = piece.QuestionType,
                    Difficulty = (int)piece.Difficulty,
                    Seed = piece.Seed
                };
                _context.questions.Add(qust);
                Qusts.Add(new QuestionSetViewModel()
                {
                    Qusts = new KeyValuePair<int, QuestionModels.Question>(qust.QuestionID, Interpreter.GenerateQuestion(@"C:\Users\Daniel Ledger 9CLW\Source\repos\GitCW\DanL\AutoQuestionGenerator\AutoQuestionGenerator\wwwroot\lib\Python\" + _context.questionTypes.SingleOrDefault(x => x.TypeID == qust.Question_Type).Class)),
                    answer = ""
                }
                );
            }
            _context.SaveChangesAsync();
            return View(Qusts);
        }

        public IActionResult Checkquestion(QuestionSetViewModel model)
        {
            return PartialView("");
        }
    }
}