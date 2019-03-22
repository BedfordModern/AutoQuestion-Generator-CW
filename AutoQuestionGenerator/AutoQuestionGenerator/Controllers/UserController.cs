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
    public class UserController : Controller
    {
        IdentityModels _context;

        public UserController()
        {
            _context = new IdentityModels();
        }

        /*public IActionResult Index()
        {
            return View();
        }*/

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel user, string returnUrl)
        {
            var DbOrganisation = _context.organisations.FirstOrDefault(x => x.Organisation_Username == user.Organisation);
            var DbUser = UserHelper.GetUser(user.Username, _context);
            if (!ModelState.IsValid || DbUser == null || DbOrganisation == null) return View(user);

            if (DbUser.OrganisationID != DbOrganisation.OrganisationID)
            {
                user.Error = "Organisation is incorrect";
                return View(user);
            }

            if (Hasher.ValidatePassword(user.Password, DbUser.Password))
            {
                HttpContext.Session.Set("OrgId", Encoding.ASCII.GetBytes(DbOrganisation.OrganisationID.ToString()));
                HttpContext.Session.Set("Org_Uname", Encoding.ASCII.GetBytes(DbOrganisation.Organisation_Username));
                HttpContext.Session.Set("UId", Encoding.ASCII.GetBytes(DbUser.UserID.ToString()));
                HttpContext.Session.Set("Username", Encoding.ASCII.GetBytes(user.Username));
                DbUser.Last_Logged_In = DateTime.Now;
                _context.SaveChanges();
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                user.Error = "Username or Password is incorrect";
                return View(user);
            }
        }

        public IActionResult Logout(string returnUrl)
        {
            UserHelper.LogOut(HttpContext.Session);
            OrganisationHelper.LogOut(HttpContext.Session);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Lost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Lost(Users user)
        {
            var DbUser = _context.users.FirstOrDefault(x => x.Username == user.Username);

            if (!ModelState.IsValid || DbUser == null) return View(user);

            DbUser.Password = Hasher.Hash(user.Password);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}