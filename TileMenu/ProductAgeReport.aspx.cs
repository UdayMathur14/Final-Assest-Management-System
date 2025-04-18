using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TileMenu
{
    public partial class ProductAgeReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillProductAll();
                FillMakeAll();
                FillProductTypeAll();
                FillProductModelAll();
                FillGrid();
                FillEmployeeCode();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the run time error "  
            //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
        }
        protected void typemaster_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillProductAll();

            FillGrid();
        }
        protected void FillProductAll()
        {

            string strqry = "select Product_Id,Product_Name  from inv_Product_master WHERE Product_ItemType='" + typemaster.SelectedValue + "'" +
                            " order by Product_name";


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

        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();

            FillMake();
            FillProductType();
            FillProductModel();

        }
        protected void ddlmake_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
            FillProductType();
            FillProductModel();



        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
            FillProductModel();


        }
        protected void ddlOAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void ddlmodel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();

        }
        protected void FillGrid()
        {
            string StrCondition = "";

            string strqry = " select ROW_NUMBER() OVER( ORDER BY stockout_OAC,Product_Name,Make_Name,ProdType_Name,ProdModel_Name,ProductDetail_CapDate,StockOut_Remarks ) AS 'sno',ProductDetail_Id,Product_Name,Make_Name,ProdType_Name,ProdModel_Name,ProductDetail_Config,ProductDetail_AssetCode,ProductDetail_SerialNo,[StockIn_id],convert(varchar(50),[StockIn_InDate],106) as InDate,StockIn_AssetType,[StockIn_ChallanNo] " +
                            " ,convert(varchar(50),ProductDetail_CapDate,106) as CapDate,convert(varchar(50),ProductDetail_ExpiryDate,106) as WED,DATEDIFF (yy, ProductDetail_CapDate, GETDATE()) as Age,DATEDIFF (dd, ProductDetail_CapDate, GETDATE()) as days,StockOut_EmpCode,stockout_empname,StockOut_CostCenter,tblCostCenter.Description,convert(varchar(50),StockOut_IssueDate,106) as 'IssueDate',StockOut_IssueType,isnull(stockout_OAC,'Available') as stockout_OAC,StockOut_Remarks, " +
                            " product_id,stockout_id from dbo.Inv_ProductDetail_Master INNER JOIN dbo.Inv_Product_Master " +
                            " ON Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
        " inner join [dbo].[Inv_StockIn] on [Inv_ProductDetail_Master].[ProductDetail_StockIn_Id]=[Inv_StockIn].[StockIn_Id] " +
        " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
        " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
        " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
    " left outer join inv_stockout on inv_stockout.stockout_proddetail_id=Inv_ProductDetail_Master.Productdetail_id and stockout_id not in (select stockreturn_stockout_id from inv_stockreturn)" +
     " left join tblCostCenter on inv_stockout.StockOut_CostCenter=tblCostCenter.CostCenter " +
                            //" where  ProductDetail_Id NOT IN (select StockOut_ProdDetail_Id from dbo.Inv_StockOut where ( (stockout_OAC='SOLD')  or (stockout_OAC='Block/Inactive')  )  ) "+
                            "where Product_ItemType='" + typemaster.SelectedValue + "'";
            //[Product_ItemType]='Hardware'";



            if (ddlproduct.SelectedValue != "-1")
            {
                StrCondition += " AND ProductDetail_Product_Id =" + ddlproduct.SelectedValue + "";
            }

            if (ddlmake.SelectedValue != "-1")
            {
                StrCondition += " AND ProductDetail_Make_Id =" + ddlmake.SelectedValue + "";
            }
            if (ddltype.SelectedValue != "-1")
            {
                StrCondition += " AND ProductDetail_ProdType_Id =" + ddltype.SelectedValue + "";
            }
            if (ddlmodel.SelectedValue != "-1")
            {
                StrCondition += " AND ProductDetail_ProdModel_Id =" + ddlmodel.SelectedValue + "";
            }
            if (ddlOAC.SelectedValue != "-1")
            {
                StrCondition += " AND StockOut_OAC ='" + ddlOAC.SelectedValue + "'";
            }
            if (ddlemployee.SelectedValue != "-1")
            {
                StrCondition += " AND StockOut_EmpCode='" + ddlemployee.SelectedValue + "'";
            }
            if (txtcostcenter.Text != "")
            {
                StrCondition += " AND (StockOut_CostCenter like  '%" + txtcostcenter.Text + "%' or ProductDetail_SerialNo like  '%" + txtcostcenter.Text + "%' or ProductDetail_AssetCode like  '%" + txtcostcenter.Text + "%') ";
            }
            if (txtoldyear.Text != "")
            {
                StrCondition += " AND DATEDIFF (yy, ProductDetail_CapDate, GETDATE())= " + txtoldyear.Text + " ";
            }

            string StrFinal = strqry + StrCondition + " order by stockout_OAC,Product_Name,Make_Name,ProdType_Name,ProdModel_Name,ProductDetail_CapDate,StockOut_Remarks";
            //Response.Write(StrFinal);
            //Response.End();
            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(StrFinal, Con);
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


        protected void optcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optcat.SelectedIndex == 0)
            {
                FillEmployeeCode();
            }
            else
            {
                FillCommon();
            }
        }
        protected void FillEmployeeCode()
        {
            string strqry = "";

            strqry = "select Emp_code,emp_name+'('+emp_code+')' as EMP from tblempmaster where  status in (1) order by emp_name ";




            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlemployee.DataSource = Ds.Tables[0];
                ddlemployee.DataTextField = "EMP";
                ddlemployee.DataValueField = "Emp_code";
                ddlemployee.DataBind();
                ddlemployee.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void FillCommon()
        {
            string strqry = "";

            strqry = "select Emp_code,emp_name+'('+emp_code+')' as EMP from tblempmaster order by emp_name ";




            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlemployee.DataSource = Ds.Tables[0];
                ddlemployee.DataTextField = "EMP";
                ddlemployee.DataValueField = "Emp_code";
                ddlemployee.DataBind();
                ddlemployee.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }


        }
        protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();

        }


        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            FillGrid();

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string type = (string)e.Row.Cells[21].Text.ToString();

                string prodid = (string)e.Row.Cells[25].Text.ToString();
                string soutid = (string)e.Row.Cells[26].Text.ToString();
                if (type != "Available")
                {
                    string strqry = "select Product_Serial_Available from Inv_Product_Master where Product_ItemType='Software' and Product_Id='" + prodid + "'";
                    SqlConnection Con = new SqlConnection();
                    Con.ConnectionString = strCon;
                    SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
                    DataSet Ds = new DataSet();
                    Da.Fill(Ds);
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        if (Ds.Tables[0].Rows[0][0].ToString() == "NO")
                        {
                            string strqrymodel = "select ProdModel_Name from Inv_ProdModel where ProdModel_Id in(select Stockout_Location from Inv_StockOut where stockout_id='" + soutid + "')";
                            SqlDataAdapter Damodel = new SqlDataAdapter(strqrymodel, Con);
                            DataSet Dsmodel = new DataSet();
                            Damodel.Fill(Dsmodel);
                            if (Dsmodel.Tables[0].Rows.Count > 0)
                            {
                                e.Row.Cells[5].Text = Dsmodel.Tables[0].Rows[0][0].ToString();
                            }

                        }
                    }
                }


                if (type == "SOLD" || type == "Scrapped")
                {
                    e.Row.BackColor = Color.Red;
                    //e.Row.ForeColor = Color.Green ;
                }
                if (type == "Available")
                {
                    //e.Row.ForeColor = Color.Gray;
                    e.Row.BackColor = Color.LightGreen;
                }

                //Label lblActive = e.Row.FindControl("lbl_assetcode") as Label;

                //lblActive.Font.Bold = true;  





            }
        }

        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.   
            GridView1.EditIndex = e.NewEditIndex;
            FillGrid();
        }
        protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            string scriptstring = "";

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());

            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];


            TextBox txtassetcode = (TextBox)row.Cells[6].Controls[0];
            TextBox txtserial = (TextBox)row.Cells[7].Controls[0];
            TextBox txtconfig = (TextBox)row.Cells[8].Controls[0];


            TextBox txtcapdate = (TextBox)row.Cells[11].Controls[0];
            TextBox txtwedate = (TextBox)row.Cells[12].Controls[0];

            string assetCode = txtassetcode.Text.Trim();
            string serialNo = txtserial.Text.Trim();

            if (Checkduplicateserial(serialNo) == true)
            {
                scriptstring = $"alert('Serial No. {serialNo} already exist in stock');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                return;
            }
            if (Checkduplicateassetcode(assetCode) == true)
            {
                scriptstring = $"alert('Serial No. {assetCode} already exist in stock');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
                return;
            }



            GridView1.EditIndex = -1;

            string strqry = "update Inv_ProductDetail_Master set   " +
                            " ProductDetail_AssetCode='" + txtassetcode.Text + "'," +
                            " ProductDetail_SerialNo='" + txtserial.Text + "'," +
                            " ProductDetail_Config='" + txtconfig.Text + "'," +
                            " ProductDetail_CapDate ='" + txtcapdate.Text + "' ," +
                            " ProductDetail_ExpiryDate='" + txtwedate.Text + "' where ProductDetail_Id='" + id + "'";
            //Response.Write(strqry);
            //Response.End();

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);

            FillGrid();
        }
        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview   
            GridView1.EditIndex = -1;
            FillGrid();
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Asset" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
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

    }
}