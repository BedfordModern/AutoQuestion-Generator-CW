using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoQuestionGenerator.DatabaseModels;
using Microsoft.AspNetCore.Http;

namespace AutoQuestionGenerator.Accounts
{
    public class OrganisationHelper
    {
        public static Organisations GetOrganisation(string organisation_Username, IdentityModels context)
        {
            return context.organisations.FirstOrDefault(x => x.Organisation_Username == organisation_Username);
        }

        public static void LogOut(ISession session)
        {
            session.Remove("OrgId");
            session.Remove("Org_Uname");
        }
    }
}
