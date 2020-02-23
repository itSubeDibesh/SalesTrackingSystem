using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTrackingSystem.Helpers
{
    public class AppAuthAttribute
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
        public class Authorization : AuthorizeAttribute
        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                if (HttpContext.Current.Session["auth"] == null && HttpContext.Current.Request.Cookies["auth"] == null)
                {
                    HttpContext.Current.Session.RemoveAll();
                    var newCookie = new HttpCookie("auth");
                    newCookie.Expires = DateTime.Now.AddSeconds(-10);
                    HttpContext.Current.Response.Cookies.Add(newCookie);
                    HttpContext.Current.Session["Warning"] = "Session and cookie has timeout for your security." + Environment.NewLine + "Please login";
                    filterContext.Result = new RedirectResult("~/Auth/Login");                  
                }               
            }

        }
    }
}