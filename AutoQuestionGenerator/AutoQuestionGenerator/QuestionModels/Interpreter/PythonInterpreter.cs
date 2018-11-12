using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.QuestionModels.Interpreter
{
    public class PythonInterpreter
    {
        private static (string, string) GetParam(int serviceid, string path)
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
            ScriptSource source = engine.CreateScriptSourceFromFile(path);
            object result = source.Execute(scope);
            Question = scope.GetVariable<string>("question");
            Answer = scope.GetVariable<string>("answer");
            return (Question, Answer);
        }

        public Question GenerateQuestion(string path)
        {
            var scriptOutput = GetParam(4000, path);

            Question ret = new StoredQuestion(scriptOutput.Item1, scriptOutput.Item2);
            return ret;
        }
    }
}
