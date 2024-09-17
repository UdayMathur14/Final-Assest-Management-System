using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
namespace TileMenu
{
    public partial class StockOut : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label11.Visible = false;
                ddlissuedhw.Visible = false;
                txtdate.Text = DateTime.Now.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                FillProductAll();
                FillMakeAll();
                FillProductTypeAll();
                FillProductModelAll();
                optcat.Visible = false;
                FillEmployeeCode();
                FillRequestedBy();

                //FillGrid();
                //FillLaptopDesktop();
                //FillRequestedBy();

                ddlOAC.SelectedValue = "Employee";
                ddlOAC.Focus();

            }
        }
        protected void FillGrid()
        {
            string strcondition = "";
            string strqry = "";

            strqry = "select  StockOut_Id,productdetail_stockin_id as IN_Id,product_id as Pid,convert(varchar(50),StockOut_IssueDate,106) as [Date],StockOut_OAC as OnAccount,StockOut_IssueType as IssueType,StockOut_EmpCode as EmpCode," +
                        " StockOut_EmpName as EmpName,StockOut_CostCenter as CostCenter,Product_Name as Product,Make_Name as Make," +
                        " ProdType_Name as Type,ProdModel_Name as Model,ProductDetail_AssetCode as AssetCode,ProductDetail_SerialNo as SerialNo,StockOut_Remarks as Remarks  from inv_stockout " +
                        " left join inv_productdetail_master on inv_stockout.StockOut_ProdDetail_Id=inv_productdetail_master.ProductDetail_Id " +
                        " left join Inv_Product_Master on inv_productdetail_master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id " +
                        " left join [dbo].[Inv_Make_Master] on inv_productdetail_master.ProductDetail_Make_Id =[Inv_Make_Master].Make_Id " +
                        " left join [dbo].[Inv_ProdType] on inv_productdetail_master.ProductDetail_ProdType_Id =[Inv_ProdType].ProdType_Id " +
                        " left join [dbo].[Inv_ProdModel] on inv_productdetail_master.ProductDetail_ProdModel_Id =[Inv_ProdModel].ProdModel_id " +
                         " where StockOut_Id not in (select StockReturn_StockOut_Id from Inv_StockReturn) " +
                        " and Product_ItemType='" + typemaster.SelectedValue + "' and stockout_oac='" + ddlOAC.SelectedValue + "'";



            //Response.Write(strqry);
            //Response.End();


            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and ProductDetail_Product_Id='" + ddlproduct.SelectedValue + "'";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcondition += " and ProductDetail_Make_Id='" + ddlmake.SelectedValue + "'";
            }
            if (ddltype.SelectedValue != "-1")
            {
                strcondition += " and ProductDetail_ProdType_Id='" + ddltype.SelectedValue + "'";
            }
            if (ddlmodel.SelectedValue != "-1")
            {
                strcondition += " and ProductDetail_ProdModel_Id='" + ddlmodel.SelectedValue + "'";
            }
            if (ddlemployee.SelectedValue != "-1")
            {
                strcondition += " and StockOut_EmpCode='" + ddlemployee.SelectedValue + "'";
            }
            if (ddlissuetype.SelectedValue != "-1")
            {
                strcondition += " and StockOut_IssueType='" + ddlissuetype.SelectedValue + "'";
            }
            strcondition += "  order by StockOut_issuedate desc";
            //Response.Write(strqry+strcondition);
            //Response.End();
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            //if (Ds.Tables[0].Rows.Count > 0)
            //{
            //    GridView1.DataSource = Ds;
            //    GridView1.DataBind();
            //}
            //else
            //{
            //    Ds.Clear();
            GridView1.DataSource = Ds;
            GridView1.DataBind();

            // }
            //updGrid.Update();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int stockoutid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string strqry = "update inv_stockout set StockOut_IssueType='Permanent',StockOut_ModifiedDate='" + DateTime.Today.ToString("yyyy-MM-dd") + "' " +
                            " where StockOut_Id='" + stockoutid + "'";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            FillGrid();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string outtype = (string)e.Row.Cells[7].Text.ToString();
                string prodid = (string)e.Row.Cells[4].Text.ToString();
                string soutid = (string)e.Row.Cells[2].Text.ToString();
                string strqry = "select Product_Serial_Available from Inv_Product_Master where Product_ItemType='Software' and Product_Id='" + prodid + "'";
                SqlConnection Con = new SqlConnection();
                Con.ConnectionString = strCon;
                SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
                DataSet Ds = new DataSet();
                Da.Fill(Ds);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    if (Ds.Tables[0].Rows[0][0].ToString() == "NO")
                    {
                        string strqrymodel = "select ProdModel_Name from Inv_ProdModel where ProdModel_Id in(select Stockout_Location from Inv_StockOut where stockout_id='" + soutid + "')";
                        SqlDataAdapter Damodel = new SqlDataAdapter(strqrymodel, Con);
                        DataSet Dsmodel = new DataSet();
                        Damodel.Fill(Dsmodel);
                        if (Dsmodel.Tables[0].Rows.Count > 0)
                        {
                            e.Row.Cells[14].Text = Dsmodel.Tables[0].Rows[0][0].ToString();
                        }

                    }
                }


                if (outtype == "Permanent")
                {
                    e.Row.Cells[1].Text = "";
                }
                //if (type == "Available")
                //{
                //    e.Row.ForeColor = Color.White;
                //    e.Row.BackColor = Color.Green;
                //}

                //Label lblActive = e.Row.FindControl("lbl_assetcode") as Label;

                //lblActive.Font.Bold = true;  





            }
        }
        protected void Clear()
        {
            txtdate.Text = "";
            ddlproduct.SelectedValue = "-1";
            ddlmake.SelectedValue = "-1";
            ddltype.SelectedValue = "-1";
            ddlmodel.SelectedValue = "-1";
            ddlsrno.SelectedValue = "-1";
            ddlassetcode.SelectedValue = "-1";
            //ddlemployee.SelectedValue = "-1";
            txtEmpName.Text = "";
            //txtcostcenter.Text = "";
            TxtEmail.Text = "";

            txtlocation.Text = "";
            txterdate.Text = "";
            txtremarks.Text = "";
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
        protected void FillProductType()
        {
            string strcondition = "";
            string strqry = "select distinct ProdType_Id,ProdType_Name from inv_ProdType " +
                 " left join Inv_Product_Mapping on inv_ProdType.Prodtype_id=Inv_Product_mapping.type_id where 1=1";
            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproduct.SelectedValue + "";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.make_id=" + ddlmake.SelectedValue + "";
            }


            strcondition += " order by ProdType_Name";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
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

            string strcondition = "";
            string strqry = "select distinct ProdModel_Id,ProdModel_Name from inv_ProdModel " +
                 " left join Inv_Product_Mapping on inv_ProdModel.ProdModel_Id=Inv_Product_mapping.model_id where 1=1";
            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproduct.SelectedValue + "";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.make_id=" + ddlmake.SelectedValue + "";
            }
            if (ddltype.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.type_id=" + ddltype.SelectedValue + "";
            }

            strcondition += " order by ProdModel_Name";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
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
            else
            {
                Ds.Clear();
                ddlmodel.DataSource = Ds.Tables[0];
                ddlmodel.DataBind();
                ddlmodel.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void typemaster_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillProductAll();
            if (typemaster.SelectedValue == "Hardware")
            {
                Label11.Visible = false;
                ddlissuedhw.Visible = false;
            }
            else
            {
                Label11.Visible = true;
                ddlissuedhw.Visible = true;
            }
            FillGrid();
        }
        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
            SrNoAvailabiltyHardware();


            FillMake();
            FillProductType();
            FillProductModel();



            FillSerialNo(true);
            FillAssetCode(true);
            ddlproduct.Focus();
        }
        protected void ddlsrno_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAssetCode(ddlsrno.SelectedValue);
            ddlsrno.Focus();
        }
        protected void ddlassetcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSerialNo(ddlassetcode.SelectedValue);
            ddlassetcode.Focus();
        }
        protected void FillEmployeeCode()
        {
            string strqry = "";

            strqry = "select Emp_code,emp_name+'('+emp_code+')' as EMP from TblEmpMaster";




            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlemployee.DataSource = Ds.Tables[0];
                ddlemployee.DataTextField = "EMP";
                ddlemployee.DataValueField = "Emp_code";
                ddlemployee.DataBind();
                ddlemployee.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillCommon()
        {
            string strqry = "";

            strqry = "select Emp_code,emp_name+'('+emp_code+')' as EMP from tblempmaster order by emp_name ";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlemployee.DataSource = Ds.Tables[0];
                ddlemployee.DataTextField = "EMP";
                ddlemployee.DataValueField = "Emp_code";
                ddlemployee.DataBind();
                ddlemployee.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillEmpDetail()
        {
            string strqry = "";
            if (optcat.SelectedIndex == 0)
            {
                //strqry = "select  Emp_name,panno as Costcenter,PassportNumber as contractenddate,Emailid  from tblempmaster where emp_code='" + ddlemployee.SelectedValue + "'";
                strqry = "select  Emp_name,Emailid  from tblempmaster where emp_code='" + ddlemployee.SelectedValue + "'";

            }
            else
            {
                strqry = "select  Emp_name,Emailid  from tblempmaster where emp_code='" + ddlemployee.SelectedValue + "'";
            }




            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtEmpName.Text = Ds.Tables[0].Rows[0]["emp_name"].ToString();
                //txtcostcenter.Text = Ds.Tables[0].Rows[0]["Costcenter"].ToString();
                TxtEmail.Text = Ds.Tables[0].Rows[0]["Emailid"].ToString();

            }

        }
        protected void optcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optcat.SelectedIndex == 0)
            {
                FillCommon();
            }
            else
            {
                FillCommon();
            }
        }
        protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmpDetail();
            FillLaptopDesktop();
            FillGrid();
            ddlemployee.Focus();

        }
        protected void FillSerialNo(bool isClear = false)
        {
            if (isClear)
            {
                ddlsrno.Items.Clear();
                ddlsrno.Items.Insert(0, new ListItem("-----Select----", "-1"));
                return;
            }
            string strqry = "";
            if (SrNoAvailabiltySoftware() == true)
            {
                strqry = "select  ProductDetail_Id,ProductDetail_SerialNo from dbo.Inv_ProductDetail_Master " +
                            " where 1=1";
            }
            else
            {
                strqry = "select  ProductDetail_Id,ProductDetail_SerialNo from dbo.Inv_ProductDetail_Master " +
                           " where ProductDetail_Id not in (select isnull(StockOut_ProdDetail_Id,0) from dbo.Inv_StockOut " +
                           " where StockOut_Id not in (select  StockReturn_StockOut_Id from dbo.Inv_StockReturn))";
            }

            if (ddlproduct.SelectedValue != "-1")
            {
                strqry += " and Inv_ProductDetail_Master.productdetail_product_id='" + ddlproduct.SelectedValue + "'";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strqry += " and Inv_ProductDetail_Master.ProductDetail_Make_Id='" + ddlmake.SelectedValue + "'";
            }
            if (ddltype.SelectedValue != "-1")
            {
                strqry += " and Inv_ProductDetail_Master.ProductDetail_ProdType_Id='" + ddltype.SelectedValue + "'";
            }
            if (ddlmodel.SelectedValue != "-1" && SrNoAvailabiltySoftware() == false)
            {
                strqry += " and Inv_ProductDetail_Master.ProductDetail_ProdModel_Id='" + ddlmodel.SelectedValue + "'";
            }
            //Response.Write(strqry);

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlsrno.DataSource = Ds.Tables[0];
                ddlsrno.DataTextField = "ProductDetail_SerialNo";
                ddlsrno.DataValueField = "ProductDetail_Id";
                ddlsrno.DataBind();
                ddlsrno.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlsrno.DataSource = Ds.Tables[0];
                ddlsrno.DataBind();
                ddlsrno.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }
        protected void FillAssetCode(bool isClear = false)
        {
            if (isClear)
            {
                ddlassetcode.Items.Clear();
                ddlassetcode.Items.Insert(0, new ListItem("-----Select----", "-1"));
                return;
            }
            string strcond = "";
            string strqry = "";
            if (SrNoAvailabiltySoftware() == true)
            {
                strqry = "select  ProductDetail_Id,ProductDetail_Assetcode from dbo.Inv_ProductDetail_Master " +
                            " where 1=1";
            }
            else
            {


                strqry = "select  ProductDetail_Id,ProductDetail_Assetcode from dbo.Inv_ProductDetail_Master  " +
   " where ProductDetail_Id not in " +
   " (select isnull(StockOut_ProdDetail_Id,0) from dbo.Inv_StockOut " +
   " where StockOut_Id not in (select  StockReturn_StockOut_Id from dbo.Inv_StockReturn ))";
            }

            if (ddlproduct.SelectedValue != "-1")
            {
                strcond += " and Inv_ProductDetail_Master.productdetail_product_id='" + ddlproduct.SelectedValue + "'";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcond += " and Inv_ProductDetail_Master.ProductDetail_Make_Id='" + ddlmake.SelectedValue + "'";
            }
            if (ddltype.SelectedValue != "-1")
            {
                strcond += " and Inv_ProductDetail_Master.ProductDetail_ProdType_Id='" + ddltype.SelectedValue + "'";
            }
            if (ddlmodel.SelectedValue != "-1")
            {
                strcond += " and Inv_ProductDetail_Master.ProductDetail_ProdModel_Id='" + ddlmodel.SelectedValue + "'";
            }
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcond, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlassetcode.DataSource = Ds.Tables[0];
                ddlassetcode.DataTextField = "ProductDetail_Assetcode";
                ddlassetcode.DataValueField = "ProductDetail_Id";
                ddlassetcode.DataBind();
                ddlassetcode.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlassetcode.DataSource = Ds.Tables[0];
                ddlassetcode.DataBind();
                ddlassetcode.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }
        protected void GetSerialNo(string ProductDetailId)
        {
            string strqry = "select  ProductDetail_Id,ProductDetail_SerialNo from dbo.Inv_ProductDetail_Master where ProductDetail_Id='" + ProductDetailId + "' ";



            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlsrno.SelectedValue = Ds.Tables[0].Rows[0][0].ToString();
            }


        }
        protected void GetAssetCode(string ProductDetailId)
        {
            string strqry = "select  ProductDetail_Id,ProductDetail_Assetcode from dbo.Inv_ProductDetail_Master where ProductDetail_Id='" + ProductDetailId + "' and ProductDetail_Assetcode<>'' ";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlassetcode.SelectedValue = Ds.Tables[0].Rows[0][0].ToString();

            }

        }
        protected bool SrNoAvailabiltySoftware()
        {

            string strqry = "select count(*) from Inv_Product_Master where Product_Serial_Available='NO' and Product_ItemType='Software' and Product_Id=" + ddlproduct.SelectedValue + "";
            bool available = false;
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows[0][0].ToString() != "0")
            {
                available = true;
            }
            return available;
        }
        protected void SrNoAvailabiltyHardware()
        {

            string strqry = "select Product_InvType from Inv_Product_Master where Product_Id=" + ddlproduct.SelectedValue + "";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            Hidden1.Text = Ds.Tables[0].Rows[0][0].ToString();


        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string empName = "";
            SqlConnection con2 = new SqlConnection(strCon);
            SqlCommand SelectCommand = new SqlCommand("Select Emp_Name from [dbo].[TblEmpMaster] where [Emp_Code] = '" + ddlemployee.SelectedValue + "'", con2);
            SqlDataReader myreader;
            con2.Open();

            myreader = SelectCommand.ExecuteReader();

            List<String> lstEmails = new List<String>();
            while (myreader.Read())
            {
                empName = myreader.GetString(0);

            }
            con2.Close();


            int result = 0;
            SqlConnection con = new SqlConnection(strCon);

            SqlCommand cmd = new SqlCommand("stp_InsertInv_StockOut", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramqty = new SqlParameter()
            {
                ParameterName = "@StockOut_Qty",
                Value = Convert.ToInt32("1")
            };
            cmd.Parameters.Add(paramqty);

            SqlParameter paramempCode = new SqlParameter()
            {
                ParameterName = "@StockOut_EmpCode",
                Value = ddlemployee.SelectedValue
            };
            cmd.Parameters.Add(paramempCode);

            SqlParameter paramEmpName = new SqlParameter()
            {
                ParameterName = "@StockOut_EmpName",
                Value = empName
            };
            cmd.Parameters.Add(paramEmpName);

            SqlParameter paramCostCenter = new SqlParameter()
            {
                ParameterName = "@StockOut_CostCenter",
                Value = ""
            };
            cmd.Parameters.Add(paramCostCenter);

            SqlParameter paramissuedDate = new SqlParameter()
            {
                ParameterName = "@StockOut_IssueDate",
                Value = Convert.ToDateTime(txtdate.Text)
            };
            cmd.Parameters.Add(paramissuedDate);

            SqlParameter paramIssueType = new SqlParameter()
            {
                ParameterName = "@StockOut_IssueType",
                Value = ddlissuetype.SelectedValue
            };
            cmd.Parameters.Add(paramIssueType);

            SqlParameter paramremarks = new SqlParameter()
            {
                ParameterName = "@StockOut_Remarks",
                Value = txtremarks.Text
            };
            cmd.Parameters.Add(paramremarks);

            SqlParameter paramProdDetails = new SqlParameter()
            {
                ParameterName = "@StockOut_ProdDetail_Id",
                Value = Convert.ToInt32(ddlsrno.SelectedValue)
            };
            cmd.Parameters.Add(paramProdDetails);

            DateTime ErDate;
            if (txterdate.Text == String.Empty)
            {
                ErDate = System.DateTime.Now;
            }
            else
            {
                ErDate = Convert.ToDateTime(txterdate.Text);
            }
            SqlParameter paramERDate = new SqlParameter()
            {
                ParameterName = "@StockOut_ERDate",
                Value = ErDate
            };
            cmd.Parameters.Add(paramERDate);

            SqlParameter paramCreatedBy = new SqlParameter()
            {
                ParameterName = "@StockOut_CreatedBy",
                Value = Session["login"].ToString()
            };
            cmd.Parameters.Add(paramCreatedBy);

            SqlParameter paramCreatedDate = new SqlParameter()
            {
                ParameterName = "@StockOut_CreatedDate",
                Value = System.DateTime.Now.Date
            };
            cmd.Parameters.Add(paramCreatedDate);



            SqlParameter paramMpdifiedDate = new SqlParameter()
            {
                ParameterName = "@StockOut_ModifiedDate",
                Value = System.DateTime.Now.Date
            };
            cmd.Parameters.Add(paramMpdifiedDate);

            SqlParameter paramOACe = new SqlParameter()
            {
                ParameterName = "@StockOut_OAC",
                Value = ddlOAC.SelectedValue
            };
            cmd.Parameters.Add(paramOACe);

            SqlParameter paramReturnable = new SqlParameter()
            {
                ParameterName = "@Stockout_Returnable",
                Value = ""
            };
            cmd.Parameters.Add(paramReturnable);

            SqlParameter paramGetPassNo = new SqlParameter()
            {
                ParameterName = "@Stockout_GatePassNo",
                Value = Convert.ToInt32("0")
            };
            cmd.Parameters.Add(paramGetPassNo);

            SqlParameter paramSohid = new SqlParameter()
            {
                ParameterName = "@Stockout_SOHID",
                Value = ddlissuedhw.SelectedValue
            };
            cmd.Parameters.Add(paramSohid);

            String a;
            if (txtresven.Text == String.Empty)
            {
                a = "";
            }
            else
            {
                a = txtresven.SelectedItem.Text;
            }

            SqlParameter paramresponsible = new SqlParameter()
            {
                ParameterName = "@Stockout_Responsible",
                Value = a
            };
            cmd.Parameters.Add(paramresponsible);

            SqlParameter paramLocation = new SqlParameter()
            {
                ParameterName = "@Stockout_Location",
                Value = txtlocation.Text
            };
            cmd.Parameters.Add(paramLocation);


            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                result = 1;


            }
            catch (Exception ex)
            {

                result = -1;
                throw;
            }
            //SqlDataAdapter Da = new SqlDataAdapter(cmd);
            //DataSet Ds = new DataSet();
            //Da.Fill(Ds);
            con.Close();



            //SqlConnection con2 = new SqlConnection(strCon);

            string strqryOut = "select top 1 StockOut_Id,Product_Name,make_name,prodtype_name,prodmodel_name,StockOut_EmpName,StockOut_CostCenter,StockOut_IssueType,StockOut_Remarks,ProductDetail_SerialNo,Product_ItemType from Inv_StockOut" +
                            " left join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                            " inner join Inv_Product_Master on Inv_Product_Master.Product_Id=Inv_ProductDetail_Master.ProductDetail_Product_Id" +
                            " left join [dbo].[Inv_Make_Master] on inv_productdetail_master.ProductDetail_Make_Id =[Inv_Make_Master].Make_Id " +
                            " left join [dbo].[Inv_ProdType] on inv_productdetail_master.ProductDetail_ProdType_Id =[Inv_ProdType].ProdType_Id " +
                            " left join [dbo].[Inv_ProdModel] on inv_productdetail_master.ProductDetail_ProdModel_Id =[Inv_ProdModel].ProdModel_id " +
                           " order by StockOut_Id desc";
            SqlDataAdapter DaOut = new SqlDataAdapter(strqryOut, con);
            DataSet DsOut = new DataSet();
            DaOut.Fill(DsOut);
            if (DsOut.Tables[0].Rows.Count > 0)
            {

                if (TxtEmail.Text != "" && TxtEmail.Text != "Ashok.Dembla@khd.com" && DsOut.Tables[0].Rows[0]["Product_ItemType"].ToString() != "Software")
                {
                    MailItToUser(DsOut.Tables[0].Rows[0]["Product_Name"].ToString(), DsOut.Tables[0].Rows[0]["make_name"].ToString(), DsOut.Tables[0].Rows[0]["Prodtype_Name"].ToString(), DsOut.Tables[0].Rows[0]["Prodmodel_Name"].ToString(), DsOut.Tables[0].Rows[0]["ProductDetail_SerialNo"].ToString(), DsOut.Tables[0].Rows[0]["Product_ItemType"].ToString(), DsOut.Tables[0].Rows[0]["StockOut_EmpName"].ToString(), DsOut.Tables[0].Rows[0]["StockOut_CostCenter"].ToString(), DsOut.Tables[0].Rows[0]["StockOut_IssueType"].ToString(), DsOut.Tables[0].Rows[0]["StockOut_Remarks"].ToString(), TxtEmail.Text.ToString());
                }

            }

            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", "alert('Successfully Added');", true);

            }


            Clear();
            FillGrid();
            txtdate.Text = DateTime.Now.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        }

        protected void MailItToUser(string Product, string Make, string ProdType, string ProdModel, string SerialNo, string Category, string Employee, string CostCenter, string issuetype, string remarks, string EmpEmail)
        {


            MailMessage mail = new MailMessage();
            mail.To.Add(EmpEmail);
            mail.Bcc.Add("Rajnish.singh@khd.com,helpdesk.administrator@khd.com");
            //mail.From = new MailAddress("jainanuj009@gmail.com", "HR ADMIN");
            mail.From = new MailAddress("Hwil.ITAdmin@khd.com", "IT ADMIN");
            mail.Subject = "IT Consumable Issued To Employee";
            if (Category == "Software")
            {
                mail.Body = "An IT Asset Has been Issued to You " +
                            " <br>Product :" + Product + "<br>Make :" + Make + "<br>Product Type :" + ProdType + "<br>ProductModel :" + ProdModel + "<br>" +
                            " User:" + Employee + "<br>CostCenter:" + CostCenter + "<br>" +
                            " Issue Type:" + issuetype + "<br>Remarks:" + remarks + "<br>" +
                            " Kindly Take a good care of Company Asset";

            }
            else
            {
                mail.Body = "An IT Asset Has been Issued to You " +
                            " <br>Product :" + Product + "<br>Make :" + Make + "<br>Product Type :" + ProdType + "<br>ProductModel :" + ProdModel + "<br>" +
                            " SerialNo. :" + SerialNo + "<br>User:" + Employee + "<br>CostCenter:" + CostCenter + "<br>" +
                            " Issue Type:" + issuetype + "<br>Remarks:" + remarks + "<br>" +
                            " Kindly Take a good care of Company Asset";

            }


            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "172.16.0.50"; //Or Your SMTP Server Address
            smtp.Host = "mailrelay.cgn.khd.top"; //Or Your SMTP Server Address

            //smtp.Host = "15.326.0.80"; // farji
            //smtp.Host = "smtp.gmail.com";
            // smtp.Host = Convert.ToString(587);


            smtp.Credentials = new System.Net.NetworkCredential
                //("jainanuj009@gmail.com", "Anujja123");
                ("Hwil.ITAdmin@khd.com", "qwert@12345");
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            //smtp.Send(mail);
        }

        protected void MailItToUserReturn(string Product, string Make, string ProdType, string ProdModel, string SerialNo, string Category, string Employee, string CostCenter, string issuetype, string remarks, string EmpEmail)
        {


            MailMessage mail = new MailMessage();
            //mail.To.Add(EmpEmail);
            mail.Bcc.Add("Rajnish.singh@khd.com,helpdesk.administrator@khd.com");
            //mail.From = new MailAddress("jainanuj009@gmail.com", "HR ADMIN");
            mail.From = new MailAddress("Hwil.ITAdmin@khd.com", "IT ADMIN");
            mail.Subject = "IT Consumable Returned From Employee";
            if (Category == "Software")
            {
                mail.Body = "An IT asset has been returned by you and updated in Inventory System" +
                            " <br>Product :" + Product + "<br>Make :" + Make + "<br>Product Type :" + ProdType + "<br>ProductModel :" + ProdModel + "<br>" +
                            " User:" + Employee + "<br>CostCenter:" + CostCenter + "<br>" +
                            " Issue Type:" + issuetype + "" +
                            " ";

            }
            else
            {
                mail.Body = "An IT asset has been returned by you and updated in Inventory System " +
                            " <br>Product :" + Product + "<br>Make :" + Make + "<br>Product Type :" + ProdType + "<br>ProductModel :" + ProdModel + "<br>" +
                            " SerialNo. :" + SerialNo + "<br>User:" + Employee + "<br>CostCenter:" + CostCenter + "<br>" +
                            " Issue Type:" + issuetype + "<br>" +
                            " ";

            }


            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "172.16.0.50"; //Or Your SMTP Server Address
            smtp.Host = "mailrelay.cgn.khd.top"; //Or Your SMTP Server Address
                                                 //smtp.Host = "15.326.0.80"; // farji
                                                 //smtp.Host = "smtp.gmail.com";
                                                 // smtp.Host = Convert.ToString(587);
            smtp.Credentials = new System.Net.NetworkCredential
                //("jainanuj009@gmail.com", "Anujja123");
                ("Hwil.ITAdmin@khd.com", "qwert@12345");
            //Or your Smtp Email ID and Password
            // smtp.EnableSsl = true;
            //smtp.Send(mail);
        }

        //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtdate.Text = Calendar1.SelectedDate.ToString("dd/MMM/yyyy");
        //    Calendar1.Visible = false;
        //}

        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    Calendar1.Visible = true;
        //}
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            //txterdate.Text = Calendar2.SelectedDate.ToString("dd/MMM/yyyy");
            //Calendar2.Visible = false;
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            //Calendar2.Visible = true;
        }
        protected void FillRequestedBy()
        {
            string strqry = "";

            strqry = "select emp_name+'('+emp_code+')' as EMP from TblEmpMaster where  status in (0) order by emp_name ";




            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                txtresven.DataSource = Ds.Tables[0];
                txtresven.DataTextField = "EMP";
                txtresven.DataValueField = "EMP";
                txtresven.DataBind();
                txtresven.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillReservedUser()
        {
            string strqry = "";

            strqry = "select emp_name+'('+emp_code+')' as EMP from tblempmaster where  status in (0) order by emp_name ";




            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                txtresven.DataSource = Ds.Tables[0];
                txtresven.DataTextField = "EMP";
                txtresven.DataValueField = "EMP";
                txtresven.DataBind();
                txtresven.Items.Insert(0, new ListItem("New Joinee", "New Joinee"));
            }


        }
        protected void FillVendor()
        {
            string strqry = "";

            strqry = "select Vendor_Name as EMP from Inv_Vendor_Master where  Vendor_Status='ACTIVE' order by Vendor_Name ";




            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                txtresven.DataSource = Ds.Tables[0];
                txtresven.DataTextField = "EMP";
                txtresven.DataValueField = "EMP";
                txtresven.DataBind();
                txtresven.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void optOAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            txtdate.Text = System.DateTime.Now.Date.ToString("dd/MMM/yyyy");

            ddlOAC.SelectedValue = optOAC.SelectedValue;
            FillGrid();
            if (ddlOAC.SelectedValue == "Employee")
            {
                Label6.Visible = true;
                //Label6.Text = "Employee Code";
                Label7.Visible = true;
                Label12.Visible = true;
                Label13.Visible = true;


                optcat.Visible = false;
                FillEmployeeCode();

                //ddlOAC.Text = ddlOAC.SelectedValue;
                Label15.Visible = false;
                Label16.Visible = false;
                txtresven.Visible = false;
                txtlocation.Visible = false;

                ddlemployee.Visible = true;
                txtEmpName.Visible = true;
                //txtcostcenter.Visible = true;
                TxtEmail.Visible = true;
            }
            else if (ddlOAC.SelectedValue == "Internal" || ddlOAC.SelectedValue == "Site")
            {

                Label6.Visible = true;
                //Label6.Text = "Requested By";
                Label7.Visible = true;
                Label12.Visible = true;
                Label13.Visible = true;


                optcat.Visible = true;
                optcat.SelectedIndex = 0;

                Label15.Text = "Requested By";
                Label15.Visible = true;
                txtresven.Visible = true;
                FillRequestedBy();
                Label16.Visible = true;
                Label16.Text = "Location";
                txtlocation.Visible = true;

                ddlemployee.Visible = true;
                txtEmpName.Visible = true;
                //txtcostcenter.Visible = true;
                TxtEmail.Visible = true;
                FillRequestedBy();

            }
            else if (ddlOAC.SelectedValue == "Repair" || ddlOAC.SelectedValue == "Return to Vendor")
            {

                Label6.Visible = false;
                Label7.Visible = false;
                Label12.Visible = false;
                Label13.Visible = false;


                optcat.Visible = false;

                Label15.Text = "Vendor";
                Label15.Visible = true;
                txtresven.Visible = true;
                Label16.Visible = true;
                Label16.Text = "Address";
                txtlocation.Visible = true;

                ddlemployee.Visible = false;
                txtEmpName.Visible = false;
                //txtcostcenter.Visible = false;
                TxtEmail.Visible = false;
                FillVendor();
            }
            else if (ddlOAC.SelectedValue == "Reserved for user")
            {

                Label6.Visible = false;
                Label7.Visible = false;
                Label12.Visible = false;
                Label13.Visible = false;


                optcat.Visible = false;

                ddlemployee.Visible = false;
                txtEmpName.Visible = false;
                //txtcostcenter.Visible = false;
                TxtEmail.Visible = false;

                Label15.Text = "User Name";
                Label15.Visible = true;
                txtresven.Visible = true;
                Label16.Visible = true;
                Label16.Text = "Reserved till(mm/dd/yyyy)";
                txtlocation.Visible = true;
                FillReservedUser();

            }

            else
            {

                Label6.Visible = false;
                Label7.Visible = false;
                Label12.Visible = false;
                Label13.Visible = false;


                optcat.Visible = false;

                Label15.Visible = false;
                Label16.Visible = false;
                txtresven.Visible = false;
                txtlocation.Visible = false;

                ddlemployee.Visible = false;
                txtEmpName.Visible = false;
                //txtcostcenter.Visible = false;
                TxtEmail.Visible = false;
            }
        }

        protected void ddlOAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            txtdate.Text = System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
            optOAC.SelectedValue = ddlOAC.SelectedValue;
            //ddlOAC.SelectedValue = optOAC.SelectedValue;
            FillGrid();
            if (ddlOAC.SelectedValue == "Employee")
            {
                Label6.Visible = true;
                //Label6.Text = "Employee Code";
                Label7.Visible = true;
                Label12.Visible = true;
                Label13.Visible = true;


                optcat.Visible = false;
                FillEmployeeCode();

                //ddlOAC.Text = ddlOAC.SelectedValue;
                Label15.Visible = false;
                Label16.Visible = false;
                txtresven.Visible = false;
                txtlocation.Visible = false;

                ddlemployee.Visible = true;
                txtEmpName.Visible = true;
                //txtcostcenter.Visible = true;
                TxtEmail.Visible = true;
            }
            else if (ddlOAC.SelectedValue == "Internal" || ddlOAC.SelectedValue == "Site")
            {

                Label6.Visible = true;
                //Label6.Text = "Requested By";
                Label7.Visible = true;
                Label12.Visible = true;
                Label13.Visible = true;


                optcat.Visible = true;
                optcat.SelectedIndex = 0;

                Label15.Text = "Requested By";
                Label15.Visible = true;
                txtresven.Visible = true;
                FillRequestedBy();
                Label16.Visible = true;
                Label16.Text = "Location";
                txtlocation.Visible = true;

                ddlemployee.Visible = true;
                txtEmpName.Visible = true;
                //txtcostcenter.Visible = true;
                TxtEmail.Visible = true;
                FillRequestedBy();

            }
            else if (ddlOAC.SelectedValue == "Repair" || ddlOAC.SelectedValue == "Return to Vendor")
            {

                Label6.Visible = false;
                Label7.Visible = false;
                Label12.Visible = false;
                Label13.Visible = false;


                optcat.Visible = false;

                Label15.Text = "Vendor";
                Label15.Visible = true;
                txtresven.Visible = true;
                Label16.Visible = true;
                Label16.Text = "Address";
                txtlocation.Visible = true;

                ddlemployee.Visible = false;
                txtEmpName.Visible = false;
                //txtcostcenter.Visible = false;
                TxtEmail.Visible = false;
                FillVendor();
            }
            else if (ddlOAC.SelectedValue == "Reserved for user")
            {

                Label6.Visible = false;
                Label7.Visible = false;
                Label12.Visible = false;
                Label13.Visible = false;


                optcat.Visible = false;

                ddlemployee.Visible = false;
                txtEmpName.Visible = false;
                //txtcostcenter.Visible = false;
                TxtEmail.Visible = false;

                Label15.Text = "User Name";
                Label15.Visible = true;
                txtresven.Visible = true;
                Label16.Visible = true;
                Label16.Text = "Reserved till(mm/dd/yyyy)";
                txtlocation.Visible = true;
                FillReservedUser();

            }

            else
            {

                Label6.Visible = false;
                Label7.Visible = false;
                Label12.Visible = false;
                Label13.Visible = false;


                optcat.Visible = false;

                Label15.Visible = false;
                Label16.Visible = false;
                txtresven.Visible = false;
                txtlocation.Visible = false;

                ddlemployee.Visible = false;
                txtEmpName.Visible = false;
                //txtcostcenter.Visible = false;
                TxtEmail.Visible = false;
            }
        }

        protected void ddlmake_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
            FillProductType();
            FillProductModel();
            FillSerialNo(true);
            FillAssetCode(true);
            ddlmake.Focus();


        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
            FillProductModel();
            FillSerialNo(true);
            FillAssetCode(true);
            ddltype.Focus();

        }

        protected void ddlmodel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
            FillSerialNo();
            FillAssetCode();
            if (SrNoAvailabiltySoftware() == true)
            {
                txtlocation.Text = ddlmodel.SelectedValue;
            }
            ddlmodel.Focus();
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            var returnType = hdRetunType.Value;
            var stockOutId = hdStockId.Value;

            string scriptstring = "";


            string strqryhid = "select * from Inv_StockOut where stockout_oac<>'subitem' and StockOut_sohId='" + stockOutId + "'";
            SqlConnection Conhid = new SqlConnection();
            Conhid.ConnectionString = strCon;
            SqlDataAdapter Dahid = new SqlDataAdapter(strqryhid, Conhid);
            DataSet Dshid = new DataSet();
            Dahid.Fill(Dshid);
            if (Dshid.Tables[0].Rows.Count > 0)
            {
                scriptstring = "alert('first return associated software issued to this Asset');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                return;
            }

            string strqry = "";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            var proddetailid = "";
            string strpdid = "select StockOut_ProdDetail_Id from inv_stockout where StockOut_Id=" + stockOutId + "";
            SqlDataAdapter Dapdid = new SqlDataAdapter(strpdid, Con);
            DataSet Dspdid = new DataSet();
            Dapdid.Fill(Dspdid);
            proddetailid = Dspdid.Tables[0].Rows[0][0].ToString();

            string date = System.DateTime.Now.Date.ToString();

            string strupdategatepass = "update tbl_GatePass_Detail set GatePass_Detail_ReturnStatus='YES',GatePass_Detail_ActualRtnDate= (Select DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))) where GatePass_Stockout_id=" + stockOutId + "";
            SqlDataAdapter Daupdategatepass = new SqlDataAdapter(strupdategatepass, Con);
            DataSet Dsupdategatepass = new DataSet();
            Daupdategatepass.Fill(Dsupdategatepass);

           


            if (returnType == "1")
            {

                strqry = "insert into Inv_StockReturn(StockReturn_StockOut_Id," +
                        " StockReturn_ReturnDate," +
                        " StockReturn_CreatedBy, StockReturn_CreatedDate,StockReturn_Cost,StockReturn_Remarks,StockReturn_billno,stockreturn_LostByUser) " +
                        " values(" + stockOutId + "," +
                        " (Select DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())))," +
                        " '" + Session["login"].ToString() + "',(Select DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())))," + txtcost.Text + ",'" + Txtscrapremarks.Text + "','" + txtbillno.Text + "','NO')";

                strqry += " update Inv_StockOut set stockout_SOHID=0 where StockOut_Id=" + stockOutId + "";

                string ProductDetailIdForSubitemQuery = "select [StockOut_ProdDetail_Id] from [Inv_StockOut] where StockOut_Id = "+ stockOutId+"";
                SqlDataAdapter Data1 = new SqlDataAdapter(ProductDetailIdForSubitemQuery, Con);
                DataSet Dasa1 = new DataSet();
                Data1.Fill(Dasa1);

                string ProductDetailIdForSubitem = Dasa1.Tables[0].Rows[0][0].ToString();

                string StockOutIdForSubItemquery = "select [StockOut_Id] from [Inv_StockOut] where [Stockout_SOHID] = " + ProductDetailIdForSubitem + "";

                SqlDataAdapter Data2 = new SqlDataAdapter(StockOutIdForSubItemquery, Con);
                DataSet Dasa2 = new DataSet();
                Data2.Fill(Dasa2);

                string StockOutIdForSubItem = Dasa2.Tables[0].Rows[0][0].ToString();


                string subitemReturn = "INSERT INTO Inv_StockReturn (StockReturn_StockOut_Id, " +
                           "StockReturn_ReturnDate, " +
                           "StockReturn_CreatedBy, StockReturn_CreatedDate, stockreturn_LostByUser) " +
                           "VALUES (" + StockOutIdForSubItem + ", " +
                           "'" + DateTime.Today.ToString("yyyy-MM-dd") + "', " +
                           "'" + Session["login"].ToString() + "', " +
                           "'" + DateTime.Today.ToString("yyyy-MM-dd") + "', 'NO')";

                subitemReturn += " update Inv_StockOut set stockout_SOHID=0 where StockOut_Id=" + StockOutIdForSubItem + "";



                SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
                DataSet Ds = new DataSet();
                Da.Fill(Ds);

                SqlDataAdapter Data3 = new SqlDataAdapter(subitemReturn, Con);
                DataSet Dasa3 = new DataSet();
                Data3.Fill(Dasa3);

                if (rdlsrno.SelectedIndex == 1 && txtnewsrno.Text != "")
                {
                    string strgetdetalid = "select StockOut_ProdDetail_Id,ProductDetail_SerialNo from Inv_StockOut " +
                                           " inner join Inv_ProductDetail_Master on " +
                                           " Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id " +
                                           " where StockOut_Id=" + stockOutId + "";
                    SqlDataAdapter Dagetid = new SqlDataAdapter(strgetdetalid, Con);
                    DataSet Dsgetid = new DataSet();
                    Dagetid.Fill(Dsgetid);


                    string strupdatesrno = "update Inv_ProductDetail_Master set " +
                                           " ProductDetail_OldSrNo='" + Dsgetid.Tables[0].Rows[0]["ProductDetail_SerialNo"].ToString() + "'," +
                                           " ProductDetail_SerialNo='" + txtnewsrno.Text + "' " +
                                           " where ProductDetail_Id='" + Dsgetid.Tables[0].Rows[0]["StockOut_ProdDetail_Id"].ToString() + "'";
                    SqlDataAdapter Daupdatesrno = new SqlDataAdapter(strupdatesrno, Con);
                    DataSet Dsupdatesrno = new DataSet();
                    Daupdatesrno.Fill(Dsupdatesrno);
                }


                string strqryOut = "select  StockOut_Id,Product_Name,make_name,prodtype_name,prodmodel_name,StockOut_EmpCode,StockOut_EmpName,StockOut_CostCenter,StockOut_IssueType,StockOut_Remarks,ProductDetail_SerialNo,Product_ItemType from Inv_StockOut" +
                           " left join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                            " inner join Inv_Product_Master on Inv_Product_Master.Product_Id=Inv_ProductDetail_Master.ProductDetail_Product_Id" +
                             " left join [dbo].[Inv_Make_Master] on inv_productdetail_master.ProductDetail_Make_Id =[Inv_Make_Master].Make_Id " +
                            " left join [dbo].[Inv_ProdType] on inv_productdetail_master.ProductDetail_ProdType_Id =[Inv_ProdType].ProdType_Id " +
                            " left join [dbo].[Inv_ProdModel] on inv_productdetail_master.ProductDetail_ProdModel_Id =[Inv_ProdModel].ProdModel_id " +
                           " where StockOut_Id=" + stockOutId + "";
                SqlDataAdapter DaOut = new SqlDataAdapter(strqryOut, Con);
                DataSet DsOut = new DataSet();
                DaOut.Fill(DsOut);
                if (DsOut.Tables[0].Rows.Count > 0)
                {

                    string strqrymail = "select  Emailid  from tblempmaster where emp_code='" + DsOut.Tables[0].Rows[0]["StockOut_EmpCode"].ToString() + "'";
                    SqlDataAdapter Damail = new SqlDataAdapter(strqrymail, Con);
                    DataSet Dsmail = new DataSet();
                    Damail.Fill(Dsmail);
                    if (Dsmail.Tables[0].Rows.Count > 0)
                    {

                        if (Dsmail.Tables[0].Rows[0][0].ToString() != "Ashok.Dembla@khd.com" && Dsmail.Tables[0].Rows[0][0].ToString() != "" && DsOut.Tables[0].Rows[0]["Product_ItemType"].ToString() != "Software")
                        {
                            MailItToUserReturn(DsOut.Tables[0].Rows[0]["Product_Name"].ToString(), DsOut.Tables[0].Rows[0]["make_name"].ToString(), DsOut.Tables[0].Rows[0]["Prodtype_Name"].ToString(), DsOut.Tables[0].Rows[0]["Prodmodel_Name"].ToString(), DsOut.Tables[0].Rows[0]["ProductDetail_SerialNo"].ToString(), DsOut.Tables[0].Rows[0]["Product_ItemType"].ToString(), DsOut.Tables[0].Rows[0]["StockOut_EmpName"].ToString(), DsOut.Tables[0].Rows[0]["StockOut_CostCenter"].ToString(), DsOut.Tables[0].Rows[0]["StockOut_IssueType"].ToString(), DsOut.Tables[0].Rows[0]["StockOut_Remarks"].ToString(), Dsmail.Tables[0].Rows[0][0].ToString());
                        }
                    }
                }

                scriptstring = "alert('Successfully Returned');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);


            }
            if (returnType == "2")
            {
                strqry = "insert into Inv_StockReturn(StockReturn_StockOut_Id," +
                        " StockReturn_ReturnDate," +
                        " StockReturn_CreatedBy, StockReturn_CreatedDate,StockReturn_Cost,StockReturn_Remarks,StockReturn_billno,stockreturn_LostByUser) " +
                        " values(" + stockOutId + "," +
                        " '" + System.DateTime.Now.Date + "'," +
                        " '" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "'," + 0 + ",'" + Txtscrapremarks.Text + "','','NO')";

                strqry += " update Inv_StockOut set stockout_SOHID=0 where StockOut_Id=" + stockOutId + "";

                strqry += " insert into Inv_StockOut(" +
                      " StockOut_Qty, StockOut_EmpCode, StockOut_EmpName, StockOut_CostCenter," +
                      " StockOut_IssueDate,StockOut_IssueType,StockOut_Remarks,StockOut_ProdDetail_Id," +
                      " StockOut_ERDate,StockOut_CreatedBy, StockOut_CreatedDate,StockOut_ModifiedDate," +
                      " StockOut_OAC,Stockout_Returnable,Stockout_GatePassNo,stockout_SOHID,Stockout_Responsible,Stockout_Location) " +
                      " values(" +
                      " '1','','','','" + System.DateTime.Now.Date + "','Permanent','" + Txtscrapremarks.Text + "','" + proddetailid + "'," +
                      " '','" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "'," +
                      " '" + System.DateTime.Now.Date + "','" + rdlscrap.SelectedValue + "','','0','','','')";

                SqlDataAdapter Dascr = new SqlDataAdapter(strqry, Con);
                DataSet Dsscr = new DataSet();
                Dascr.Fill(Dsscr);
                scriptstring = "alert('Successfully Scrapped');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            }
            FillGrid();


        }
        protected void FillLaptopDesktop()
        {

            string strqry = "select [Productdetail_Id],Product_Name+' - '+ProductDetail_SerialNo+' - '+ProductDetail_AssetCode as Product  from  " +
                            " [dbo].[Inv_ProductDetail_Master]  " +
                            " inner join [dbo].[Inv_Product_Master] on [dbo].[Inv_Product_Master].Product_Id=[Inv_ProductDetail_Master].ProductDetail_Product_Id " +
                            " where product_id in (1,2,3)";

            if (ddlemployee.SelectedValue != "-1")
            {
                strqry = "select [Productdetail_Id],Product_Name+' - '+ProductDetail_SerialNo+' - '+ProductDetail_AssetCode as Product  from [dbo].[Inv_StockOut] " +
                           " inner join [dbo].[Inv_ProductDetail_Master] on [dbo].[Inv_ProductDetail_Master].Productdetail_Id=[Inv_StockOut].StockOut_ProdDetail_Id " +
                           " inner join [dbo].[Inv_Product_Master] on [dbo].[Inv_Product_Master].Product_Id=[Inv_ProductDetail_Master].ProductDetail_Product_Id " +
                           " where stockout_id not in (select [StockReturn_StockOut_Id] from [dbo].[Inv_StockReturn]) " +
                           " and [Product_Id] in (1,2,3) " +
                           " and StockOut_EmpCode='" + ddlemployee.SelectedValue + "'";
            }

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlissuedhw.DataSource = Ds.Tables[0];
                ddlissuedhw.DataTextField = "Product";
                ddlissuedhw.DataValueField = "Productdetail_Id";
                ddlissuedhw.DataBind();
                ddlissuedhw.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlissuedhw.DataSource = Ds.Tables[0];
                ddlissuedhw.DataBind();
                ddlissuedhw.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }

        protected void ddlissuetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}