using AutoQuestionGenerator.DatabaseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Accounts
{
    /// <summary>
    /// This class generates and deals with the user content and controls of my solution:
    /// it creates new users are required; it can retrieve the user data from the session;
    /// will get the groups they own or are a part of;
    /// Check if they have access to control or do certain actions.
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// These strings are the names of the possible roles,
        /// session keys, and error messages that this class can return and use,
        /// making them constant means that they can be accessed and changed only
        /// in this single place if it is ever required.
        /// </summary>
        public const string
            ROLE_ADMIN = "Admin",
            ROLE_STUDENT = "Student",
            ROLE_TEACHER = "Teacher",
            USER_ERROR = "Error",
            SESSION_UID = "UId",
            SESSION_UName = "Username";


        /// <summary>
        /// This method collects the User form the database
        /// it used to do more before I tranfered my queries to one file
        /// but I have left if so I can keep all user information in one file.
        /// </summary>
        /// <param name="uid">The userID</param>
        /// <returns>The user requested</returns>
        public static Users GetUser(int uid)
        {
            return DatabaseConnector.GetUser(uid);
        }

        /// <summary>
        /// Gets the user based on their username and the organisation they are a part of.
        /// It used to do more before I tranfered my queries to one file
        /// but I have left if so I can keep all user information in one file.
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="organisationID"></param>
        /// <returns></returns>
        public static Users GetUser(string username, int organisationID)
        {
            return DatabaseConnector.GetUser(username, organisationID);
        }

        /// <summary>
        /// This gets the groups that are own / accessible to that teacher
        /// this means that the teacher must have either created
        /// the group or it had been created by a system admin
        /// who had made it accessible to the entire organisation.
        /// </summary>
        /// <param name="uid">Id of the user whose groups we are collecting</param>
        /// <returns></returns>
        public static IEnumerable<Groups> GetGroups(int uid)
        {
            //Adds the groups that are needed to "Save" and "Create New" worksheets
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
            //Gets the groups from the database that the teacher can access
            groups.AddRange(DatabaseConnector.GetTeacherGroups(GetUser(uid).OrganisationID, uid));
            return groups;
        }

        /// <summary>
        /// This file gets the userid from where it is stored in the session
        /// this allows it to easily used to find out information about the user
        /// as it is passed into every calling method of the website.
        /// </summary>
        /// <param name="session">The HttpContext session</param>
        /// <returns>The userid of the session</returns>
        public static int GetUserId(ISession session)
        {
            // Gets the byte array stored in the session.
            byte[] val;
            if(session.TryGetValue(UserHelper.SESSION_UID, out val))
            {
                //Decodes the byte array using the UTF8 standard.
                return Convert.ToInt32(Encoding.UTF8.GetString(val));
            }
            // Returns -1 if there is no information in the session
            return -1;
        }

        /// <summary>
        /// Completely logs the user out of their session by removing
        /// and invalidating the session keys for the user meaning
        /// that they will have to log back in again.
        /// </summary>
        /// <param name="session">The HttpContext session</param>
        public static void LogOut(ISession session)
        {
            session.Remove(UserHelper.SESSION_UID);
            session.Remove(UserHelper.SESSION_UName);
        }

        /// <summary>
        /// Checks to see if the user is in a specific role
        /// </summary>
        /// <param name="uid">UserID as byte[]</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>Whether the user is in the role or not</returns>
        public static bool UserInRole(byte[] uid, string roleName)
        {
            // Checks the userID exists
            if(uid == null || uid.Length == 0)
                return false;

            // Converts the uid byte[] into an into to pass into the overload for this method.
            return UserInRole(Convert.ToInt32(Encoding.UTF8.GetString(uid)), roleName);
        }

        /// <summary>
        /// Checks to see if the user is in a specific role
        /// It used to do more before I tranfered my queries to one file
        /// but I have left if so I can keep all user information in one file
        /// making it easier to access.
        /// </summary>
        /// <param name="uid">UserID as an int</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns></returns>
        public static bool UserInRole(int uid, string roleName)
        {
            return DatabaseConnector.UserInRole(uid, roleName);
        }

        /// <summary>
        /// Checks to see if the user has rights to view the workset
        /// This includes checking if they own the workset or
        /// are just a student in that group trying to view that workset.
        /// It used to do more before I tranfered my queries to one file
        /// but I have left if so I can keep all user information in one file
        /// making it easier to access.
        /// </summary>
        /// <param name="uid">User id</param>
        /// <param name="worksetID">Id of the workset</param>
        /// <returns>Whether the student has the right to view that workset</returns>
        public static bool UserHasBasicAccess(int uid, int worksetID)
        {
            return (DatabaseConnector.OwnsWorkset(uid, worksetID) || DatabaseConnector.HasAccessTo(uid, worksetID));
        }

        /// <summary>
        /// Checks to see if a user owns a workset:
        /// It used to do more before I tranfered my queries to one file
        /// but I have left if so I can keep all user information in one file
        /// making it easier to access.
        /// </summary>
        /// <param name="uid">the User ID</param>
        /// <param name="worksetID">the Workset ID</param>
        /// <returns></returns>
        public static bool OwnsWorkset(int uid, int worksetID)
        {
            return DatabaseConnector.OwnsWorkset(uid, worksetID);
        }

        /// <summary>
        /// Checks if two users are in the same organisation
        /// This is to check whether they should be able to view eachothers
        /// information.
        /// </summary>
        /// <param name="user1">The id of either of the users</param>
        /// <param name="user2">the id of the other user</param>
        /// <returns></returns>
        public static bool UsersInSameOrganisation(int user1, int user2)
        {
            var u1 = GetUser(user1);
            // Check on each users existance induvidually
            //becuase it is much faster than the query times
            if (u1 == null) return false;
            var u2 = GetUser(user2);
            if (u2 == null) return false;
            return u2.OrganisationID == u1.OrganisationID;
        }

        /// <summary>
        /// Checks to see if the workset owner and the person trying to access
        /// the workset are in the same organisation. This is important for admins
        /// who may be trying to check all worksets in their organisation.
        /// </summary>
        /// <param name="uid">The id of the user trying to gain access</param>
        /// <param name="worksetID">the Id of the workset being checked</param>
        /// <returns></returns>
        public static bool InSameOrganisation(int uid, int worksetID)
        {
            Worksets workset = DatabaseConnector.GetWorkset(worksetID);
            if (workset == null) return false;
            var setBy = GetUser(workset.SetBy);
            var user = GetUser(uid);
            if (user == null) return false;
            return user.OrganisationID == setBy.OrganisationID;
        }

        /// <summary>
        /// Creates a new user with the specified details
        /// This is used when an admin is making a single user
        /// for their organisation who may have been missed off the
        /// long user list.
        /// </summary>
        /// <param name="uid">User ID of the Admin</param>
        /// <param name="uname">Username of new user</param>
        /// <param name="password">The new users password</param>
        /// <param name="firstname">the First name of the new user</param>
        /// <param name="lastname">Last name of the new user</param>
        /// <returns>The user that has been added</returns>
        internal static Users CreateNewUser(int uid, string uname, string password, string firstname, string lastname)
        {
            Users usr = new Users()
            {
                Username = uname,
                //Makes sure to hash the password before it is put in the database
                Password = Hasher.Hash(password),
                First_Name = firstname,
                Last_Name = lastname,
                // Gets the organisation of the user calling this method
                OrganisationID = GetUser(uid).OrganisationID
            };
            try
            {
                //Creates the new user using the database methods and setup the new userID
                usr.UserID = DatabaseConnector.AddUser(usr);
                return usr;
            }
            // An error was returned when adding to the database
            catch(Exception ex)
            {
                return new Users()
                {
                    Username = USER_ERROR
                };
            }
        }

        /// <summary>v
        /// Creates a new user with the specified details
        /// This is used when an admin is making a single user
        /// for their organisation who may have been missed off the
        /// long user list.
        /// </summary>
        /// <param name="uname">Username of new user</param>
        /// <param name="organisationID">Id of the organisation</param>
        /// <param name="password">The new users password</param>
        /// <param name="firstname">the First name of the new user</param>
        /// <param name="lastname">Last name of the new user</param>
        /// <returns>The user that has been added</returns>
        internal static Users CreateNewUser(string uname, int organisationID, string password, string firstname, string lastname)
        {
            Users usr = new Users()
            {
                Username = uname,
                //Makes sure to hash the password before it is put in the database
                Password = Hasher.Hash(password),
                First_Name = firstname,
                Last_Name = lastname,
                OrganisationID = organisationID,
                //Indicates that the user has not logged in yet.
                Last_Logged_In = DateTime.MinValue
            };
            try
            {
                //Creates the new user using the database methods and setup the new userID
                usr.UserID = DatabaseConnector.AddUser(usr);
                return usr;
            }
            // An error was returned when adding to the database
            catch (Exception ex)
            {
                return new Users()
                {
                    Username = USER_ERROR
                };
            }
        }

        /// <summary>
        /// Gives the user a specific role when requeated.
        /// It will check the rolename against the database then attempt
        /// to give the user this role.
        /// </summary>
        /// <param name="userID">The ID of the user to give the role</param>
        /// <param name="role">Name of the role to be added</param>
        internal static void GiveRole(int userID, string role)
        {
            //Gets the role from the database.
            var dbRole = DatabaseConnector.GetRoleID(role);

            //IF the role is not found it retuns -1.
            if(dbRole == -1)
                throw new KeyNotFoundException("Unable to find the key matching this role");

            // Gets the database to add the user role.
            DatabaseConnector.AddUserRole(new UserRoles()
            {
                UserID = userID,
                RoleID = dbRole
            });
        }
    }
}
