using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class WorksetViewModel
    {
        public int WorksetID { get; set; }
        public string WorksetName { get; set; }
        public string Group_Name { get; set; }
        public string SetBy { get; set; }
        public string SetType { get; set; }
        public int Time_Allowed { get; set; }
        public bool ExamStyle { get; set; }
        public bool RandomOrdering { get; set; }
        public DateTime Date_Set { get; set; }
        public DateTime Date_Due { get; set; }
    }
}
