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
            if (!filterContext.HttpContext.Session.TryGetValue("User", out usr))
                filterContext.Result = new RedirectResult(string.Format("/User/Login?targetUrl={0}", filterContext.HttpContext.Request.Path));
        }
    }
}