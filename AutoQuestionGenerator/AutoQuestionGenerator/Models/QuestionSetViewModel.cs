using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class QuestionSetViewModel
    {
        public int QuestionSetID { get; set; }
        public QuestionViewModel[] questions { get; set; }
        public bool PerQuestion { get; set; }
    }

    public class QuestionViewModel
    {
        public int questionID { get; set; }
        public QuestionModels.Question question { get; set; }
        public int correct { get; set; }
        public string answer { get; set; }
    }
}
