using AutoQuestionGenerator.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class CompleteQuestionViewModel
    {
        public CompletedQuestion[] Question{ get; set; }
    }

    public class CompletedQuestion
    {
        public int AnsweredCorrent { get; set; }
        public QuestionTypes Type { get; set; }
    }
}
