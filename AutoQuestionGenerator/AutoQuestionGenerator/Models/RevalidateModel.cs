using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class RevalidateModel
    {
        public int id { get; set; }
        public string Password { get; set; }
        public string AspAction { get; set; }
        public string AspController { get; set; }
    }
}
