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

        private (string, string, string) GetParam(int serviceid, string path)
        {
            string Question= "", Answer = "", Boxes = "";
            int RandInt = rand.Next(1000);

            ScriptRuntimeSetup setup = Python.CreateRuntimeSetup(null);
            ScriptRuntime runtime = new ScriptRuntime(setup);
            var engine = Python.GetEngine(runtime);
            var paths = engine.GetSearchPaths();
            paths.Add(@"C:\Python27\Lib");
            engine.SetSearchPaths(paths);
            ScriptSource source = engine.CreateScriptSourceFromFile(path);
            var scope = engine.CreateScope();
            var d = new Dictionary<string, object>
            {
                { "serviceid", serviceid},
                { "question", Question},
                { "answer", Answer },
                { "ansName", Boxes },
            };
            

            scope.SetVariable("params", d);
            scope.SetVariable("seed", RandInt);
            object result = source.Execute(scope);
                Question = scope.GetVariable<string>("question");

            Answer = scope.GetVariable<string>("answer");
            try
            {
                Boxes = scope.GetVariable<string>("ansName");
            }
            catch
            {
                Boxes = "";
            }

            return (Question, Answer, Boxes);
        }

        public StoredQuestion GenerateQuestion(string path, int seed = 0)
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

            if (scriptOutput.Item2.Contains(","))
            {
                return new StoredQuestion(scriptOutput.Item1, scriptOutput.Item2, scriptOutput.Item3);
            }
            else
            {
                return new StoredQuestion(scriptOutput.Item1, scriptOutput.Item2);
            }
        }

        public async Task<string> CheckQuestion(string path)
        {
            await Task.Delay(0);
            string error = "";
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
                        return "None";
                    }
                    else
                    {
                        error = "File returned the same value for multiple different seeds!";
                    }
                }
                else
                {
                    error = "File did not return same value for the same seed!";
                }
            }
            catch {
                error = "The file is not a python file!";
            }
            return error;
        }
    }
}
