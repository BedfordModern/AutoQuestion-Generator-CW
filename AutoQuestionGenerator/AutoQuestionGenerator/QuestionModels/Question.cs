using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.QuestionModels
{
    public interface Question
    {
        object GetQuestion();
        object GetAnswer();

        object Question(string random);
        string Answer(object Question);
    }

    public class StoredQuestion : Question
    {
        public static int GenerateSeed(int seed = 0)
        {
            if (seed != 0)
            {
                Random rand = new Random(seed);
                return rand.Next();
            }
            else
            {
                Random rand = new Random();
                return rand.Next();
            }
        }

        private object Quest;
        private object Ans;

        public StoredQuestion(object Question, object Answer)
        {
            Quest = Question;
            Ans = Answer;
        }

        public string Answer(object Question)
        {
            try
            {
                return Ans as string;
            }
            catch
            {
                return "!Nstr";
            }
        }

        public object GetAnswer()
        {
            return Ans;
        }

        public object GetQuestion()
        {
            return Quest;
        }

        public object Question(string random)
        {
            try
            {
                return Quest as string;
            }
            catch
            {
                return "!Nstr";
            }
        }
    }
}
