using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models.Statistics
{
    public class PercentageModel
    {
        public int Total { get; set; }
        public double Current { get; set; }
        public double Percentage { get
            {
                return (Current / (double)Total) * 100.0;
            } }
    }
}
