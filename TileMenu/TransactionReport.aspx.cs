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
    public partial class TransactionReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetData();
                

            }
        }

        protected void GetData()
        {
            string StrCondition = "";

            string strqry = " with  StockLedger as(select 'Out' as TransType, stockout_id as id,product_Id as [ProductId],"+
                            " Product_Name,StockOut_EmpCode,StockOut_EmpName,StockOut_CostCenter," +
                            " StockOut_IssueDate as tdate,StockOut_IssueType,StockOut_OAC,ProductDetail_SerialNo,ProductDetail_AssetCode from  [dbo].[Inv_StockOut] " +
                            " left outer join Inv_ProductDetail_Master on [Inv_StockOut].StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id " +
                            " inner join Inv_Product_Master on  [Inv_ProductDetail_Master].ProductDetail_Product_Id=Inv_Product_Master.Product_Id" +
                            " union " +
                            " select 'Return' as TransType,StockReturn_StockOut_Id as id,Product_Id as [ProductId],Product_Name,"+
                            " StockOut_EmpCode,StockOut_EmpName,StockOut_CostCenter,StockReturn_ReturnDate as tdate,'','',ProductDetail_SerialNo, "+
                            " ProductDetail_AssetCode from [dbo].[Inv_StockReturn] inner join [Inv_StockOut] " +
                            " on [Inv_StockReturn].StockReturn_StockOut_Id=[Inv_StockOut].StockOut_Id "+
                            " left outer join Inv_ProductDetail_Master on [Inv_StockOut].StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id "+
                            " inner join Inv_Product_Master on [Inv_ProductDetail_Master].ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                            ") " +
    " select Transtype as [Transaction Type],Product_name as [Product Name],StockOut_EmpCode as [Employee Code],StockOut_EmpName as [Employee Name]," +
    " convert(varchar(50),tdate,106) as [Transcation Date],StockOut_IssueType as [Issue Type],StockOut_OAC as [OAC],"+
    " ProductDetail_SerialNo as [Serial Number],ProductDetail_AssetCode as [Asset Code] from StockLedger "+
    " where 1=1";

           
            if (txtserial.Text != "")
            {
                StrCondition += " and ProductDetail_SerialNo like '%" + txtserial.Text + "%' ";
            }
            if (txtEmpName.Text != "")
            {
                StrCondition += " and StockOut_EmpName like '%" + txtEmpName.Text + "%' ";
            }

            if (txtFdate.Text != "" && txtTdate.Text != "")
            {
                StrCondition += " and tdate between '" + txtFdate.Text + "' and '" + txtTdate.Text + "'";
            }

            StrCondition += " order by ProductDetail_SerialNo,tdate";


            //Response.Write(strqry+StrCondition);
            //Response.End();
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + StrCondition, Con);
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
    }
}