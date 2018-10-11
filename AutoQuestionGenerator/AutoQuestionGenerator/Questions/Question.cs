using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionApp.Questions
{
    interface Question
    {
        object GetQuestion();
        object GetAnswer();

        object Question(string random);
        string Answer(object Question);
    }
}
