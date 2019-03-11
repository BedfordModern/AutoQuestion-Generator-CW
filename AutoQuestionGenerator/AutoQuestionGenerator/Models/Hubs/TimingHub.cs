using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Models.Hubs
{
    public class TimingHub : Hub
    {
        public static Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();
        public async Task SendTimes(string UserID, int timeM)
        {
            int timeS = timeM * 10;
            for (int i = 0; i < timeS; i++)
            {
                Clients.Client(UserID).SendAsync("Timer", (i*6).ToString(), "Recieved");
                await Task.Delay(6000);
            }
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
