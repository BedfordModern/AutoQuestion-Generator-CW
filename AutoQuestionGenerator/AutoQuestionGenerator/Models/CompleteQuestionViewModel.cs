using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Helper;
using AutoQuestionGenerator.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class CompleteQuestionViewModel
    {
        /// <summary>
        /// Generates statistical model about the users performance from the
        /// question set that they have completed
        /// </summary>
        /// <param name="QuestionSetID"></param>
        public CompleteQuestionViewModel(int QuestionSetID)
        {
            var questions = DatabaseConnector.GetWhere<Questions>($"QuestionSetID={QuestionSetID}").ToList();
            List<CompletedQuestion> qs = new List<CompletedQuestion>();
            var types = DatabaseConnector.Get<QuestionTypes>();
            foreach (var q in questions)
            {
                qs.Add(new CompletedQuestion()
                {
                    Type = types.First(x => x.TypeID == q.Question_Type),
                    AnsweredCorrect = q.AnswerCorrect
                });
            }

            Percentage = new PercentageModel()
            {
                Current = questions.Where(x => x.AnswerCorrect > 0).Count(),
                Total = questions.Count
            };

            Question = qs.ToArray();
            List = (from question in Question
                    group question by question.Type.TypeID into QGroup
                    select new PercentageModel
                    {
                        Current = QGroup.Where(x => x.AnsweredCorrect > 0).Count(),
                        Total = QGroup.Count()
                    }).ToArray();
            questionTypes = (from question in Question
             select question.Type.Type_Name).ToArray();
            questionTypes = questionTypes.Unique().ToArray();
        }
        public PercentageModel Percentage { get; set; }
        public CompletedQuestion[] Question { get; set; }
        public PercentageModel[] List { get; set; }
        public string[] questionTypes { get; set; }

    }
    public class CompletedQuestion
    {
        public int AnsweredCorrect { get; set; }
        public QuestionTypes Type { get; set; }
    }
}
