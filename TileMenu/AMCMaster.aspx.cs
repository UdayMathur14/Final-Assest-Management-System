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
    public partial class AMCMaster : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        string UserADID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UserADID = Session["login"].ToString();
            if (!IsPostBack)
            {
                FillOEM();
                FillVendor();
                FillGrid();


            }
        }

        protected void FillOEM()
        {
            string strqry = "select OEM_Id,OEM_Name from OEM_Master order by OEM_Name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddloem.DataSource = Ds.Tables[0];
                ddloem.DataTextField = "OEM_Name";
                ddloem.DataValueField = "OEM_Id";
                ddloem.DataBind();

            }

        }

        protected void FillVendor()
        {
            string strqry = "select Vendor_Id,Vendor_Name from Inv_Vendor_Master order by Vendor_Name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlvendor.DataSource = Ds.Tables[0];
                ddlvendor.DataTextField = "Vendor_Name";
                ddlvendor.DataValueField = "Vendor_Id";
                ddlvendor.DataBind();
                ddlvendor.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }
        protected void FillSales(string VendorId)
        {
            string strqry = "select Id,SMName from Inv_VendorSalesManager where VendorId='" + VendorId + "' order by SMName";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlsales.DataSource = Ds.Tables[0];
                ddlsales.DataTextField = "SMName";
                ddlsales.DataValueField = "Id";
                ddlsales.DataBind();
                ddlsales.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                ddlsales.DataSource = "";
                ddlsales.DataBind();
                ddlsales.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillAccount(string VendorId)
        {
            string strqry = "select Id,AMName from Inv_VendorAccountManager where VendorId='" + VendorId + "' order by AMName";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlaccount.DataSource = Ds.Tables[0];
                ddlaccount.DataTextField = "AMName";
                ddlaccount.DataValueField = "Id";
                ddlaccount.DataBind();
                ddlaccount.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                ddlaccount.DataSource = "";
                ddlaccount.DataBind();
                ddlaccount.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }

        protected void FillGrid()
        {
            string StrCondition = "";
            string strqry = "select ROW_NUMBER() OVER( ORDER BY AMCMaster_POEndDate desc) AS 'sno' " +
           " ,[AMCMaster_Id] " +
           " ,[AMCMaster_PONO] " +
           " ,[OEM_Name] " +
           " ,[Vendor_Name]" +
           // " ,[AMCMaster_Vendor] "+
           " ,[AMCMaster_Description] " +
           // ,[AMCMaster_Location]
           //,[AMCMaster_ProductType]
           //,[AMCMaster_ProductCategory]
           ",[AMCMaster_ProcurementType] " +
          //,[AMCMaster_LicenseKey]
          //,[AMCMaster_PODate]
          //,[AMCMaster_POStartDate]
          " ,convert(varchar(50),AMCMaster_POEndDate,106) as POEndDate " +
          //,[AMCMaster_POValue]
          "  ,[AMCMaster_Event] " +
          " ,left(AMCMaster_Responsibility,charindex('@',AMCMaster_Responsibility)-1) as Responsible" +
          //,[AMCMaster_Attachment]
          // ,[AMCMaster_SLA]
          " ,[AMCMaster_Status] " +
          // ,[AMCMaster_CreatedBy]
          // ,[AMCMaster_CreatedDate]
          // ,[AMCMaster_ModifiedBy]
          // ,[AMCMaster_ModifiedDate]
          " from AMC_master inner join Inv_Vendor_Master on Inv_Vendor_Master.[Vendor_Id]=AMC_Master.AMCMaster_Vendor " +
          " inner join OEM_Master on OEM_Master.[OEM_Id]=AMC_Master.AMCMaster_OEM" +
          " where 1=1";
            if (ddlvendor.SelectedValue != "-1")
            {
                StrCondition += " and AMCMaster_Vendor='" + ddlvendor.SelectedValue + "'";
            }
            if (ddlstatus.SelectedValue != "-1")
            {
                StrCondition += " and AMCMaster_status='" + ddlstatus.SelectedValue + "'";
            }
            if (ddlresponsible.SelectedValue != "-1")
            {
                StrCondition += " and AMCMaster_Responsibility='" + ddlresponsible.SelectedValue + "'";
            }
            if (ddlprocurementtype.SelectedValue != "-1")
            {
                StrCondition += " and AMCMaster_ProcurementType='" + ddlprocurementtype.SelectedValue + "'";
            }
            if (ddlalert.SelectedValue != "-1")
            {
                StrCondition += " and AMCMaster_Event='" + ddlalert.SelectedValue + "'";
            }

            if (txtpo.Text != "")
            {
                StrCondition += " and AMCMaster_Pono like '%" + txtpo.Text + "%'";
            }


            StrCondition += " order by AMCMaster_POEndDate desc";
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
                GridView1.DataSource = Ds;
                GridView1.DataBind();

            }
        }
        protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSales(ddlvendor.SelectedValue);
            FillAccount(ddlvendor.SelectedValue);
            FillGrid();
        }
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void ddlresponsible_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void ddlprocurementtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void ddlalert_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void txtpo_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string scriptstring = "";

            btnsubmit.Text = "Update";
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());


            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];


            string strqry = "select AmcMaster_Id,AMCMaster_PONO,AMCMaster_OEM,AMCMaster_Vendor, " +
                            " AMCMaster_Description,AMCMaster_Location,AMCMaster_ProductType,AMCMaster_ProductCategory, " +
                            " AMCMaster_ProcurementType,AMCMaster_LicenseKey,convert(varchar(50),AMCMaster_PODate,106) as pod," +
                            " convert(varchar(50),AMCMaster_POStartDate,106) as sdate," +
                            " convert(varchar(50),AMCMaster_POEndDate,106) as edate,AMCMaster_POValue," +
                            " AMCMaster_Event,AMCMaster_Responsibility,AMCMaster_SLA,AMCMaster_ResponseTime,AMCMaster_ResolutionTime," +
                            " AMCMaster_Availability,AMCMaster_SupportStartDate,AMCMaster_SupportEndDate,AMCMaster_SalesManager,AMCMaster_AccountManager," +
                            " AMCMaster_Status" +
                            " from AMC_Master where AMCMaster_Id='" + id + "'";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            lblid.Text = Ds.Tables[0].Rows[0]["AmcMaster_Id"].ToString();
            txtpo.Text = Ds.Tables[0].Rows[0]["AMCMaster_PONO"].ToString();
            ddloem.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_OEM"].ToString();
            ddlvendor.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_Vendor"].ToString();
            FillSales(ddlvendor.SelectedValue);
            FillAccount(ddlvendor.SelectedValue);

            txtdescription.Text = Ds.Tables[0].Rows[0]["AMCMaster_Description"].ToString();
            txtlocation.Text = Ds.Tables[0].Rows[0]["AMCMaster_Location"].ToString();
            ddlproducttype.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_ProductType"].ToString();
            ddlproductcategory.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_ProductCategory"].ToString();
            ddlprocurementtype.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_ProcurementType"].ToString();
            txtlicense.Text = Ds.Tables[0].Rows[0]["AMCMaster_LicenseKey"].ToString();
            txtpodate.Text = Ds.Tables[0].Rows[0]["POD"].ToString();
            txtpostartdate.Text = Ds.Tables[0].Rows[0]["sdate"].ToString();
            txtpoenddate.Text = Ds.Tables[0].Rows[0]["edate"].ToString();
            txtpovalue.Text = Ds.Tables[0].Rows[0]["AMCMaster_POValue"].ToString();
            ddlalert.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_Event"].ToString();
            ddlresponsible.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_Responsibility"].ToString();
            txtSLA.Text = Ds.Tables[0].Rows[0]["AMCMaster_SLA"].ToString();
            txtresponsetime.Text = Ds.Tables[0].Rows[0]["AMCMaster_ResponseTime"].ToString();
            txtresolutiontime.Text = Ds.Tables[0].Rows[0]["AMCMaster_ResolutionTime"].ToString();
            txtavailability.Text = Ds.Tables[0].Rows[0]["AMCMaster_Availability"].ToString();
            txtsupportstartdate.Text = Ds.Tables[0].Rows[0]["AMCMaster_SupportStartDate"].ToString();
            txtsupportenddate.Text = Ds.Tables[0].Rows[0]["AMCMaster_SupportEndDate"].ToString();
            if (Ds.Tables[0].Rows[0]["AMCMaster_SalesManager"].ToString() != "-1")
            {
                ddlsales.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_SalesManager"].ToString();
            }
            if (Ds.Tables[0].Rows[0]["AMCMaster_AccountManager"].ToString() != "-1")
            {
                ddlaccount.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_AccountManager"].ToString();
            }


            ddlstatus.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_Status"].ToString();

        }


        private bool checkDuplicate(string TableName, string Fieldname, string value)
        {
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
        protected void Clear()
        {
            lblid.Text = "";
            txtpo.Text = "";
            //ddloem.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_OEM"].ToString();
            ddlvendor.SelectedValue = "-1";

            txtdescription.Text = "";
            txtlocation.Text = "";
            //ddlproducttype.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_ProductType"].ToString();
            // ddlproductcategory.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_ProductCategory"].ToString();
            ddlprocurementtype.SelectedValue = "-1";
            txtlicense.Text = "";
            txtpodate.Text = "";
            txtpostartdate.Text = "";
            txtpoenddate.Text = "";
            txtpovalue.Text = "";
            ddlalert.SelectedValue = "-1";
            ddlresponsible.SelectedValue = "-1";
            txtSLA.Text = "";
            // ddlstatus.SelectedValue = Ds.Tables[0].Rows[0]["AMCMaster_Status"].ToString();

            txtresponsetime.Text = "";
            txtresolutiontime.Text = "";
            txtavailability.Text = "";
            txtsupportstartdate.Text = "";
            txtsupportenddate.Text = "";

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
            btnsubmit.Text = "Save";
            lblid.Text = "";
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string scriptstring;
            string strqry = "";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            if (btnsubmit.Text == "Save")
            {
                if (!checkDuplicate("AMC_Master", "AMCMaster_PONO", "'" + txtpo.Text + "'"))
                {
                    strqry = "insert into AMC_Master(AMCMaster_PONO " +
                                    ",[AMCMaster_OEM] " +
                                    ",[AMCMaster_Vendor] " +
                                    ",[AMCMaster_Description] " +
                                    ",[AMCMaster_Location] " +
                                    ",[AMCMaster_ProductType] " +
                                    ",[AMCMaster_ProductCategory] " +
                                    ",[AMCMaster_ProcurementType] " +
                                    ",[AMCMaster_LicenseKey] " +
                                    ",[AMCMaster_PODate] " +
                                    ",[AMCMaster_POStartDate] " +
                                    ",[AMCMaster_POEndDate] " +
                                    ",[AMCMaster_POValue] " +
                                    ",[AMCMaster_Event] " +
                                    ",[AMCMaster_Responsibility] " +
                                    ",[AMCMaster_SLA] " +
                                    ", AMCMaster_ResponseTime " +
                                    ", AMCMaster_ResolutionTime " +
                                    ", AMCMaster_Availability " +
                                    ", AMCMaster_SupportStartDate " +
                                    ", AMCMaster_SupportEndDate " +
                                    ", AMCMaster_SalesManager " +
                                    ", AMCMaster_AccountManager " +
                                    ",[AMCMaster_Status] " +
                                    ",[AMCMaster_CreatedBy],[AMCMaster_CreatedDate],[AMCMaster_ModifiedBy],[AMCMaster_ModifiedDate])" +
                                    " values('" + txtpo.Text + "','" + ddloem.SelectedValue + "','" + ddlvendor.SelectedValue + "'," +
                                    " '" + txtdescription.Text + "','" + txtlocation.Text + "','" + ddlproducttype.SelectedValue + "'," +
                                    " '" + ddlproductcategory.SelectedValue + "','" + ddlprocurementtype.SelectedValue + "','" + txtlicense.Text + "','" + txtpodate.Text + "','" + txtpostartdate.Text + "'," +
                                    " '" + txtpoenddate.Text + "','" + txtpovalue.Text + "','" + ddlalert.SelectedValue + "','" + ddlresponsible.SelectedValue + "','" + txtSLA.Text + "'," +
                                    " '" + txtresponsetime.Text + "','" + txtresolutiontime.Text + "','" + txtavailability.Text + "','" + txtsupportstartdate.Text + "','" + txtsupportenddate.Text + "','" + ddlsales.SelectedValue + "','" + ddlaccount.SelectedValue + "'," +
                                    " '" + ddlstatus.SelectedValue + "', " +
                                    " '" + UserADID + "','" + DateTime.Now + "','" + UserADID + "','" + DateTime.Now + "')";


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
                    scriptstring = "alert('PO Number Already Exist');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                    Clear();
                }
            }
            else
            {

                string strqrycurrent = "select * from AMC_Master where AMCMaster_id=" + lblid.Text + "";
                SqlDataAdapter Dacurrent = new SqlDataAdapter(strqrycurrent, Con);
                DataSet Dscurrent = new DataSet();
                Dacurrent.Fill(Dscurrent);

                string strqryhistory = "insert into AMC_Master_History(AMCMaster_id,AMCMaster_PONO " +
                                ",[AMCMaster_OEM] " +
                                ",[AMCMaster_Vendor] " +
                                ",[AMCMaster_Description] " +
                                ",[AMCMaster_Location] " +
                                ",[AMCMaster_ProductType] " +
                                ",[AMCMaster_ProductCategory] " +
                                ",[AMCMaster_ProcurementType] " +
                                ",[AMCMaster_LicenseKey] " +
                                ",[AMCMaster_PODate] " +
                                ",[AMCMaster_POStartDate] " +
                                ",[AMCMaster_POEndDate] " +
                                ",[AMCMaster_POValue] " +
                                ",[AMCMaster_Event] " +
                                ",[AMCMaster_Responsibility] " +
                                ",[AMCMaster_SLA] " +
                                ",[AMCMaster_Status] " +
                                ",[AMCMaster_CreatedBy],[AMCMaster_CreatedDate])" +
                                " values(" + lblid.Text + ",'" + Dscurrent.Tables[0].Rows[0]["AMCMaster_PONO"] + "'," +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_OEM"] + "','" + Dscurrent.Tables[0].Rows[0]["AMCMaster_Vendor"] + "'," +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_Description"] + "'," +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_Location"] + "','" + Dscurrent.Tables[0].Rows[0]["AMCMaster_ProductType"] + "'," +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_ProductCategory"] + "','" + Dscurrent.Tables[0].Rows[0]["AMCMaster_ProcurementType"] + "'," +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_LicenseKey"] + "','" + Dscurrent.Tables[0].Rows[0]["AMCMaster_PODate"] + "'," +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_POStartDate"] + "','" + Dscurrent.Tables[0].Rows[0]["AMCMaster_POEndDate"] + "'," +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_POValue"] + "','" + Dscurrent.Tables[0].Rows[0]["AMCMaster_Event"] + "'," +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_Responsibility"] + "','" + Dscurrent.Tables[0].Rows[0]["AMCMaster_SLA"] + "', " +
                                " '" + Dscurrent.Tables[0].Rows[0]["AMCMaster_Status"] + "', '" + UserADID + "','" + DateTime.Now + "')";




                SqlDataAdapter Dahistory = new SqlDataAdapter(strqryhistory, Con);
                DataSet Dshistory = new DataSet();
                Dahistory.Fill(Dshistory);




                strqry = "update AMC_Master set AMCMaster_PONO='" + txtpo.Text + "' " +
                                ",[AMCMaster_OEM]='" + ddloem.SelectedValue + "' " +
                                ",[AMCMaster_Vendor]='" + ddlvendor.SelectedValue + "' " +
                                ",[AMCMaster_Description]='" + txtdescription.Text + "' " +
                                ",[AMCMaster_Location]='" + txtlocation.Text + "' " +
                                ",[AMCMaster_ProductType]='" + ddlproducttype.SelectedValue + "' " +
                                ",[AMCMaster_ProductCategory]='" + ddlproductcategory.SelectedValue + "' " +
                                ",[AMCMaster_ProcurementType]='" + ddlprocurementtype.SelectedValue + "' " +
                                ",[AMCMaster_LicenseKey]='" + txtlicense.Text + "' " +
                                ",[AMCMaster_PODate]='" + txtpodate.Text + "' " +
                                ",[AMCMaster_POStartDate]='" + txtpostartdate.Text + "' " +
                                ",[AMCMaster_POEndDate]='" + txtpoenddate.Text + "' " +
                                ",[AMCMaster_POValue]='" + txtpovalue.Text + "' " +
                                ",[AMCMaster_Event]='" + ddlalert.SelectedValue + "' " +
                                ",[AMCMaster_Responsibility]='" + ddlresponsible.SelectedValue + "' " +
                                ",[AMCMaster_SLA]='" + txtSLA.Text + "' " +
                                ",AMCMaster_ResponseTime='" + txtresponsetime.Text + "' " +
                                ",AMCMaster_ResolutionTime='" + txtresolutiontime.Text + "' " +
                                ",AMCMaster_Availability='" + txtavailability.Text + "' " +
                                ",AMCMaster_SupportStartDate='" + txtsupportstartdate.Text + "' " +
                                ",AMCMaster_SupportEndDate='" + txtsupportenddate.Text + "' " +
                                ",AMCMaster_SalesManager='" + ddlsales.SelectedValue + "' " +
                                ",AMCMaster_AccountManager='" + ddlaccount.SelectedValue + "' " +
                                ",[AMCMaster_Status]='" + ddlstatus.SelectedValue + "' " +
                                ",[AMCMaster_ModifiedBy]='" + UserADID + "',[AMCMaster_ModifiedDate]='" + DateTime.Now + "' " +
                                " where AMCMaster_id=" + lblid.Text + "";



                //Response.Write(strqry);
                //Response.End();

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