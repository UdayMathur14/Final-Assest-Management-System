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
using System.Net.Mail;
using System.Drawing;

namespace TileMenu
{
    public partial class GatePassApproval : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetData()
        {
            string strCondition = "";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;

            pnlData.Visible = true;
            string strqry = " select distinct([GatePass_Id]),convert(varchar(50),GatePass_Date,106) as IssueDate,[GatePass_Returnable] " +
                           " ,[GatePass_IssuedTo],[GatePass_IssuedCompany],[GatePass_IssuedAddress] " +
                           " ,[GatePass_IssuedReason],[GatePass_IssuedContact],[Gatepass_status] from tbl_GatePass " +
                           "  inner join [dbo].[tbl_GatePass_Detail] on tbl_GatePass.GatePass_Id=[tbl_GatePass_Detail].GatePass_Detail_GatePass_Id " +
                           " left join [dbo].[Inv_StockReturn] on [tbl_GatePass_detail].GatePass_Stockout_id=[Inv_StockReturn].[StockReturn_StockOut_Id] " +
                           " where 1=1";

            if (TxtGatePassNo.Text != "")
            {
                strqry += " and GatePass_Id='" + TxtGatePassNo.Text + "'";
            }
            if (txtIssuedateFrom.Text != "" && txtIssuedateTo.Text != "")
            {
                strqry += " and GatePass_Date >= '" + txtIssuedateFrom.Text + "' and GatePass_Date <= '" + Convert.ToDateTime(txtIssuedateTo.Text).AddDays(1) + "'";
            }
            if (txtIssuedTo.Text != "")
            {
                strqry += " and GatePass_IssuedTo like '%" + txtIssuedTo.Text + "%'";
            }
            if (txtCompanyName.Text != "")
            {
                strqry += " and GatePass_IssuedCompany like '%" + txtCompanyName.Text + "%'";
            }
            if (IssueType.SelectedValue != "-1")
            {
                strqry += " and GatePass_Returnable = '" + IssueType.SelectedValue + "'";
            }
            if (txtproduct.Text != "")
            {
                strqry += " and GatePass_Detail_Item like '%" + txtproduct.Text + "%'";
            }
            if (txtdetail.Text != "")
            {
                strqry += " and GatePass_Detail_Type like '%" + txtdetail.Text + "%'";
            }
            strCondition = " order by GatePass_Id desc ";
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strCondition, Con);
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
                GridView1.DataSource = Ds;
                GridView1.DataBind();

            }
        }
        protected DataSet GetDetail(string gatepassid)
        {
            //string strqry = " select ROW_NUMBER() OVER( ORDER BY tbl_GatePass_Detail.GatePass_Detail_Id ) AS 'S.NO.',GatePass_Detail_Qty as 'QTY.',GatePass_Detail_Item as PARTICULARS,GatePass_Detail_Type AS 'DETAIL',GatePass_Detail_Remarks AS REMARKS from   tbl_GatePass_Detail where GatePass_Detail_GatePass_Id='" + gatepassid + "'";
            string strqry = " select GatePass_Detail_Id as id,GatePass_Detail_Item,GatePass_Detail_Type,convert(varchar(50),[StockReturn_ReturnDate],106) as srd,GatePass_Detail_ReturnStatus,case when convert(varchar(50),GatePass_Detail_ActualRtnDate,106)='01 Jan 1900' then '' else convert(varchar(50),GatePass_Detail_ActualRtnDate,106) end as RtnDate from   tbl_GatePass_Detail " +
                            "  left join [dbo].[Inv_StockReturn] on [tbl_GatePass_detail].GatePass_Stockout_id=[Inv_StockReturn].[StockReturn_StockOut_Id] where GatePass_Detail_GatePass_Id='" + gatepassid + "'";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            return Ds;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
        protected void TxtGatePassNo_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void txtIssuedTo_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void IssueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void txtproduct_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void txtdetail_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            GetData();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //if (UserADID == "Rajnish.Singh" || UserADID == "Santosh.Roy")
                //{
                // btnapprove.Visible=true;
                //}


                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    e.Row.Cells[9].Text = "";
                    


                }
                else
                {
                    Label status = (Label)e.Row.FindControl("lbl_Status");

                    if (status.Text == "Submitted")
                    {
                        e.Row.Cells[9].Text = "";
                       
                    }


                    if (status.Text == "Cancelled")
                    {
                        e.Row.BackColor = Color.Red;
                        e.Row.Cells[9].Text = "";
                       
                        e.Row.Cells[0].Text = "";
                    }
                }



                string gatepassid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

                GridView gvdetail = e.Row.FindControl("gvdetail") as GridView;

                gvdetail.DataSource = GetDetail(gatepassid);

                gvdetail.DataBind();




            }
        }
        protected void gvdetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string scriptstring = "";
            GridView gvdetail = sender as GridView;

            int id = Convert.ToInt32(gvdetail.DataKeys[e.RowIndex].Value.ToString());


            //GridViewRow row = (GridViewRow)gvdetail.Rows[e.RowIndex];


            string strqry = "update [tbl_GatePass_Detail]  set GatePass_Detail_ReturnStatus='YES' ,GatePass_Detail_ActualRtnDate=getdate() where GatePass_Detail_Id='" + id + "'";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);


            scriptstring = "alert('Successfully Returned');";
            GetData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);


        }


        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.   
            GridView1.EditIndex = e.NewEditIndex;
            GetData();
        }
        protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {


            GridView1.EditIndex = -1;
            string gatepassid = GridView1.DataKeys[e.RowIndex].Value.ToString();
            DropDownList status = GridView1.Rows[e.RowIndex].FindControl("ddlstatus") as DropDownList;
            string strqry = "update tbl_GatePass set Gatepass_status='" + status.SelectedValue + "'," +
                            " GatePass_ModifiedBy='" + Session["login"].ToString() + "'," +
                            " GatePass_ModifiedDate='" + System.DateTime.Now.Date + "' where  GatePass_Id='" + gatepassid + "'";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            GetData();
        }
        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview   
            GridView1.EditIndex = -1;
            GetData();
        }



        protected void txtIssuedateFrom_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        protected void txtIssuedateTo_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
    }
}