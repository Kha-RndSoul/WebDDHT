using System.Web.Mvc;
using WebDDHT.Helpers;

namespace WebDDHT.Filters
{
    /// <summary>
    /// Custom authorization filter - Yêu c?u customer ph?i login
    /// </summary>
    public class AuthorizeCustomerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!SessionHelper.IsLoggedIn())
            {
                // Redirect to login page
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" },
                        { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                    }
                );
            }

            base.OnActionExecuting(filterContext);
        }
    }
}