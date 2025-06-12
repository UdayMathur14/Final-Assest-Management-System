using System;
using System.Data;
using System.Data.SqlClient;

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
    -- Out transaction
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
        StockOut_CreatedBy AS CreatedBy
    FROM  
        [dbo].[Inv_StockOut]
    LEFT OUTER JOIN 
        Inv_ProductDetail_Master 
    ON 
        Inv_StockOut.StockOut_ProdDetail_Id = Inv_ProductDetail_Master.ProductDetail_Id
    INNER JOIN 
        Inv_Product_Master 
    ON  
        Inv_ProductDetail_Master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id

    UNION 

    -- Return transaction
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
        StockOut_CreatedBy AS CreatedBy
    FROM 
        [dbo].[Inv_StockReturn]
    INNER JOIN 
        Inv_StockOut 
    ON 
        Inv_StockReturn.StockReturn_StockOut_Id = Inv_StockOut.StockOut_Id
    LEFT OUTER JOIN 
        Inv_ProductDetail_Master 
    ON 
        Inv_StockOut.StockOut_ProdDetail_Id = Inv_ProductDetail_Master.ProductDetail_Id
    INNER JOIN 
        Inv_Product_Master 
    ON 
        Inv_ProductDetail_Master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id

    UNION

    -- In transaction
    SELECT 
        'In' AS TransType,
        StockIn_Id AS id,
        Product_Id AS [ProductId],
        Product_Name,
        '' AS StockOut_EmpCode,
        '' AS StockOut_EmpName,
        '' AS StockOut_CostCenter,
        StockIn_InDate AS tdate,
        '' AS StockOut_IssueType,
        '' AS StockOut_OAC,
        ProductDetail_SerialNo,
        ProductDetail_AssetCode,
        StockIn_CreatedBy AS CreatedBy
    FROM 
        [dbo].[Inv_StockIn]
    INNER JOIN 
        Inv_ProductDetail_Master 
    ON 
        Inv_StockIn.StockIn_Id = Inv_ProductDetail_Master.ProductDetail_StockIn_Id
    INNER JOIN 
        Inv_Product_Master 
    ON 
        Inv_ProductDetail_Master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id
)
SELECT 
    TransType AS [Transaction Type],
    Product_Name AS [Product Name],
    StockOut_EmpCode AS [Employee Code],
    StockOut_EmpName AS [Employee Name],
    CONVERT(VARCHAR(50), tdate, 106) AS [Transaction Date],
    StockOut_IssueType AS [Issue Type],
    StockOut_OAC AS [OAC],
    ProductDetail_SerialNo AS [Serial Number],
    ProductDetail_AssetCode AS [Asset Code],
    CreatedBy AS [Assigned By]
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