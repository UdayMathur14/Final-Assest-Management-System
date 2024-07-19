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

namespace TileMenu
{
    public partial class AMCReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        string UserADID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UserADID = Session["login"].ToString();
            if (!IsPostBack)
            {
                GetData();
            }
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected void optsearch_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtsearch.Text = "";
            GetData();


        }

        protected void GetData()
        {

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            string StrCondition = "";

            string strqry = "select AMC_PONO as [PONumber],convert(varchar(50),AMCMaster_PODate,106) as [PODate],convert(varchar(50),AMCMaster_POStartDate,106) as POStartDate,convert(varchar(50),AMCMaster_POEndDate,106) as POEndDate,AMCMaster_POValue as [PO Value],convert(varchar(50),AMC_CreatedDate,106) as Createddate ,convert(varchar(50),AMC_StartDate,106) as [AMCStartDate],convert(varchar(50),AMC_EndDate,106) as [AMCEndDate],AMC_Period as Period,OEM_Name as OEM,Vendor_name as Vendor,AMC_Product as Product,AMC_OrderValue as InvoiceValue,amc_total as Total," +
    " isnull((select amctrans_days from AMCTransaction_Detail where amctrans_amc_id=amc_id and amctrans_year=2020),0) as [2020 Days]," +
    " isnull((select amctrans_value from AMCTransaction_Detail where amctrans_amc_id=amc_id and amctrans_year=2020),0) as [2020 Value]" +

    " from dbo.AMC_Detail inner join AMC_Master on AMC_Detail.AMC_PONO=AMC_Master.AMCMaster_PONO " +
    " inner join Inv_Vendor_Master on AMC_Master.AMCMaster_vendor=Inv_Vendor_Master.vendor_id " +
    " inner join OEM_Master on amc_master.amcmaster_oem=oem_master.OEM_Id where amcmaster_status='ACTIVE'";


            if (txtsearch.Text != "")
            {
                if (optsearch.SelectedValue == "0")
                {
                    StrCondition += " and AMC_PONO like '%" + txtsearch.Text + "%'";
                }
                if (optsearch.SelectedValue == "1")
                {
                    StrCondition += " and vendor_name like '%" + txtsearch.Text + "%'";
                }
                if (optsearch.SelectedValue == "2")
                {
                    StrCondition += " and AMC_Product like '%" + txtsearch.Text + "%'";
                }
            }

            StrCondition += " order by AMCMaster_POEndDate desc";

            // Response.Write(strqry + StrCondition);
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
    }
}