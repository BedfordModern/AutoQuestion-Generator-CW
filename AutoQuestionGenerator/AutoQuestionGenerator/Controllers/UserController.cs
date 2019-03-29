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
        public UserController()
        {
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel user, string returnUrl)
        {
            var DbOrganisation = DatabaseConnector.Get<Organisations>().FirstOrDefault(x => x.Organisation_Username == user.Organisation);
            if (!ModelState.IsValid || DbOrganisation == null)
            {
                if (DbOrganisation == null)
                {
                    user.Error = "Organisation does not exist.";
                    return View(user);
                }
                return View(user);
            }
            var DbUser = UserHelper.GetUser(user.Username, DbOrganisation.OrganisationID);
            if (DbUser == null)
            {
                user.Error = "Username or password incorrect.";
                return View(user);
            }

            if (DbUser.OrganisationID != DbOrganisation.OrganisationID)
            {
                user.Error = "Organisation is incorrect";
                return View(user);
            }

            if (Hasher.ValidatePassword(user.Password, DbUser.Password))
            {
                HttpContext.Session.Set("OrgId", Encoding.UTF8.GetBytes(DbOrganisation.OrganisationID.ToString()));
                HttpContext.Session.Set("Org_Uname", Encoding.UTF8.GetBytes(DbOrganisation.Organisation_Name));
                HttpContext.Session.Set(UserHelper.SESSION_UID, Encoding.UTF8.GetBytes(DbUser.UserID.ToString()));
                HttpContext.Session.Set("Username", Encoding.UTF8.GetBytes(user.Username));
                DbUser.Last_Logged_In = DateTime.Now;
                DatabaseConnector.Update(DbUser);
                DatabaseConnector.PushChanges();
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                user.Error = "Username or password incorrect.";
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
        public IActionResult Lost(LoginViewModel user)
        {
            var DbOrganisation = DatabaseConnector.Get<Organisations>().FirstOrDefault(x => x.Organisation_Username == user.Organisation);
            if (!ModelState.IsValid || DbOrganisation == null)
            {
                return View(user);
            }
            var DbUser = UserHelper.GetUser(user.Username, DbOrganisation.OrganisationID);

            if (!ModelState.IsValid || DbUser == null) return View(user);

            DbUser.Password = Hasher.Hash(user.Password);
            DatabaseConnector.Update(DbUser);
            DatabaseConnector.PushChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}