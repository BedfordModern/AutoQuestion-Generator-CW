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
        public CompleteQuestionViewModel(int QuestionSetID, IdentityModels _context)
        {
            var questions = _context.questions.Where(x => x.QuestionSetID == QuestionSetID).ToList();
            List<CompletedQuestion> qs = new List<CompletedQuestion>();
            foreach (var q in questions)
            {
                qs.Add(new CompletedQuestion()
                {
                    Type = _context.questionTypes.First(x => x.TypeID == q.Question_Type),
                    AnsweredCorrent = q.AnswerCorrect
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
                        Current = QGroup.Where(x => x.AnsweredCorrent > 0).Count(),
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
        public int AnsweredCorrent { get; set; }
        public QuestionTypes Type { get; set; }
    }
}
