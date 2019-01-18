using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models.Hubs
{
    public class FileHub : Hub
    {
        public static Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();
        public async Task SendMessage(string UserID, string user, string message)
        {
            await Clients.Client(UserID).SendAsync("ReceiveMessage", user, "Recieved");
        }

        public async Task GetUserID(string UserID, string post)
        {
            await Clients.Client(UserID).SendAsync("ReceiveMessage", UserID, "Recieved");
            ConnectedUsers.Add(post, UserID);
        }

        public static async Task<string> WaitForUserID(string guid)
        {
            for (int i = 0; i < 20; i++)
            {
                string id;
                if (ConnectedUsers.TryGetValue(guid, out id))
                    return id;
                await Task.Delay(150);
            }
            return "no reply";
        }
    }
}
