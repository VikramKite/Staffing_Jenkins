using System;
using System.Web.Mvc;

namespace staffing.endpoints.Controllers.Common
{
    public abstract class BaseController : Controller
    {
        //protected int CurrentUserId => Session["UserId"] == null ? 0 : int.Parse(Session["UserId"].ToString());
        protected int CurrentUserId => Session["UserId"] == null ? 1 : int.Parse(Session["UserId"].ToString());
        protected DateTime CurrentDateTime => DateTime.UtcNow;
    }
}