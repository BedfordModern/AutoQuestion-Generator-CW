using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Questions
{
    public class BinaryToDenaryQuestion : Question
    {
        public Random rand;
        public BinaryToDenaryQuestion(int seed)
        {
            rand = new Random(seed);
        }

        public object Question(string input)
        {
            foreach(char chars in input)
            {
                if(!(chars == '0' || chars == '1'))
                {
                    return false;
                }
            }
            return input;
        }

        public string Answer(object Answer)
        {
            if(Answer is int)
            {
                return (Answer as string);
            }
            return "Error";
        }
    }
    public class DenaryToBinaryQuestion : Question
    {

        Random rand;
        public DenaryToBinaryQuestion(int seed)
        {
            rand = new Random(seed);
        }

        public string Answer(object Answer)
        {
            string input = Answer as string;
            foreach (char chars in input)
            {
                if (!(chars == '0' || chars == '1' || chars == '.'))
                {
                    return "Error";
                }
            }
            return input;
        }

        public object Question(string input)
        {
            int output;
            if (int.TryParse(input, out output))
            {
                return output;
            }
            return false;
        }
    }

    public class BinaryConverter
    {
        public int BinaryToDenary(string Binary)
        {
            int startPos = 2 ^ Binary.Trim().Length,
                runningTotal = 0;
            foreach(char c in Binary.Trim())
            {
                if(c == '1')
                {
                    runningTotal += startPos;
                }
                startPos /= 2;
            }

            return startPos;
        }
        public string DenaryToBinary(int Denary)
        {
            return "";
        }
    }
}
