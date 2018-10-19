using AutoQuestionGenerator.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
