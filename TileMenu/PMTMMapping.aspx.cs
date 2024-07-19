using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace TileMenu
{
    public partial class PMTMMapping : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillProductAll();
                FillMakeAll();
                FillProductTypeAll();
                FillProductModelAll();
            }
        }
        protected void FillProductAll()
        {

            string strqry = "select Product_Id,Product_Name  from inv_Product_master order by Product_name";


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
            


        }
        protected void FillProductTypeAll()
        {
            string strqry = "select * from inv_ProdType  order by ProdType_Name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddltype.DataSource = Ds.Tables[0];
                ddltype.DataTextField = "ProdType_Name";
                ddltype.DataValueField = "ProdType_Id";
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            


        }
        protected void FillProductModelAll()
        {
            string strqry = "select * from inv_ProdModel order by ProdModel_Name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlmodel.DataSource = Ds.Tables[0];
                ddlmodel.DataTextField = "ProdModel_Name";
                ddlmodel.DataValueField = "ProdModel_Id";
                ddlmodel.DataBind();
                ddlmodel.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
           


        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            string scriptstring = "";
            string strqry = "insert into Inv_Product_Mapping(" +
                       " Product_Id, Make_Id, Type_Id, Model_Id) " +
                       " values(" +
                       " '" + ddlproduct.SelectedValue + "','" + ddlmake.SelectedValue + "'," +
                       " '" + ddltype.SelectedValue + "','" + ddlmodel.SelectedValue + "')";
           
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
           
        }
    }
}