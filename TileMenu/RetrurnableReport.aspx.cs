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
    public partial class RetrurnableReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                GetData();
                GetDataNI();
               


            }


        }

        protected void GridViewNI_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string scriptstring = "";


            int id = Convert.ToInt32(GridViewNI.DataKeys[e.RowIndex].Value.ToString());


            GridViewRow row = (GridViewRow)GridViewNI.Rows[e.RowIndex];


            string strqry = "update [tbl_GatePass_Detail]  set GatePass_Detail_ReturnStatus='YES' ,GatePass_Detail_ActualRtnDate=getdate() where GatePass_Detail_Id='" + id + "'";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);


            scriptstring = "alert('Successfully Returned');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            GetDataNI();

        }


        protected void GetDataNI()
        {

            string strqry = "SELECT  [GatePass_Id] as GatePassNo,convert(varchar(50),[GatePass_Date],106) as IssueDate,[GatePass_IssuedTo]," +
                            " [GatePass_IssuedCompany],[GatePass_Returnable] " +
                            " ,[GatePass_Detail_Item],GatePass_Detail_Remarks,convert(varchar(50),[GatePass_Detail_ExpRtnDate],106) as EXPReturnDate," +
                            " GatePass_Detail_Id  as id FROM [dbo].[tbl_GatePass] " +
                            " inner join [dbo].[tbl_GatePass_Detail] on  [tbl_GatePass].GatePass_Id=[tbl_GatePass_Detail].GatePass_Detail_GatePass_Id " +
                            " where [GatePass_Returnable]='Returnable' and [GatePass_Stockout_id]=0 and GatePass_Detail_ReturnStatus='NO'";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridViewNI.DataSource = Ds;
                GridViewNI.DataBind();
            }
            else
            {
                Ds.Clear();
                GridViewNI.DataBind();
            }
        }



        protected void GetData()
        {

            string strqry = "select StockOut_EmpCode as EmpCode,StockOut_EmpName,convert(varchar(50),StockOut_IssueDate,106) as IssueDate," +
                            " StockOut_IssueType as IssueType," +
                            "convert(varchar(50),StockOut_ERDate,106) as ExpRtnDate,DATEDIFF(dd,StockOut_ERDate,getdate()) as Overdue,stockout_Remarks as Remarks " +
                            ",Product_Name,[ProductDetail_SerialNo] as SerialNo,ProductDetail_AssetCode as AssetCode" +
                            " from  [dbo].[Inv_StockOut] inner join Inv_ProductDetail_Master " +
                            " on Inv_ProductDetail_Master.ProductDetail_Id=[Inv_StockOut] .StockOut_ProdDetail_Id " +
                            " inner join Inv_Product_Master on Inv_Product_Master.Product_Id=[Inv_ProductDetail_Master].ProductDetail_Product_Id "+  
                            " where StockOut_Id not in (select [StockReturn_StockOut_Id] from Inv_StockReturn ) and  StockOut_ERDate " +
                            " not in('1900-01-01 00:00:00.000')and StockOut_OAC='Employee'and StockOut_ProdDetail_Id<>0 order by StockOut_ERDate";

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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string overdues = (string)e.Row.Cells[5].Text.ToString();

                Int32 overdue = Convert.ToInt32(overdues);

                if (overdue > 0)
                {
                    e.Row.Font.Bold = true;
                    e.Row.ForeColor = Color.White;

                    e.Row.BackColor = Color.Red;
                }




            }

        }

    }
}