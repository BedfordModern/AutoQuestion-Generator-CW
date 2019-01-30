using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoQuestionGenerator.DatabaseModels;

namespace AutoQuestionGenerator.Models
{
    public class UploadCompleteViewModel
    {
        public Catagory[] Catagories { get; set; }
        public CatagoryType[] Types { get; set; }
    }
}
