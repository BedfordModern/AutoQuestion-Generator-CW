using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoQuestionGenerator.DatabaseModels;

namespace AutoQuestionGenerator.Accounts
{
    public class OrganisationHelper
    {
        public static Organisations getOrganisation(string organisation_Username, IdentityModels context)
        {
            return context.organisations.FirstOrDefault(x => x.Organisation_Username == organisation_Username);
        }
    }
}
