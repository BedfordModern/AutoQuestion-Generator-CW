using System;
using QuestionApp.Accounts;
namespace QuestionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string hash = Hasher.Hash("ThisIsAPassword");
            Console.WriteLine(hash);
            Console.WriteLine(Hasher.ValidatePassword("ThisIsAPassword", hash));
            Console.ReadLine();
        }
    }
}
