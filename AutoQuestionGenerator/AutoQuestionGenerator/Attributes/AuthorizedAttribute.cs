using AutoQuestionGenerator.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AutoQuestionGenerator.Controllers
{
    internal class AuthorizedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            byte[] usr;
            if (!filterContext.HttpContext.Session.TryGetValue(UserHelper.SESSION_UID, out usr))
                filterContext.Result = new RedirectResult(string.Format("/User/Login?returnUrl={0}{1}", filterContext.HttpContext.Request.Path, filterContext.HttpContext.Request.QueryString));
        }
    }
}