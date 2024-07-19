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
    public partial class UserMaster : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillUsertbl();
                FillUserDropdown();
            }
        }



        protected void SubmitDetails(object sender, EventArgs e)
        {
            var email = txt_email.Text;
            var pwd = txt_pwd.Text;
            var status = ddlStatus.SelectedValue;
            var role = ddlRoleCreate.SelectedValue;


            string scriptstring = "";
            string strqry = "INSERT INTO Tbl_UserMaster(Email,Password,Status,Role) VALUES ('" + email + "', '" + pwd + "', '" + status + "','"+ role + "')";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            txt_email.Text = "";
            txt_pwd.Text = "";


            FillUserDropdown();
            FillUsertbl();
        }


        protected void FillUsertbl()
        {

            string strqry = "SELECT Id, Email, Role FROM Tbl_UserMaster ORDER BY Id";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                tbluser.DataSource = Ds;
                tbluser.DataBind();
            }
            else
            {
                Ds.Clear();
                tbluser.DataSource = Ds;
                tbluser.DataBind();

            }
        }

        protected void FillUserDropdown()
        {

            string strqry = "select *  from Tbl_UserMaster";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlemail.DataSource = Ds.Tables[0];
                ddlemail.DataTextField = "Email";
                ddlemail.DataValueField = "Email";
                ddlemail.DataBind();
                ddlemail.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                ddlemail.DataSource = null;
                ddlemail.DataBind();
                ddlemail.Items.Clear();
                ddlemail.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }
        protected void ddluserChange(object sender, EventArgs e)
        {

            var email = ddlemail.Text;
            if (email == "-1")
            {
                txtpwd1.Text = "";
            }
            string strqry = "SELECT * FROM Tbl_UserMaster WHERE Email = '" + ddlemail.SelectedValue + "' ORDER BY Email";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = Ds.Tables[0].Rows[0]; 

                

                txtpwd1.Text = row["Password"].ToString();
                string status = row["Status"].ToString();
                string role = row["Role"].ToString();

                if (status == "1")
                {
                    status1.SelectedValue = "1";
                }
                else if (status == "0")
                {
                    status1.SelectedValue = "0";
                }

                ddlRoleUpdate.SelectedValue = role;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "switchToTab2", "$('.nav-tabs a[href=\"#tab2\"]').tab('show');", true);
        }
        protected void UpdateBtn(object sender, EventArgs e)
        {
            var email = ddlemail.Text;
            var pwd = txtpwd1.Text;
            var role = ddlRoleUpdate.SelectedValue;

            var status = status1.SelectedValue;

            string scriptstring = "";
            string strqry = "UPDATE Tbl_UserMaster SET Password = '" + pwd + "', Status = '" + status + "', Role = '" + role + "' WHERE Email = '" + email + "'";



            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Updated');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            
            txtpwd1.Text = "";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "switchToTab2", "$('.nav-tabs a[href=\"#tab2\"]').tab('show');", true);
            FillUsertbl();
            FillUserDropdown();

        }


        protected void DeleteBtn(object sender, EventArgs e)
        {
            var email = ddlemail.Text;


            string scriptstring = "";
            string strqry = "Delete from Tbl_UserMaster where Email = '" + email + "'";



            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Deleted');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            
            txtpwd1.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "switchToTab2", "$('.nav-tabs a[href=\"#tab2\"]').tab('show');", true);
            FillUsertbl();
            FillUserDropdown();

        }
    }


}