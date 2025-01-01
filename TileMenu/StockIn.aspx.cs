using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace TileMenu
{
    public partial class StockIn : System.Web.UI.Page
    {
        DataTable dtSerial = null;
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtdate.Text = System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
                FillProductAll();
                FillMakeAll();
                FillProductTypeAll();
                FillProductModelAll();
                FillVendor();
                FillGrid();
                ddlassettype.Focus();
            }


        }
        protected void Clear()
        {
            txtdate.Text = "";
            ddlvendor.SelectedValue = "-1";
            ddlproduct.SelectedValue = "-1";
            ddlmake.SelectedValue = "-1";
            ddltype.SelectedValue = "-1";
            ddlmodel.SelectedValue = "-1";
            txtchallan.Text = "";
            txtqty.Text = "";
            txtcapdate.Text = "";
            txtexpirydate.Text = "";
            gvSerial.DataSource = null;
            gvSerial.DataBind();

        }
        protected bool SrNoAvailabilty()
        {

            string strqry = "select count(*) from Inv_Product_Master where Product_Serial_Available='YES' and Product_Id=" + ddlproduct.SelectedValue + "";
            bool available = false;
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows[0][0].ToString() != "0")
            {
                available = true;
            }
            return available;
        }
        protected void CapAvailabilty()
        {

            string strqry = "select Product_Capdate_available from Inv_Product_Master where Product_Id=" + ddlproduct.SelectedValue + "";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);


            hdncap.Text = Ds.Tables[0].Rows[0][0].ToString();


        }
        protected void WarrAvailabilty()
        {

            string strqry = "select Product_WarrantyDate_Available from Inv_Product_Master where Product_Id=" + ddlproduct.SelectedValue + "";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            hdnwarr.Text = Ds.Tables[0].Rows[0][0].ToString();


        }
        protected void FillGrid()
        {
            string strcondition = "";
            string strqry = " select convert(varchar(50),StockIn_InDate,106) as [In date],stockin_id as IN_id, Upload_File_Name as View_File,Vendor_Name,StockIn_Assettype as AssetType,Product_Name as Product,Make_Name as Make," +
                            " ProdType_Name as Type,ProdModel_Name as Model,StockIn_ChallanNo as [Challna No],StockIn_Qty as Qty  from inv_stockin " +
                            " inner join Inv_Product_Master on inv_stockin.StockIn_Product_Id = Inv_Product_Master.Product_Id " +
                            " left join Inv_Vendor_Master on Inv_Vendor_Master.Vendor_Id=inv_stockin.StockIn_Vendor_Id " +
                            " inner join [dbo].[Inv_Make_Master] on inv_stockin.StockIn_Make_Id =[Inv_Make_Master].Make_Id " +
                            " inner join [dbo].[Inv_ProdType] on inv_stockin.StockIn_Prodtype_Id =[Inv_ProdType].ProdType_Id " +
                            " inner join [dbo].[Inv_ProdModel] on inv_stockin.StockIn_ProdModel_Id =[Inv_ProdModel].ProdModel_id where 1=1";
            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and stockin_Product_Id='" + ddlproduct.SelectedValue + "'";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcondition += " and stockin_Make_Id='" + ddlmake.SelectedValue + "'";
            }
            if (ddlvendor.SelectedValue != "-1")
            {
                strcondition += " and StockIn_Vendor_Id='" + ddlvendor.SelectedValue + "'";
            }
            strcondition += "  order by StockIn_InDate desc";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
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

        protected void FillProductAll()
        {

            string strqry = "select Product_Id,Product_Name  from inv_Product_master where Product_ItemType='" + typemaster.SelectedValue + "' order by Product_name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlproduct.DataSource = Ds.Tables[0];
                ddlproduct.DataTextField = "Product_Name";
                ddlproduct.DataValueField = "Product_Id";
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }
        protected void FillMake()
        {
            string strcondition = "";
            string strqry = "select distinct inv_Make_master.make_id,Make_name from inv_Make_master " +
                " left join Inv_Product_Mapping on inv_make_master.Make_id=Inv_Product_mapping.make_id where 1=1 ";

            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproduct.SelectedValue + "";
            }


            strcondition += " order by make_Name";
            string strfinal = strqry + strcondition;

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlmake.DataSource = Ds.Tables[0];
                ddlmake.DataTextField = "Make_Name";
                ddlmake.DataValueField = "Make_Id";
                ddlmake.DataBind();
                ddlmake.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlmake.DataSource = Ds.Tables[0];
                ddlmake.DataBind();
                ddlmake.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillMakeAll()
        {

            string strqry = "select make_id,Make_name from inv_Make_master order by make_Name";



            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlmake.DataSource = Ds.Tables[0];
                ddlmake.DataTextField = "Make_Name";
                ddlmake.DataValueField = "Make_Id";
                ddlmake.DataBind();
                ddlmake.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlmake.DataSource = Ds.Tables[0];
                ddlmake.DataBind();
                ddlmake.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillProductType()
        {
            string strcondition = "";
            string strqry = "select distinct ProdType_Id,ProdType_Name from inv_ProdType " +
                 " left join Inv_Product_Mapping on inv_ProdType.Prodtype_id=Inv_Product_mapping.type_id where 1=1";
            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproduct.SelectedValue + "";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.make_id=" + ddlmake.SelectedValue + "";
            }


            strcondition += " order by ProdType_Name";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddltype.DataSource = Ds.Tables[0];
                ddltype.DataTextField = "ProdType_Name";
                ddltype.DataValueField = "ProdType_Id";
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddltype.DataSource = Ds.Tables[0];
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillProductTypeAll()
        {
            string strqry = "select * from inv_ProdType  order by ProdType_Name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddltype.DataSource = Ds.Tables[0];
                ddltype.DataTextField = "ProdType_Name";
                ddltype.DataValueField = "ProdType_Id";
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddltype.DataSource = Ds.Tables[0];
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillProductModel()
        {

            string strcondition = "";
            string strqry = "select distinct ProdModel_Id,ProdModel_Name from inv_ProdModel " +
                 " left join Inv_Product_Mapping on inv_ProdModel.ProdModel_Id=Inv_Product_mapping.model_id where 1=1";
            if (ddlproduct.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.product_id=" + ddlproduct.SelectedValue + "";
            }
            if (ddlmake.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.make_id=" + ddlmake.SelectedValue + "";
            }
            if (ddltype.SelectedValue != "-1")
            {
                strcondition += " and inv_product_mapping.type_id=" + ddltype.SelectedValue + "";
            }

            strcondition += " order by ProdModel_Name";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry + strcondition, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlmodel.DataSource = Ds.Tables[0];
                ddlmodel.DataTextField = "ProdModel_Name";
                ddlmodel.DataValueField = "ProdModel_Id";
                ddlmodel.DataBind();
                ddlmodel.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlmodel.DataSource = Ds.Tables[0];
                ddlmodel.DataBind();
                ddlmodel.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillProductModelAll()
        {
            string strqry = "select * from inv_ProdModel order by ProdModel_Name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlmodel.DataSource = Ds.Tables[0];
                ddlmodel.DataTextField = "ProdModel_Name";
                ddlmodel.DataValueField = "ProdModel_Id";
                ddlmodel.DataBind();
                ddlmodel.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                Ds.Clear();
                ddlmodel.DataSource = Ds.Tables[0];
                ddlmodel.DataBind();
                ddlmodel.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillVendor()
        {
            string strqry = "select Vendor_Id,Vendor_Name from Inv_Vendor_Master order by Vendor_Name ";


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
        protected void typemaster_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillProductAll();

        }
        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapAvailabilty();
            WarrAvailabilty();
            FillMake();
            FillProductType();
            FillProductModel();
            FillGrid();
            ddlproduct.Focus();
        }

        protected bool Checkduplicateserial(string Serial)
        {
            string strqry = "select ProductDetail_SerialNo from  Inv_ProductDetail_Master where " +
                            " ProductDetail_SerialNo='" + Serial + "'";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool Checkduplicateassetcode(string assetcode)
        {
            string strqry = "select ProductDetail_Assetcode from  Inv_ProductDetail_Master where " +
                            " ProductDetail_Assetcode='" + assetcode + "'";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string scriptstring = "";
            if (!Page.IsValid)
            {
                scriptstring = $"alert('Please enter all mandatory fields');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                return;
            }
            string filext = System.IO.Path.GetExtension(doc.FileName);
            if(filext!="" )
            {
                if (filext.ToLower() != ".pdf" && filext.ToLower() != ".doc" && filext.ToLower() != ".png")
                {
                    lblmessage.Text = "Please Upload file in (doc/pdf/png) format";
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    return;

                }
                if (doc.HasFile)
                {
                    int filesize = doc.PostedFile.ContentLength;
                    if (filesize > 30971532)
                    {
                        lblmessage.Text = "Maximum file size (3MB) exceeded";
                        lblmessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    doc.SaveAs(Server.MapPath("~/Uploads/" + doc.FileName));
                    lblmessage.Text = "";
                    lblmessage.ForeColor = System.Drawing.Color.Green;
                }

                //else
                //{
                //    lblmessage.Text = "No File Uploaded";
                //    lblmessage.ForeColor = System.Drawing.Color.Red;
                //}
                string filePath = "~/Uploads/" + doc.FileName;
                string viewLink = "<a href='" + ResolveUrl(filePath) + "' target='_blank'>View Image</a>";

            }




            if (SrNoAvailabilty() == true)
            {
                for (int i = 0; i < Convert.ToInt32(txtqty.Text); i++)
                {
                    var seriala = ((TextBox)gvSerial.Rows[i].Cells[2].FindControl("txtSerial")).Text;
                    for (int j = i + 1; j < Convert.ToInt32(txtqty.Text); j++)
                    {
                        var tempSerial = ((TextBox)gvSerial.Rows[j].Cells[2].FindControl("txtSerial")).Text;
                        if (!string.IsNullOrWhiteSpace(tempSerial) && seriala.Equals(tempSerial, StringComparison.InvariantCultureIgnoreCase))
                        {
                            scriptstring = $"alert('Serial No. {tempSerial} duplicate in form');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                            return;
                        }
                    }
                }
                for (int i = 0; i < Convert.ToInt32(txtqty.Text); i++)
                {
                    var seriala = ((TextBox)gvSerial.Rows[i].Cells[2].FindControl("txtSerial")).Text;

                    if (Checkduplicateserial(seriala) == true)
                    {
                        scriptstring = $"alert('Serial No. {seriala} already exist in stock');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                        return;
                    }
                }

            }
            string strqry = "insert into Inv_StockIn(StockIn_Product_Id, StockIn_Make_Id,StockIn_ProdType_Id,StockIn_ProdModel_Id,StockIn_Vendor_Id," +
                       " StockIn_InDate, StockIn_ChallanNo, StockIn_Qty, StockIn_Price, StockIn_Tax," +
                       " StockIn_Net, StockIn_CreatedBy, StockIn_CreatedDate,StockIn_AssetType,Upload_File_Name) " +
                       " values(" + ddlproduct.SelectedValue + "," + ddlmake.SelectedValue + "," + ddltype.SelectedValue + "," +
                       " " + ddlmodel.SelectedValue + ",'" + ddlvendor.SelectedValue + "'," +
                       " '" + txtdate.Text + "'," +
                       " '" + txtchallan.Text + "','" + txtqty.Text + "'," +
            " '0','0','0'," +
                       " '" + Session["login"].ToString() + "',GetDate(),'" + ddlassettype.SelectedValue + "', '" + doc.FileName + "')";
            // Response.Write(strqry);
            //Response.End();
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();

            Da.Fill(Ds);

            string strqryOut = "select top 1 Stockin_Id from Inv_StockIn order by Stockin_Id desc";
            SqlDataAdapter DaOut = new SqlDataAdapter(strqryOut, Con);
            DataSet DsOut = new DataSet();
            DaOut.Fill(DsOut);
            string strdetail = "";
            var assetCode = "";
            var serial = "";
            SqlConnection Condetail = new SqlConnection();
            Condetail.ConnectionString = strCon;

            if (SrNoAvailabilty() == false && typemaster.SelectedValue == "Hardware")
            {
                string strqryPExist = "select * from  Inv_ProductDetail_Master where ProductDetail_Product_Id=" + ddlproduct.SelectedValue + "";
                SqlDataAdapter DaPExist = new SqlDataAdapter(strqryPExist, Con);
                DataSet DsPExist = new DataSet();
                DaPExist.Fill(DsPExist);

                string strqryPFormat = "select Product_Serial_Format from  Inv_Product_Master where Product_Id=" + ddlproduct.SelectedValue + "";
                SqlDataAdapter DaPFormat = new SqlDataAdapter(strqryPFormat, Con);
                DataSet DsPFormat = new DataSet();
                DaPFormat.Fill(DsPFormat);


                for (int i = 0; i < Convert.ToInt32(txtqty.Text); i++)
                {


                    if (DsPExist.Tables[0].Rows.Count > 0)
                    {
                        string strqryPmax = "select max(right(ProductDetail_SerialNo,1))+1 from [Inv_ProductDetail_Master] where ProductDetail_Product_Id=" + ddlproduct.SelectedValue + "";
                        SqlDataAdapter DaPmax = new SqlDataAdapter(strqryPmax, Con);
                        DataSet DsPmax = new DataSet();
                        DaPmax.Fill(DsPmax);

                        serial = string.Concat(DsPFormat.Tables[0].Rows[0]["Product_Serial_Format"].ToString(), DsPmax.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        serial = string.Concat(DsPFormat.Tables[0].Rows[0]["Product_Serial_Format"].ToString(), '1');
                    }

                    strdetail = "insert into Inv_ProductDetail_Master(ProductDetail_Product_Id,ProductDetail_Make_Id,ProductDetail_ProdType_Id,ProductDetail_ProdModel_Id,ProductDetail_AssetCode," +
                           " ProductDetail_SerialNo,Status," +
                           " ProductDetail_CapDate,ProductDetail_ExpiryDate,ProductDetail_OldSrNo,ProductDetail_StockIN_Id) " +
                           " values(" + ddlproduct.SelectedValue + "," + ddlmake.SelectedValue + "," + ddltype.SelectedValue + "," + ddlmodel.SelectedValue + "," +
                           $" '{assetCode}','{serial}'," +
                           " '1','" + txtcapdate.Text + "','" + txtexpirydate.Text + "','','" + DsOut.Tables[0].Rows[0]["Stockin_Id"].ToString() + "')";

                    SqlDataAdapter Dadetail = new SqlDataAdapter(strdetail, Condetail);
                    DataSet Dsdetail = new DataSet();
                    Dadetail.Fill(Dsdetail);


                }

            }
            if (SrNoAvailabilty() == false && typemaster.SelectedValue == "Software")
            {
                for (int i = 0; i < 1; i++)
                {


                    assetCode = ((TextBox)gvSerial.Rows[i].Cells[1].FindControl("txtAssetCode")).Text;
                    serial = ((TextBox)gvSerial.Rows[i].Cells[2].FindControl("txtSerial")).Text;


                    strdetail = "insert into Inv_ProductDetail_Master(ProductDetail_Product_Id,ProductDetail_Make_Id,ProductDetail_ProdType_Id,ProductDetail_ProdModel_Id,ProductDetail_AssetCode," +
                           " ProductDetail_SerialNo,Status," +
                           " ProductDetail_CapDate,ProductDetail_ExpiryDate,ProductDetail_OldSrNo,ProductDetail_StockIN_Id) " +
                           " values(" + ddlproduct.SelectedValue + "," + ddlmake.SelectedValue + "," + ddltype.SelectedValue + "," + ddlmodel.SelectedValue + "," +
                           $" '{assetCode}','{serial}'," +
                           " '1','" + txtcapdate.Text + "','" + txtexpirydate.Text + "','','" + DsOut.Tables[0].Rows[0]["Stockin_Id"].ToString() + "')";

                    SqlDataAdapter Dadetail = new SqlDataAdapter(strdetail, Condetail);
                    DataSet Dsdetail = new DataSet();
                    Dadetail.Fill(Dsdetail);


                }
            }
            if (SrNoAvailabilty() == true)
            {

                for (int i = 0; i < Convert.ToInt32(txtqty.Text); i++)
                {


                    assetCode = ((TextBox)gvSerial.Rows[i].Cells[1].FindControl("txtAssetCode")).Text;
                    serial = ((TextBox)gvSerial.Rows[i].Cells[2].FindControl("txtSerial")).Text;


                    strdetail = "insert into Inv_ProductDetail_Master(ProductDetail_Product_Id,ProductDetail_Make_Id,ProductDetail_ProdType_Id,ProductDetail_ProdModel_Id,ProductDetail_AssetCode," +
                           " ProductDetail_SerialNo,Status," +
                           " ProductDetail_CapDate,ProductDetail_ExpiryDate,ProductDetail_OldSrNo,ProductDetail_StockIN_Id) " +
                           " values(" + ddlproduct.SelectedValue + "," + ddlmake.SelectedValue + "," + ddltype.SelectedValue + "," + ddlmodel.SelectedValue + "," +
                           $" '{assetCode}','{serial}'," +
                           " '1','" + txtcapdate.Text + "','" + txtexpirydate.Text + "','','" + DsOut.Tables[0].Rows[0]["Stockin_Id"].ToString() + "')";

                    SqlDataAdapter Dadetail = new SqlDataAdapter(strdetail, Condetail);
                    DataSet Dsdetail = new DataSet();
                    Dadetail.Fill(Dsdetail);


                }
            }





            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            FillGrid();
            Clear();

        }

        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            if (SrNoAvailabilty() == false && typemaster.SelectedValue == "Hardware")
            {
                gvSerial.DataSource = null;
                gvSerial.DataBind();
                return;
            }
            if (SrNoAvailabilty() == false && typemaster.SelectedValue == "Software")
            {
                gvSerial.DataSource = CreateTable(1);
                gvSerial.DataBind();
            }
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
            if (SrNoAvailabilty() == true)
            {
                gvSerial.DataSource = CreateTable(qty);
                gvSerial.DataBind();
            }
            txtqty.Focus();

        }

        private DataTable CreateTable(int qty)
        {
            dtSerial = new DataTable();
            dtSerial.Columns.Add(new DataColumn("SlNo", typeof(System.String)));
            dtSerial.Columns.Add(new DataColumn("AssetCode", typeof(System.String)));
            dtSerial.Columns.Add(new DataColumn("SerialNumber", typeof(System.String)));
            for (int i = 0; i < qty; i++)
            {
                DataRow dr = dtSerial.NewRow();
                dr[0] = (i + 1).ToString();

                dtSerial.Rows.Add(dr);
            }
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.Black;
            GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
            return dtSerial;
        }

        //protected void caldate_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtdate.Text = caldate.SelectedDate.ToString("dd/MMM/yyyy");
        //    caldate.Visible = false;
        //}

        //protected void imgcaldate_Click(object sender, ImageClickEventArgs e)
        //{
        //    caldate.Visible = true;
        //}
        //protected void Calcap_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtcapdate.Text = Calcap.SelectedDate.ToString("dd/MMM/yyyy");
        //    Calcap.Visible = false;
        //}

        //protected void Imagecap_Click(object sender, ImageClickEventArgs e)
        //{
        //    Calcap.Visible = true;
        //}
        //protected void Calexp_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtexpirydate.Text = Calexp.SelectedDate.ToString("dd/MMM/yyyy");
        //    Calexp.Visible = false;
        //}

        //protected void Imageexp_Click(object sender, ImageClickEventArgs e)
        //{
        //    Calexp.Visible = true;
        //}

        protected void ddlmake_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProductType();
            FillGrid();
            ddlmake.Focus();
        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProductModel();
            ddltype.Focus();
        }

        protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
            ddlvendor.Focus();
        }

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    string filext = System.IO.Path.GetExtension(doc.FileName);

        //    if (filext.ToLower() != ".doc")
        //    {
        //        lblmessage.Text = "upload only doc file";

        //    }
        //    if (doc.HasFile)
        //    {
        //        int filesize = doc.PostedFile.ContentLength;
        //        if (filesize > 30971532)
        //        {
        //            lblmessage.Text = "Maximum file size (3MB) exceeded";
        //        }
        //        doc.SaveAs(Server.MapPath("~/Uploads/" + doc.FileName));
        //        lblmessage.Text = "Success";
        //        lblmessage.ForeColor = System.Drawing.Color.Green;
        //    }

        //    else
        //    {
        //        lblmessage.Text = "No File Uploaded";
        //        lblmessage.ForeColor = System.Drawing.Color.Red;
        //    }

        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming that the column index of Upload_File_Name is known (you may need to adjust it accordingly)
                int columnIndex = 2; // Adjust this index based on the position of Upload_File_Name column in the GridView
                string fileName = e.Row.Cells[columnIndex].Text;
                string filePath = Server.MapPath("~/uploads/" + fileName);

                // Create anchor tag dynamically
                HyperLink link = new HyperLink();
                link.Text = fileName;
                link.NavigateUrl = "~/uploads/" + fileName;

                // Add the anchor tag to the cell
                e.Row.Cells[columnIndex].Controls.Add(link);
            }
        }



    }
}