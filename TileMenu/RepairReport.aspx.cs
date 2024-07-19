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
    public partial class RepairReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }

        protected void GetData()
        {

            string strqry = " select Product_Name,ProductDetail_AssetCode as AssetCode," +
                            " ProductDetail_SerialNo as SerialNo," +
                            " convert(varchar(50),StockOut_IssueDate,106) as IssueDate,StockOut_Remarks as Remarks,Stockout_GatePassNo as GatePass," +
                            " convert(varchar(50),StockReturn_ReturnDate,106) as ReturnDate,stockReturn_BillNo as BillNo,stockreturn_remarks as RepaireRemarks,ProductDetail_OldSrNo as [Old SerialNo],StockReturn_cost as RepaireCost " +
                            " ,CAST(ROUND((StockReturn_cost *18)/100, 2) AS decimal(10,2))  as TaxAmount, CAST(ROUND(((StockReturn_cost *18)/100)+StockReturn_cost, 2) AS decimal(10,2)) as TotalCost from [dbo].[Inv_StockOut] " +
                            " left join [Inv_StockReturn] on [Inv_StockOut].StockOut_Id=[Inv_StockReturn].StockReturn_StockOut_Id " +
                            " left join [dbo].[Inv_ProductDetail_Master] on [Inv_StockOut].StockOut_ProdDetail_Id=[Inv_ProductDetail_Master].ProductDetail_Id " +
                            " inner join [dbo].[Inv_Product_Master] on [Inv_Product_Master].Product_Id=[Inv_ProductDetail_Master].ProductDetail_Product_Id " +
                            " where StockOut_OAC='Repair'";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
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


    }
}