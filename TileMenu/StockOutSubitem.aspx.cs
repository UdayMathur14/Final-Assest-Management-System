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
    public partial class StockOutSubitem : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtdate.Text = System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
                FillProductAll();
                FillProductAllIn();
                FillMakeAll();
                FillProductTypeAll();
                FillProductModelAll();
                FillEmployeeCode();
                //FillGrid();
                FillLaptopDesktop();
            }
        }

        protected void FillGrid()
        {
            string strcondition = "";
            string strqry = "select  stockout_id,convert(varchar(50),StockOut_IssueDate,106) as [Issue Date],StockOut_OAC as OnAccount,StockOut_IssueType as IssueType,StockOut_EmpCode as EmpCode," +
                            " StockOut_EmpName as EmpName,Product_Name as Product,Make_Name as Make," +
                            " ProdType_Name as Type,ProdModel_Name as Model,inv_productdetail_master.ProductDetail_AssetCode as AssetCode,inv_productdetail_master.ProductDetail_SerialNo as SerialNo,StockOut_Remarks as Remarks,issuedhw.ProductDetail_SerialNo IssuedHW from inv_stockout " +
                            " left join inv_productdetail_master on inv_stockout.StockOut_ProdDetail_Id=inv_productdetail_master.ProductDetail_Id " +
                            " left join Inv_Product_Master on inv_productdetail_master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id " +
                            " left join [dbo].[Inv_Make_Master] on inv_productdetail_master.ProductDetail_Make_Id =[Inv_Make_Master].Make_Id " +
                            " left join [dbo].[Inv_ProdType] on inv_productdetail_master.ProductDetail_ProdType_Id =[Inv_ProdType].ProdType_Id " +
                            " left join [dbo].[Inv_ProdModel] on inv_productdetail_master.ProductDetail_ProdModel_Id =[Inv_ProdModel].ProdModel_id " +
                            " left join inv_productdetail_master issuedhw on issuedhw.ProductDetail_Id=inv_stockout.Stockout_SOHID " +
                            " where StockOut_Id not in (select StockReturn_StockOut_Id from Inv_StockReturn) and stockout_oac='" + ddlOAC.SelectedValue + "'";

            //Response.Write(strqry);
            //Response.End();
            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and inv_productdetail_master.ProductDetail_Product_Id='" + ddlproduct.SelectedValue + "'";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcondition += " and inv_productdetail_master.ProductDetail_Make_Id='" + ddlmake.SelectedValue + "'";
            }
            if (ddltype.SelectedValue != "-1")
            {
                strcondition += " and inv_productdetail_master.ProductDetail_ProdType_Id='" + ddltype.SelectedValue + "'";
            }
            if (ddlmodel.SelectedValue != "-1")
            {
                strcondition += " and inv_productdetail_master.ProductDetail_ProdModel_Id='" + ddlmodel.SelectedValue + "'";
            }
            if (ddlemployee.SelectedValue != "-1")
            {
                strcondition += " and StockOut_EmpCode='" + ddlemployee.SelectedValue + "'";
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
            GridView3.DataSource = Ds;
            GridView3.DataBind();

            // }
            //updGrid.Update();
        }
        protected void FillGridNotInUse()
        {

            string strqry = "select [Productdetail_Id],Product_Name+' - '+ProductDetail_SerialNo+' - '+ProductDetail_AssetCode as Product  from  " +
                            " [dbo].[Inv_ProductDetail_Master]  " +
                            " inner join [dbo].[Inv_Product_Master] on [dbo].[Inv_Product_Master].Product_Id=[Inv_ProductDetail_Master].ProductDetail_Product_Id " +
                            " where Productdetail_Id ='" + ddlissuedhw.SelectedValue + "'";

            //Response.Write(strqry);
            //Response.End();
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

        }
        protected void Clear()
        {
            txtdate.Text = System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
            ddlproduct.SelectedValue = "-1";
            ddlmake.SelectedValue = "-1";
            ddltype.SelectedValue = "-1";
            ddlmodel.SelectedValue = "-1";
            //ddlsrno.SelectedValue = "";
            ddlissuedhw.SelectedValue = "-1";
            ddlemployee.SelectedValue = "-1";

            txtremarks.Text = "";
        }
        protected void FillGridAvailable()
        {

            string strqry = "select [Productdetail_Id],Product_Name,ProductDetail_SerialNo  from  " +
                            " [dbo].[Inv_ProductDetail_Master]  inner join inv_product_master on Inv_ProductDetail_Master.ProductDetail_Product_Id=inv_product_master.product_id" +
                            " where Productdetail_serialno ='" + txtcheckserial.Text + "'";

            //Response.Write(strqry);
            //Response.End();
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.DataBind();
            }
            else
            {
                Ds.Clear();
                GridView2.DataSource = Ds;
                GridView2.DataBind();

            }

        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            var returnType = hdRetunType.Value;
            var stockOutId = hdStockId.Value;
            string scriptstring = "";
            string strqry = "";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            var proddetailid = "";
            string strpdid = "select StockOut_ProdDetail_Id from inv_stockout where StockOut_Id=" + stockOutId + "";
            SqlDataAdapter Dapdid = new SqlDataAdapter(strpdid, Con);
            DataSet Dspdid = new DataSet();
            Dapdid.Fill(Dspdid);
            proddetailid = Dspdid.Tables[0].Rows[0][0].ToString();


            if (returnType == "1")
            {

                strqry = "insert into Inv_StockReturn(StockReturn_StockOut_Id," +
                        " StockReturn_ReturnDate," +
                        " StockReturn_CreatedBy, StockReturn_CreatedDate,StockReturn_Cost,StockReturn_Remarks,StockReturn_billno,stockreturn_LostByUser) " +
                        " values(" + stockOutId + "," +
                        " '" + System.DateTime.Now.Date + "'," +
                        " '" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "'," + 0 + ",'','','NO')";





                SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
                DataSet Ds = new DataSet();
                Da.Fill(Ds);
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
                        " '" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "'," + 0 + ",'','','YES')";

                strqry += " insert into Inv_StockOut(" +
                      " StockOut_Qty, StockOut_EmpCode, StockOut_EmpName, StockOut_CostCenter," +
                      " StockOut_IssueDate,StockOut_IssueType,StockOut_Remarks,StockOut_ProdDetail_Id," +
                      " StockOut_ERDate,StockOut_CreatedBy, StockOut_CreatedDate,StockOut_ModifiedDate," +
                      " StockOut_OAC,Stockout_Returnable,Stockout_GatePassNo,stockout_SOHID,Stockout_Responsible,Stockout_Location) " +
                      " values(" +
                      " '1','','','','" + System.DateTime.Now.Date + "','Permanent','Lost By User','" + proddetailid + "'," +
                      " '','" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "'," +
                      " '" + System.DateTime.Now.Date + "','Scrapped','','0','','','')";




                SqlDataAdapter Dascr = new SqlDataAdapter(strqry, Con);
                DataSet Dsscr = new DataSet();
                Dascr.Fill(Dsscr);
                scriptstring = "alert('Successfully Scrapped');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            }
            //FillGrid();


        }

        protected void FillProductAll()
        {

            string strqry = "select Product_Id,Product_Name  from inv_Product_master where Product_ItemType='" + typemaster.SelectedValue + "'" + "order by Product_name";



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
            else
            {
                Ds.Clear();
                ddlproduct.DataSource = Ds.Tables[0];
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }
        protected void FillProductAllIn()
        {

            string strqry = "select Product_Id,Product_Name  from inv_Product_master where Product_ItemType='" + typemaster.SelectedValue + "'" +
                           " and product_id  in (10,16,28,30,46,47,48,49,52) order by Product_name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlproductin.DataSource = Ds.Tables[0];
                ddlproductin.DataTextField = "Product_Name";
                ddlproductin.DataValueField = "Product_Id";
                ddlproductin.DataBind();
                ddlproductin.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlproductin.DataSource = Ds.Tables[0];
                ddlproductin.DataBind();
                ddlproductin.Items.Insert(0, new ListItem("-----Select----", "-1"));
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
        protected void FillMakeIn()
        {
            string strcondition = "";
            string strqry = "select distinct inv_Make_master.make_id,Make_name from inv_Make_master " +
                " left join Inv_Product_Mapping on inv_make_master.Make_id=Inv_Product_mapping.make_id where 1=1 ";

            if (ddlproductin.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproductin.SelectedValue + "";
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
                ddlmakein.DataSource = Ds.Tables[0];
                ddlmakein.DataTextField = "Make_Name";
                ddlmakein.DataValueField = "Make_Id";
                ddlmakein.DataBind();
                ddlmakein.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlmakein.DataSource = Ds.Tables[0];
                ddlmakein.DataBind();
                ddlmakein.Items.Insert(0, new ListItem("-----Select----", "-1"));
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
        protected void FillProductTypeIn()
        {
            string strcondition = "";
            string strqry = "select distinct ProdType_Id,ProdType_Name from inv_ProdType " +
                 " left join Inv_Product_Mapping on inv_ProdType.Prodtype_id=Inv_Product_mapping.type_id where 1=1";
            if (ddlproductin.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproductin.SelectedValue + "";
            }
            if (ddlmakein.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.make_id=" + ddlmakein.SelectedValue + "";
            }


            strcondition += " order by ProdType_Name";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddltypein.DataSource = Ds.Tables[0];
                ddltypein.DataTextField = "ProdType_Name";
                ddltypein.DataValueField = "ProdType_Id";
                ddltypein.DataBind();
                ddltypein.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddltypein.DataSource = Ds.Tables[0];
                ddltypein.DataBind();
                ddltypein.Items.Insert(0, new ListItem("-----Select----", "-1"));
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
        protected void FillProductModelIn()
        {

            string strcondition = "";
            string strqry = "select distinct ProdModel_Id,ProdModel_Name from inv_ProdModel " +
                 " left join Inv_Product_Mapping on inv_ProdModel.ProdModel_Id=Inv_Product_mapping.model_id where 1=1";
            if (ddlproductin.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproductin.SelectedValue + "";
            }
            if (ddlmakein.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.make_id=" + ddlmakein.SelectedValue + "";
            }
            if (ddltypein.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.type_id=" + ddltypein.SelectedValue + "";
            }

            strcondition += " order by ProdModel_Name";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlmodelin.DataSource = Ds.Tables[0];
                ddlmodelin.DataTextField = "ProdModel_Name";
                ddlmodelin.DataValueField = "ProdModel_Id";
                ddlmodelin.DataBind();
                ddlmodelin.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlmodelin.DataSource = Ds.Tables[0];
                ddlmodelin.DataBind();
                ddlmodelin.Items.Insert(0, new ListItem("-----Select----", "-1"));
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


        }
        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillMake();
            FillProductType();
            FillProductModel();



            FillSerialNo();
            FillAssetCode();
            FillGrid();
        }
        protected void ddlsrno_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAssetCode(ddlsrno.SelectedValue);
        }
        protected void ddlassetctcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSerialNo(ddlassetctcode.SelectedValue);
        }
        protected void FillEmployeeCode()
        {
            string strqry = "";

            strqry = "select Emp_code,emp_name+'('+emp_code+')' as EMP from tblempmaster where  status in (1) order by Emp_Code ";




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
        protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillLaptopDesktop();

        }
        protected void FillSerialNo()
        {
            string strcond = "";
            string strqry = "select  ProductDetail_Id,ProductDetail_SerialNo from dbo.Inv_ProductDetail_Master " +
                            " where ProductDetail_Id not in (select isnull(StockOut_ProdDetail_Id,0) from dbo.Inv_StockOut " +
                            " where StockOut_Id not in (select  StockReturn_StockOut_Id from dbo.Inv_StockReturn))";

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
            if (ddlmodel.SelectedValue != "-1")
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
                ddlsrno.Items.Insert(0, new ListItem("NA", ""));
            }

        }
        protected void FillAssetCode()
        {
            string strcond = "";
            string strqry = "select  ProductDetail_Id,ProductDetail_Assetcode from dbo.Inv_ProductDetail_Master  " +
    " where ProductDetail_Id not in " +
    " (select isnull(StockOut_ProdDetail_Id,0) from dbo.Inv_StockOut " +
    " where StockOut_Id not in (select  StockReturn_StockOut_Id from dbo.Inv_StockReturn ))";


            if (ddlproduct.SelectedValue != "-1")
            {
                strcond = " and Inv_ProductDetail_Master.productdetail_product_id='" + ddlproduct.SelectedValue + "'";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcond = " and Inv_ProductDetail_Master.ProductDetail_Make_Id='" + ddlmake.SelectedValue + "'";
            }
            if (ddltype.SelectedValue != "-1")
            {
                strcond = " and Inv_ProductDetail_Master.ProductDetail_ProdType_Id='" + ddltype.SelectedValue + "'";
            }
            if (ddlmodel.SelectedValue != "-1")
            {
                strcond = " and Inv_ProductDetail_Master.ProductDetail_ProdModel_Id='" + ddlmodel.SelectedValue + "'";
            }
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcond, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlassetctcode.DataSource = Ds.Tables[0];
                ddlassetctcode.DataTextField = "ProductDetail_Assetcode";
                ddlassetctcode.DataValueField = "ProductDetail_Id";
                ddlassetctcode.DataBind();
                ddlassetctcode.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlassetctcode.DataSource = Ds.Tables[0];
                ddlassetctcode.DataBind();
                ddlassetctcode.Items.Insert(0, new ListItem("NA", ""));
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
                ddlassetctcode.SelectedValue = Ds.Tables[0].Rows[0][0].ToString();

            }

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string scriptstring = "";
            //string strqry = "insert into Inv_StockOut(" +
            //           " StockOut_Qty, StockOut_EmpCode, StockOut_EmpName, StockOut_CostCenter," +
            //           " StockOut_IssueDate,StockOut_IssueType,StockOut_Remarks,StockOut_ProdDetail_Id," +
            //           " StockOut_ERDate,StockOut_CreatedBy, StockOut_CreatedDate,StockOut_ModifiedDate," +
            //           " StockOut_OAC,Stockout_Returnable,Stockout_GatePassNo,stockout_SOHID,Stockout_Responsible,Stockout_Location) " +
            //           " values(" +
            //           " '1'," +
            //           " 'ddlemployee.value','ddlemployee.Text'," +
            //           " '','" + txtdate.Text + "'," +
            //           " '" + ddlissuetype.SelectedValue + "','" + txtremarks.Text + "','" + ddlsrno.SelectedValue + "'," +
            //           " '','"+ Session["login"].ToString() + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "'," +
            //           " '" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + ddlOAC.SelectedValue + "','','0','" + ddlissuedhw.SelectedValue + "','','')";

            string strqry = "insert into Inv_StockOut(" +
                " StockOut_Qty, StockOut_EmpCode, StockOut_EmpName," +
                " StockOut_IssueDate, StockOut_IssueType, StockOut_Remarks, StockOut_ProdDetail_Id," +
                " StockOut_ERDate, StockOut_CreatedBy, StockOut_CreatedDate, StockOut_ModifiedDate," +
                " StockOut_OAC, Stockout_Returnable, Stockout_GatePassNo, stockout_SOHID, Stockout_Responsible, Stockout_Location) " +
                " values(" +
                " '1'," +
                " '" + ddlemployee.SelectedValue + "','" + ddlemployee.SelectedItem.Text + "'," +
                " '" + txtdate.Text + "'," +
                " '" + ddlissuetype.SelectedValue + "','" + txtremarks.Text + "','" + ddlsrno.SelectedValue + "'," +
                " '', '" + Session["login"].ToString() + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "'," +
                " '" + DateTime.Today.ToString("yyyy-MM-dd") + "','" + ddlOAC.SelectedValue + "','','0','" + ddlissuedhw.SelectedValue + "','','')";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            //FillGrid();
            Clear();
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



        protected void ddlmake_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillProductType();
            FillProductModel();
            FillSerialNo();
            FillAssetCode();
            FillGrid();

        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillProductModel();
            FillSerialNo();
            FillAssetCode();
            FillGrid();

        }

        protected void ddlmodel_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillSerialNo();
            FillAssetCode();
            FillGrid();
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {


            string scriptstring = "";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            string strpdid = "select ProductDetail_Id from Inv_ProductDetail_Master where ProductDetail_SerialNo='" + txtcheckserial.Text + "'";
            SqlDataAdapter Dapdid = new SqlDataAdapter(strpdid, Con);
            DataSet Dspdid = new DataSet();
            Dapdid.Fill(Dspdid);
            var proddetailid = "";
            if (Dspdid.Tables[0].Rows.Count > 0)
            {
                proddetailid = Dspdid.Tables[0].Rows[0][0].ToString();
            }

            if (proddetailid != "")
            {
                GridView1.Visible = true;
                string strstockout = "select StockOut_Id,StockOut_EmpName,Product_Name,stockout_OAC as oac from Inv_StockOut" +
                                     " inner join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id " +
                                     " inner join Inv_Product_Master on Inv_Product_Master.Product_Id=Inv_ProductDetail_Master.ProductDetail_Product_Id " +
                                     " WHERE stockout_id not in (select StockReturn_StockOut_Id from Inv_StockReturn) and StockOut_ProdDetail_Id=" + proddetailid + "";
                SqlDataAdapter Dasdid = new SqlDataAdapter(strstockout, Con);
                DataSet Dssdid = new DataSet();
                Dasdid.Fill(Dssdid);
                if (Dssdid.Tables[0].Rows.Count > 0)
                {

                    GridView1.DataSource = Dssdid;
                    GridView1.DataBind();
                    lblavl.Visible = false;
                    GridView2.Visible = false;

                }
                else
                {
                    lblavl.Visible = true;
                    Dssdid.Clear();
                    GridView1.DataSource = Dssdid;
                    GridView1.DataBind();
                    GridView2.Visible = true;
                    FillGridAvailable();
                }



                lblusable.Visible = false;
                tblstockin.Visible = false;


            }
            else
            {
                lblavl.Visible = false;
                lblusable.Visible = true;
                tblstockin.Visible = true;
                txtserial.Text = txtcheckserial.Text;
                GridView1.Visible = false;
                GridView2.Visible = true;
                FillGridAvailable();
            }


            //scriptstring = "alert('Successfully Scrapped');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);


        }

        protected void ddlissuedhw_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillLaptopDesktop();
        }

        protected void ddlproductin_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMakeIn();
            FillProductTypeIn();
            FillProductModelIn();
        }

        protected void ddlmakein_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProductTypeIn();
            FillProductModelIn();
        }

        protected void ddltypein_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillProductModelIn();

        }

        protected void btnentry_Click(object sender, EventArgs e)
        {
            string scriptstring = "";
            string strqry = "insert into Inv_StockIn(StockIn_Product_Id, StockIn_Make_Id,StockIn_ProdType_Id,StockIn_ProdModel_Id,StockIn_Vendor_Id," +
                      " StockIn_InDate, StockIn_ChallanNo, StockIn_Qty, StockIn_Price, StockIn_Tax," +
                      " StockIn_Net, StockIn_CreatedBy, StockIn_CreatedDate,StockIn_AssetType) " +
                      " values(" + ddlproductin.SelectedValue + "," + ddlmakein.SelectedValue + "," + ddltypein.SelectedValue + "," +
                      " " + ddlmodelin.SelectedValue + ",'3'," +
                      " '" + txtdate.Text + "'," +
                      " 'NA','1'," +
                      " '0','0','0'," +
                      " '" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "','HWI Asset')";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            string strqryOut = "select top 1 Stockin_Id from Inv_StockIn order by Stockin_Id desc";
            SqlDataAdapter DaOut = new SqlDataAdapter(strqryOut, Con);
            DataSet DsOut = new DataSet();
            DaOut.Fill(DsOut);

            string strdetail = "insert into Inv_ProductDetail_Master(ProductDetail_Product_Id,ProductDetail_Make_Id,ProductDetail_ProdType_Id,ProductDetail_ProdModel_Id,ProductDetail_AssetCode," +
                      " ProductDetail_SerialNo,Status," +
                      " ProductDetail_CapDate,ProductDetail_ExpiryDate,ProductDetail_OldSrNo,ProductDetail_StockIN_Id) " +
                      " values(" + ddlproductin.SelectedValue + "," + ddlmakein.SelectedValue + "," + ddltypein.SelectedValue + "," + ddlmodelin.SelectedValue + "," +
                      " '','" + txtserial.Text + "'," +
                      " '1','" + System.DateTime.Now.Date + "','','','" + DsOut.Tables[0].Rows[0]["Stockin_Id"].ToString() + "')";

            SqlConnection Condetail = new SqlConnection();
            Condetail.ConnectionString = strCon;
            SqlDataAdapter Dadetail = new SqlDataAdapter(strdetail, Condetail);
            DataSet Dsdetail = new DataSet();
            Dadetail.Fill(Dsdetail);


            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

            lblavl.Visible = true;
            lblusable.Visible = false;
            GridView2.Visible = true;
            FillGridAvailable();
            tblstockin.Visible = false;
            txtserial.Text = "";
            GridView1.Visible = false;
        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string scriptstring = "";
            int id = (int)GridView2.DataKeys[e.RowIndex].Value;
            string strqry = "insert into Inv_StockOut(" +
                       " StockOut_Qty, StockOut_EmpCode, StockOut_EmpName, StockOut_CostCenter," +
                       " StockOut_IssueDate,StockOut_IssueType,StockOut_Remarks,StockOut_ProdDetail_Id," +
                       " StockOut_ERDate,StockOut_CreatedBy, StockOut_CreatedDate,StockOut_ModifiedDate," +
                       " StockOut_OAC,Stockout_Returnable,Stockout_GatePassNo,stockout_SOHID,Stockout_Responsible,Stockout_Location) " +
                       " values(" +
                       " '1'," +
                       " '',''," +
                       " '','" + txtdate.Text + "'," +
                       " 'Permanent','','" + id + "'," +
                       " '','" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "'," +
                       " '" + System.DateTime.Now.Date + "','To be scrap','','0','','','')";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            scriptstring = "alert('Successfully Scrapped');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

            GridView2.Visible = false;
            GridView1.Visible = true;
            string strstockout = "select StockOut_Id,StockOut_EmpCode,StockOut_EmpName,stockout_OAC from Inv_StockOut where " +
                                 " stockout_id not in (select StockReturn_StockOut_Id from Inv_StockReturn) and StockOut_ProdDetail_Id=" + id + "";
            SqlDataAdapter Dasdid = new SqlDataAdapter(strstockout, Con);
            DataSet Dssdid = new DataSet();
            Dasdid.Fill(Dssdid);
            if (Dssdid.Tables[0].Rows.Count > 0)
            {

                GridView1.DataSource = Dssdid;
                GridView1.DataBind();

            }
            else
            {
                Dssdid.Clear();
                GridView1.DataSource = Dssdid;
                GridView1.DataBind();

            }


        }
        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int stockOutId = Convert.ToInt32(GridView3.DataKeys[e.RowIndex].Value.ToString());
            string strqry = "";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            strqry = "INSERT INTO Inv_StockReturn (StockReturn_StockOut_Id, " +
                            "StockReturn_ReturnDate, " +
                            "StockReturn_CreatedBy, StockReturn_CreatedDate, stockreturn_LostByUser) " +
                            "VALUES (" + stockOutId + ", " +
                            "'" + DateTime.Today.ToString("yyyy-MM-dd") + "', " +
                            "'" + Session["login"].ToString() + "', " +
                            "'" + DateTime.Today.ToString("yyyy-MM-dd") + "', 'NO')";

            strqry += " update Inv_StockOut set stockout_SOHID=0 where StockOut_Id=" + stockOutId + "";



            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            FillGrid();

        }
    }
}