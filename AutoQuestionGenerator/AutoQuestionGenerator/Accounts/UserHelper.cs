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
        public const string
            ROLE_ADMIN = "Admin",
            ROLE_STUDENT = "Student",
            ROLE_TEACHER = "Teacher";

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
            groups.Add(new Groups
            {
                GroupID = -2,
                Group_Name = "No Group"
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

        public static bool UserInRole(byte[] uid, string roleName, IdentityModels dbContext)
        {
            if(uid == null || uid.Length == 0)
                return false;

            return UserInRole(Convert.ToInt32(Encoding.ASCII.GetString(uid)), roleName, dbContext);
        }

        public static bool UserInRole(int uid, string roleName, IdentityModels dbContext)
        {
            var role = dbContext.roles.FirstOrDefault(x => x.Role_Name == roleName);
            if (role == null) return false;

            return dbContext.userroles.FirstOrDefault(x => x.UserID == uid && x.RoleID == role.RoleID) != null;
        }

        public static bool UserHasBasicAccess(int uid, int worksetID, IdentityModels dbContext)
        {
            var workset = dbContext.worksets.FirstOrDefault(x => x.WorksetID == worksetID);
            if (workset == null) return false;

            return (dbContext.groupUsers.FirstOrDefault(x => x.GroupID == workset.GroupID && x.UserID == uid) != null) || (workset.SetBy == uid);
        }

        public static bool OwnsWorkset(int uid, int worksetID, IdentityModels dbContext)
        {
            var workset = dbContext.worksets.FirstOrDefault(x => x.WorksetID == worksetID);

            return (workset != null) && (workset.SetBy == uid);
        }

        public static bool InSameOrganisation(int uid, int worksetID, IdentityModels dbContext)
        {

            var workset = dbContext.worksets.FirstOrDefault(x => x.WorksetID == worksetID);
            if (workset == null) return false;
            var setBy = dbContext.users.FirstOrDefault(x => x.UserID == workset.SetBy);
            var user = dbContext.users.FirstOrDefault(x => x.UserID == uid);
            if (user == null) return false;
            return user.OrganisationID == setBy.OrganisationID;
        }
    }
}
