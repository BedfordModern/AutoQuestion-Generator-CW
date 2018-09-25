using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Questions
{
    interface Question
    {
        object Question(string random);
        string Answer(object Question);
    }
}
