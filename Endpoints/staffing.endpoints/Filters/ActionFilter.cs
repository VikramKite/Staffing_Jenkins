using staffing.data.ef;
using System;
using System.Web;
using System.Web.Mvc;

namespace staffing.endpoints.Filters
{
    public class ActionFilter : FilterAttribute, IExceptionFilter
    {
        public InternalSystemEntities _entities = new InternalSystemEntities();
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                logging logger = new logging()
                {
                    exception_message = filterContext.Exception.Message,
                    exception_stack_trace = filterContext.Exception.StackTrace,
                    controller_name = filterContext.RouteData.Values["controller"].ToString(),
                    action_name = filterContext.RouteData.Values["action"].ToString(),
                    user_ip = filterContext.HttpContext.Request.UserHostAddress,
                    user_id = Convert.ToInt32(HttpContext.Current.Session["UserId"]),
                    log_time = DateTime.Now
                };
                _entities.loggings.Add(logger);
                _entities.SaveChangesAsync();
                filterContext.ExceptionHandled = true;

                //Redirect or return a view, but not both.
                filterContext.Result = new ViewResult()
                {
                    ViewName = "InnerErrorView",
                };

            }
        }

    }
}