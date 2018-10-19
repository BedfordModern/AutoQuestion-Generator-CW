using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoQuestionGenerator.Accounts;
using AutoQuestionGenerator.DatabaseModels;
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
        public IActionResult Login(Users user, string returnUrl)
        {
            var DbUser = UserHelper.getUser(user.Username, _context);

            if (!ModelState.IsValid || DbUser == null) return View(user);

            if(Hasher.ValidatePassword(user.Password, DbUser.Password))
            {
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
                return View(user);
            }
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