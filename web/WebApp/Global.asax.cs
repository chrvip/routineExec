using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApp
{
    public class Global : System.Web.HttpApplication
    {
        void RegisterRoutes(RouteCollection routes)
        {
            string urlbase = "";
            routes.MapHttpHandlerRoute("data", urlbase + "data", "~/data.ashx");
            routes.MapHttpHandlerRoute("list", urlbase + "list", "~/list.ashx");
            routes.MapHttpHandlerRoute("lists", urlbase + "lists", "~/lists.ashx");
            routes.MapHttpHandlerRoute("docList", urlbase + "docList", "~/docList.ashx");
            routes.MapHttpHandlerRoute("doc", urlbase + "doc", "~/doc.ashx");
            routes.MapHttpHandlerRoute("search", urlbase + "search", "~/search.ashx");
            routes.MapHttpHandlerRoute("log", urlbase + "log", "~/log.ashx");
            routes.MapHttpHandlerRoute("update", urlbase + "update", "~/update.ashx");
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}