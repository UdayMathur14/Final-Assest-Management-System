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

            string strqry = @"
WITH StockLedger AS (
    SELECT 
        'Out' AS TransType, 
        stockout_id AS id,
        product_Id AS [ProductId],
        Product_Name,
        StockOut_EmpCode,
        StockOut_EmpName,
        StockOut_CostCenter,
        StockOut_IssueDate AS tdate,
        StockOut_IssueType,
        StockOut_OAC,
        ProductDetail_SerialNo,
        ProductDetail_AssetCode,
        StockOut_CreatedBy -- Include StockOut_CreatedBy
    FROM  
        [dbo].[Inv_StockOut]
    LEFT OUTER JOIN 
        Inv_ProductDetail_Master 
    ON 
        [Inv_StockOut].StockOut_ProdDetail_Id = Inv_ProductDetail_Master.ProductDetail_Id
    INNER JOIN 
        Inv_Product_Master 
    ON  
        [Inv_ProductDetail_Master].ProductDetail_Product_Id = Inv_Product_Master.Product_Id
    UNION 
    SELECT 
        'Return' AS TransType,
        StockReturn_StockOut_Id AS id,
        Product_Id AS [ProductId],
        Product_Name,
        StockOut_EmpCode,
        StockOut_EmpName,
        StockOut_CostCenter,
        StockReturn_ReturnDate AS tdate,
        '' AS StockOut_IssueType,
        '' AS StockOut_OAC,
        ProductDetail_SerialNo,
        ProductDetail_AssetCode,
        StockOut_CreatedBy -- Include StockOut_CreatedBy
    FROM 
        [dbo].[Inv_StockReturn]
    INNER JOIN 
        [Inv_StockOut] 
    ON 
        [Inv_StockReturn].StockReturn_StockOut_Id = [Inv_StockOut].StockOut_Id
    LEFT OUTER JOIN 
        Inv_ProductDetail_Master 
    ON 
        [Inv_StockOut].StockOut_ProdDetail_Id = Inv_ProductDetail_Master.ProductDetail_Id
    INNER JOIN 
        Inv_Product_Master 
    ON 
        [Inv_ProductDetail_Master].ProductDetail_Product_Id = Inv_Product_Master.Product_Id
) 
SELECT 
    Transtype AS [Transaction Type],
    Product_name AS [Product Name],
    StockOut_EmpCode AS [Employee Code],
    StockOut_EmpName AS [Employee Name],
    CONVERT(VARCHAR(50), tdate, 106) AS [Transaction Date],
    StockOut_IssueType AS [Issue Type],
    StockOut_OAC AS [OAC],
    ProductDetail_SerialNo AS [Serial Number],
    ProductDetail_AssetCode AS [Asset Code],
    StockOut_CreatedBy AS [Assigned By] -- Alias it as Assigned By
FROM 
    StockLedger
WHERE 
    1 = 1";




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

            StrCondition += " order by tdate DESC, ProductDetail_SerialNo";


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