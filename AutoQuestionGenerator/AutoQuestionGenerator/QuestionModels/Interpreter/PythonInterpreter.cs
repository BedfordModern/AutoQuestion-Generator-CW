using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronPython.Runtime;

namespace AutoQuestionGenerator.QuestionModels.Interpreter
{
    public class PythonInterpreter
    {
        private Random rand;

        private (string, string) GetParam(int serviceid, string path)
        {
            string Question= "", Answer = "";
            int RandInt = rand.Next(1000);

            ScriptRuntimeSetup setup = Python.CreateRuntimeSetup(null);
            ScriptRuntime runtime = new ScriptRuntime(setup);
            var engine = Python.GetEngine(runtime);
            ScriptSource source = engine.CreateScriptSourceFromFile(path);
            var scope = engine.CreateScope();
            var d = new Dictionary<string, object>
            {
                { "serviceid", serviceid},
                { "question", Question},
                { "answer", Answer },
            };
            

            scope.SetVariable("params", d);
            scope.SetVariable("random", RandInt);
            object result = source.Execute(scope);
            Question = scope.GetVariable<string>("question");
            Answer = scope.GetVariable<string>("answer");
            return (Question, Answer);
        }

        public Question GenerateQuestion(string path, int seed = 0)
        {
            if (seed != 0)
            {
                rand = new Random(seed);
            }
            else
            {
                rand = new Random();
            }

            var scriptOutput = GetParam(4000, path);

            Question ret = new StoredQuestion(scriptOutput.Item1, scriptOutput.Item2);
            return ret;
        }

        public async Task<bool> CheckQuestion(string path)
        {
            rand = new Random();
            try
            {
                int seed = rand.Next();
                var questionBase = GenerateQuestion(path, seed);
                var questionCheck = GenerateQuestion(path, seed);
                if (questionBase.GetAnswer().ToString() == questionCheck.GetAnswer().ToString())
                {
                    questionCheck = GenerateQuestion(path);
                    if (questionBase.GetAnswer().ToString() != questionCheck.GetAnswer().ToString())
                    {
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }
    }
}
