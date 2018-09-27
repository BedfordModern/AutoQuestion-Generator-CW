using System;
using QuestionApp.Questions;
namespace QuestionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            BinaryIntegers qu = new BinaryIntegers(0, 12);
            Console.WriteLine(qu.Qust);
            int input = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(qu.Answer(input));
            Console.WriteLine(qu.Ans);
            Console.ReadLine();
        }
    }
}
