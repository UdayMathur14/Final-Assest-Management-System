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
    public partial class ProductWiseReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillProductAll();
                
                GetData();



            }
            if (Request.QueryString["ProductId"] != null)
            {
                if (Request.QueryString["Name"] == "O")
                {
                    pnldetail.Visible = true;
                    pnlData.Visible = false;
                   
                    //GetProductName();
                    GetDataItemOut();
                    if (SrNoAvailabiltySoftware() == true)
                    {
                        GetDistinctRecord();
                    }
                }
                if (Request.QueryString["Name"] == "I")
                {
                    pnldetail.Visible = true;
                    pnlData.Visible = false;
                   
                    //GetProductName();
                    GetDataItemIn();
                }
                if (Request.QueryString["Name"] == "R")
                {
                    pnldetail.Visible = true;
                    pnlData.Visible = false;
                    
                    //GetProductName();
                    GetDataRepair();
                }
                if (Request.QueryString["Name"] == "T")
                {
                    pnldetail.Visible = true;
                    pnlData.Visible = false;
                    
                    //GetProductName();
                    GetDataScrap();
                }
                if (Request.QueryString["Name"] == "S")
                {
                    pnldetail.Visible = true;
                    pnlData.Visible = false;

                   // GetProductName();
                    GetDataSold();
                }
                if (Request.QueryString["Name"] == "A")
                {
                    pnldetail.Visible = true;
                    pnlData.Visible = false;
                    
                    //GetProductName();
                    GetDataItemAvailable();

                }

                //GetDataItemIn();

                //GetDataBlocked();
                //GetDataItemReturn();
            }


        }

        protected bool SrNoAvailabiltySoftware()
        {

            string strqry = "select count(*) from Inv_Product_Master where Product_Serial_Available='NO' and Product_ItemType='Software' and Product_Id=" + Request.QueryString["ProductId"].ToString() + "";
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
        protected void typemaster_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillProductAll();

            GetData();
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
        
        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            

           
            GetData();
        }
      
        private void GetProductName()
        {
            string strqry = " select Product_Name from Inv_Product_Master where Product_Id=" + Request.QueryString["ProductId"].ToString() + " ";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblproductname.Text = Ds.Tables[0].Rows[0][0].ToString();
            }
        }

        protected void GetDistinctRecord()
        {
            string strqry = " select count(distinct StockOut_EmpCode) from dbo.Inv_ProductDetail_Master INNER JOIN dbo.Inv_Product_Master ON " +
                           " Inv_ProductDetail_Master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id " +
                           " left outer join inv_stockout on inv_stockout.stockout_proddetail_id = Inv_ProductDetail_Master.Productdetail_id " +
                           " and stockout_id not in (select stockreturn_stockout_id from inv_stockreturn) " +
                            " where stockout_OAC in('employee', 'Internal', 'Site', 'Subitem') and product_id = " + Request.QueryString["ProductId"].ToString() + " ";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lbldistinctrecord.Text = "No. Of Unique Out:" + Ds.Tables[0].Rows[0][0].ToString();
            }
        }
        protected void GetData()
        {
            string StrCondition = "";
            string StrCondition1 = "";
            //string strqry = " select Product_Name,sum(StockIn_Qty) as TotalInQty,Product_Id," +
            //              " (select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut where stockout_OAC='Employee' " +
            //              " and Inv_StockOut.StockOut_Product_Id=Inv_StockIn.StockIn_Product_Id)-(select isnull(sum(isnull(StockReturn_Qty,0)),0) " +
            //              " from Inv_StockReturn where Inv_StockReturn.StockReturn_Product_Id=Inv_Stockin.Stockin_Product_Id and " +
            //              " stockreturn_stockout_id in(select stockout_id from inv_stockout where stockout_oac='employee' and " +
            //              " stockout_product_id=StockReturn_Product_Id)) as TotalOut," +
            //              " (select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut where stockout_OAC in('SubItem','To be Scrap','StandBy','For Repair') "+
            //              " and Inv_StockOut.StockOut_Product_Id=Inv_StockIn.StockIn_Product_Id)-(select isnull(sum(isnull(StockReturn_Qty,0)),0) "+
            //              " from Inv_StockReturn where Inv_StockReturn.StockReturn_Product_Id=Inv_Stockin.Stockin_Product_Id and "+
            //              " stockreturn_stockout_id in(select stockout_id from inv_stockout where stockout_oac in('SubItem','To be Scrap','StandBy','For Repair') "+
            //              " and stockout_product_id=StockReturn_Product_Id)) as TotalOutNEU," +
            //              " (select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut where stockout_OAC in('SOLD','Scrapped','Block/Inactive') "+
            //              " and Inv_StockOut.StockOut_Product_Id=Inv_StockIn.StockIn_Product_Id)-(select isnull(sum(isnull(StockReturn_Qty,0)),0) from "+
            //              " Inv_StockReturn where Inv_StockReturn.StockReturn_Product_Id=Inv_Stockin.Stockin_Product_Id and stockreturn_stockout_id "+
            //              " in(select stockout_id from inv_stockout where stockout_oac in('SOLD','Scrapped','Block/Inactive') and "+
            //              " stockout_product_id=StockReturn_Product_Id)) as TotalOutNENU," +
            //              " sum(StockIn_Qty)-(select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut where "+
            //              " Inv_StockOut.StockOut_Product_Id=Inv_StockIn.StockIn_Product_Id) " +
            //              " + (select isnull(sum(isnull(StockReturn_Qty,0)),0) from Inv_StockReturn where "+
            //              " Inv_StockReturn.StockReturn_Product_Id=Inv_StockIn.StockIn_Product_Id) as Available " +
            //              " from Inv_StockIn inner join " +
            //              " Inv_Product_Master on Inv_Product_Master.product_id=Inv_StockIn.StockIn_product_Id " +
            //              " inner join Inv_Vendor_Master on Inv_Vendor_Master.vendor_Id=Inv_StockIn.StockIn_vendor_Id WHERE [Product_ItemType]='Hardware'";

            string strqry = " select Product_Name,product_id,sum(StockIn_Qty) as TotalInQty,Product_Id," +
                             " (select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut  " +
                             " inner join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                             " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                             " where stockout_OAC in('employee','Internal','Site','Subitem')  " +
                             " and Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_Product_Id)-(select isnull(count(isnull(StockReturn_id, 0)), 0) " +
                            " from Inv_StockReturn where " +
                            " stockreturn_stockout_id in(select stockout_id from inv_stockout where stockout_oac in('employee','Internal','Site','Subitem') and " +
                            " stockout_proddetail_id in (select ProductDetail_Id from Inv_ProductDetail_Master " +
                           " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                           " where Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_product_Id)) ) as TotalOut," +
                           " (select count(distinct StockOut_EmpCode) from dbo.Inv_ProductDetail_Master INNER JOIN dbo.Inv_Product_Master ON "+
                           " Inv_ProductDetail_Master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id "+
                           " left outer join inv_stockout on inv_stockout.stockout_proddetail_id = Inv_ProductDetail_Master.Productdetail_id "+
                           " and stockout_id not in (select stockreturn_stockout_id from inv_stockreturn) "+
                            " where stockout_OAC in('employee', 'Internal', 'Site', 'Subitem') and stockout_proddetail_id in "+
                           " (select ProductDetail_Id from Inv_ProductDetail_Master inner join Inv_Product_Master on "+
                           " Inv_ProductDetail_Master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id "+
                           " where Inv_Product_Master.Product_Id = Inv_StockIn.StockIn_product_Id)) as uniqueout,"+
                           " (select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut  " +
                             " inner join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                             " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                             " where stockout_OAC in('Repair','Standby','Reserved for user')  " +
                             " and Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_Product_Id)-(select isnull(count(isnull(StockReturn_id, 0)), 0) " +
                            " from Inv_StockReturn where " +
                            " stockreturn_stockout_id in(select stockout_id from inv_stockout where stockout_oac in('Repair','Standby','Reserved for user') and " +
                            " stockout_proddetail_id in (select ProductDetail_Id from Inv_ProductDetail_Master " +
                           " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                           " where Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_product_Id)) ) as TotalOutRepair," +
                           " (select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut  " +
                             " inner join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                             " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                             " where stockout_OAC in('To be scrap')  " +
                             " and Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_Product_Id)-(select isnull(count(isnull(StockReturn_id, 0)), 0) " +
                            " from Inv_StockReturn where " +
                            " stockreturn_stockout_id in(select stockout_id from inv_stockout where stockout_oac in('To be scrap') and " +
                            " stockout_proddetail_id in (select ProductDetail_Id from Inv_ProductDetail_Master " +
                           " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                           " where Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_product_Id)) ) as TotalOutScrap," +
                            " (select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut  " +
                             " inner join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                             " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                             " where stockout_OAC in('Sold','Scrapped','Return to Vendor')  " +
                             " and Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_Product_Id)-(select isnull(count(isnull(StockReturn_id, 0)), 0) " +
                            " from Inv_StockReturn where " +
                            " stockreturn_stockout_id in(select stockout_id from inv_stockout where stockout_oac in('Sold','Scrapped','Return to Vendor') and " +
                            " stockout_proddetail_id in (select ProductDetail_Id from Inv_ProductDetail_Master " +
                           " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                           " where Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_product_Id)) ) as TotalOutSold," +
                            " sum(StockIn_Qty)-(select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut " +
                            " inner join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                             " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                             " and Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_Product_Id)+(select isnull(count(isnull(StockReturn_id, 0)), 0) " +
                            " from Inv_StockReturn where " +
                            " stockreturn_stockout_id in(select stockout_id from inv_stockout where  " +
                            " stockout_proddetail_id in (select ProductDetail_Id from Inv_ProductDetail_Master " +
                           " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                           " where Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_product_Id))) as Available" +
                             " from Inv_StockIn inner join " +
                             " Inv_Product_Master on Inv_Product_Master.product_id=Inv_StockIn.StockIn_product_Id " +
                             " inner join Inv_Vendor_Master on Inv_Vendor_Master.vendor_Id=Inv_StockIn.StockIn_vendor_Id WHERE [Product_ItemType]='" + typemaster.SelectedValue + "'";

            //Response.Write(strqry);
            //Response.End();
            //-(select isnull(sum(isnull(StockReturn_Qty, 0)), 0) " +
            //                " from Inv_StockReturn where " +
            //                " stockreturn_stockout_id in(select stockout_id from inv_stockout where stockout_oac='employee' and " +
            //                " stockout_proddetail_id in (select ProductDetail_Id from Inv_ProductDetail_Master " +
            //                " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
            //                " where Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_product_Id)) )

            if (ddlproduct.SelectedValue != "-1")
            {
                StrCondition += " AND StockIn_Product_Id='" + ddlproduct.SelectedValue + "'";
            }
            
            if (txtqty.Text != "")
            {
                StrCondition1 = " having sum(StockIn_Qty)-(select isnull(sum(isnull(StockOut_Qty,0)),0) from Inv_StockOut " +
                           " inner join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                            " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                            " and Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_Product_Id)+(select isnull(count(isnull(StockReturn_id, 0)), 0) " +
                           " from Inv_StockReturn where " +
                           " stockreturn_stockout_id in(select stockout_id from inv_stockout where  " +
                           " stockout_proddetail_id in (select ProductDetail_Id from Inv_ProductDetail_Master " +
                          " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                          " where Inv_Product_Master.Product_Id=Inv_StockIn.StockIn_product_Id)))<= " + Convert.ToInt32(txtqty.Text) + "";
            }
           
            StrCondition += " group by Product_Name,StockIn_Product_Id,Product_Id";
            string StrFinal = strqry + StrCondition + StrCondition1 + " order by product_name";

            //Response.Write(StrFinal);
            //Response.End();
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(StrFinal, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataBind();

                //decimal total = Ds.AsEnumerable().Sum(row => row.Field<decimal>("TotalInQty"));

                GridView1.FooterRow.Cells[2].Text = "";

                GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                //GridView1.FooterRow.Cells[3].Text = total.ToString("N2");
            }
            else
            {
                Ds.Clear();
                GridView1.DataBind();
            }
        }
        protected void GetDataItemIn()
        {
            string StrCondition = "";

            string strqry = " select convert(varchar(50),StockIn_InDate,106) as [Item In Date],Stockin_id as S_id,vendor_name as Vendor,StockIn_ChallanNo as [Challan No.],StockIn_Qty as Qty,StockIn_AssetType as Type from Inv_StockIn " +
                " left join Inv_Vendor_Master on Inv_Vendor_Master.Vendor_Id=Inv_StockIn.StockIn_Vendor_Id" +
                " where StockIn_Product_Id=" + Request.QueryString["ProductId"].ToString() + "";



            //if(txtdatefrom.Text!="")
            //{
            //    StrCondition += " and StockIn_InDate>='"+ txtdatefrom.Text  +"'";
            //}
            //if (txtdateto.Text != "")
            //{
            //    StrCondition += " and StockIn_InDate<='" + txtdateto.Text + "'";
            //}
            string StrFinal = strqry + StrCondition;

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(StrFinal, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                trinheader.Visible = true;
                trindetail.Visible = true;
                grvItemIn.DataSource = Ds;
                grvItemIn.DataBind();
            }
            else
            {
                Ds.Clear();
                grvItemIn.DataBind();
                trinheader.Visible = false;
                trindetail.Visible = false;
            }
        }
        protected void GetDataItemOut()
        {
            
            string strqry = " select ROW_NUMBER() OVER( ORDER BY stockout_OAC,Product_Name,Make_Name,ProdType_Name,ProdModel_Name,ProductDetail_CapDate,StockOut_Remarks ) AS [Sr. No.], "+
                            " Product_Name as [Product Name],Make_Name as Make,ProdType_Name as Type,ProdModel_Name as Model,"+
                            " ProductDetail_AssetCode as [Asset Code],ProductDetail_SerialNo as [Serial No.],ProductDetail_Config as [Configuration],isnull(stockout_OAC,'Available') as OAC, " +
                            " StockOut_EmpCode as [Employee Code],stockout_empname as [Employee Name],StockOut_CostCenter as [Cost Center],tblCostCenter.Description as [Cost Center Name], " +
                            " convert(varchar(50),StockOut_IssueDate,106) as [Issued On],StockOut_IssueType as [Issue Type],StockOut_Remarks as Remarks, "+
                            " convert(varchar(50),[StockIn_InDate],106) as [In Date],StockIn_AssetType as [Asset Type], "+
                            " [StockIn_ChallanNo] as [Challan No.],convert(varchar(50),ProductDetail_CapDate,106) as [Capitilized On], "+
                            " convert(varchar(50),ProductDetail_ExpiryDate,106) as [Expired On],DATEDIFF (yy, ProductDetail_CapDate, GETDATE()) as Age, "+
                            " DATEDIFF (dd, ProductDetail_CapDate, GETDATE()) as days,ProductDetail_Id,[StockIn_id],product_id,stockout_id from dbo.Inv_ProductDetail_Master INNER JOIN dbo.Inv_Product_Master " +
                            " ON Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                            " left join [dbo].[Inv_StockIn] on [Inv_ProductDetail_Master].[ProductDetail_StockIn_Id]=[Inv_StockIn].[StockIn_Id] " +
                            " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
                            " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
                            " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
                            " left join inv_stockout on inv_stockout.stockout_proddetail_id=Inv_ProductDetail_Master.Productdetail_id "+
                            " and stockout_id not in (select stockreturn_stockout_id from inv_stockreturn)" +
                            " left join tblCostCenter on inv_stockout.StockOut_CostCenter=tblCostCenter.CostCenter " +
                            //" where  ProductDetail_Id NOT IN (select StockOut_ProdDetail_Id from dbo.Inv_StockOut where ( (stockout_OAC='SOLD')  or (stockout_OAC='Block/Inactive')  )  ) "+
                            " where  stockout_OAC in('employee','Internal','Site','Subitem') and product_id='" + Request.QueryString["ProductId"].ToString() + "'";

            //Response.Write(strqry);
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
               
                troutdetail.Visible = true;
                grvItemOut.DataSource = Ds;
                grvItemOut.DataBind();
            }
            else
            {
                Ds.Clear();
                grvItemOut.DataBind();
                
                troutdetail.Visible = false;
            }
        }
        protected void GetDataRepair()
        {
            

            string strqry = " select ROW_NUMBER() OVER( ORDER BY stockout_OAC,Product_Name,Make_Name,ProdType_Name,ProdModel_Name,ProductDetail_CapDate,StockOut_Remarks ) AS [Sr. No.], "+
                            " Product_Name as [Product Name],Make_Name as Make,ProdType_Name as Type,ProdModel_Name as Model,ProductDetail_AssetCode as [Asset Code], "+
                            " ProductDetail_SerialNo as [Serial No.],ProductDetail_Config as [Configuration],isnull(stockout_OAC,'Available') as OAC, " +
                            " StockOut_EmpCode as [Employee Code],stockout_empname as [Employee Name],StockOut_CostCenter as [Cost Center],tblCostCenter.Description as [Cost Center Name], " +
                            " convert(varchar(50),StockOut_IssueDate,106) as [Issued On],StockOut_IssueType as [Issue Type], "+
                            " StockOut_Remarks as Remarks,convert(varchar(50),[StockIn_InDate],106) as [In Date],StockIn_AssetType as [Asset Type], "+
                            " [StockIn_ChallanNo] as [Challan No.], "+
                            " convert(varchar(50),ProductDetail_CapDate,106) as [Capitilized On],convert(varchar(50),ProductDetail_ExpiryDate,106) as [Expired On], "+
                            " DATEDIFF (yy, ProductDetail_CapDate, GETDATE()) as Age,DATEDIFF (dd, ProductDetail_CapDate, GETDATE()) as days, "+
                            " [StockIn_id],ProductDetail_Id,product_id,stockout_id " +
                            " from dbo.Inv_ProductDetail_Master INNER JOIN dbo.Inv_Product_Master " +
                            " ON Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                            " inner join [dbo].[Inv_StockIn] on [Inv_ProductDetail_Master].[ProductDetail_StockIn_Id]=[Inv_StockIn].[StockIn_Id] " +
                            " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
                            " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
                            " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
                            " left outer join inv_stockout on inv_stockout.stockout_proddetail_id=Inv_ProductDetail_Master.Productdetail_id "+
                            " and stockout_id not in (select stockreturn_stockout_id from inv_stockreturn)" +
                             " left join tblCostCenter on inv_stockout.StockOut_CostCenter=tblCostCenter.CostCenter " +
                            //" where  ProductDetail_Id NOT IN (select StockOut_ProdDetail_Id from dbo.Inv_StockOut where ( (stockout_OAC='SOLD')  or (stockout_OAC='Block/Inactive')  )  ) "+
                            "where  stockout_OAC in('Repair','Standby','Reserved for user') and product_id='" + Request.QueryString["ProductId"].ToString() + "'";

            //if (txtdatefrom.Text != "")
            //{
            //    StrCondition += " and StockOut_IssueDate>='" + txtdatefrom.Text + "'";
            //}
            //if (txtdateto.Text != "")
            //{
            //    StrCondition += " and StockOut_IssueDate<='" + txtdateto.Text + "'";
            //}
            //if (txtempcode.Text != "")
            //{
            //    StrCondition += " and StockOut_EmpCode like '%" + txtempcode.Text + "%'";
            //}
            //if (txtempname.Text != "")
            //{
            //    StrCondition += " and StockOut_EmpName like '%" + txtempname.Text + "%'";
            //}
            //if (txtcostcenter.Text != "")
            //{
            //    StrCondition += " and StockOut_CostCenter like '%" + txtcostcenter.Text + "%'";
            //}

            

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
               
                troutdetail.Visible = true;
                grvItemOut.DataSource = Ds;
                grvItemOut.DataBind();
            }
            else
            {
                Ds.Clear();
                grvItemOut.DataBind();
                
                troutdetail.Visible = false;
            }
        }

        protected void GetDataScrap()
        {


            string strqry = " select ROW_NUMBER() OVER( ORDER BY stockout_OAC,Product_Name,Make_Name,ProdType_Name,ProdModel_Name,ProductDetail_CapDate,StockOut_Remarks ) AS [Sr. No.], " +
                            " Product_Name as [Product Name],Make_Name as Make,ProdType_Name as Type,ProdModel_Name as Model," +
                            " ProductDetail_AssetCode as [Asset Code],ProductDetail_SerialNo as [Serial No.],ProductDetail_Config as [Configuration],isnull(stockout_OAC,'Available') as OAC, " +
                            " StockOut_EmpCode as [Employee Code],stockout_empname as [Employee Name],StockOut_CostCenter as [Cost Center],tblCostCenter.Description as [Cost Center Name], " +
                            " convert(varchar(50),StockOut_IssueDate,106) as [Issued On],StockOut_IssueType as [Issue Type],StockOut_Remarks as Remarks, " +
                            " convert(varchar(50),[StockIn_InDate],106) as [In Date],StockIn_AssetType as [Asset Type], " +
                            " [StockIn_ChallanNo] as [Challan No.],convert(varchar(50),ProductDetail_CapDate,106) as [Capitilized On], " +
                            " convert(varchar(50),ProductDetail_ExpiryDate,106) as [Expired On],DATEDIFF (yy, ProductDetail_CapDate, GETDATE()) as Age, " +
                            " DATEDIFF (dd, ProductDetail_CapDate, GETDATE()) as days,ProductDetail_Id,[StockIn_id],product_id,stockout_id from dbo.Inv_ProductDetail_Master INNER JOIN dbo.Inv_Product_Master " +
                            " ON Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                            " inner join [dbo].[Inv_StockIn] on [Inv_ProductDetail_Master].[ProductDetail_StockIn_Id]=[Inv_StockIn].[StockIn_Id] " +
                            " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
                            " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
                            " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
                            " left outer join inv_stockout on inv_stockout.stockout_proddetail_id=Inv_ProductDetail_Master.Productdetail_id " +
                            " and stockout_id not in (select stockreturn_stockout_id from inv_stockreturn)" +
                            " left join tblCostCenter on inv_stockout.StockOut_CostCenter=tblCostCenter.CostCenter " +
                            " where stockout_OAC in('To Be Scrap') and product_id='" + Request.QueryString["ProductId"].ToString() + "'";

            //if (txtdatefrom.Text != "")
            //{
            //    StrCondition += " and StockOut_IssueDate>='" + txtdatefrom.Text + "'";
            //}
            //if (txtdateto.Text != "")
            //{
            //    StrCondition += " and StockOut_IssueDate<='" + txtdateto.Text + "'";
            //}
            //if (txtempcode.Text != "")
            //{
            //    StrCondition += " and StockOut_EmpCode like '%" + txtempcode.Text + "%'";
            //}
            //if (txtempname.Text != "")
            //{
            //    StrCondition += " and StockOut_EmpName like '%" + txtempname.Text + "%'";
            //}
            //if (txtcostcenter.Text != "")
            //{
            //    StrCondition += " and StockOut_CostCenter like '%" + txtcostcenter.Text + "%'";
            //}

           

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                
                troutdetail.Visible = true;
                grvItemOut.DataSource = Ds;
                grvItemOut.DataBind();
            }
            else
            {
                Ds.Clear();
                grvItemOut.DataBind();
                
                troutdetail.Visible = false;
            }
        }
        protected void GetDataSold()
        {
            string strqry = " select ROW_NUMBER() OVER( ORDER BY stockout_OAC,Product_Name,Make_Name,ProdType_Name,ProdModel_Name,ProductDetail_CapDate,StockOut_Remarks ) AS [Sr. No.], " +
                            " Product_Name as [Product Name],Make_Name as Make,ProdType_Name as Type,ProdModel_Name as Model," +
                            " ProductDetail_AssetCode as [Asset Code],ProductDetail_SerialNo as [Serial No.],ProductDetail_Config as [Configuration],isnull(stockout_OAC,'Available') as OAC, " +
                            " StockOut_EmpCode as [Employee Code],stockout_empname as [Employee Name],StockOut_CostCenter as [Cost Center],tblCostCenter.Description as [Cost Center Name], " +
                            " convert(varchar(50),StockOut_IssueDate,106) as [Issued On],StockOut_IssueType as [Issue Type],StockOut_Remarks as Remarks, " +
                            " convert(varchar(50),[StockIn_InDate],106) as [In Date],StockIn_AssetType as [Asset Type], " +
                            " [StockIn_ChallanNo] as [Challan No.],convert(varchar(50),ProductDetail_CapDate,106) as [Capitilized On], " +
                            " convert(varchar(50),ProductDetail_ExpiryDate,106) as [Expired On],DATEDIFF (yy, ProductDetail_CapDate, GETDATE()) as Age, " +
                            " DATEDIFF (dd, ProductDetail_CapDate, GETDATE()) as days,ProductDetail_Id,[StockIn_id],product_id,stockout_id from dbo.Inv_ProductDetail_Master INNER JOIN dbo.Inv_Product_Master " +
                            " ON Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                            " inner join [dbo].[Inv_StockIn] on [Inv_ProductDetail_Master].[ProductDetail_StockIn_Id]=[Inv_StockIn].[StockIn_Id] " +
                            " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
                            " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
                            " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
                            " left outer join inv_stockout on inv_stockout.stockout_proddetail_id=Inv_ProductDetail_Master.Productdetail_id " +
                            " and stockout_id not in (select stockreturn_stockout_id from inv_stockreturn)" +
                              " left join tblCostCenter on inv_stockout.StockOut_CostCenter=tblCostCenter.CostCenter " +
                            " where  stockout_OAC in('Sold','Scrapped','Return to Vendor') and product_id='" + Request.QueryString["ProductId"].ToString() + "'";


           

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                
                troutdetail.Visible = true;
                grvItemOut.DataSource = Ds;
                grvItemOut.DataBind();
            }
            else
            {
                Ds.Clear();
                grvItemOut.DataBind();
                
                troutdetail.Visible = false;
            }
        }
        protected void GetDataItemAvailable()
        {
            

            string strqry = " select ROW_NUMBER() OVER( ORDER BY ProductDetail_Make_id) AS 'Sr.No.', "+
                            " Product_name as [Product Name],Make_Name as Make,ProdType_Name as Type, " +
                            " ProdModel_Name as Model, ProductDetail_AssetCode as [Asset Code],ProductDetail_SerialNo as [Serial No.] from Inv_ProductDetail_Master " +
                            " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                            " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
                            " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
                            " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
                            " where ProductDetail_Product_Id=" + Request.QueryString["ProductId"].ToString() + " and ProductDetail_Id not in (select isnull(StockOut_ProdDetail_Id,0) " +
                            "  from Inv_StockOut " +
                            " left outer join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                            " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                            " where Product_Id=" + Request.QueryString["ProductId"].ToString() + " and Inv_StockOut.StockOut_Id not in (select StockReturn_StockOut_Id from Inv_StockReturn))";

            //Response.Write(strqry);

            

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                travailable.Visible = true;
                grvAvailable.DataSource = Ds;
                grvAvailable.DataBind();
            }
            else
            {
                Ds.Clear();
                grvAvailable.DataBind();
                
                travailable.Visible = false;
            }
        }
        protected void grvItemOut_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string prodid = (string)e.Row.Cells[25].Text.ToString();
                string soutid = (string)e.Row.Cells[26].Text.ToString();
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
                            e.Row.Cells[4].Text = Dsmodel.Tables[0].Rows[0][0].ToString();
                        }

                    }
                }

                //string type = (string)e.Row.Cells[12].Text.ToString();


                //if (type == "Temporary")
                //{
                //    e.Row.BackColor = Color.Orange;
                //}






            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                
                string pid = (string)e.Row.Cells[8].Text.ToString();
                string strqry = "select Product_Serial_Available from Inv_Product_Master where Product_Id=" + pid + "";
               
                SqlConnection Con = new SqlConnection();
                Con.ConnectionString = strCon;
                SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
                DataSet Ds = new DataSet();
                Da.Fill(Ds);

                if (Ds.Tables[0].Rows[0][0].ToString()=="YES")
                {
                    e.Row.Cells[3].Text = "";
                }




            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            GetData();


        }

        
        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }


        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    GetDataItemIn();
        //    GetDataItemOut();
        //    GetDataItemReturn();
        //}
        protected void btnclose_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Productwisereport.aspx");
        }
    }
}