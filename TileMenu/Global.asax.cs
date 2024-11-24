using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace TileMenu
{
    public class Global : HttpApplication, IRequiresSessionState
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a session starts
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Get the user email from the session
            string userEmail = Session["login"]?.ToString();

            if (!string.IsNullOrEmpty(userEmail))
            {
                using (SqlConnection con = new SqlConnection("YourConnectionStringHere"))
                {
                    string query = "UPDATE UserSessionLog SET LogoutTime = @LogoutTime WHERE UserEmail = @UserEmail AND LogoutTime IS NULL";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@LogoutTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string userEmail = context.Session["login"]?.ToString();

            if (!string.IsNullOrEmpty(userEmail))
            {
                using (SqlConnection Con = new SqlConnection("YourConnectionStringHere"))
                {
                    string query = "UPDATE UserSessionLog SET LogoutTime = @LogoutTime WHERE UserEmail = @UserEmail AND LogoutTime IS NULL";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@LogoutTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();
                }
            }
        }

        public bool IsReusable => false;
    }
}
