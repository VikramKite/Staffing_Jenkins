using System;
using System.Web;
using System.Web.Mvc;

namespace staffing.endpoints.Filters
{
    public class AuthorizeUserFilter : AuthorizeAttribute
    {
        // Checking user is authorize or not
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool IsValidUser = true;
            // Authorization for valid member
            if (string.IsNullOrEmpty(Convert.ToString(httpContext.Session["UserName"])))
                IsValidUser = false;

            if (IsValidUser)
            {
                // Authorization for valid role
                if (!string.IsNullOrEmpty(Roles))
                {
                    string privilegeLevels = string.Join("", Convert.ToString(HttpContext.Current.Session["RoleID"]));
                    if (Roles.Contains(privilegeLevels))
                        IsValidUser = true;
                    else
                        IsValidUser = false;
                }
            }
            return IsValidUser;
        }

        // Based on Authorization result, redirct user to specific page
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    { "action", "Login" },
                    { "controller", "Account" },
                    {"returnUrl",filterContext.HttpContext.Request.Url.PathAndQuery}
                });
        }
    }
}