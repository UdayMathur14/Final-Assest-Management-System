using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

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
            using (SqlConnection Con = new SqlConnection(strCon))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT PM.Product_Name, PM.Product_Id, ");

                // 1. Total In Qty
                sb.Append(" (SELECT ISNULL(SUM(StockIn_Qty), 0) FROM Inv_StockIn WHERE StockIn_Product_Id = PM.Product_Id) AS TotalInQty, ");

                // 2. Total Out (Employee/Internal/Site) - Isme original logic 'sum - count' ka tha wahi rakha hai
                sb.Append(" ((SELECT ISNULL(SUM(SO.StockOut_Qty), 0) FROM Inv_StockOut SO ");
                sb.Append("   INNER JOIN Inv_ProductDetail_Master PDM ON SO.StockOut_ProdDetail_Id = PDM.ProductDetail_Id ");
                sb.Append("   WHERE PDM.ProductDetail_Product_Id = PM.Product_Id AND SO.stockout_OAC IN ('employee','Internal','Site','Subitem')) - ");
                sb.Append("  (SELECT ISNULL(COUNT(SR.StockReturn_id), 0) FROM Inv_StockReturn SR ");
                sb.Append("   WHERE SR.stockreturn_stockout_id IN (SELECT SO2.stockout_id FROM Inv_StockOut SO2 ");
                sb.Append("   INNER JOIN Inv_ProductDetail_Master PDM2 ON SO2.StockOut_ProdDetail_Id = PDM2.ProductDetail_Id ");
                sb.Append("   WHERE PDM2.ProductDetail_Product_Id = PM.Product_Id AND SO2.stockout_OAC IN ('employee','Internal','Site','Subitem')))) AS TotalOut, ");

                // 3. Unique Out (Distinct Employee Count)
                sb.Append(" (SELECT COUNT(DISTINCT SO3.StockOut_EmpCode) FROM Inv_StockOut SO3 ");
                sb.Append("  INNER JOIN Inv_ProductDetail_Master PDM3 ON SO3.StockOut_ProdDetail_Id = PDM3.ProductDetail_Id ");
                sb.Append("  WHERE PDM3.ProductDetail_Product_Id = PM.Product_Id ");
                sb.Append("  AND SO3.stockout_OAC IN ('employee', 'Internal', 'Site', 'Subitem') ");
                sb.Append("  AND SO3.stockout_id NOT IN (SELECT stockreturn_stockout_id FROM Inv_StockReturn)) AS uniqueout, ");

                // 4. Repair / StandBy / UserReserve - (SUM - COUNT logic as per your original)
                sb.Append(" ((SELECT ISNULL(SUM(SO4.StockOut_Qty), 0) FROM Inv_StockOut SO4 ");
                sb.Append("   INNER JOIN Inv_ProductDetail_Master PDM4 ON SO4.StockOut_ProdDetail_Id = PDM4.ProductDetail_Id ");
                sb.Append("   WHERE PDM4.ProductDetail_Product_Id = PM.Product_Id AND SO4.stockout_OAC IN ('Repair','Standby','Reserved for user')) - ");
                sb.Append("  (SELECT ISNULL(COUNT(SR3.StockReturn_id), 0) FROM Inv_StockReturn SR3 ");
                sb.Append("   WHERE SR3.stockreturn_stockout_id IN (SELECT SO4a.stockout_id FROM Inv_StockOut SO4a ");
                sb.Append("   INNER JOIN Inv_ProductDetail_Master PDM4a ON SO4a.StockOut_ProdDetail_Id = PDM4a.ProductDetail_Id ");
                sb.Append("   WHERE PDM4a.ProductDetail_Product_Id = PM.Product_Id AND SO4a.stockout_OAC IN ('Repair','Standby','Reserved for user')))) AS TotalOutRepair, ");

                // 5. To Be Scrap
                sb.Append(" ((SELECT ISNULL(SUM(SO5.StockOut_Qty), 0) FROM Inv_StockOut SO5 ");
                sb.Append("   INNER JOIN Inv_ProductDetail_Master PDM5 ON SO5.StockOut_ProdDetail_Id = PDM5.ProductDetail_Id ");
                sb.Append("   WHERE PDM5.ProductDetail_Product_Id = PM.Product_Id AND SO5.stockout_OAC IN ('To be scrap')) - ");
                sb.Append("  (SELECT ISNULL(COUNT(SR4.StockReturn_id), 0) FROM Inv_StockReturn SR4 ");
                sb.Append("   WHERE SR4.stockreturn_stockout_id IN (SELECT SO5a.stockout_id FROM Inv_StockOut SO5a ");
                sb.Append("   INNER JOIN Inv_ProductDetail_Master PDM5a ON SO5a.StockOut_ProdDetail_Id = PDM5a.ProductDetail_Id ");
                sb.Append("   WHERE PDM5a.ProductDetail_Product_Id = PM.Product_Id AND SO5a.stockout_OAC IN ('To be scrap')))) AS TotalOutScrap, ");

                // 6. Sold / Scrapped / VendorReturn
                sb.Append(" ((SELECT ISNULL(SUM(SO6.StockOut_Qty), 0) FROM Inv_StockOut SO6 ");
                sb.Append("   INNER JOIN Inv_ProductDetail_Master PDM6 ON SO6.StockOut_ProdDetail_Id = PDM6.ProductDetail_Id ");
                sb.Append("   WHERE PDM6.ProductDetail_Product_Id = PM.Product_Id AND SO6.stockout_OAC IN ('Sold','Scrapped','Return to Vendor')) - ");
                sb.Append("  (SELECT ISNULL(COUNT(SR5.StockReturn_id), 0) FROM Inv_StockReturn SR5 ");
                sb.Append("   WHERE SR5.stockreturn_stockout_id IN (SELECT SO6a.stockout_id FROM Inv_StockOut SO6a ");
                sb.Append("   INNER JOIN Inv_ProductDetail_Master PDM6a ON SO6a.StockOut_ProdDetail_Id = PDM6a.ProductDetail_Id ");
                sb.Append("   WHERE PDM6a.ProductDetail_Product_Id = PM.Product_Id AND SO6a.stockout_OAC IN ('Sold','Scrapped','Return to Vendor')))) AS TotalOutSold, ");

                // 7. Available
                sb.Append(" ((SELECT ISNULL(SUM(StockIn_Qty), 0) FROM Inv_StockIn WHERE StockIn_Product_Id = PM.Product_Id) - ");
                sb.Append("  (SELECT ISNULL(SUM(StockOut_Qty), 0) FROM Inv_StockOut SO7 INNER JOIN Inv_ProductDetail_Master PDM7 ON SO7.StockOut_ProdDetail_Id = PDM7.ProductDetail_Id WHERE PDM7.ProductDetail_Product_Id = PM.Product_Id) + ");
                sb.Append("  (SELECT ISNULL(COUNT(SR2.StockReturn_id), 0) FROM Inv_StockReturn SR2 WHERE SR2.stockreturn_stockout_id IN (SELECT SO8.stockout_id FROM Inv_StockOut SO8 INNER JOIN Inv_ProductDetail_Master PDM8 ON SO8.StockOut_ProdDetail_Id = PDM8.ProductDetail_Id WHERE PDM8.ProductDetail_Product_Id = PM.Product_Id))) AS Available ");

                sb.Append(" FROM Inv_Product_Master PM ");
                sb.Append(" WHERE PM.Product_ItemType = '" + typemaster.SelectedValue + "' ");

                if (ddlproduct.SelectedValue != "-1")
                {
                    sb.Append(" AND PM.Product_Id = '" + ddlproduct.SelectedValue + "' ");
                }

                sb.Append(" ORDER BY PM.Product_Name");

                SqlDataAdapter Da = new SqlDataAdapter(sb.ToString(), Con);
                Da.SelectCommand.CommandTimeout = 300; // 5 minutes timeout
                DataSet Ds = new DataSet();
                Da.Fill(Ds);

                GridView1.DataSource = Ds;
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