using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;

namespace TileMenu
{
    public partial class GatePassPrint : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["GPId"] != null)
                {
                    GetData();
                    GetDetail();
                }
            }
        }

        protected void GetData()
        {
            string strqry = " select GatePass_Id, convert(varchar(50),GatePass_Date,106) as IssueDate ,GatePass_Returnable,GatePass_IssuedTo,GatePass_IssuedCompany, " +
                            " GatePass_IssuedAddress,GatePass_IssuedReason,GatePass_IssuedContact from tbl_GatePass where GatePass_Id=" + Request.QueryString["GPId"].ToString() + " ";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            lblGPtype.Text = Ds.Tables[0].Rows[0]["GatePass_Returnable"].ToString();
            lblGPNumber.Text = Ds.Tables[0].Rows[0]["GatePass_Id"].ToString();
            lbldate.Text = Ds.Tables[0].Rows[0]["IssueDate"].ToString();
            lblissuedto.Text = Ds.Tables[0].Rows[0]["GatePass_IssuedTo"].ToString();
            lblcompany.Text = Ds.Tables[0].Rows[0]["GatePass_IssuedCompany"].ToString();
            lbladdress.Text = Ds.Tables[0].Rows[0]["GatePass_IssuedAddress"].ToString();
            lblreason.Text = Ds.Tables[0].Rows[0]["GatePass_IssuedReason"].ToString();
            lblcontact.Text = Ds.Tables[0].Rows[0]["GatePass_IssuedContact"].ToString();
        }
        protected void GetDetail()
        {
            string strqry = "";
            string strqrysoutid = "select GatePass_Stockout_id,GatePass_Detail_Id from tbl_GatePass_Detail" +
                               " where GatePass_Detail_GatePass_Id ='" + Request.QueryString["GPId"].ToString() + "'";

            SqlConnection Condetailsoutid = new SqlConnection();
            Condetailsoutid.ConnectionString = strCon;
            SqlDataAdapter Dadetailsoutid = new SqlDataAdapter(strqrysoutid, Condetailsoutid);
            DataSet Dsdetailsoutid = new DataSet();
            Dadetailsoutid.Fill(Dsdetailsoutid);


            for(int i=0;i< Dsdetailsoutid.Tables[0].Rows.Count;i++)
            {
                if (Dsdetailsoutid.Tables[0].Rows[i]["GatePass_Stockout_id"].ToString() == "0")
                {
                    if (i == 0)
                    {
                        strqry += " select GatePass_Detail_Qty as 'QTY.',GatePass_Detail_Item as PARTICULARS,GatePass_Detail_Type AS 'DETAIL',GatePass_Detail_Remarks AS REMARKS from   tbl_GatePass_Detail where GatePass_Detail_Id=" + Dsdetailsoutid.Tables[0].Rows[i]["GatePass_Detail_Id"].ToString() + " ";
                    }
                    else
                    {
                        strqry += " Union select GatePass_Detail_Qty as 'QTY.',GatePass_Detail_Item as PARTICULARS,GatePass_Detail_Type AS 'DETAIL',GatePass_Detail_Remarks AS REMARKS from   tbl_GatePass_Detail where GatePass_Detail_Id=" + Dsdetailsoutid.Tables[0].Rows[i]["GatePass_Detail_Id"].ToString() + " ";

                    }
                }
                else
                {
                    if (i == 0)
                    {
                        strqry += " select GatePass_Detail_Qty as 'QTY.'," +
                " Product_Name+'_'+Make_Name+'_'+ProdType_Name+'_'+ProdModel_Name as PARTICULARS,GatePass_Detail_Type AS 'DETAIL'," +
                " StockOut_Remarks AS REMARKS from tbl_GatePass_Detail " +
                " left join inv_stockout on tbl_GatePass_Detail.GatePass_Stockout_id=inv_stockout.Stockout_id" +
                " left join inv_productdetail_master on inv_stockout.StockOut_ProdDetail_Id=inv_productdetail_master.ProductDetail_Id " +
                            " left join Inv_Product_Master on inv_productdetail_master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id " +
                            " left join [dbo].[Inv_Make_Master] on inv_productdetail_master.ProductDetail_Make_Id =[Inv_Make_Master].Make_Id " +
                            " left join [dbo].[Inv_ProdType] on inv_productdetail_master.ProductDetail_ProdType_Id =[Inv_ProdType].ProdType_Id " +
                            " left join [dbo].[Inv_ProdModel] on inv_productdetail_master.ProductDetail_ProdModel_Id =[Inv_ProdModel].ProdModel_id " +
                " where GatePass_Stockout_id=" + Dsdetailsoutid.Tables[0].Rows[i]["GatePass_Stockout_id"].ToString() + " ";
                    }
                    else
                    {

                        strqry += " Union select GatePass_Detail_Qty as 'QTY.'," +
                " Product_Name+'_'+Make_Name+'_'+ProdType_Name+'_'+ProdModel_Name as PARTICULARS,GatePass_Detail_Type AS 'DETAIL'," +
                " StockOut_Remarks AS REMARKS from tbl_GatePass_Detail " +
                " left join inv_stockout on tbl_GatePass_Detail.GatePass_Stockout_id=inv_stockout.Stockout_id" +
                " left join inv_productdetail_master on inv_stockout.StockOut_ProdDetail_Id=inv_productdetail_master.ProductDetail_Id " +
                            " left join Inv_Product_Master on inv_productdetail_master.ProductDetail_Product_Id = Inv_Product_Master.Product_Id " +
                            " left join [dbo].[Inv_Make_Master] on inv_productdetail_master.ProductDetail_Make_Id =[Inv_Make_Master].Make_Id " +
                            " left join [dbo].[Inv_ProdType] on inv_productdetail_master.ProductDetail_ProdType_Id =[Inv_ProdType].ProdType_Id " +
                            " left join [dbo].[Inv_ProdModel] on inv_productdetail_master.ProductDetail_ProdModel_Id =[Inv_ProdModel].ProdModel_id " +
                " where GatePass_Stockout_id=" + Dsdetailsoutid.Tables[0].Rows[i]["GatePass_Stockout_id"].ToString() + " ";
                    }
                }
               
            }
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            GridView1.DataSource = Ds;
            GridView1.DataBind();
            //string strqry = " select ROW_NUMBER() OVER( ORDER BY tbl_GatePass_Detail.GatePass_Detail_Id ) AS 'S.NO.',GatePass_Detail_Qty as 'QTY.',GatePass_Detail_Item as PARTICULARS,GatePass_Detail_Type AS 'DETAIL',GatePass_Detail_Remarks AS REMARKS from   tbl_GatePass_Detail where GatePass_Detail_GatePass_Id=" + Request.QueryString["GPId"].ToString() + " ";




        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)

            {

                (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();

            }
        }
    }
}