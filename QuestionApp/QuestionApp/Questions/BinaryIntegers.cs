using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionApp.Questions
{
    public class BinaryIntegers : BinaryToDenaryQuestion
    {
        public string Qust;
        public int Ans;
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
                Ans = num;
                int count = (int)Math.Log(num, 2);

                for (int i = count; i > 0; i--)
                {
                    if (num > (int)Math.Pow(2, i))
                    {
                        Qust += "1";
                        num -= (int)Math.Pow(2, i);
                    }
                    else
                    {
                        Qust += "0";
                    }
                }
            }
            return base.Question(Qust);
        }

        public new string Answer(object Question)
        {
            if (Question is int)
            {
                if ((int)Question == Ans)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            return "Error";
        }
    }
}
