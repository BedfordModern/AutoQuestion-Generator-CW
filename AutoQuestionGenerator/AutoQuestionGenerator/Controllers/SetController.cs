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
        PythonInterpreter Interpreter;

        /// <summary>
        /// When the controller is loaded generate a new python interpreter.
        /// </summary>
        public SetController()
        {
            Interpreter = new PythonInterpreter();
        }

        // Uses a method to convert a workset to a PDF
        public IActionResult Print(int setID)
        {
            var set = DatabaseConnector.GetWorkset(setID);
            var path = set.ToPDF();
            return LocalRedirect("~" + path);
        }

        /// <summary>
        /// Return the index page for viewing the workset
        /// however the index is the page used to complete a workset
        /// so this file also sets up all the questions and 
        /// questionset information and it then saves this.
        /// </summary>
        /// <param name="WorkSetID"></param>
        /// <returns></returns>
        public IActionResult Index(int WorkSetID)
        {
            int uid = UserHelper.GetUserId(HttpContext.Session);

            // Checks whether the user can access and therefore complete the workset
            // If they can set it up.
            if (UserHelper.UserHasBasicAccess(uid, WorkSetID))
            {
                var set = DatabaseConnector.GetWorkset(WorkSetID);

                if (set == null)
                {
                    return RedirectToAction("Index", "Work");
                }

                var work = DatabaseConnector.GetWhere<Work>($"WorkSetID={set.WorksetID}");

                // Shuffle the work if it is supposed to be in a random order.
                if (set.RandomOrdering)
                    work.Shuffle();

                // Create the question set.
                QuestionSets qSet = new QuestionSets()
                {
                    UserID = uid,
                    WorkSetID = WorkSetID,
                    Date_Asked = DateTime.Today
                };

                // Save to the datbase and get the ID of the question set
                qSet.QuestionSetID = DatabaseConnector.AddQuestionSet(qSet);

                List<QuestionViewModel> Qusts = new List<QuestionViewModel>();

                // Go through each piece of required work and update and save it.
                foreach (var piece in work)
                {
                    // Generate the seed for the question
                    var seed = StoredQuestion.GenerateSeed(piece.Seed);

                    // Create the question and save it to the database.
                    Questions qust = new Questions()
                    {
                        QuestionSetID = qSet.QuestionSetID,
                        Question_Type = piece.QuestionType,
                        Difficulty = (int)piece.Difficulty,
                        Seed = seed,
                        AnswerCorrect = 0
                    };
                    qust.QuestionID = DatabaseConnector.AddQuestion(qust);

                    // Gets the question informataion from the databse (the python file) and passes it into the pythond interpreter.
                    var curQuest = Interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\"
                        + DatabaseConnector.Get<QuestionTypes>().SingleOrDefault(x => x.TypeID == qust.Question_Type).Class, seed);

                    // From the scriptOuput get the names of the boxes and the hints for them
                    var boxes = curQuest.Boxes().Split(',');
                    var Hints = new List<string>();
                    Hints.AddRange(curQuest.Hints.Split(','));

                    //If we don't int for each box add some blank ones.
                    for (int i = Hints.Count - 1; i < boxes.Length; i++)
                    {
                        Hints.Add("");
                    }

                    // Create the question view model for the question set
                    var questionSet = new QuestionViewModel()
                    {
                        questionID = qust.QuestionID,
                        question = curQuest,
                        answer = new string[boxes.Length],
                        correct = new int[boxes.Length],
                        Hints = Hints.ToArray(),
                        Boxes = boxes
                    };
                    //Add it to the list of questions.
                    Qusts.Add(questionSet);

                    // Add the answer into the Http session to get the 
                    HttpContext.Session.Set("Q" + qust.QuestionID, Encoding.UTF8.GetBytes(questionSet.question.GetAnswer().ToString()));
                }
                // Return the entire question set 
                return View(new QuestionSetViewModel()
                {
                    QuestionSetID = qSet.QuestionSetID,
                    PerQuestion = (set.SetType == 1),
                    questions = Qusts.ToArray()
                });
            }
            return Unauthorized();
        }

        /// <summary>
        /// this will delete a workset and all matched information
        /// including any quesion sets that may have been created from it
        /// and all the work and questions from it.
        /// </summary>
        /// <param name="setID"></param>
        /// <returns></returns>
        public IActionResult Delete(int setID)
        {
            if (UserHelper.OwnsWorkset(UserHelper.GetUserId(HttpContext.Session), setID))
            {
                // Gets the questionsets with this ID
                var questionSets = DatabaseConnector.GetWhere<QuestionSets>($"WorkSetID={setID}");

                // Deletes all the questions from all of the question sets
                foreach (var qset in questionSets)
                {
                    DatabaseConnector.DeleteQuestions(qset.QuestionSetID);
                }
                // Deletes the question sets
                DatabaseConnector.DeleteQuestionSets(setID);

                // Gets the workset from that id
                var workset = DatabaseConnector.GetWorkset(setID);

                // Deletes all of the work from the worksets
                DatabaseConnector.DeleteWork(setID);

                //Informs the user what they have deleted
                string Name = workset.WorksetName + " - " + workset.WorksetID;

                // Deletes the workset
                DatabaseConnector.DeleteWorkset(setID);

                // Return the delete infomation.
                return View("Delete", Name);
            }
            return Unauthorized();
        }

        /// <summary>
        /// Gets the result of a workset using statistical modeling
        /// </summary>
        /// <param name="setID">the worksetid</param>
        /// <returns></returns>
        public IActionResult Results(int setID)
        {
            // Checjs that the user owns the workset
            if (UserHelper.OwnsWorkset(UserHelper.GetUserId(HttpContext.Session), setID))
            {
                GroupResultsViewModel model = new GroupResultsViewModel(setID);
                return View(model);
            }
            return Unauthorized();
        }

        /// <summary>
        /// When the user completes a workset give them information about the workset they just completed
        /// </summary>
        /// <param name="setID"></param>
        /// <returns></returns>
        public IActionResult Complete(int setID)
        {
            // Get the workset information and model.
            CompleteQuestionViewModel model = new CompleteQuestionViewModel(setID);

            return View(model);
        }

        /// <summary>
        /// This method opens the creation page of a new workset
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            // Checks if the user has the privilages to create a new workset
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_TEACHER)
                || UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN))
            {
                // Creates the new createworkset model
                var Model = new CreateSetViewModel();

                // Gets the groups the teacher has access too from the database.
                Model.Groups = UserHelper.GetGroups(UserHelper.GetUserId(HttpContext.Session));
                return View(Model);
            }
            // Errors if they do not have access.
            return Unauthorized();
        }

        /// <summary>
        /// Gets the information about the workset that the user selected
        /// and then creates the database model of the workset
        /// which is then saved in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CreateSetViewModel model)
        {
            // Checks that valid model has been returned
            if (!ModelState.IsValid)
            {
                model.Groups = UserHelper.GetGroups(UserHelper.GetUserId(HttpContext.Session));
                return View(model);
            }

            // Creates the workset from the given information
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

            // Checks if the workset should have no group because it was saved for later
            if (sets.GroupID == -2) sets.GroupID = null;

            // Adds the workset to the database and gets its ID
            sets.WorksetID = DatabaseConnector.AddWorkset(sets);

            // Creates the model that nit used to build the workset
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

        /// <summary>
        /// If you want to edit a workset it has to be reloaded into the page
        /// this is what this function does
        /// it also then generates the models for that work.
        /// </summary>
        /// <param name="setID"></param>
        /// <returns></returns>
        public IActionResult Build(int setID)
        {
            // See
            if (setID == 0) return Unauthorized();
            if (UserHelper.OwnsWorkset(UserHelper.GetUserId(HttpContext.Session), setID)
                || (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN) && UserHelper.InSameOrganisation(UserHelper.GetUserId(HttpContext.Session), setID)))
            {
                // Gets the workset information from the database
                var workset = DatabaseConnector.GetWorkset(setID);
                if (workset == null) return NotFound();

                //Sets up the array of work components
                List<WorkPartial> currentwork = new List<WorkPartial>();

                // Get the work already in this sheet from the database
                var current = DatabaseConnector.GetWhere<Work>($"WorkSetID={setID}");

                // Iterates through every piece of work currently avaliable.
                foreach (var piece in current)
                {
                    // Gets the information about the question.
                    var workPartial = new WorkPartial()
                    {
                        TypeName = DatabaseConnector.Get<QuestionTypes>().FirstOrDefault(x => x.TypeID == piece.QuestionType).Type_Name,
                        TypeID = piece.QuestionType,
                        Seed = piece.Seed,
                        Answer = ""
                    };

                    // If the work is supposed to have a set answer do this
                    if (workset.ExamStyle)
                    {
                        workPartial.Answer = Interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\" + DatabaseConnector.Get<QuestionTypes>().SingleOrDefault(x => x.TypeID == piece.QuestionType).Class, piece.Seed).GetAnswer().ToString();
                    }
                    currentwork.Add(workPartial);
                }

                // Create the build model from all of the given information and pass it to the page.
                var model = new BuildViewModel()
                {
                    WorkSetID = setID,
                    createdWork = new CreatedWork()
                    {
                        SelectFromList = DatabaseConnector.GetWorkset(setID).ExamStyle,
                        CatagoryTypes = GetAllQuestions()
                    },
                    Work = currentwork.ToArray()
                };

                return View(model);
            }
            return Unauthorized();
        }

        /// <summary>
        /// This will take in the model from the webpage and use it to setup and build the workset for students.
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Build(BuildViewModel Model)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                Model.createdWork = new CreatedWork()
                {
                    SelectFromList = DatabaseConnector.GetWorkset(Model.WorkSetID).ExamStyle,
                    CatagoryTypes = GetAllQuestions()
                };
                return View(Model);
            }

            // Delete any work that may already exist for that workset
            DatabaseConnector.DeleteWork(Model.WorkSetID);

            // Add each piece of work to the database
            foreach (var work in Model.Work)
            {
                DatabaseConnector.AddWork(new Work()
                {
                    Difficulty = 1,
                    QuestionType = work.TypeID,
                    Seed = work.Seed,
                    WorkSetID = Model.WorkSetID
                });
            }
            // Commit any changes to the database
            DatabaseConnector.PushChanges();

            return Redirect("~/Work/Set");
        }

        /// <summary>
        /// Gets all of the possible questions
        /// and question types that could be asked to the 
        /// user by collecting and setting up all that are on the database.
        /// </summary>
        /// <returns></returns>
        public CatagoryTypes[] GetAllQuestions()
        {
            // Gets the list of all the catagory types and iterates through them
            List<CatagoryTypes> catagoryTypes = new List<CatagoryTypes>();
            CatagoryType[] catTypes = DatabaseConnector.Get<CatagoryType>();
            foreach (var CatType in catTypes)
            {
                // gets the list of all the catagories for that type
                List<LengthCatagory> lengthCatagories = new List<LengthCatagory>();
                var catagories = DatabaseConnector.GetWhere<Catagory>($"CatagoryType={CatType.CatTypeID}");
                foreach (var Cat in catagories)
                {
                    // Get all of the question types for that catagory
                    List<TypedWork> works = new List<TypedWork>();
                    var qtypes = DatabaseConnector.GetWhere<QuestionTypes>($"Catagory={Cat.CatagoryID}").ToArray();
                    int seed = 0;
                    foreach (var item in qtypes)
                    {
                        // Gets all of the work for that item
                        WorkPartial[] possibleWork = new WorkPartial[5];
                        for (int i = 0; i < 5; i++)
                        {
                            // Generates a random piece of work for that question type to show the user.
                            seed = StoredQuestion.GenerateSeed();
                            var work = Interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\" + item.Class, 0);

                            possibleWork[i] = new WorkPartial()
                            {
                                Seed = seed,
                                Answer = work.GetAnswer().ToString()
                            };
                        }
                        // Adds all of the work to list of question types
                        works.Add(new TypedWork()
                        {
                            WorkType = item.Type_Name,
                            TypeID = item.TypeID,
                            PossibleWork = possibleWork.ToArray()
                        });
                    }
                    // adds that catagory and the question types in it to the list of catagorys
                    lengthCatagories.Add(new LengthCatagory()
                    {
                        catagoryName = Cat.CatagoryName,
                        WorkTypes = works.ToArray()
                    });
                }
                // adds the catagory type and its list of catagories to the returning list
                catagoryTypes.Add(new CatagoryTypes()
                {
                    CatTypeName = CatType.CatTypeName,
                    Catagories = lengthCatagories.ToArray()
                });
            }
            // return the list of catagory types.
            return catagoryTypes.ToArray();
        }

        // this section are the functions that are specific to webpages put are called in
        // scope and therefore run that way they are called when a webpage wants to update
        // without being refreshed or to check some information.
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

            string answer = Encoding.UTF8.GetString(vals);
            var question = DatabaseConnector.Get<Questions>().FirstOrDefault(x => x.QuestionID == model.questionID);

            // If the question contains two parts check each one induvidually
            CheckOneQuestion(model, answer, question);
            // Updates the databases question.
            DatabaseConnector.PushChanges();

            return PartialView("_Question", model);
        }

        private void CheckOneQuestion(QuestionViewModel model, string answer, Questions question)
        {
            if (answer.Contains(","))
            {
                // If they are all correct we can get rid of some of the page information
                var allCorrect = true;
                string[] answers = answer.Split(',');
                model.correct = new int[answers.Length];

                // Iterate though the actual and given answers
                for (int i = 0; i < answers.Length; i++)
                {
                    // Check the answer against the correct answer
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
                // If they are all correct set the question to be correct in the database
                if (allCorrect)
                {
                    question.AnswerCorrect = (int)Math.Abs(question.AnswerCorrect) + 1;
                }
            }
            // If the question only has one part check it
            else
            {
                // Set the model to be correct if the question is.
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
            DatabaseConnector.Update(question);
        }

        public IActionResult Checkworkset(QuestionSetViewModel modelSet)
        {

            var questions = DatabaseConnector.GetWhere<Questions>($"QuestionSetID={modelSet.QuestionSetID}");

            // This is the same as the check question model however it will check each question in the worksheet.
            for (int i = 0; i < modelSet.questions.Length; i++)
            {
                var question = questions[i];

                byte[] vals;
                HttpContext.Session.TryGetValue("Q" + modelSet.questions[i].questionID, out vals);

                string answer = Encoding.UTF8.GetString(vals);
                CheckOneQuestion(modelSet.questions[i], answer, question);
                modelSet.questions[i].question = Interpreter.GenerateQuestion(AppContext.BaseDirectory + @"wwwroot\lib\Python\" + DatabaseConnector.Get<QuestionTypes>().SingleOrDefault(x => x.TypeID == question.Question_Type).Class, question.Seed);

            }
            // Pushes all the changes to the database
            DatabaseConnector.PushChanges();

            // returns the question set
            return PartialView("_FullSet", modelSet);
        }
        #endregion
    }
}