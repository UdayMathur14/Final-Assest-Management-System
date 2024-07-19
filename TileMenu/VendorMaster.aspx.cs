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

namespace TileMenu
{
    public partial class VendorMaster : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        string UserADID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UserADID = Session["login"].ToString();
            if (!IsPostBack)
            {
                GetNextId();
                FillGrid();


            }

        }

        protected void GetNextId()
        {

            string strqry = "select max(vendor_id)+1 vendor_id from Inv_vendor_master";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            lblid.Text = Ds.Tables[0].Rows[0]["Vendor_Id"].ToString();

        }


        protected void FillGrid()
        {
            string StrCondition = "";
            string strqry = "select " +
           "  [Vendor_Id] " +
           " ,[Vendor_Name] " +
           " ,[Vendor_SAPCode] " +
           ",[Vendor_CustomerId] " +
          ",[Vendor_BusinessPhone] " +
          ",[Vendor_TollFreeNo] " +
          //",[Vendor_Address] "+
          //",[Vendor_City] "+
          //",[Vendor_Zip] "+
          //",[Vendor_Region] "+
          ",[Vendor_SupportPage] " +
          //",[Vendor_Notes] "+
          "  ,[Vendor_Status] from Inv_vendor_master order by vendor_id";


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
                GridView1.DataSource = Ds;
                GridView1.DataBind();

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string vendorid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

                GridView gvSM = e.Row.FindControl("gvSM") as GridView;

                gvSM.DataSource = GetSM(vendorid);

                gvSM.DataBind();


                GridView gvAM = e.Row.FindControl("gvAM") as GridView;

                gvAM.DataSource = GetAM(vendorid);

                gvAM.DataBind();

            }


        }

        protected DataSet GetSM(string vendorid)
        {
            string StrCondition = "";
            string strqry = "select * from Inv_VendorSalesManager where VendorId='" + vendorid + "'";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            return Ds;
        }

        protected DataSet GetAM(string vendorid)
        {
            string StrCondition = "";
            string strqry = "select * from Inv_VendorAccountManager where VendorId='" + vendorid + "'";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            return Ds;
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string scriptstring = "";

            btnsubmit.Text = "Update";
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());


            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];


            string strqry = "select * from Inv_Vendor_Master where Vendor_Id='" + id + "'";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            lblid.Text = Ds.Tables[0].Rows[0]["Vendor_Id"].ToString();
            txtvendor.Text = Ds.Tables[0].Rows[0]["Vendor_Name"].ToString();
            txtvendorcode.Text = Ds.Tables[0].Rows[0]["Vendor_SAPCode"].ToString();
            txtCustomerId.Text = Ds.Tables[0].Rows[0]["Vendor_CustomerId"].ToString();
            txtbusinessphone.Text = Ds.Tables[0].Rows[0]["Vendor_BusinessPhone"].ToString();
            txttollfree.Text = Ds.Tables[0].Rows[0]["Vendor_TollFreeNo"].ToString();
            txtaddress.Text = Ds.Tables[0].Rows[0]["Vendor_Address"].ToString();
            txtcity.Text = Ds.Tables[0].Rows[0]["Vendor_City"].ToString();
            txtregion.Text = Ds.Tables[0].Rows[0]["Vendor_Region"].ToString();
            txtzip.Text = Ds.Tables[0].Rows[0]["Vendor_Zip"].ToString();
            txtsupportpage.Text = Ds.Tables[0].Rows[0]["Vendor_SupportPage"].ToString();
            txtnotes.Text = Ds.Tables[0].Rows[0]["Vendor_Notes"].ToString();
            ddltype.SelectedValue = Ds.Tables[0].Rows[0]["Vendor_Status"].ToString();

            gvSMnew.DataSource = GetSM(Ds.Tables[0].Rows[0]["Vendor_Id"].ToString());
            gvSMnew.DataBind();
            gvAMnew.DataSource = GetAM(Ds.Tables[0].Rows[0]["Vendor_Id"].ToString());
            gvAMnew.DataBind();
        }


        private bool checkDuplicate(string TableName, string Fieldname, string value)
        {
            if (value == "''")
            {
                return false;
            }
            bool result = false;
            string strqry = "select " + Fieldname + " from " + TableName + " where " + Fieldname + "=" + value + "";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                result = true;
            }
            return result;

        }
        protected void ClearSM()
        {

            txtsmname.Text = "";
            txtsmmobile.Text = "";
            txtsmemail.Text = "";
        }
        protected void ClearAM()
        {

            txtamname.Text = "";
            txtammobile.Text = "";
            txtamemail.Text = "";
        }
        protected void Clear()
        {
            lblid.Text = "";
            txtvendor.Text = "";
            txtvendorcode.Text = "";
            txtCustomerId.Text = "";
            txtbusinessphone.Text = "";
            txttollfree.Text = "";
            txtaddress.Text = "";
            txtcity.Text = "";
            txtregion.Text = "";
            txtzip.Text = "";
            txtsupportpage.Text = "";
            txtnotes.Text = "";
            ddltype.SelectedValue = "ACTIVE";
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
            btnsubmit.Text = "Save";
            lblid.Text = "";
        }

        protected void btnaddSM_Click(object sender, EventArgs e)
        {

            string strqry = "insert into Inv_VendorSalesManager(VendorId,SMName,SMMobile,SMEmail) values('" + lblid.Text + "','" + txtsmname.Text + "','" + txtsmmobile.Text + "','" + txtsmemail.Text + "')";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            ClearSM();
            gvSMnew.DataSource = GetSM(lblid.Text);
            gvSMnew.DataBind();

        }
        protected void btnaddAM_Click(object sender, EventArgs e)
        {

            string strqry = "insert into Inv_VendorAccountManager(VendorId,AMName,AMMobile,AMEmail) values('" + lblid.Text + "','" + txtamname.Text + "','" + txtammobile.Text + "','" + txtamemail.Text + "')";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            ClearAM();
            gvAMnew.DataSource = GetAM(lblid.Text);
            gvAMnew.DataBind();
        }


        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string scriptstring;
            string strqry = "";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            if (btnsubmit.Text == "Save")
            {
                if (!checkDuplicate("Inv_Vendor_Master", "Vendor_SAPCode", "'" + txtvendorcode.Text + "'"))
                {


                    strqry = "INSERT INTO Inv_Vendor_Master (Vendor_Name" +
                ", [Vendor_CustomerId]" +
                ", [Vendor_BusinessPhone]" +
                ", [Vendor_TollFreeNo]" +
                ", [Vendor_Address]" +
                ", [Vendor_City]" +
                ", [Vendor_Zip]" +
                ", [Vendor_Region]" +
                ", [Vendor_SupportPage]" +
                ", [Vendor_Notes]" +
                ", [Vendor_Status]" +
                ", [Vendor_CreatedBy], [Vendor_CreatedDate], [Vendor_ModifiedBy], [Vendor_ModifiedDate], [Vendor_SAPCode])" +
                " VALUES ('" + txtvendor.Text + "'" +
                ", '" + txtCustomerId.Text + "'" +
                ", '" + txtbusinessphone.Text + "'" +
                ", '" + txttollfree.Text + "'" +
                ", '" + txtaddress.Text + "'" +
                ", '" + txtcity.Text + "'" +
                ", '" + txtzip.Text + "'" +
                ", '" + txtregion.Text + "'" +
                ", '" + txtsupportpage.Text + "'" +
                ", '" + txtnotes.Text + "'" +
                ", '" + ddltype.SelectedValue + "'" +
                ", '" + UserADID + "'" +
                ", GETDATE()" +
                ", '" + UserADID + "'" +
                ", GETDATE()" +
                ", '" + txtvendorcode.Text + "')";




                    SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
                    DataSet Ds = new DataSet();
                    Da.Fill(Ds);
                    Clear();

                    scriptstring = "alert('Successfully Added');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                    FillGrid();
                }
                else
                {
                    scriptstring = "alert('Vendor Already Exist');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                    Clear();
                }
            }
            else
            {

                string strqrycurrent = "select * from Inv_Vendor_Master where vendor_id=" + lblid.Text + "";
                SqlDataAdapter Dacurrent = new SqlDataAdapter(strqrycurrent, Con);
                DataSet Dscurrent = new DataSet();
                Dacurrent.Fill(Dscurrent);

                string strqryhistory = "insert into Inv_Vendor_Master_History(Vendor_Id,Vendor_Name " +
                                   ",[Vendor_CustomerId] " +
                                   ",[Vendor_BusinessPhone] " +
                                   ",[Vendor_TollFreeNo] " +
                                   ",[Vendor_Address] " +
                                   ",[Vendor_City] " +
                                   ",[Vendor_Zip] " +
                                   ",[Vendor_Region] " +
                                   ",[Vendor_SupportPage] " +
                                   ",[Vendor_Notes] " +
                                   ",[Vendor_Status] " +
                                   ",[Vendor_CreatedBy],[Vendor_CreatedDate],[Vendor_SAPCode])" +
                                   " values(" + lblid.Text + ",'" + Dscurrent.Tables[0].Rows[0]["Vendor_Name"] + "'," +
                                   " '" + Dscurrent.Tables[0].Rows[0]["Vendor_CustomerId"] + "','" + Dscurrent.Tables[0].Rows[0]["Vendor_BusinessPhone"] + "'," +
                                   " '" + Dscurrent.Tables[0].Rows[0]["Vendor_TollFreeNo"] + "','" + Dscurrent.Tables[0].Rows[0]["Vendor_Address"] + "'," +
                                   " '" + Dscurrent.Tables[0].Rows[0]["Vendor_City"] + "','" + Dscurrent.Tables[0].Rows[0]["Vendor_Zip"] + "'," +
                                   " '" + Dscurrent.Tables[0].Rows[0]["Vendor_Region"] + "','" + Dscurrent.Tables[0].Rows[0]["Vendor_SupportPage"] + "'," +
                                   " '" + Dscurrent.Tables[0].Rows[0]["Vendor_Notes"] + "','" + Dscurrent.Tables[0].Rows[0]["Vendor_Status"] + "', " +
                                   " '" + UserADID + "','" + DateTime.Now + "','" + Dscurrent.Tables[0].Rows[0]["Vendor_SAPCode"] + "')";




                SqlDataAdapter Dahistory = new SqlDataAdapter(strqryhistory, Con);
                DataSet Dshistory = new DataSet();
                Dahistory.Fill(Dshistory);




                strqry = "update Inv_Vendor_Master set Vendor_Name='" + txtvendor.Text + "' " +
                                  ",[Vendor_CustomerId]='" + txtCustomerId.Text + "' " +
                                  ",[Vendor_BusinessPhone]='" + txtbusinessphone.Text + "' " +
                                  ",[Vendor_TollFreeNo]='" + txttollfree.Text + "' " +
                                  ",[Vendor_Address]='" + txtaddress.Text + "' " +
                                  ",[Vendor_City]='" + txtcity.Text + "' " +
                                  ",[Vendor_Zip]='" + txtzip.Text + "' " +
                                  ",[Vendor_Region]='" + txtregion.Text + "' " +
                                  ",[Vendor_SupportPage]='" + txtsupportpage.Text + "' " +
                                  ",[Vendor_Notes]='" + txtnotes.Text + "' " +
                                  ",[Vendor_Status]='" + ddltype.SelectedValue + "' " +
                                  ",[Vendor_ModifiedBy]='" + UserADID + "',[Vendor_ModifiedDate]='" + DateTime.Now + "',[Vendor_SAPCode]='" + txtvendorcode.Text + "' " +
                                  " where vendor_id=" + lblid.Text + "";





                SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
                DataSet Ds = new DataSet();
                Da.Fill(Ds);
                Clear();

                scriptstring = "alert('Successfully Updated');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                FillGrid();
                btnsubmit.Text = "save";


            }

        }

    }
}