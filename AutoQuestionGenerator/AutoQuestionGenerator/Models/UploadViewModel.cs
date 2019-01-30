using AutoQuestionGenerator.DatabaseModels;
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
        public NamedType[] Items { get; set; }
        public Catagory[] Catagories { get; set; }
    }

    public class NamedType
    {
        public string Catagory { get; set; }
        public string Name { get; set; }
    }
}
