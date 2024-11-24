using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace TileMenu
{
    public partial class SiteMaster : MasterPage
    {
        public DataSet menuDs = new DataSet();
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (Session["role"] != null)
            {
                // Retrieve the role from the session
                string role = Session["role"].ToString();

                // Display the role in a label
                hiddenRole.Value = Session["role"].ToString();
            }

        }
        protected void logoutClickBtn(object sender, EventArgs e)
        {
            if (Session["login"] != null)
            {
                string userEmail = Session["login"].ToString();

                // Log logout time
                LogUserLogout(userEmail);

                // Clear session data
                Session.Abandon();

                // Redirect with a logout query parameter
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LogUserLogout(string userEmail)
        {
            using (SqlConnection Con = new SqlConnection(strCon))
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
}