using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class QuestionSetViewModel
    {
        public KeyValuePair<int, QuestionModels.Question> Qusts { get; set; }

        public string answer { get; set; }
    }
}
