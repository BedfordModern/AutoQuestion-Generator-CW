using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class LoginViewModel
    {
        public string Organisation { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Error { get; set; }
    }
}
