using AutoQuestionGenerator.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class CreateGroupViewModel
    {
        public string GroupName { get; set; }
        public int AccessType { get; set; }
        public int[] GroupUsers { get; set; }

        public bool CloseAfter { get; set; }

        public Users[] Users { get; set; }
        public AccessTypes[] AccessTypes { get; set; }
    }
}
