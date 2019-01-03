using AutoQuestionGenerator.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class CreateSetViewModel
    {
        public bool RandomQuestions { get; set; }
        public bool SelectFromList { get; set; }
        public int SetType { get; set; }
        public int GroupID { get; set; }
        public int TimeAllowed { get; set; }
        public DateTime Date_Due { get; set; }

        public static IEnumerable<SetTypes> SetTypes;
        public IEnumerable<Groups> Groups;
    }
}
