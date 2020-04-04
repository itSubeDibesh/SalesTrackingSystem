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
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
        public class Authorization : AuthorizeAttribute
        {
            private Users_Interface UsersAuth = new Users_Service();
            //private UserProfile_Interface userProfileAUTH = new UserProfile_Service();
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                var LoginSession = (Users_Model)HttpContext.Current.Session["auth"];
                var AuthSession = new List<UserAuth_Model>();

                //Checking user session and redirecting to logout
                if (HttpContext.Current.Session["auth"] == null)
                {                   
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Session["Warning"] = "Not authorized." + Environment.NewLine + "Please login";
                    filterContext.Result = new RedirectResult("~/Auth/Login");
                }                    

                if (LoginSession!=null)
                {
                    //Checking user verification 
                    if (HttpContext.Current.Session["auth"] != null && LoginSession.IsVerified.Value == false)
                    {
                        filterContext.Result = new RedirectResult("~/Auth/CheckVerification");
                    }

                    //Access controller for developer
                    if (LoginSession.ProfileName == "Developer")
                    {
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/Distributor/"))
                        {
                            filterContext.Result = new RedirectResult("~/Error/E401");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/Reseller/"))
                        {
                            filterContext.Result = new RedirectResult("~/Error/E401");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/Transaction/"))
                        {
                            filterContext.Result = new RedirectResult("~/Error/E401");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/Products/"))
                        {
                            filterContext.Result = new RedirectResult("~/Error/E401");
                        }                      
                    }

                    //Access controller for Company
                    if (LoginSession.ProfileName == "Company")
                    {
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/Module/"))
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath == "/User/UserProfile")
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                    }

                    //Access controller for Distributor
                    if (LoginSession.ProfileName == "Distributor")
                    {
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/Module/"))
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath == "/Home/Dashboard")
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath == "/Home/Reports")
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/User/"))
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath == "/Distributor/Distributor")
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath == "/Products/Batch")
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath == "/Products/Batch")
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                        if (HttpContext.Current.Request.Url.AbsolutePath == "/Products/Unit")
                        {
                            filterContext.Result = new RedirectResult("~/Error/E403");
                        }
                    }



                    //Blocking Dashboard Controller Access through get unless it is built
                    if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/Dashboard/"))
                    {
                        filterContext.Result = new RedirectResult("~/Error/E401");
                    }
                }

                //Redirecting to 404 page when page not found
                if (HttpContext.Current.Response.StatusCode==404)
                {
                    filterContext.Result = new RedirectResult("~/Error/E404");
                }


                //Redirecting to 401 page when un authorized access
                if (HttpContext.Current.Response.StatusCode == 401)
                {
                    filterContext.Result = new RedirectResult("~/Error/E401");
                }

                //Redirecting to 403 page when un authorized access
                if (HttpContext.Current.Response.StatusCode == 403)
                {
                    filterContext.Result = new RedirectResult("~/Error/E403");
                }

                //Redirecting to 500 page when server error
                if (HttpContext.Current.Response.StatusCode == 500)
                {
                    filterContext.Result = new RedirectResult("~/Error/E500");
                }
              
            }

        }
    }
}