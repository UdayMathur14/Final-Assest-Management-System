using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Globalization;
using System.IO;
using System.Drawing;

namespace TileMenu
{
    public partial class InventoryReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetData();
                FillProductAll();


            }
        }

        protected void GetData()
        {
            // Base query with INNER JOIN
            string strqry = "SELECT Product_Name, [ProductDetail_AssetCode], [ProductDetail_SerialNo], [Status], [ProductDetail_CapDate] " +
                            "FROM [Inv_ProductDetail_Master] " +
                            "INNER JOIN [Inv_Product_Master] ON Inv_Product_Master.Product_Id = Inv_ProductDetail_Master.ProductDetail_Product_Id";

            // Start building the WHERE clause dynamically
            List<string> conditions = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            // Add condition for Product
            if (!string.IsNullOrEmpty(ddlproduct.SelectedValue))
            {
                conditions.Add("Inv_Product_Master.Product_Id = @ProductId");
                parameters.Add(new SqlParameter("@ProductId", ddlproduct.SelectedValue));
            }

            // Add condition for Serial Number
            if (!string.IsNullOrEmpty(txtserial.Text))
            {
                conditions.Add("[ProductDetail_SerialNo] LIKE @SerialNo");
                parameters.Add(new SqlParameter("@SerialNo", "%" + txtserial.Text.Trim() + "%"));
            }

            // Add condition for Date From
            if (!string.IsNullOrEmpty(txtFdate.Text))
            {
                conditions.Add("[ProductDetail_CapDate] >= @FromDate");
                parameters.Add(new SqlParameter("@FromDate", txtFdate.Text.Trim()));
            }

            // Add condition for Date To
            if (!string.IsNullOrEmpty(txtTdate.Text))
            {
                conditions.Add("[ProductDetail_CapDate] <= @ToDate");
                parameters.Add(new SqlParameter("@ToDate", txtTdate.Text.Trim()));
            }

            // Combine conditions with "AND" and append to the query
            if (conditions.Count > 0)
            {
                strqry += " WHERE " + string.Join(" AND ", conditions);
            }

            // Add order by clause
            strqry += " ORDER BY [ProductDetail_CapDate] DESC, Product_Name";

            // Execute the query
            using (SqlConnection Con = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(strqry, Con))
                {
                    // Add parameters to the command
                    cmd.Parameters.AddRange(parameters.ToArray());

                    SqlDataAdapter Da = new SqlDataAdapter(cmd);
                    DataSet Ds = new DataSet();
                    Da.Fill(Ds);

                    // Bind results to the GridView
                    if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = Ds;
                        GridView1.DataBind();
                    }
                    else
                    {
                        Ds.Clear();
                        GridView1.DataBind();
                    }
                }
            }
        }





        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            GetData();


        }

        protected void txtserial_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void FillProductAll()
        {
            

            string strqry = "select Product_Id,Product_Name  from inv_Product_master order by Product_Name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlproduct.DataSource = Ds.Tables[0];
                ddlproduct.DataTextField = "Product_Name";
                ddlproduct.DataValueField = "Product_Id";
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }

        protected void product_changed(object sender, EventArgs e)
        {
            GetData();
        }
    }
}