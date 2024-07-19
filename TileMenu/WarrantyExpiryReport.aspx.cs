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
    public partial class WarrantyExpiryReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillProductAll();
                FillMakeAll();
                FillExpiryOnDropdown();


            }
        }

        protected void GetData()
        {
            string strcondition = "";

           string strqry = "select Product_Name as Product,Make_Name as Make," +
                       " ProdType_Name as Type,ProdModel_Name as Model,ProductDetail_AssetCode as AssetCode,ProductDetail_SerialNo as SerialNo, "+
                       " convert(varchar(50),ProductDetail_CapDate,106) as [Capitalized on],convert(varchar(50),ProductDetail_ExpiryDate,106) as [Expired On] from inv_productdetail_master " +
                       " left join Inv_Product_Master on inv_productdetail_master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id " +
                       " left join [dbo].[Inv_Make_Master] on inv_productdetail_master.ProductDetail_Make_Id =[Inv_Make_Master].Make_Id " +
                       " left join [dbo].[Inv_ProdType] on inv_productdetail_master.ProductDetail_ProdType_Id =[Inv_ProdType].ProdType_Id " +
                       " left join [dbo].[Inv_ProdModel] on inv_productdetail_master.ProductDetail_ProdModel_Id =[Inv_ProdModel].ProdModel_id " +
                       " where Product_ItemType='" + typemaster.SelectedValue + "'";





            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and ProductDetail_Product_Id='" + ddlproduct.SelectedValue + "'";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcondition += " and ProductDetail_Make_Id='" + ddlmake.SelectedValue + "'";
            }
            if (ddlEmonth.SelectedValue != "-1")
            {
                strcondition += " and month(ProductDetail_ExpiryDate)='" + ddlEmonth.SelectedValue + "'";
            }
            if (ddlEyear.SelectedValue != "-1" && ddlEyear.SelectedValue != "-2")
            {
                strcondition += " and Year(ProductDetail_ExpiryDate)='" + ddlEyear.SelectedValue + "'";
            }
            if (ddlEyear.SelectedValue == "-2")
            {
                strcondition += " and (ProductDetail_ExpiryDate='1900-01-01 00:00:00.000' or ProductDetail_ExpiryDate is null)";
            }


            strcondition += " order by ProductDetail_SerialNo";


            //Response.Write(strqry+ strcondition);
            //Response.End();
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
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

        protected void FillProductAll()
        {

            string strqry = "select Product_Id,Product_Name  from inv_Product_master where Product_ItemType='" + typemaster.SelectedValue + "'" +
                            " order by Product_name";


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
        protected void FillMake()
        {
            string strcondition = "";
            string strqry = "select distinct inv_Make_master.make_id,Make_name from inv_Make_master " +
                " left join Inv_Product_Mapping on inv_make_master.Make_id=Inv_Product_mapping.make_id where 1=1 ";

            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproduct.SelectedValue + "";
            }


            strcondition += " order by make_Name";
            string strfinal = strqry + strcondition;

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlmake.DataSource = Ds.Tables[0];
                ddlmake.DataTextField = "Make_Name";
                ddlmake.DataValueField = "Make_Id";
                ddlmake.DataBind();
                ddlmake.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlmake.DataSource = Ds.Tables[0];
                ddlmake.DataBind();
                ddlmake.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillMakeAll()
        {

            string strqry = "select make_id,Make_name from inv_Make_master order by make_Name";



            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlmake.DataSource = Ds.Tables[0];
                ddlmake.DataTextField = "Make_Name";
                ddlmake.DataValueField = "Make_Id";
                ddlmake.DataBind();
                ddlmake.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlmake.DataSource = Ds.Tables[0];
                ddlmake.DataBind();
                ddlmake.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            GetData();


        }

        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMake();
            GetData();
        }

        protected void typemaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProductAll();
        }

        protected void ddlmake_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected void ddlEmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected void ddlEyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void FillExpiryOnDropdown()
        {
            string strqry = "SELECT MAX(YEAR(ProductDetail_ExpiryDate)) AS MaxYear FROM Inv_ProductDetail_Master";

            // Establish connection
            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlCommand cmd = new SqlCommand(strqry, con);
                con.Open();

                // Execute the query to get the maximum year
                object maxYearObj = cmd.ExecuteScalar();
                if (maxYearObj != DBNull.Value)
                {
                    int maxYear;
                    if (int.TryParse(maxYearObj.ToString(), out maxYear))
                    {
                        // Clear existing items in DropDownList
                        ddlEyear.Items.Clear();

                        // Add items from 2020 to the maximum year found
                        ddlEyear.Items.Add(new ListItem("Any", "-1"));
                        ddlEyear.Items.Add(new ListItem("Not Filled", "-2"));

                        for (int year = 2020; year <= maxYear; year++)
                        {
                            ddlEyear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                        }
                    }
                    else
                    {
                        // Handle parsing error if necessary
                        // For example, log the error or set a default range.
                    }
                }
                con.Close();
            }
        }
    }
}