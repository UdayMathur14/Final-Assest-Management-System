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
            string selectedProductId = ddlproduct.SelectedValue;

            // Base query with an INNER JOIN
            string strqry = "SELECT Product_Name, [ProductDetail_AssetCode], [ProductDetail_SerialNo], [Status], [ProductDetail_CapDate] " +
                            "FROM [Inv_ProductDetail_Master] " +
                            "INNER JOIN [Inv_Product_Master] ON Inv_Product_Master.Product_Id = Inv_ProductDetail_Master.ProductDetail_Product_Id " +
                            "WHERE Inv_Product_Master.Product_Id = @ProductId";

            // Create the SQL connection
            using (SqlConnection Con = new SqlConnection(strCon))
            {
                // Add additional filtering conditions if necessary
                if (!string.IsNullOrEmpty(txtserial.Text))
                {
                    strqry += " AND [ProductDetail_SerialNo] LIKE @SerialNo";
                }

                if (!string.IsNullOrEmpty(txtFdate.Text) && !string.IsNullOrEmpty(txtTdate.Text))
                {
                    strqry += " AND [ProductDetail_CapDate] BETWEEN @FromDate AND @ToDate";
                }

                // Add order by clause
                strqry += " ORDER BY [ProductDetail_CapDate] desc, Product_Name ";

                // Set up the SQL command with the parameterized query
                using (SqlCommand cmd = new SqlCommand(strqry, Con))
                {
                    // Define parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@ProductId", selectedProductId);

                    if (!string.IsNullOrEmpty(txtserial.Text))
                    {
                        cmd.Parameters.AddWithValue("@SerialNo", "%" + txtserial.Text + "%");
                    }

                    if (!string.IsNullOrEmpty(txtFdate.Text) && !string.IsNullOrEmpty(txtTdate.Text))
                    {
                        cmd.Parameters.AddWithValue("@FromDate", txtFdate.Text);
                        cmd.Parameters.AddWithValue("@ToDate", txtTdate.Text);
                    }

                    // Execute the command and bind the results to the GridView
                    SqlDataAdapter Da = new SqlDataAdapter(cmd);
                    DataSet Ds = new DataSet();
                    Da.Fill(Ds);

                    if (Ds.Tables[0].Rows.Count > 0)
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