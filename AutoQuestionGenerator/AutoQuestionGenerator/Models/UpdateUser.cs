using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class UpdateUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string VerifyPassword { get; set; }
        public bool SamePass
        {
            get
            {
                return Password == VerifyPassword;
            }
        }
    }
}
