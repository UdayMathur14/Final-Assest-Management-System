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
    public partial class AMCDetail : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        string UserADID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UserADID = Session["login"].ToString();
            if (!IsPostBack)
            {
                FillPO();
                Clear();
                GetData();
            }

        }

        protected void FillPO()
        {
            string strqry = "select AMCMaster_PONO from AMC_Master where amcmaster_status='ACTIVE' order by AMCMaster_PONO ";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlPO.DataSource = Ds.Tables[0];
                ddlPO.DataTextField = "AMCMaster_PONO";
                ddlPO.DataValueField = "AMCMaster_PONO";
                ddlPO.DataBind();

            }

        }




        protected void Clear()
        {



            txtStartdate.Text = "";
            txtenddate.Text = "";
            txtPeriod.Text = "";
            txtinvno.Text = "";
            txtinvdate.Text = "";
            txtproduct.Text = "";
            txtordervalue.Text = "0";
            txtservicepercent.Text = "0";
            txtserviceamount.Text = "0";
            txtcesspercent.Text = "0";

            txtcessamount.Text = "0";
            txtvatpercent.Text = "0";
            txtvatamount.Text = "0";
            txtservicelosspercent.Text = "0";
            txtservicelossamount.Text = "0";
            txttotalalvalue.Text = "0";



        }

        protected void GetData()
        {
            string StrCondition = "";
            string strqry = " select AMC_ID,AMC_PONO,convert(varchar(50),AMCMaster_PODate,106) as PODate," +
                            " convert(varchar(50),AMCMaster_POStartDate,106) as POStartDate,convert(varchar(50),AMCMaster_POEndDate,106) as POEndDate," +
                            " convert(varchar(50),AMC_StartDate,106) as StartDate," +
                            " convert(varchar(50),AMC_EndDate,106) as Enddate,AMC_InvNo,convert(varchar(50),AMC_InvDate,106) as InvDate," +
                            " AMC_Period, Vendor_name, AMC_Product, AMCMaster_POValue,AMC_OrderValue,AMC_STaxPer,AMC_CTaxPer,AMC_VATPer,AMC_Total" +
                            " from AMC_Detail inner join AMC_Master on AMC_Detail.AMC_PONO=AMC_Master.AMCMaster_PONO " +
                            " inner join Inv_Vendor_Master on AMC_Master.AMCMaster_vendor=Inv_Vendor_Master.vendor_id where 1=1";

            if (ddlPO.SelectedValue != "-1")
            {
                StrCondition += " and AMC_PONO='" + ddlPO.SelectedValue + "'";
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
        protected DataSet GetDetail(string AMCID)
        {
            string StrCondition = "";
            string strqry = " select AMCTrans_Year,AMCTrans_Days,AMCTrans_Value " +
                            " from AMCTransaction_Detail where AMCTrans_AMC_ID='" + AMCID + "'";



            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + StrCondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            return Ds;


        }


        protected void btnsubmit_Click(object sender, EventArgs e)
        {



            string strqry = "insert into AMC_Detail(AMC_PONO,AMC_StartDate,AMC_EndDate,AMC_InvNo,AMC_InvDate," +
                             " AMC_Period, AMC_OrderValue," +
                             " AMC_STaxPer,AMC_STaxAmt,AMC_CTaxPer,AMC_CTaxAmt,AMC_VATPer," +
                             " AMC_VATAmt,AMC_STaxLossPer,AMC_STaxLossAmt,AMC_Total," +
                             " AMC_CreatedBy, AMC_CreatedDate,AMC_Product) " +
                             " values('" + ddlPO.SelectedValue + "','" + txtStartdate.Text + "','" + txtenddate.Text + "','" + txtinvno.Text + "','" + txtinvdate.Text + "'," +
                             " '" + txtPeriod.Text + "','" + txtordervalue.Text + "'," +
                             " '" + txtservicepercent.Text + "','" + txtserviceamount.Text + "','" + txtcesspercent.Text + "','" + txtcessamount.Text + "','" + txtvatpercent.Text + "'," +
                             " '" + txtvatamount.Text + "','" + txtservicelosspercent.Text + "','" + txtservicelossamount.Text + "','" + txttotalalvalue.Text + "'," +
                             " '" + Session["login"].ToString() + "','" + System.DateTime.Now.Date + "','" + txtproduct.Text + "')";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            string stramcid = "select top 1 AMC_id from AMC_Detail order by AMC_Id desc";
            SqlDataAdapter Daamcid = new SqlDataAdapter(stramcid, Con);
            DataSet Dsamcid = new DataSet();
            Daamcid.Fill(Dsamcid);

            calculateTransaction(Dsamcid.Tables[0].Rows[0][0].ToString());
            MailIt();

            Clear();
            GetData();

            string scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
        }






        //protected void MailIt(string StockId,string Product,string Employee,string CostCenter,string issuetype,string remarks)
        //{


        //    MailMessage mail = new MailMessage();
        //    mail.To.Add("rajnish.singh@khd.com");
        //    //mail.From = new MailAddress("jainanuj009@gmail.com", "HR ADMIN");
        //    mail.From = new MailAddress("hradmin.hwil@khd.com", "HR ADMIN");
        //    mail.Subject ="IT Consumable Request";
        //    mail.Body = "A Request Has been Raised for "+
        //                " <br>Product :" + Product + "<br>User:" + Employee + "<br>CostCenter:" + CostCenter + "<br>"+
        //                " Issue Type:" + issuetype + "<br>Remarks:" + remarks + "" +
        //                " Kindly Approve/Reject using Link <a href=" + "http://khdin/ITInventory/Master/StockOut.aspx?Stockid=" + StockId + "" + "" + ">Click here to go to Application</a>";



        //    mail.IsBodyHtml = true;
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "172.16.0.50"; //Or Your SMTP Server Address

        //    //smtp.Host = "15.326.0.80"; // farji
        //    //smtp.Host = "smtp.gmail.com";
        //    // smtp.Host = Convert.ToString(587);
        //    smtp.Credentials = new System.Net.NetworkCredential
        //        //("jainanuj009@gmail.com", "Anujja123");
        //        ("hradmin.hwil@khd.com", "hwil@2012");
        //    //Or your Smtp Email ID and Password
        //    // smtp.EnableSsl = true;
        //    smtp.Send(mail);
        //}
        protected void txtordervalue_TextChanged(object sender, EventArgs e)
        {
            //ServicetaxAmount();
            Totalvalue();
            TotalGST();
        }
        protected void txtservicepercent_TextChanged(object sender, EventArgs e)
        {
            ServicetaxAmount();
            //ServicetaxLossAmount();
            Totalvalue();
            TotalGST();
        }
        protected void txtcesspercent_TextChanged(object sender, EventArgs e)
        {
            CesstaxAmount();
            Totalvalue();
            TotalGST();

        }
        protected void txtvatpercent_TextChanged(object sender, EventArgs e)
        {
            VATAmount();
            Totalvalue();
            TotalGST();
        }
        protected void txtservicelosspercent_TextChanged(object sender, EventArgs e)
        {
            ServicetaxLossAmount();
            Totalvalue();
            TotalGST();
        }
        private void ServicetaxAmount()
        {
            if (txtordervalue.Text != "" && txtservicepercent.Text != "")
            {
                txtserviceamount.Text = (Convert.ToDouble(txtordervalue.Text) * Convert.ToDouble(txtservicepercent.Text) / 100).ToString();
            }
        }
        private void CesstaxAmount()
        {
            if (txtordervalue.Text != "" && txtcesspercent.Text != "")
            {
                txtcessamount.Text = (Convert.ToDouble(txtordervalue.Text) * Convert.ToDouble(txtcesspercent.Text) / 100).ToString();
            }
        }
        private void VATAmount()
        {
            if (txtordervalue.Text != "" && txtvatpercent.Text != "")
            {
                txtvatamount.Text = (Convert.ToDouble(txtordervalue.Text) * Convert.ToDouble(txtvatpercent.Text) / 100).ToString();
            }
        }
        private void ServicetaxLossAmount()
        {
            if (txtservicelosspercent.Text != "" && txtserviceamount.Text != "")
            {
                txtservicelossamount.Text = (Convert.ToDouble(txtserviceamount.Text) * Convert.ToDouble(txtservicelosspercent.Text) / 100).ToString();

            }
        }
        private void Totalvalue()
        {
            txttotalalvalue.Text = (Convert.ToDouble(txtordervalue.Text) + Convert.ToDouble(txtserviceamount.Text) + Convert.ToDouble(txtvatamount.Text) + Convert.ToDouble(txtcessamount.Text)).ToString();
        }
        private void TotalGST()
        {
            txtservicelossamount.Text = (Convert.ToDouble(txtserviceamount.Text) + Convert.ToDouble(txtvatamount.Text) + Convert.ToDouble(txtcessamount.Text)).ToString();
        }




        public static double GetDaysDifference(DateTime startDate, DateTime endDate)
        {

            //int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            //return Math.Abs(monthsApart);
            double Days = ((endDate - startDate).TotalDays) + 1;
            return Days;

        }




        protected void Button1_Click(object sender, EventArgs e)
        {
            double diff = 0;
            if (txtStartdate.Text != "" && txtenddate.Text != "")
            {
                if (txtStartdate.Text == txtenddate.Text)
                {

                    diff = GetDaysDifference(Convert.ToDateTime(txtStartdate.Text), Convert.ToDateTime(txtenddate.Text)) + 1;
                }
                else
                {
                    diff = GetDaysDifference(Convert.ToDateTime(txtStartdate.Text), Convert.ToDateTime(txtenddate.Text));
                }
                txtPeriod.Text = diff.ToString();
            }
        }

        protected void ddlPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void calculateTransaction(string AMCId)
        {


            string StrCondition = "";
            string strqry = "select year(AMC_Startdate)as SY,year(AMC_Enddate)as EY,* from amc_detail where amc_id='" + AMCId + "'";
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + StrCondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            int AMCStartdate = 0;
            int AMCEnddate = 0;
            int YearDiff = 0;
            if (Ds.Tables[0].Rows.Count > 0)
            {

                AMCStartdate = Convert.ToInt32(Ds.Tables[0].Rows[0]["SY"]);
                AMCEnddate = Convert.ToInt32(Ds.Tables[0].Rows[0]["EY"]);
                YearDiff = AMCEnddate - AMCStartdate;
                if (YearDiff == 0)
                {


                    string strqryi10 = "select DATEDIFF ( dd ,AMC_Startdate , AMC_Enddate )+1 as days, " +
                          " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,AMC_Startdate , AMC_Enddate )+1)) as value" +
                          " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                    SqlConnection Coni10 = new SqlConnection();
                    Coni10.ConnectionString = strCon;
                    SqlDataAdapter Dai10 = new SqlDataAdapter(strqryi10, Coni10);
                    DataSet Dsi10 = new DataSet();
                    Dai10.Fill(Dsi10);
                    string strqryi100 = "insert into AMCTransaction_Detail" +
                   " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                   " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCStartdate + "','" + Dsi10.Tables[0].Rows[0]["days"] + "','" + Dsi10.Tables[0].Rows[0]["value"] + "')";
                    SqlConnection Coni100 = new SqlConnection();
                    Coni100.ConnectionString = strCon;
                    SqlDataAdapter Dai100 = new SqlDataAdapter(strqryi100, Coni100);
                    DataSet Dsi100 = new DataSet();
                    Dai100.Fill(Dsi100);


                }
                else if (YearDiff == 1)
                {

                    for (int i = 0; i <= YearDiff; i++)
                    {
                        if (i == 0)
                        {
                            string strqryi0 = "select DATEDIFF ( dd ,AMC_Startdate , '" + AMCStartdate + "'+'-12-31' )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,AMC_Startdate , '" + AMCStartdate + "'+'-12-31' )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni0 = new SqlConnection();
                            Coni0.ConnectionString = strCon;
                            SqlDataAdapter Dai0 = new SqlDataAdapter(strqryi0, Coni0);
                            DataSet Dsi0 = new DataSet();
                            Dai0.Fill(Dsi0);
                            string strqryi00 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCStartdate + "','" + Dsi0.Tables[0].Rows[0]["days"] + "','" + Dsi0.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni00 = new SqlConnection();
                            Coni00.ConnectionString = strCon;
                            SqlDataAdapter Dai00 = new SqlDataAdapter(strqryi00, Coni00);
                            DataSet Dsi00 = new DataSet();
                            Dai00.Fill(Dsi00);
                        }
                        if (i == 1)
                        {
                            string strqryi1 = "select DATEDIFF ( dd ,'" + AMCEnddate + "'+'-01-01',amc_enddate )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,'" + AMCEnddate + "'+'-01-01',AMC_enddate )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni1 = new SqlConnection();
                            Coni1.ConnectionString = strCon;
                            SqlDataAdapter Dai1 = new SqlDataAdapter(strqryi1, Coni1);
                            DataSet Dsi1 = new DataSet();
                            Dai1.Fill(Dsi1);
                            string strqryi11 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCEnddate + "','" + Dsi1.Tables[0].Rows[0]["days"] + "','" + Dsi1.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni11 = new SqlConnection();
                            Coni11.ConnectionString = strCon;
                            SqlDataAdapter Dai11 = new SqlDataAdapter(strqryi11, Coni11);
                            DataSet Dsi11 = new DataSet();
                            Dai11.Fill(Dsi11);
                        }


                    }
                }
                else if (YearDiff == 2)
                {
                    for (int i = 0; i <= YearDiff; i++)
                    {
                        if (i == 0)
                        {
                            string strqryi0 = "select DATEDIFF ( dd ,AMC_Startdate , '" + AMCStartdate + "'+'-12-31' )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,AMC_Startdate , '" + AMCStartdate + "'+'-12-31' )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni0 = new SqlConnection();
                            Coni0.ConnectionString = strCon;
                            SqlDataAdapter Dai0 = new SqlDataAdapter(strqryi0, Coni0);
                            DataSet Dsi0 = new DataSet();
                            Dai0.Fill(Dsi0);
                            string strqryi00 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCStartdate + "','" + Dsi0.Tables[0].Rows[0]["days"] + "','" + Dsi0.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni00 = new SqlConnection();
                            Coni00.ConnectionString = strCon;
                            SqlDataAdapter Dai00 = new SqlDataAdapter(strqryi00, Coni00);
                            DataSet Dsi00 = new DataSet();
                            Dai00.Fill(Dsi00);
                        }
                        if (i == 1)
                        {
                            int AMCStartdate1 = 0;
                            AMCStartdate1 = AMCStartdate + 1;
                            string strqryi1 = "select DATEDIFF ( dd ,'" + AMCStartdate1 + "'+'-01-01','" + AMCStartdate1 + "'+'-12-31' )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,'" + AMCStartdate1 + "'+'-01-01','" + AMCStartdate1 + "'+'-12-31' )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni1 = new SqlConnection();
                            Coni1.ConnectionString = strCon;
                            SqlDataAdapter Dai1 = new SqlDataAdapter(strqryi1, Coni1);
                            DataSet Dsi1 = new DataSet();
                            Dai1.Fill(Dsi1);
                            string strqryi11 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCStartdate1 + "','" + Dsi1.Tables[0].Rows[0]["days"] + "','" + Dsi1.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni11 = new SqlConnection();
                            Coni11.ConnectionString = strCon;
                            SqlDataAdapter Dai11 = new SqlDataAdapter(strqryi11, Coni11);
                            DataSet Dsi11 = new DataSet();
                            Dai11.Fill(Dsi11);
                        }
                        if (i == 2)
                        {
                            string strqryi1 = "select DATEDIFF ( dd ,'" + AMCEnddate + "'+'-01-01',amc_enddate )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,'" + AMCEnddate + "'+'-01-01',AMC_enddate )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni1 = new SqlConnection();
                            Coni1.ConnectionString = strCon;
                            SqlDataAdapter Dai1 = new SqlDataAdapter(strqryi1, Coni1);
                            DataSet Dsi1 = new DataSet();
                            Dai1.Fill(Dsi1);
                            string strqryi11 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCEnddate + "','" + Dsi1.Tables[0].Rows[0]["days"] + "','" + Dsi1.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni11 = new SqlConnection();
                            Coni11.ConnectionString = strCon;
                            SqlDataAdapter Dai11 = new SqlDataAdapter(strqryi11, Coni11);
                            DataSet Dsi11 = new DataSet();
                            Dai11.Fill(Dsi11);
                        }

                    }
                }
                else if (YearDiff == 3)
                {
                    for (int i = 0; i <= YearDiff; i++)
                    {
                        if (i == 0)
                        {
                            string strqryi0 = "select DATEDIFF ( dd ,AMC_Startdate , '" + AMCStartdate + "'+'-12-31' )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,AMC_Startdate , '" + AMCStartdate + "'+'-12-31' )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni0 = new SqlConnection();
                            Coni0.ConnectionString = strCon;
                            SqlDataAdapter Dai0 = new SqlDataAdapter(strqryi0, Coni0);
                            DataSet Dsi0 = new DataSet();
                            Dai0.Fill(Dsi0);
                            string strqryi00 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCStartdate + "','" + Dsi0.Tables[0].Rows[0]["days"] + "','" + Dsi0.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni00 = new SqlConnection();
                            Coni00.ConnectionString = strCon;
                            SqlDataAdapter Dai00 = new SqlDataAdapter(strqryi00, Coni00);
                            DataSet Dsi00 = new DataSet();
                            Dai00.Fill(Dsi00);
                        }
                        if (i == 1)
                        {
                            int AMCStartdate1 = 0;
                            AMCStartdate1 = AMCStartdate + 1;
                            string strqryi1 = "select DATEDIFF ( dd ,'" + AMCStartdate1 + "'+'-01-01','" + AMCStartdate1 + "'+'-12-31' )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,'" + AMCStartdate1 + "'+'-01-01','" + AMCStartdate1 + "'+'-12-31' )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni1 = new SqlConnection();
                            Coni1.ConnectionString = strCon;
                            SqlDataAdapter Dai1 = new SqlDataAdapter(strqryi1, Coni1);
                            DataSet Dsi1 = new DataSet();
                            Dai1.Fill(Dsi1);
                            string strqryi11 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCStartdate1 + "','" + Dsi1.Tables[0].Rows[0]["days"] + "','" + Dsi1.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni11 = new SqlConnection();
                            Coni11.ConnectionString = strCon;
                            SqlDataAdapter Dai11 = new SqlDataAdapter(strqryi11, Coni11);
                            DataSet Dsi11 = new DataSet();
                            Dai11.Fill(Dsi11);
                        }
                        if (i == 2)
                        {
                            int AMCStartdate2 = 0;
                            AMCStartdate2 = AMCStartdate + 2;
                            string strqryi1 = "select DATEDIFF ( dd ,'" + AMCStartdate2 + "'+'-01-01','" + AMCStartdate2 + "'+'-12-31' )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,'" + AMCStartdate2 + "'+'-01-01','" + AMCStartdate2 + "'+'-12-31' )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni1 = new SqlConnection();
                            Coni1.ConnectionString = strCon;
                            SqlDataAdapter Dai1 = new SqlDataAdapter(strqryi1, Coni1);
                            DataSet Dsi1 = new DataSet();
                            Dai1.Fill(Dsi1);
                            string strqryi11 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCStartdate2 + "','" + Dsi1.Tables[0].Rows[0]["days"] + "','" + Dsi1.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni11 = new SqlConnection();
                            Coni11.ConnectionString = strCon;
                            SqlDataAdapter Dai11 = new SqlDataAdapter(strqryi11, Coni11);
                            DataSet Dsi11 = new DataSet();
                            Dai11.Fill(Dsi11);
                        }
                        if (i == 3)
                        {
                            string strqryi1 = "select DATEDIFF ( dd ,'" + AMCEnddate + "'+'-01-01',amc_enddate )+1 as days, " +
                                " (AMC_Total/AMC_Period *(DATEDIFF ( dd ,'" + AMCEnddate + "'+'-01-01',AMC_enddate )+1)) as value" +
                                " from amc_detail where amc_id='" + Ds.Tables[0].Rows[0]["AMC_id"] + "'";
                            SqlConnection Coni1 = new SqlConnection();
                            Coni1.ConnectionString = strCon;
                            SqlDataAdapter Dai1 = new SqlDataAdapter(strqryi1, Coni1);
                            DataSet Dsi1 = new DataSet();
                            Dai1.Fill(Dsi1);
                            string strqryi11 = "insert into AMCTransaction_Detail" +
                           " (AMCTrans_AMC_ID,AMCTrans_Year,AMCTrans_Days,AMCTrans_Value) " +
                           " values('" + Ds.Tables[0].Rows[0]["AMC_id"] + "','" + AMCEnddate + "','" + Dsi1.Tables[0].Rows[0]["days"] + "','" + Dsi1.Tables[0].Rows[0]["value"] + "')";
                            SqlConnection Coni11 = new SqlConnection();
                            Coni11.ConnectionString = strCon;
                            SqlDataAdapter Dai11 = new SqlDataAdapter(strqryi11, Coni11);
                            DataSet Dsi11 = new DataSet();
                            Dai11.Fill(Dsi11);
                        }

                    }
                }
            }

        }

        protected void MailIt()
        {
            string VendorName = "";
            string strqry = " select  Vendor_name " +
                            " from AMC_Detail inner join AMC_Master on AMC_Detail.AMC_PONO=AMC_Master.AMCMaster_PONO " +
                            " inner join Inv_Vendor_Master on AMC_Master.AMCMaster_vendor=Inv_Vendor_Master.vendor_id where AMC_PONO='" + ddlPO.SelectedValue + "'";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                VendorName = Ds.Tables[0].Rows[0][0].ToString();
            }





            MailMessage mail = new MailMessage();
            mail.To.Add("rajnish.singh@khd.com");
            mail.Bcc.Add("santosh.roy@khd.com");
            //mail.From = new MailAddress("jainanuj009@gmail.com", "HR ADMIN");
            mail.From = new MailAddress("Hwil.ITAdmin@khd.com", "IT ADMIN");
            mail.Subject = "AMC Detail Filled";
            mail.Body = "An AMC Detail submitted : " +
                        " <br>Product Name :" + txtproduct.Text + "" +
                        " <br>PO Number :" + ddlPO.SelectedValue + "<br>Vendor Name:" + VendorName + "<br>" +
                        " Invoice Start Date:" + txtStartdate.Text + "<br>Invoice End Date:" + txtenddate.Text + "<br>" +
                        " Invoice Value:" + txtordervalue.Text + "";




            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "172.16.0.50"; //Or Your SMTP Server Address
            smtp.Host = "mailrelay.cgn.khd.top";
            //smtp.Host = "15.326.0.80"; // farji
            //smtp.Host = "smtp.gmail.com";
            // smtp.Host = Convert.ToString(587);
            smtp.Credentials = new System.Net.NetworkCredential
                //("jainanuj009@gmail.com", "Anujja123");
                ("Hwil.ITAdmin@khd.com", "qwert@12345");
            //Or your Smtp Email ID and Password
            // smtp.EnableSsl = true;
            //smtp.Send(mail);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string AMC = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

                GridView gvdetail = e.Row.FindControl("gvdetail") as GridView;

                gvdetail.DataSource = GetDetail(AMC);

                gvdetail.DataBind();



            }
        }
    }
}