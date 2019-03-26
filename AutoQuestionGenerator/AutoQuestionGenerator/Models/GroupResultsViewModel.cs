using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Helper;
using AutoQuestionGenerator.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class GroupResultsViewModel
    {
        public GroupResultsViewModel(int WorksetID, IdentityModels _context)
        {
            var sets = _context.questionSets.Where(x => x.WorkSetID == WorksetID).ToList();
            int correct = 0, total = 0;
            List<UserQuestions> usrQs = new List<UserQuestions>();
            List<CompletedQuestion> qs = new List<CompletedQuestion>();
            foreach (var set in sets)
            {
                var questions = _context.questions.Where(x => x.QuestionSetID == set.QuestionSetID).ToList();
                List<CompletedQuestion> qusts = new List<CompletedQuestion>();
                foreach (var q in questions)
                {
                    qusts.Add(new CompletedQuestion()
                    {
                        Type = _context.questionTypes.First(x => x.TypeID == q.Question_Type),
                        AnsweredCorrent = q.AnswerCorrect
                    });
                    total++;
                    if (q.AnswerCorrect > 0)
                        correct++;
                }
                qs.AddRange(qusts);
                string name = "User not found / deleted";
                Users usr = _context.users.FirstOrDefault(x => x.UserID == set.UserID);
                if (usr != null)
                {
                    name = usr.First_Name + usr.Last_Name;
                }
                if (usrQs.FirstOrDefault(x => x.UserID == set.UserID) == null)
                {
                    usrQs.Add(new UserQuestions()
                    {
                        UserID = set.UserID,
                        Name = name,
                        WorstPercentage = new PercentageModel()
                        {
                            Current = qusts.Where(x => x.AnsweredCorrent > 0).Count(),
                            Total = qusts.Count()
                        },
                        Percentage = new PercentageModel()
                        {
                            Current = qusts.Where(x => x.AnsweredCorrent > 0).Count(),
                            Total = qusts.Count()
                        },
                        Questions = new CompletedQuestion[][] { qusts.ToArray() }
                    });
                }
                else
                {
                    UserQuestions qust = usrQs.FirstOrDefault(x => x.UserID == set.UserID);
                    qust.Attempts += 1;
                    var qrs = new List<CompletedQuestion[]>() { qusts.ToArray() };
                    qrs.AddRange(qust.Questions);
                    qust.Questions = qrs.ToArray();
                    if (qust.Percentage.Percentage < new PercentageModel()
                    {
                        Current = qusts.Where(x => x.AnsweredCorrent > 0).Count(),
                        Total = qusts.Count()
                    }.Percentage)
                    {
                        qust.Percentage = new PercentageModel()
                        {
                            Current = qusts.Where(x => x.AnsweredCorrent > 0).Count(),
                            Total = qusts.Count()
                        };
                    }
                    else if (qust.WorstPercentage.Percentage > new PercentageModel()
                    {
                        Current = qusts.Where(x => x.AnsweredCorrent > 0).Count(),
                        Total = qusts.Count()
                    }.Percentage)
                    {
                        qust.WorstPercentage = new PercentageModel()
                        {
                            Current = qusts.Where(x => x.AnsweredCorrent > 0).Count(),
                            Total = qusts.Count()
                        };
                    }
                }
            }

            Averages = usrQs.ToArray();

            Percentage = new PercentageModel()
            {
                Current = correct,
                Total = total
            };

            Question = qs.ToArray();
            if (sets.Count != 0)
            {
                for (int i = 0; i < usrQs.Count; i++)
                {
                    usrQs[i].sameCount = usrQs.Where(x => x.Percentage.Percentage == usrQs[i].Percentage.Percentage).Count();
                }
                Question = qs.ToArray();
                List = (from question in Question
                        group question by question.Type.TypeID into QGroup
                        select new PercentageModel
                        {
                            Current = QGroup.Where(x => x.AnsweredCorrent > 0).Count(),
                            Total = QGroup.Count()
                        }).ToArray();
                questionTypes = (from question in Question
                                 select question.Type.Type_Name).ToArray();
                questionTypes = questionTypes.Unique().ToArray();
            }
        }
        public PercentageModel Percentage { get; set; }
        public CompletedQuestion[] Question { get; set; }
        public UserQuestions[] Averages { get; set; }
        public PercentageModel[] List { get; set; }
        public string[] questionTypes { get; set; }
    }

    public class UserQuestions
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int Attempts { get; set; }
        public PercentageModel Percentage { get; set; }
        public PercentageModel WorstPercentage { get; set; }
        public int sameCount { get; set; }
        public CompletedQuestion[][] Questions { get; set; }
    }
}
