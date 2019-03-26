using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoQuestionGenerator.Accounts;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Models;
using AutoQuestionGenerator.QuestionModels;
using AutoQuestionGenerator.QuestionModels.Interpreter;
using AutoQuestionGenerator.Helper;
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
            int uid = UserHelper.GetUserId(HttpContext.Session);

            if (UserHelper.UserHasBasicAccess(uid, WorkSetID, _context))
            {
                var set = _context.worksets.FirstOrDefault(x => x.WorksetID == WorkSetID);

                if (set == null)
                {
                    return RedirectToAction("Index", "Work");
                }

                var work = _context.work.Where(x => x.WorkSetID == set.WorksetID).ToList();

                if (set.RandomOrdering)
                    work.Shuffle();

                QuestionSets qSet = new QuestionSets()
                {
                    UserID = uid,
                    WorkSetID = WorkSetID,
                    Date_Asked = DateTime.Today
                };

                _context.questionSets.Add(qSet);
                _context.SaveChanges();

                List<QuestionViewModel> Qusts = new List<QuestionViewModel>();

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
                    var curQuest = Interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\" + _context.questionTypes.SingleOrDefault(x => x.TypeID == qust.Question_Type).Class, seed);
                    var boxes = curQuest.Boxes().Split(',');
                    var Hints = new List<string>();
                    Hints.AddRange(curQuest.Hints.Split(','));

                    for (int i = Hints.Count - 1; i < boxes.Length; i++)
                    {
                        Hints.Add("");
                    }

                    var questionSet = new QuestionViewModel()
                    {
                        questionID = qust.QuestionID,
                        question = curQuest,
                        answer = new string[boxes.Length],
                        correct = new int[boxes.Length],
                        Hints = Hints.ToArray(),
                        Boxes = boxes
                    };
                    Qusts.Add(questionSet);
                    HttpContext.Session.Set("Q" + qust.QuestionID, Encoding.ASCII.GetBytes(questionSet.question.GetAnswer().ToString()));
                }
                return View(new QuestionSetViewModel()
                {
                    QuestionSetID = qSet.QuestionSetID,
                    PerQuestion = (set.SetType == 1 ? true : false),
                    questions = Qusts.ToArray()
                });
            }
            return Unauthorized();
        }

        public IActionResult Delete(int setID)
        {
            if (UserHelper.OwnsWorkset(UserHelper.GetUserId(HttpContext.Session), setID, _context))
            {
                var questionSets = _context.questionSets.Where(x => x.WorkSetID == setID);

                foreach (var qset in questionSets)
                {
                    _context.questions.RemoveRange(_context.questions.Where(x => x.QuestionSetID == qset.QuestionSetID));
                }
                _context.questionSets.RemoveRange(questionSets);

                var workset = _context.worksets.FirstOrDefault(x => x.WorksetID == setID);
                _context.work.RemoveRange(_context.work.Where(x => x.WorkSetID == setID));

                string Name = workset.WorksetName + " - " + workset.WorksetID;
                _context.worksets.Remove(workset);
                _context.SaveChanges();
                return View("Delete", Name);
            }
            return Unauthorized();
        }

        public IActionResult Results(int setID)
        {
            if (UserHelper.OwnsWorkset(UserHelper.GetUserId(HttpContext.Session), setID, _context))
            {
                GroupResultsViewModel model = new GroupResultsViewModel(setID, _context);
                return View(model);
            }
            return Unauthorized();
        }

        public IActionResult Complete(int setID)
        {

            CompleteQuestionViewModel model = new CompleteQuestionViewModel(setID, _context);

            return View(model);
        }

        public IActionResult Create()
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_TEACHER, _context) || UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN, _context))
            {
                var Model = new CreateSetViewModel();
                Model.Groups = UserHelper.GetGroups(UserHelper.GetUserId(HttpContext.Session), _context);
                return View(Model);
            }
            return Unauthorized();
        }

        [HttpPost]
        public IActionResult Create(CreateSetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Groups = UserHelper.GetGroups(UserHelper.GetUserId(HttpContext.Session), _context);
                return View(model);
            }

            Worksets sets = new Worksets()
            {
                GroupID = model.GroupID,
                WorksetName = model.WorksetName,
                SetBy = UserHelper.GetUserId(HttpContext.Session),
                Time_Allowed = model.TimeAllowed,
                Date_Set = DateTime.Now.Date,
                SetType = model.SetType,
                Date_Due = model.Date_Due,
                ExamStyle = model.SelectFromList,
                RandomOrdering = model.RandomQuestions
            };

            if (sets.GroupID == -2) sets.GroupID = null;

            _context.worksets.Add(sets);
            _context.SaveChanges();


            var Model = new BuildViewModel()
            {
                WorkSetID = sets.WorksetID,
                createdWork = new CreatedWork()
                {
                    SelectFromList = sets.ExamStyle,
                    CatagoryTypes = GetAllQuestions()
                }
            };

            return View("Build", Model);
        }

        public IActionResult Build(int setID)
        {
            if (setID == 0) return Unauthorized();
            if (UserHelper.OwnsWorkset(UserHelper.GetUserId(HttpContext.Session), setID, _context) || (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN, _context) && UserHelper.InSameOrganisation(UserHelper.GetUserId(HttpContext.Session), setID, _context)))
            {
                List<WorkPartial> currentwork = new List<WorkPartial>();
                var current = _context.work.Where(x => x.WorkSetID == setID);

                foreach (var piece in current)
                {
                    currentwork.Add(new WorkPartial()
                    {
                        TypeName = _context.questionTypes.FirstOrDefault(x => x.TypeID == piece.QuestionType).Type_Name,
                        TypeID = piece.QuestionType,
                        Seed = piece.Seed,
                        Answer = Interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\" + _context.questionTypes.SingleOrDefault(x => x.TypeID == piece.QuestionType).Class, piece.Seed).GetAnswer().ToString()
                    });
                }

                var model = new BuildViewModel()
                {
                    WorkSetID = setID,
                    createdWork = new CreatedWork()
                    {
                        SelectFromList = _context.worksets.FirstOrDefault(x => x.WorksetID == setID).ExamStyle,
                        CatagoryTypes = GetAllQuestions()
                    },
                    Work = currentwork.ToArray()
                };

                return View(model);
            }
            return Unauthorized();
        }

        [HttpPost]
        public IActionResult Build(BuildViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                Model.createdWork = new CreatedWork()
                {
                    SelectFromList = _context.worksets.FirstOrDefault(x => x.WorksetID == Model.WorkSetID).ExamStyle,
                    CatagoryTypes = GetAllQuestions()
                };
                return View(Model);
            }

            _context.work.RemoveRange(_context.work.Where(x => x.WorkSetID == Model.WorkSetID).ToArray());

            foreach (var work in Model.Work)
            {
                _context.work.Add(new Work()
                {
                    Difficulty = 1,
                    QuestionType = work.TypeID,
                    Seed = work.Seed,
                    WorkSetID = Model.WorkSetID
                });
            }
            _context.SaveChanges();

            return Redirect("~/Work/Set");
        }

        public CatagoryTypes[] GetAllQuestions()
        {
            List<CatagoryTypes> catagoryTypes = new List<CatagoryTypes>();
            var catTypes = _context.CatagoryTypes.ToArray();
            foreach (var CatType in catTypes)
            {
                List<LengthCatagory> lengthCatagories = new List<LengthCatagory>();
                var catagories = _context.catagories.Where(x => x.CatagoryType == CatType.CatTypeID).ToArray();
                foreach (var Cat in catagories)
                {
                    List<TypedWork> works = new List<TypedWork>();
                    var qtypes = _context.questionTypes.Where(x => x.Catagory == Cat.CatagoryID).ToArray();
                    int seed = 0;
                    foreach (var item in qtypes)
                    {
                        WorkPartial[] possibleWork = new WorkPartial[5];
                        for (int i = 0; i < 5; i++)
                        {
                            seed = StoredQuestion.GenerateSeed();
                            var work = Interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\" + _context.questionTypes.SingleOrDefault(x => x.TypeID == item.TypeID).Class, 0);

                            possibleWork[i] = new WorkPartial()
                            {
                                Seed = seed,
                                Answer = work.GetAnswer().ToString()
                            };
                        }
                        works.Add(new TypedWork()
                        {
                            WorkType = item.Type_Name,
                            TypeID = item.TypeID,
                            PossibleWork = possibleWork.ToArray()
                        });
                    }
                    lengthCatagories.Add(new LengthCatagory()
                    {
                        catagoryName = Cat.CatagoryName,
                        WorkTypes = works.ToArray()
                    });
                }
                catagoryTypes.Add(new CatagoryTypes()
                {
                    CatTypeName = CatType.CatTypeName,
                    Catagories = lengthCatagories.ToArray()
                });
            }

            return catagoryTypes.ToArray();
        }

        #region Ajax Callbacks

        public IActionResult UpdatePossibleQuestion()
        {
            return PartialView("_workSets", new CreatedWork()
            {
                SelectFromList = true,
                CatagoryTypes = GetAllQuestions()
            });
        }

        public IActionResult Checkquestion(QuestionViewModel model)
        {
            byte[] vals;
            HttpContext.Session.TryGetValue("Q" + model.questionID, out vals);

            string answer = Encoding.ASCII.GetString(vals);
            var question = _context.questions.FirstOrDefault(x => x.QuestionID == model.questionID);
            if (answer.Contains(","))
            {
                var allCorrect = true;
                string[] answers = answer.Split(',');
                model.correct = new int[answers.Length];
                for (int i = 0; i < answers.Length; i++)
                {
                    if (model.answer[i] == null)
                    {
                        allCorrect = false;
                        model.correct[i] -= 1;
                        question.AnswerCorrect -= 1;
                    }
                    else if (answers[i] == model.answer[i].Trim())
                    {
                        model.correct[i] = 1;
                    }
                    else
                    {
                        allCorrect = false;
                        model.correct[i] -= 1;
                        question.AnswerCorrect -= 1;
                    }
                }
                if (allCorrect)
                {
                    question.AnswerCorrect = (int)Math.Abs(question.AnswerCorrect) + 1;
                }
            }
            else
            {
                model.correct = new int[1];
                if (model.answer[0] == null)
                {
                    question.AnswerCorrect -= 1;
                }
                else if (answer == model.answer[0].Trim())
                {
                    question.AnswerCorrect = (int)Math.Abs(question.AnswerCorrect) + 1;
                }
                else if (question.AnswerCorrect <= 0)
                {
                    question.AnswerCorrect -= 1;
                }
                model.correct[0] = question.AnswerCorrect;
            }
            _context.SaveChanges();

            return PartialView("_Question", model);
        }

        public IActionResult Checkworkset(QuestionSetViewModel modelSet)
        {
            for (int i = 0; i < modelSet.questions.Length; i++)
            {

                byte[] vals;
                HttpContext.Session.TryGetValue("Q" + modelSet.questions[i].questionID, out vals);

                string answer = Encoding.ASCII.GetString(vals);
                var question = _context.questions.FirstOrDefault(x => x.QuestionID == modelSet.questions[i].questionID);
                if (answer.Contains(","))
                {
                    var allCorrect = true;
                    string[] answers = answer.Split(',');
                    modelSet.questions[i].correct = new int[answers.Length];
                    for (int q = 0; q < answers.Length; q++)
                    {
                        if (modelSet.questions[i].answer[q] == null)
                        {
                            allCorrect = false;
                            modelSet.questions[i].correct[q] -= 1;
                            question.AnswerCorrect -= 1;
                        }
                        else if (answers[q] == modelSet.questions[i].answer[q].Trim())
                        {
                            modelSet.questions[i].correct[q] = 1;
                        }
                        else if (question.AnswerCorrect <= 0)
                        {
                            allCorrect = false;
                            modelSet.questions[i].correct[q] -= 1;
                            question.AnswerCorrect -= 1;
                        }
                    }
                    if (allCorrect)
                    {
                        question.AnswerCorrect = (int)Math.Abs(question.AnswerCorrect) + 1;
                    }
                }
                else
                {
                    if (modelSet.questions[i].answer[0] == null)
                    {
                        question.AnswerCorrect -= 1;
                    }
                    else if (answer == modelSet.questions[i].answer[0].Trim())
                    {
                        question.AnswerCorrect = (int)Math.Abs(question.AnswerCorrect) + 1;
                    }
                    else if (question.AnswerCorrect <= 0)
                    {
                        question.AnswerCorrect -= 1;
                    }
                    modelSet.questions[i].correct[0] = question.AnswerCorrect;
                }
                modelSet.questions[i].question = Interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\" + _context.questionTypes.SingleOrDefault(x => x.TypeID == question.Question_Type).Class, question.Seed);
            }

            _context.SaveChanges();

            return PartialView("_FullSet", modelSet);
        }
        #endregion
    }
}