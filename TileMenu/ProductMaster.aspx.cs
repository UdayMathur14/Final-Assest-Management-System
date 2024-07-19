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
    public partial class ProductMaster : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmaster.Text = "Make";
                FillGridMake();
            }
        }
        protected void FillGridMake()
        {

            string strqry = "select * from inv_make_master";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvdetail.DataSource = Ds;
                grvdetail.DataBind();
            }
            else
            {
                Ds.Clear();
                grvdetail.DataSource = Ds;
                grvdetail.DataBind();

            }
        }
        protected void FillGridProduct()
        {

            string strqry = "select * from inv_Product_master";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvdetail.DataSource = Ds;
                grvdetail.DataBind();
            }
            else
            {
                Ds.Clear();
                grvdetail.DataSource = Ds;
                grvdetail.DataBind();

            }
        }
        protected void FillGridProductType()
        {

            string strqry = "select * from Inv_ProdType";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvdetail.DataSource = Ds;
                grvdetail.DataBind();
            }
            else
            {
                Ds.Clear();
                grvdetail.DataSource = Ds;
                grvdetail.DataBind();

            }
        }

        protected void FillGridProductModel()
        {

            string strqry = "select * from Inv_ProdModel";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvdetail.DataSource = Ds;
                grvdetail.DataBind();
            }
            else
            {
                Ds.Clear();
                grvdetail.DataSource = Ds;
                grvdetail.DataBind();

            }
        }

        protected void rblMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMaster.SelectedIndex == 0)
            {
                lblmaster.Text = "Make";
                FillGridMake();
            }
            if (rblMaster.SelectedIndex == 1)
            {
                lblmaster.Text = "Product";
                FillGridProduct();
            }
            if (rblMaster.SelectedIndex == 2)
            {
                lblmaster.Text = "Product Type";
                FillGridProductType();
            }
            if (rblMaster.SelectedIndex == 3)
            {
                lblmaster.Text = "Product Model";
                FillGridProductModel();
            }
        }


        protected bool Checkduplicate(string Field,string tablename )
        {
            string strqry = "select "+ Field + " from  " + tablename + " where " +
                            " " + Field + " ='" + txtmaster.Text  + "'";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void Insert(string tablename)
        {
            string scriptstring = "";
            string strqry = "insert into " + tablename + "" +
                       " values('" + txtmaster.Text + "')";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            txtmaster.Text = "";

        }
        protected void InsertProduct()
        {
            string scriptstring = "";
            //string strqry = "insert into Inv_Product_Master" +
            //           " values('" + txtmaster.Text + "','Non Consumable','Hardware',0,'YES')";
            string strqry = "INSERT INTO Inv_Product_Master(Product_Name, Product_InvType, Product_ItemType, Product_Min_Qty, Product_Serial_Available) VALUES('" + txtmaster.Text + "', 'Non Consumable', 'Hardware', 0, 'YES')";



            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            txtmaster.Text = "";

        }
        protected void Btnsave_Click(object sender, EventArgs e)
        {
            string scriptstring = "";
            if (rblMaster.SelectedIndex == 0)
            {
                if (Checkduplicate("Make_Name", "Inv_Make_Master") == true)
                {
                    scriptstring = $"alert('Name {txtmaster.Text} already exist');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                    return;
                }
                else
                {
                    Insert("Inv_Make_Master");
                    FillGridMake();
                }
            }
            if (rblMaster.SelectedIndex == 1)
            {
                if (Checkduplicate("Product_Name", "Inv_Product_Master") == true)
                {
                    scriptstring = $"alert('Name {txtmaster.Text} already exist');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                    return;
                }
                else
                {
                    InsertProduct();
                    FillGridProduct();
                }
            }
            if (rblMaster.SelectedIndex == 2)
            {
                if (Checkduplicate("ProdType_Name", "Inv_ProdType") == true)
                {
                    scriptstring = $"alert('Name {txtmaster.Text} already exist');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                    return;
                }
                else
                {
                    Insert("Inv_ProdType");
                    FillGridProductType();
                }
            }
            if (rblMaster.SelectedIndex == 3)
            {
                if (Checkduplicate("ProdModel_Name", "Inv_ProdModel") == true)
                {
                    scriptstring = $"alert('Name {txtmaster.Text} already exist');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                    return;
                }
                else
                {
                    Insert("Inv_ProdModel");
                    FillGridProductModel();
                }
            }

        }
    }
}