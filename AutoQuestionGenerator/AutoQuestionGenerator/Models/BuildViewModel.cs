using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models
{
    public class BuildViewModel
    {
        public int WorkSetID { get; set; }
        public WorkPartial[] Work { get; set; }
        public CreatedWork createdWork { get; set; }
    }

    public class WorkPartial
    {
        public string TypeName { get; set; }
        public int TypeID { get; set; }
        public int Seed { get; set; }
        public string Answer { get; set; }
    }

    public class CreatedWork
    {
        public bool SelectFromList { get; set; }
        public CatagoryTypes[] CatagoryTypes { get; set; }
    }

    public class CatagoryTypes
    {
        public string CatTypeName { get; set; }
        public LengthCatagory[] Catagories { get; set; }
    }

    public class LengthCatagory
    {
        public string catagoryName { get; set; }
        public TypedWork[] WorkTypes { get; set; }
    }

    public class TypedWork
    {
        public int TypeID { get; set; }
        public string WorkType { get; set; }
        public WorkPartial[] PossibleWork { get; set; }
    }
}
