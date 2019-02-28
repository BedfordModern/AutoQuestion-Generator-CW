using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoQuestionGenerator.Accounts;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoQuestionGenerator.Controllers
{
    public class OrganisationController : Controller
    {
        IdentityModels _context;

        public OrganisationController()
        {
            _context = new IdentityModels();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Organisations org, string returnUrl)
        {
            var Organisation = OrganisationHelper.getOrganisation(org.Organisation_Username, _context);

            if (!ModelState.IsValid || Organisation == null) return View(org);

            if (Hasher.ValidatePassword(org.Organisation_Password, Organisation.Organisation_Password))
            {
                HttpContext.Session.Set("OrgId", Encoding.ASCII.GetBytes(Organisation.OrganisationID.ToString()));
                HttpContext.Session.Set("Org_Uname", Encoding.ASCII.GetBytes(Organisation.Organisation_Username));
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(org);
            }
        }

        public IActionResult Reset()
        {
            if (UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN, _context))
            {
                return View();
            }
            return LocalRedirect("~/");
        }

        [HttpPost]
        public IActionResult Reset(UpdateUser org)
        {
            if (!ModelState.IsValid || !org.SamePass)
            {
                return View(org);
            }
            _context.organisations.FirstOrDefault(x => x.Organisation_Username == org.Username).Organisation_Password = Hasher.Hash(org.Password);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Lost()
        {
            if(UserHelper.UserInRole(UserHelper.GetUserId(HttpContext.Session), UserHelper.ROLE_ADMIN, _context))
            {
                return View();
            }
            return LocalRedirect("~/");
        }

        [HttpPost]
        public IActionResult Lost(UpdateUser model)
        {
            var user = _context.users.FirstOrDefault(x => x.Username == model.Username);
            var userid = UserHelper.GetUserId(HttpContext.Session);
            if (user.OrganisationID == UserHelper.getUser(userid, _context).OrganisationID && UserHelper.UserInRole(userid, UserHelper.ROLE_ADMIN, _context))
            {
                user.Password = Hasher.Hash(model.Password);
            }
            return Unauthorized();
        }
    }
}