using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class WorkViewModel
    {
        public int WorkID { get; set; }
        public string WorkSetName { get; set; }
        public int Seed { get; set; }
        public string QuestionType { get; set; }
        public int? Difficulty { get; set; }
    }
}
