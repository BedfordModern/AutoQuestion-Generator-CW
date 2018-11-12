using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.QuestionModels
{
    public class BinaryToIntegers : BinaryToDenaryQuestion
    {
        public BinaryToIntegers(int seed, int dificulty) : base(seed, dificulty)
        {
            Question();
        }

        private object Question()
        {
            if (string.IsNullOrEmpty(Question_Value))
            {
                Question_Value = "";
                int num = base.rand.Next((int)Math.Pow(2, Dificulty));
                Answer_Value = num;

                for (int i = (Dificulty - 1); i >= 0; i--)
                {
                    if (num >= (int)Math.Pow(2, i))
                    {
                        Question_Value += "1";
                        num -= (int)Math.Pow(2, i);
                    }
                    else
                    {
                        Question_Value += "0";
                    }
                }
            }
            return base.Question(Question_Value);
        }

        public new string Answer(object Question)
        {
            if (Question is int)
            {
                if ((int)Question == Answer_Value)
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

    public class IntegerToBinary : DenaryToBinaryQuestion
    {

        public IntegerToBinary(int seed, int difficulty) : base(seed, difficulty)
        {
            Question();
        }

        public object Question()
        {
            if (string.IsNullOrEmpty(Answer_Value))
            {
                Answer_Value = "";
                int num = base.rand.Next((int)Math.Pow(2, Dificulty));
                Question_Value = num;

                for (int i = (Dificulty - 1); i >= 0; i--)
                {
                    if (num >= (int)Math.Pow(2, i))
                    {
                        Answer_Value += "1";
                        num -= (int)Math.Pow(2, i);
                    }
                    else
                    {
                        Answer_Value += "0";
                    }
                }
            }
            return base.Question(Question_Value.ToString());
        }

        public new string Answer(object Question)
        {
            if(Question is string)
            {
                if((Question as string) == Answer_Value)
                {
                    return "True";
                }
            }
            return "False";
        }
    }
}
