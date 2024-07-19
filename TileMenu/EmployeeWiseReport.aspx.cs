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
    public partial class EmployeeWiseReport : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetDataItemOutNotReturnedNonCons()
        {
            noncons.Visible = true;
            string StrCondition = "";

            string strqry = " select ROW_NUMBER() OVER( ORDER BY Product_InvType Desc ) AS 'Sr.No.',Product_Name as [Product Name],Make_Name as Make,ProdType_Name as Type,ProdModel_Name as Model," +
                " convert(varchar(50),StockOut_IssueDate,106) as [Issued On] ,StockOut_Qty as Qty," +
                " StockOut_EmpCode as [Employee Code],StockOut_EmpName as [Employee Name],StockOut_CostCenter as [Cost Center],StockOut_IssueType as [Issue Type]," +
                " Stockout_Remarks as Remarks,ProductDetail_SerialNo as [Sr.No/DataCard No.]," +
                " ProductDetail_AssetCode as [Asset Code],StockOut_OAC as [OAC],product_id as P_id,Stockout_id as S_id" +
                //"(select convert(varchar(50),StockReturn_ReturnDate,106) from Inv_StockReturn "+
                //" where StockReturn_stockout_Id=stockout_Id) as ItemReturndate, "+
                " from Inv_StockOut " +
                " left outer join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
        " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
        " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
                " left join tblCostCenter on Inv_StockOut.StockOut_CostCenter=tblCostCenter.CostCenter " +
                " where Inv_StockOut.StockOut_Id not in (select StockReturn_StockOut_Id from Inv_StockReturn) and Product_InvType='Non Consumable'";

            //string strqry = " select Product_Name,convert(varchar(50),StockOut_IssueDate,106) as IssueDate ," +
            //    " StockOut_Qty as Qty,StockOut_EmpCode as EmpCode,StockOut_EmpName as EmpName,StockOut_CostCenter as CostCenter," +
            //    " Description,StockOut_IssueType as IssueType from Inv_StockOut " +
            //    " inner join Inv_Product_Master on Inv_StockOut.StockOut_Product_Id=Inv_Product_Master.Product_Id " +
            //    " inner join tblCostCenter on Inv_StockOut.StockOut_CostCenter=tblCostCenter.CostCenter " +
            //    " where 1=1";

            if (txtdatefrom.Text != "")
            {
                StrCondition += " and StockOut_IssueDate>='" + txtdatefrom.Text + "'";
            }
            if (txtdateto.Text != "")
            {
                StrCondition += " and StockOut_IssueDate<='" + txtdateto.Text + "'";
            }
            if (txtEmpCode.Text != "")
            {
                StrCondition += " and StockOut_EmpCode like '%" + txtEmpCode.Text + "%'";
            }
            if (txtEmpName.Text != "")
            {
                StrCondition += " and StockOut_EmpName like '%" + txtEmpName.Text + "%'";
            }
            if (txtcostcenter.Text != "")
            {
                StrCondition += " and (StockOut_CostCenter like '%" + txtcostcenter.Text + "%' or Description like '%" + txtcostcenter.Text + "%')";
            }
            if (txtpName.Text != "")
            {
                StrCondition += " and Product_Name like '%" + txtpName.Text + "%'";
            }
            if (txtassetcode.Text != "")
            {
                StrCondition += " and ProductDetail_AssetCode like '%" + txtassetcode.Text + "%'";
            }
            if (txtsrno.Text != "")
            {
                StrCondition += " and ProductDetail_SerialNo like '%" + txtsrno.Text + "%'";
            }

            string StrFinal = strqry + StrCondition + "ORDER BY StockOut_IssueDate Desc";

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
        protected void GetDataItemOutNotReturnedCons()
        {
            cons.Visible = true;
            string StrCondition = "";

            string strqry = " select ROW_NUMBER() OVER( ORDER BY Product_InvType Desc ) AS 'Sr.No.',Product_Name as [Product Name],Make_Name as Make,ProdType_Name as Type,ProdModel_Name as Model," +
                " convert(varchar(50),StockOut_IssueDate,106) as [Issued On] ,StockOut_Qty as Qty," +
                " StockOut_EmpCode as [Employee Code],StockOut_EmpName as [Employee Name],StockOut_CostCenter as [Cost Center],StockOut_IssueType as [Issue Type]," +
                " Stockout_Remarks as Remarks" +
                //"(select convert(varchar(50),StockReturn_ReturnDate,106) from Inv_StockReturn "+
                //" where StockReturn_stockout_Id=stockout_Id) as ItemReturndate, "+
                " from Inv_StockOut " +
                " left outer join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
        " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
        " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
                " left join tblCostCenter on Inv_StockOut.StockOut_CostCenter=tblCostCenter.CostCenter " +
                " where Inv_StockOut.StockOut_Id not in (select StockReturn_StockOut_Id from Inv_StockReturn) and Product_InvType='Consumable'";

            //string strqry = " select Product_Name,convert(varchar(50),StockOut_IssueDate,106) as IssueDate ," +
            //    " StockOut_Qty as Qty,StockOut_EmpCode as EmpCode,StockOut_EmpName as EmpName,StockOut_CostCenter as CostCenter," +
            //    " Description,StockOut_IssueType as IssueType from Inv_StockOut " +
            //    " inner join Inv_Product_Master on Inv_StockOut.StockOut_Product_Id=Inv_Product_Master.Product_Id " +
            //    " inner join tblCostCenter on Inv_StockOut.StockOut_CostCenter=tblCostCenter.CostCenter " +
            //    " where 1=1";

            if (txtdatefrom.Text != "")
            {
                StrCondition += " and StockOut_IssueDate>='" + txtdatefrom.Text + "'";
            }
            if (txtdateto.Text != "")
            {
                StrCondition += " and StockOut_IssueDate<='" + txtdateto.Text + "'";
            }
            if (txtEmpCode.Text != "")
            {
                StrCondition += " and StockOut_EmpCode like '%" + txtEmpCode.Text + "%'";
            }
            if (txtEmpName.Text != "")
            {
                StrCondition += " and StockOut_EmpName like '%" + txtEmpName.Text + "%'";
            }
            if (txtcostcenter.Text != "")
            {
                StrCondition += " and (StockOut_CostCenter like '%" + txtcostcenter.Text + "%' or Description like '%" + txtcostcenter.Text + "%')";
            }
            if (txtpName.Text != "")
            {
                StrCondition += " and Product_Name like '%" + txtpName.Text + "%'";
            }
            if (txtassetcode.Text != "")
            {
                StrCondition += " and ProductDetail_AssetCode like '%" + txtassetcode.Text + "%'";
            }
            if (txtsrno.Text != "")
            {
                StrCondition += " and ProductDetail_SerialNo like '%" + txtsrno.Text + "%'";
            }

            string StrFinal = strqry + StrCondition + "ORDER BY StockOut_IssueDate Desc";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(StrFinal, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.DataBind();
            }
            else
            {
                Ds.Clear();
                GridView2.DataBind();
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string type1 = (string)e.Row.Cells[10].Text.ToString();

                string prodid = (string)e.Row.Cells[15].Text.ToString();
                string soutid = (string)e.Row.Cells[16].Text.ToString();
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
                            e.Row.Cells[4].Text = Dsmodel.Tables[0].Rows[0][0].ToString();
                        }

                    }
                }


                if (type1 == "Temporary")
                {
                    e.Row.BackColor = Color.Yellow;
                    e.Row.ForeColor = Color.Black;
                }





            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string type1 = (string)e.Row.Cells[10].Text.ToString();




                if (type1 == "Temporary")
                {
                    e.Row.BackColor = Color.Yellow;
                    e.Row.ForeColor = Color.Black;
                }





            }
        }
       
        protected void GetDataItemOut()
        {
            ret.Visible = true;
            string StrCondition = "";

            string strqry = " select Product_Name as [Product Name],Make_Name as Make,ProdType_Name as Type,ProdModel_Name as Model,convert(varchar(50),StockOut_IssueDate,106) as [Issued On] ," +
                " StockOut_Qty as Qty,StockOut_EmpCode as [Employee Code],StockOut_EmpName as [Employee Name],ProductDetail_SerialNo as [Sr.No/DataCard No.]," +
                " ProductDetail_AssetCode as [Asset Code]," +
                " Description,convert(varchar(50),StockReturn_ReturnDate,106) as [Returned On] from Inv_StockOut " +
                " left join Inv_StockReturn on Inv_StockOut.StockOut_Id=Inv_StockReturn.StockReturn_StockOut_Id" +
                " left join tblCostCenter on Inv_StockOut.StockOut_CostCenter=tblCostCenter.CostCenter " +
                " left outer join Inv_ProductDetail_Master on Inv_StockOut.StockOut_ProdDetail_Id=Inv_ProductDetail_Master.ProductDetail_Id" +
                 " inner join Inv_Product_Master on Inv_ProductDetail_Master.ProductDetail_Product_Id=Inv_Product_Master.Product_Id " +
                 " left join Inv_Make_Master on Inv_ProductDetail_Master.ProductDetail_Make_Id=Inv_Make_Master.Make_Id" +
        " left join Inv_ProdType on Inv_ProductDetail_Master.ProductDetail_ProdType_Id=Inv_ProdType.ProdType_Id" +
        " left join inv_ProdModel on Inv_ProductDetail_Master.ProductDetail_ProdModel_Id=inv_ProdModel.ProdModel_Id" +
                " where Inv_StockOut.StockOut_Id in (select StockReturn_StockOut_Id from Inv_StockReturn)";

            if (txtdatefrom.Text != "")
            {
                StrCondition += " and StockOut_IssueDate>='" + txtdatefrom.Text + "'";
            }
            if (txtdateto.Text != "")
            {
                StrCondition += " and StockOut_IssueDate<='" + txtdateto.Text + "'";
            }
            if (txtEmpCode.Text != "")
            {
                StrCondition += " and StockOut_EmpCode like '%" + txtEmpCode.Text + "%'";
            }
            if (txtEmpName.Text != "")
            {
                StrCondition += " and StockOut_EmpName like '%" + txtEmpName.Text + "%'";
            }
            if (txtcostcenter.Text != "")
            {
                StrCondition += " and (StockOut_CostCenter like '%" + txtcostcenter.Text + "%' or Description like '%" + txtcostcenter.Text + "%' )";
            }
            if (txtpName.Text != "")
            {
                StrCondition += " and Product_Name like '%" + txtpName.Text + "%'";
            }
            if (txtassetcode.Text != "")
            {
                StrCondition += " and ProductDetail_AssetCode like '%" + txtassetcode.Text + "%'";
            }
            if (txtsrno.Text != "")
            {
                StrCondition += " and ProductDetail_SerialNo like '%" + txtsrno.Text + "%'";
            }

            string StrFinal = strqry + StrCondition + "order by StockReturn_ReturnDate desc";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(StrFinal, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvItemOut.DataSource = Ds;
                grvItemOut.DataBind();
            }
            else
            {
                Ds.Clear();
                grvItemOut.DataBind();
            }
        }
       


        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            GetDataItemOutNotReturnedNonCons();
            GetDataItemOutNotReturnedCons();
            //GetDataItemIn();
            GetDataItemOut();
            //GetDataItemReturn();


        }


        protected void txtEmpCode_TextChanged(object sender, EventArgs e)
        {
            GetDataItemOutNotReturnedNonCons();
            GetDataItemOutNotReturnedCons();
            //GetDataItemIn();
            GetDataItemOut();
            //GetDataItemReturn();

        }
        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            GetDataItemOutNotReturnedNonCons();
            GetDataItemOutNotReturnedCons();
            //GetDataItemIn();
            GetDataItemOut();
            //GetDataItemReturn();

        }
        protected void txtcostcenter_TextChanged(object sender, EventArgs e)
        {
            GetDataItemOutNotReturnedNonCons();
            GetDataItemOutNotReturnedCons();
            //GetDataItemIn();
            GetDataItemOut();
            //GetDataItemReturn();

        }
        protected void txtpName_TextChanged(object sender, EventArgs e)
        {
            GetDataItemOutNotReturnedNonCons();
            GetDataItemOutNotReturnedCons();
            //GetDataItemIn();
            GetDataItemOut();
            //GetDataItemReturn();

        }
    }
}