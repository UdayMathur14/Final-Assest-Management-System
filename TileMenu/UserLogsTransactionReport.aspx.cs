using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace TileMenu
{
    public partial class UserLogsTransactionReport : System.Web.UI.Page
    {
        public string strCon = ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillUserDropdown();
            }
        }

        protected void FillUserDropdown()
        {
            string strqry = "SELECT * FROM Tbl_UserMaster";

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = ddlemail.SelectedValue;
            DateTime fromDate, toDate;

            // Check if the FromDate and ToDate are valid
            //bool isValidFromDate = DateTime.TryParse(txtFdate.Text, out fromDate);
            //bool isValidToDate = DateTime.TryParse(txtTdate.Text, out toDate);

            //if (!isValidFromDate || !isValidToDate)
            //{
            //    return; // Stop further processing
            //}

            // Proceed only if a valid email is selected
            if (email != "-1")
            {
                BindStockIn(email);
                BindStockOut(email);
                BindLogs(email);
            }
        }


        private void BindStockIn(string email)
        {
            string query = "SELECT * FROM [Inv_StockIn] " +
                           "WHERE [StockIn_CreatedBy] = @Email ";

            using (SqlConnection con = new SqlConnection(strCon))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
               

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewStockIn.DataSource = dt;
                GridViewStockIn.DataBind();
            }
        }

        private void BindStockOut(string email)
        {
            string query = "SELECT * FROM [Inv_StockOut] " +
                           "WHERE [StockOut_CreatedBy] = @Email";

            using (SqlConnection con = new SqlConnection(strCon))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewStockOut.DataSource = dt;
                GridViewStockOut.DataBind();
            }
        }

        private void BindLogs(string email)
        {
            string query = "SELECT * FROM [Inv_StockOut] " +
                           "WHERE [StockOut_CreatedBy] = @Email";

            using (SqlConnection con = new SqlConnection(strCon))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
               

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewStockOut.DataSource = dt;
                GridViewStockOut.DataBind();
            }
        }
    }
}
