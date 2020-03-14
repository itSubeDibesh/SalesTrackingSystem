using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Services.Interface;
using Services.Service;

namespace SalesTrackingSystem.Helpers
{
    public class AppAuthAttribute
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true),AsyncTimeout(100)]
        public class Authorization : AuthorizeAttribute
        {
            private new readonly Users_Interface Users = new Users_Service();
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                var LoginSession = (Users_Model)HttpContext.Current.Session["auth"];                

                if (HttpContext.Current.Session["auth"] == null)
                {                   
                    HttpContext.Current.Session.Abandon();                   
                    HttpContext.Current.Session["Warning"] = "Session has timeout for your security." + Environment.NewLine + "Please login";
                    filterContext.Result = new RedirectResult("~/Auth/Login");                  
                }

                if (LoginSession!=null)
                {
                    if (HttpContext.Current.Session["auth"] != null && LoginSession.IsVerified.Value == false)
                    {
                        filterContext.Result = new RedirectResult("~/Auth/CheckVerification");
                    }

                    if (LoginSession.ProfileName != "Developer")
                    {
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/Module/"))
                        {
                            filterContext.Result = new RedirectResult("~/Error/E401");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath == "/User/UserProfile")
                        {
                            filterContext.Result = new RedirectResult("~/Error/E401");
                        }
                    }                   

                }

                if (HttpContext.Current.Response.StatusCode==404)
                {
                    filterContext.Result = new RedirectResult("~/Error/E404");
                }

                if (HttpContext.Current.Response.StatusCode == 401)
                {
                    filterContext.Result = new RedirectResult("~/Error/E401");
                }

                if (HttpContext.Current.Response.StatusCode == 500)
                {
                    filterContext.Result = new RedirectResult("~/Error/E500");
                }
              
            }

        }
    }
}