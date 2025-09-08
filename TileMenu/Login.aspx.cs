using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TileMenu
{
    public partial class Login : System.Web.UI.Page
    {
        public DataSet menuDs = new DataSet();
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }

        }


        protected void loginClickBtn(object sender, EventArgs e)
        {
            var Email = txt_mail.Text;
            var Pwd = txt_password.Text;

            string strqry = "SELECT * FROM Tbl_UserMaster WHERE Email = @Email AND Password = @Pwd";

            using (SqlConnection Con = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(strqry, Con))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Pwd", Pwd);

                    SqlDataAdapter Da = new SqlDataAdapter(cmd);
                    DataSet Ds = new DataSet();
                    Da.Fill(Ds);

                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        string UserADID = Email;
                        string userRole = Ds.Tables[0].Rows[0]["Role"].ToString();
                        Session["login"] = UserADID;
                        Session["role"] = userRole;

                        // Log user login time
                        LogUserLogin(UserADID);

                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", "alert('Invalid Users Details');", true);
                    }
                }
            }
        }

        private void LogUserLogin(string userEmail)
        {
            using (SqlConnection Con = new SqlConnection(strCon))
            {
                string query = "INSERT INTO UserSessionLog (UserEmail, LoginTime) VALUES (@UserEmail, @LoginTime)";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@UserEmail", userEmail);
                cmd.Parameters.AddWithValue("@LoginTime", DateTime.Now);

                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();
            }
        }

    }
}