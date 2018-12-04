using AutoQuestionGenerator.DatabaseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Accounts
{
    public class UserHelper
    {
        public static Users getUser(int uid, IdentityModels dbContext)
        {
            return dbContext.users.FirstOrDefault(x => x.UserID == uid);
        }

        public static Users getUser(string uname, IdentityModels dbContext)
        {
            return dbContext.users.FirstOrDefault(x => x.Username == uname);
        }

        public static IEnumerable<Groups> GetGroups(int uid, IdentityModels dbContext)
        {
            List<Groups> groups = new List<Groups>();
            groups.Add(new Groups
            {
                GroupID = -1,
                Group_Name = "Create New Group"
            });
            groups.AddRange(dbContext.groups.Where(x => x.CreatedBy == uid || x.AccessType == 0).ToList());
            return groups;
        }

        public static int GetUserId(ISession session)
        {
            byte[] val;
            if(session.TryGetValue("UId", out val))
            {
                return Convert.ToInt32(Encoding.ASCII.GetString(val));
            }
            return -1;
        }
    }
}
