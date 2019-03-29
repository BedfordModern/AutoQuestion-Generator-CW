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
        public GroupUser[] GroupUsers { get; set; }

        public bool CloseAfter { get; set; }

        public Users[] Users { get; set; }
        public AccessTypes[] AccessTypes { get; set; }
    }

    public class GroupUser
    {
        public int UserID { get; set; }
        public bool Selected { get; set; }
    }
}
