using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoQuestionGenerator.DatabaseModels;
using Microsoft.AspNetCore.Http;

namespace AutoQuestionGenerator.Accounts
{
    /// <summary>
    /// This class delas with collecting ans returning organisation information
    /// It means that enay class can access the organisations information
    /// if that is needed.
    /// </summary>
    public class OrganisationHelper
    {
        /// <summary>
        /// These constants are used to as session keys to store the organisation's
        /// details in order to be used in page.
        /// </summary>
        public const string OrganisationID = "OrgId",
            OrganisationUsername = "Org_Uname";

        /// <summary>
        /// Gets the organisation beased in its ID
        /// used the session keys to get the organistion then
        /// gets the keys.
        /// </summary>
        /// <param name="session">httpcontext session with information</param>
        /// <returns>reruns the organisation</returns>
        public static Organisations GetOrganisation(ISession session)
        {
            // Tries to get organisation ID from session
            byte[] val;
            if (session.TryGetValue(OrganisationID, out val))
            {
                // Converts the session informaion into a int which is then passed.
                return DatabaseConnector.GetOrganisation(Convert.ToInt32(Encoding.UTF8.GetString(val)));
            }
            return null;
        }

        /// <summary>
        /// Gets the organisation from the database from its username
        /// this method used to do more but now I have put all the DB
        /// methods in one file it is here to make accssing the organisation easier.
        /// </summary>
        /// <param name="organisation_Username">The organisations username</param>
        /// <returns>The organisation if it has that username</returns>
        public static Organisations GetOrganisation(string organisation_Username)
        {
            return DatabaseConnector.Get<Organisations>().FirstOrDefault(x => x.Organisation_Username == organisation_Username);
        }

        /// <summary>
        /// Logs the user out be removing and invalidating the session key
        /// for buth the organisation id and username.
        /// </summary>
        /// <param name="session">The HTTP context</param>
        public static void LogOut(ISession session)
        {
            session.Remove(OrganisationID);
            session.Remove(OrganisationUsername);
        }

        /// <summary>
        /// Gets the organisation ID from the session.
        /// </summary>
        /// <param name="session">The Context for this.</param>
        /// <returns>the integer organisation id</returns>
        internal static int GetOrganisationID(ISession session)
        {
            int ID = -1;
            byte[] Vals;
            if (session.TryGetValue(OrganisationID, out Vals))
                ID = Convert.ToInt32(Encoding.UTF8.GetString(Vals));

            return ID;
        }
    }
}
