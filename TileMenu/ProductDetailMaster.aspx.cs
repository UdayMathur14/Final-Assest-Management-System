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
    public partial class ProductDetailMaster : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

        }

        protected void FillProduct()
        {
            string strqry = "select * from inv_Product_master";


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
            string strqry = "select * from inv_Make_master left join Inv_Product_Make_Relation on inv_make_master.Make_id=Inv_Product_Make_Relation.make_id where Product_id='"+ ddlproduct.SelectedValue +"' ";


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
        protected void FillProductType()
        {
            string strqry = "select * from inv_ProdType left join Inv_Product_Type_Relation on inv_ProdType.ProdType_id=Inv_Product_Type_Relation.ProdType_id where Product_id='" + ddlproduct.SelectedValue + "' ";


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
            else
            {
                Ds.Clear();
                ddltype.DataSource = Ds.Tables[0];
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillProductModel()
        {
            string strqry = "select * from inv_ProdModel left join Inv_Product_Model_Relation on inv_ProdModel.ProdModel_id=Inv_Product_Model_Relation.ProdModel_id where Product_id='" + ddlproduct.SelectedValue + "' ";


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
            else
            {
                Ds.Clear();
                ddlmodel.DataSource = Ds.Tables[0];
                ddlmodel.DataBind();
                ddlmodel.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }

        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMake();
            FillProductType();
            FillProductModel();
        }
    }
}