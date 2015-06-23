using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.Security;
using System.Web.SessionState;

/// <summary>
/// Summary description for UrlRewriting
/// </summary>
/// 
namespace ITManiacs
{
    public class UrlRewriting : IHttpModule
    {
        #region IHttpModule Members
        public void Init(HttpApplication context)
        {
          //  context.AuthenticateRequest += new EventHandler(ContextAuthenticateRequest);
            context.BeginRequest += new EventHandler(WebspixeApplication_BeginRequest);
        }
        private static void ContextAuthenticateRequest(object sender, EventArgs e)
        {
            // look if any security information exists for this request
            if (HttpContext.Current.User != null)
            {
                // see if this user is authenticated, any authenticated cookie (ticket) exists for this user
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // see if the authentication is done using FormsAuthentication
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        // Get the roles stored for this request from the ticket
                        // get the identity of the user
                        FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                        // get the forms authetication ticket of the user
                        FormsAuthenticationTicket ticket = identity.Ticket;
                        // get the roles stored as UserData into the ticket 
                        string[] roles = ticket.UserData.Split(',');
                        // create generic principal and assign it to the current request
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles);
                    }
                }
            }

        }


        void WebspixeApplication_BeginRequest(object sender, EventArgs e)
       {
            try
            {
                HttpContext context = ((HttpApplication)sender).Context;
                HttpRequest request = context.Request;
                string _AbsolutePath = request.RawUrl.ToLowerInvariant();
                string _Page = request.Url.Segments[request.Url.Segments.Length - 1].ToLowerInvariant();
                string url_extension = Function.UrlExtension;

                if (_Page.EndsWith(url_extension))
                {
                    string strUrlUnLowered = request.Url.Segments[request.Url.Segments.Length - 1];
                    string strNormalUrl = strUrlUnLowered.ToLowerInvariant();

                    //List<string> lstPages = new List<string>();
                    //lstPages.Add("default.aspx");
                    //lstPages.Add("post.aspx");
                    //lstPages.Add("sitemap.ashx"); && !lstPages.Contains(strNormalUrl)

                    if (context.Request.QueryString.Count == 0)
                    {
                        string[] Paths = _AbsolutePath.Split('/');

                        string strUrl = request.Url.PathAndQuery;

                        RegexOptions eROP = RegexOptions.CultureInvariant | RegexOptions.IgnoreCase;

                        if (new Regex(@"/homesearch/([A-Z0-9a-z-]+)/([A-Z0-9a-z-]+)/(.+)" + url_extension + "$", eROP).Match(strUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/homesearch/([A-Z0-9a-z-]+)/([A-Z0-9a-z-]+)/(.+)" + url_extension + "$", eROP)
                            .Replace(strUrl, "/searchresult.aspx?search=$1&city=$2&college=$3"), false);
                        }
                        else
                        if (new Regex(@"/search/([A-Z0-9a-z-]+)/(.+)" + url_extension + "$", eROP).Match(strUrl).Success)
                        {
                            HttpContext.Current.RewritePath(new Regex(@"/search/([A-Z0-9a-z-]+)/(.+)" + url_extension + "$", eROP)
                            .Replace(strUrl, "/searchresult.aspx?cat=$1&subcat=$2"), false);
                        }
                        else
                            if (new Regex(@"/search/(.+)" + url_extension + "$", eROP).Match(strUrl).Success)
                        {
                            strUrl = new Regex(@"/search/(.+)" + url_extension + "$", eROP).Replace(strUrl, "/searchresult.aspx?cat=$1");
                            HttpContext.Current.RewritePath(strUrl, false);
                        }
                       
                        else if (new Regex(@"(.+)" + url_extension + "$", eROP).Match(strUrl).Success)
                        {
                            strUrl = new Regex(@"(.+)" + url_extension + "$", eROP).Replace(strUrl, "$1.aspx");
                            HttpContext.Current.RewritePath(strUrl, false);
                        }
                    }
                    else
                    {
                        string[] Paths = _AbsolutePath.Split('/');
                        string strUrl = request.Url.PathAndQuery;
                        RegexOptions eROP = RegexOptions.CultureInvariant | RegexOptions.IgnoreCase;
                        if (new Regex(@"(.+)" + url_extension + "(.+)", eROP).Match(strUrl).Success)
                        {
                            strUrl = new Regex(@"(.+)" + url_extension + "(.+)", eROP).Replace(strUrl, "$1.aspx$2");
                            HttpContext.Current.RewritePath(strUrl, false);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        public void Dispose() { }
        #endregion
       
    }
    
}

