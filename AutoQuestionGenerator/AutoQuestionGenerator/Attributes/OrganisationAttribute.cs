using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace AutoQuestionGenerator.Controllers
{
    internal class OrganisationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            byte[] usr;
            if (!filterContext.HttpContext.Session.TryGetValue("OrgId", out usr))
                filterContext.Result = new RedirectResult(string.Format("/Organisation/Login?returnUrl={0}{1}", filterContext.HttpContext.Request.Path, filterContext.HttpContext.Request.QueryString));
        }
    }
}
