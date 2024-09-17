using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;

namespace TileMenu
{
    public partial class EmployeeMaster : System.Web.UI.Page
    {
        public string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["Khdconnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillEmptbl();
                FillEmployeeAll();

            }

        }

        protected void Btnsave_Click(object sender, EventArgs e)
        {
            var name = txt_name.Text;
            var code = txt_code.Text;
            var dept = txt_dept.Text;
            var des = txt_designation.Text;
            var phone = txt_number.Text;
            var email = txt_mail.Text;
            var status = ddlStatus.SelectedValue;

            string scriptstring = "";
            string strqry = "INSERT INTO TblEmpMaster(Emp_Code, Emp_Name, Department, Designation, EmailId, Status, Phone_Number) VALUES ('" + code + "', '" + name + "', '" + dept + "', '" + des + "', '" + email + "', '" + status + "', '" + phone + "')";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Added');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            txt_name.Text = "";
            txt_code.Text = "";
            txt_dept.Text = "";
            txt_designation.Text = "";
            txt_number.Text = "";
            txt_mail.Text = "";

            FillEmployeeAll();
            FillEmptbl();
        }

        //protected void EmployeeIndexChanged(object sender, EventArgs e)
        //{


        //}

        protected void FillEmptbl()
        {

            string strqry = "select * from TblEmpMaster ORDER BY Emp_Name";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                tblemp.DataSource = Ds;
                tblemp.DataBind();
            }
            else
            {
                Ds.Clear();
                tblemp.DataSource = Ds;
                tblemp.DataBind();

            }
        }

        protected void FillEmployeeAll()
        {

            string strqry = "select *  from TblEmpMaster order by Emp_Code";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlname.DataSource = Ds.Tables[0];
                ddlname.DataTextField = "Emp_Name";
                ddlname.DataValueField = "Emp_Name";
                ddlname.DataBind();
                ddlname.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }
            else
            {
                ddlname.DataSource = null;
                ddlname.DataBind();
                ddlname.Items.Clear();
                ddlname.Items.Insert(0, new ListItem("-----Select----", "-1"));
            }

        }
        protected void ddlemplChange(object sender, EventArgs e)
        {


            string strqry = "SELECT * FROM TblEmpMaster WHERE Emp_Name = '" + ddlname.SelectedValue + "' ORDER BY Emp_Name";

            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = Ds.Tables[0].Rows[0]; // Assuming you're only interested in the first row

                // Binding values to UI elements
                txtcode1.Text = row["Emp_Code"].ToString();
                txt_department.Text = row["Department"].ToString();
                txtdesignation.Text = row["Designation"].ToString();
                phone1.Text = row["Phone_Number"].ToString();
                email1.Text = row["EmailId"].ToString();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "switchToTab2", "$('.nav-tabs a[href=\"#tab2\"]').tab('show');", true);
        }
        protected void UpdateBtn(object sender, EventArgs e)
        {
            var name = ddlname.SelectedValue;
            var code = txtcode1.Text;
            var dept = txt_department.Text;
            var des = txtdesignation.Text;
            var phone = phone1.Text;
            var email = email1.Text;
            var status = status1.SelectedValue;

            string scriptstring = "";
            string strqry = "UPDATE TblEmpMaster SET Emp_Name = '" + name + "', Department = '" + dept + "', Designation = '" + des + "', EmailId = '" + email + "', Status = '" + status + "', Phone_Number = '" + phone + "' WHERE Emp_Code = '" + code + "'";


            SqlConnection Con = new SqlConnection();
            Con.ConnectionString = strCon;
            SqlDataAdapter Da = new SqlDataAdapter(strqry, Con);
            DataSet Ds = new DataSet();
            Da.Fill(Ds);
            scriptstring = "alert('Successfully Updated');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);
            ddlname.SelectedValue = "-1";
            txtcode1.Text = "";
            txt_department.Text = "";
            txtdesignation.Text = "";
            phone1.Text = "";
            email1.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "switchToTab2", "$('.nav-tabs a[href=\"#tab2\"]').tab('show');", true);
            FillEmptbl();

        }


        protected void DeleteBtn(object sender, EventArgs e)
        {
            var code = txtcode1.Text;

            string scriptstring = "";
            string strqry = "DELETE FROM TblEmpMaster WHERE Emp_Code = @EmpCode";

            using (SqlConnection Con = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(strqry, Con))
                {
                    cmd.Parameters.AddWithValue("@EmpCode", code); // Correctly set parameter value

                    try
                    {
                        Con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            scriptstring = "alert('Successfully Deleted');";
                        }
                        else
                        {
                            scriptstring = "alert('No records found to delete');";
                        }
                    }
                    catch (Exception ex)
                    {
                        scriptstring = $"alert('Error: {ex.Message}');";
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

            // Clear form fields
            ddlname.SelectedIndex = -1;
            txtcode1.Text = "";
            txt_department.Text = "";
            txtdesignation.Text = "";
            phone1.Text = "";
            email1.Text = "";

            // Switch to tab2
            ScriptManager.RegisterStartupScript(this, this.GetType(), "switchToTab2", "$('.nav-tabs a[href=\"#tab2\"]').tab('show');", true);

            // Re-bind data
            FillEmptbl();
            FillEmployeeAll();

        }
    }
}