using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TileMenu
{
    public partial class SiteMaster : MasterPage
    {
        public DataSet menuDs = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (Session["role"] != null)
            {
                // Retrieve the role from the session
                string role = Session["role"].ToString();

                // Display the role in a label
                hiddenRole.Value = Session["role"].ToString();
            }

        }
    }
}