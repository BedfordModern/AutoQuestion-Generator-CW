using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class UploadViewModel
    {
        public Guid identifier { get; set; }
        public Dictionary<int, string> documents { get; set; }
    }
}
