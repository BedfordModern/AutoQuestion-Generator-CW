using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Questions
{
    public class BinaryIntegers : BinaryToDenaryQuestion
    {
        public string Qust;
        public int Answer;
        public BinaryIntegers(int seed) : base(seed)
        {
            Question();
        }
        
        private object Question()
        {
            if (string.IsNullOrEmpty(Qust))
            {
                Qust = "";
                int num = base.rand.Next(2000);
                Answer = num;
                int count = (int)Math.Log(num, 2);

                for (int i = count; i > 0; i--)
                {
                    if (num > (2 ^ i))
                    {
                        Qust += "1";
                        num -= (2 ^ i);
                    }
                    else
                    {
                        Qust += "0";
                    }
                }
            }
            return base.Question(Qust);
        }
    }
}
