using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoQuestionGenerator.Models;
using AutoQuestionGenerator.Models.Hubs;
using AutoQuestionGenerator.QuestionModels.Interpreter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AutoQuestionGenerator.Controllers
{
    public class AccountController : Controller
    {
        IHubContext<FileHub> HubContext;
        PythonInterpreter pythonInterpreter;
        public AccountController(IHubContext<FileHub> hubcontext)
        {
            HubContext = hubcontext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddQuestions()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPython(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            Dictionary<int, string> diction = new Dictionary<int, string>(),
                file = new Dictionary<int, string>();

            int i = 1;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    diction.Add(i, formFile.FileName);
                    file.Add(i, filePath);
                }
            }
            var model = new UploadViewModel()
            {
                identifier = Guid.NewGuid(),
                documents = diction
            };
            TestPython(new UploadViewModel()
            {
                identifier = model.identifier,
                documents = file
            });

            return View(model);
        }

        public async Task TestPython(UploadViewModel model)
        {
            pythonInterpreter = new PythonInterpreter();
            var user = await FileHub.WaitForUserID(model.identifier.ToString());

            foreach (var item in model.documents)
            {
                TestFile(user, item);
            }
        }

        public async Task TestFile(string id, KeyValuePair<int, string> pair)
        {
            if (await pythonInterpreter.CheckQuestion(pair.Value))
            {
                SendMessage(id, pair.Key, "success");
            }
            SendMessage(id, pair.Key, "fail");
        }

        public async Task SendMessage(string UserID, int item, string message)
        {
            await HubContext.Clients.Client(UserID).SendAsync("ReceiveMessage", item, message);
        }
    }
}