using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public bool IsCollaboratorExclusive { get; set; }
        public bool IsAdminExclusive { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            
            var user = filterContext.HttpContext.Session["user"] as User;

            if (user == null)
            {
                base.HandleUnauthorizedRequest(filterContext);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action="Login", returnUrl=filterContext.HttpContext.Request.Url })); 
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login" }));
                return;
            }

            bool isAuthorized = user != null;

            if (isAuthorized && !IsCollaboratorExclusive && !IsAdminExclusive)
            {
                return;
            }

            if (!user.IsAdmin == IsCollaboratorExclusive)
            {
                return;
            }

            if (isAuthorized && user.IsAdmin == IsAdminExclusive)
            {
                return;
            }

            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Ticket", action = "All" }));
        }
    } 
}
