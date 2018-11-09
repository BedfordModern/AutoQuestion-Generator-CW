using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Questions.Interpreter
{
    public class PythonInterpreter
    {
        public static (string, string) GetParam(int serviceid)
        {
            string Question= "", Answer = "";


            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            var d = new Dictionary<string, object>
            {
                { "serviceid", serviceid},
                { "question", Question},
                { "answer", Answer}
            };

            scope.SetVariable("params", d);
            ScriptSource source = engine.CreateScriptSourceFromFile(@"C:\Users\Daniel Ledger 9CLW\Documents\Python\testPart.py");
            object result = source.Execute(scope);
            Question = scope.GetVariable<string>("question");
            Answer = scope.GetVariable<string>("answer");
            return (Question, Answer);
        }
    }
}
