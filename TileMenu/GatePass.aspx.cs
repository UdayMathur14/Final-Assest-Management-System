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
    public partial class GatePass : System.Web.UI.Page
    {
        DataTable dtSerial = null;
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtIssuedate.Text = System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
                NextGetGetPassId();
            }
        }
        protected void txtqty_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtqty.Text))
            {
                gvSerial.DataSource = null;
                gvSerial.DataBind();
                return;
            }

            int qty = 0;
            if (!Int32.TryParse(txtqty.Text, out qty))
            {
                gvSerial.DataSource = null;
                gvSerial.DataBind();
                return;
            }
            else
            {
                gvSerial.DataSource = CreateTable(qty);
                gvSerial.DataBind();
            }

        }
        private DataTable CreateTable(int qty)
        {
            dtSerial = new DataTable();
            dtSerial.Columns.Add(new DataColumn("SlNo", typeof(System.String)));
            dtSerial.Columns.Add(new DataColumn("ddlsoutid", typeof(System.String)));

            for (int i = 0; i < qty; i++)
            {
                DataRow dr = dtSerial.NewRow();
                dr[0] = (i + 1).ToString();

                dtSerial.Rows.Add(dr);
            }
            return dtSerial;
        }
        protected void gvSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)

            {

                //Find the DropDownList in the Row.

                DropDownList ddlsoutid = (e.Row.FindControl("ddlsoutid") as DropDownList);
                string strcondition = "";

                string strqry = "select StockOut_Id,CONCAT('[ Stockout ID :- ', StockOut_Id, ' ] -> ( Product :- ', Product_Name, ' ) -> SerialNo :- ', ProductDetail_SerialNo) as productname from Inv_StockOut " +
                                " left join Inv_ProductDetail_Master " +
                                " on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                                " left join Inv_Product_Master " +
                                " on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id" +
                                " where stockout_id not in (select StockReturn_StockOut_Id from Inv_StockReturn)";
                //and StockOut_IssueDate>DATEADD(day, -30, getdate())
                if (IssuedTo.SelectedIndex == 0)
                {
                    strcondition += " and stockout_OAC in('Employee','Site','Internal')";
                }
                else
                {
                    strcondition += " and stockout_OAC in ('SOLD','Repair','Scrapped','Return to Vendor')";
                }

                strcondition += " order by StockOut_Id desc";
                SqlConnection Con = new SqlConnection();
                Con.ConnectionString = strCon;
                SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
                DataSet Ds = new DataSet();
                Da.Fill(Ds);

                ddlsoutid.DataSource = Ds;

                ddlsoutid.DataTextField = "productname";

                ddlsoutid.DataValueField = "StockOut_Id";

                ddlsoutid.DataBind();





            }
        }

        protected void IssuedTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtqty.Text))
            {
                gvSerial.DataSource = null;
                gvSerial.DataBind();
                return;
            }

            int qty = 0;
            if (!Int32.TryParse(txtqty.Text, out qty))
            {
                gvSerial.DataSource = null;
                gvSerial.DataBind();
                return;
            }
            else
            {
                gvSerial.DataSource = CreateTable(qty);
                gvSerial.DataBind();
            }
        }
        protected void Clear()
        {
            TxtGatePassNo.Text = "";
            txtIssuedate.Text = "";
            txtIssuedTo.Text = "";
            txtCompanyName.Text = "";
            txtAddress.Text = "";
            txtreason.Text = "";

        }
        protected void NextGetGetPassId()
        {
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            string strqrygatepassid = "select isnull(max(GatePass_Id),0)+1 from tbl_GatePass";
            SqlDataAdapter Daid = new SqlDataAdapter(strqrygatepassid, Con);
            DataSet Dsid = new DataSet();
            Daid.Fill(Dsid);
            TxtGatePassNo.Text = Dsid.Tables[0].Rows[0][0].ToString();
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string scriptstring = "";
            string strqry = "insert into tbl_GatePass(Gatepass_id," +
                        " GatePass_Date, GatePass_Returnable, GatePass_IssuedTo, GatePass_IssuedCompany," +
                        " GatePass_IssuedAddress,GatePass_IssuedReason,GatePass_IssuedContact,GatePass_CreatedBy," +
                        " GatePass_CreatedDate,Gatepass_Status,GatePass_ModifiedBy,GatePass_ModifiedDate) " +
                        " values('" + TxtGatePassNo.Text + "'," +
                        " '" + txtIssuedate.Text + "'," +
                        " '" + IssueType.SelectedValue + "','" + txtIssuedTo.Text + "'," +
                        " '" + txtCompanyName.Text + "','" + txtAddress.Text + "'," +
                        " '" + txtreason.Text + "','" + txtmobile.Text + "'," +
                        " '" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "','Submitted'," +
                        " '" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "')";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            for (int i = 0; i < Convert.ToInt32(txtqty.Text); i++)
            {
                var stockout = ((DropDownList)gvSerial.Rows[i].Cells[1].FindControl("ddlsoutid")).SelectedValue;

                string strqryitemdetail = "select Product_Name,ProductDetail_SerialNo from Inv_StockOut " +
                                " left join Inv_ProductDetail_Master " +
                                " on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                                " left join Inv_Product_Master " +
                                " on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id" +
                                " where stockout_id ='" + stockout + "'";

                SqlConnection Condetailitemdetail = new SqlConnection();
                Condetailitemdetail.ConnectionString = strCon;
                SqlDataAdapter Dadetailitemdetail = new SqlDataAdapter(strqryitemdetail, Condetailitemdetail);
                DataSet Dsdetailitemdetail = new DataSet();
                Dadetailitemdetail.Fill(Dsdetailitemdetail);

                string strdetail = "insert into tbl_GatePass_Detail(GatePass_Detail_GatePass_Id," +
                          " GatePass_Detail_Qty, GatePass_Detail_Type, GatePass_Detail_Item, GatePass_Detail_Remarks," +
                          " GatePass_Detail_ExpRtnDate,GatePass_Detail_DvtExpRtnDate," +
                          " GatePass_Detail_ActualRtnDate,GatePass_Detail_ReturnStatus,GatePass_Stockout_id) " +
                          " values('" + TxtGatePassNo.Text + "'," +
                          " '1'," +
                          " '" + Dsdetailitemdetail.Tables[0].Rows[0]["ProductDetail_SerialNo"].ToString() + "'," +
                          " '" + Dsdetailitemdetail.Tables[0].Rows[0]["Product_Name"].ToString() + "'," +
                          " '',''," +
                          " '','','NO','" + stockout + "')";

                SqlConnection Condetail = new SqlConnection();
                Condetail.ConnectionString = strCon;
                SqlDataAdapter Dadetail = new SqlDataAdapter(strdetail, Condetail);
                DataSet Dsdetail = new DataSet();
                Dadetail.Fill(Dsdetail);


            }
            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            Clear();
        }
    }
}