using System;
using System.Web.UI;
using WebClient.QueueServiceRef;

namespace WebClient.Admin
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            User user = (User)Session["User"];

            if (user.Role != "Admin")
            {
                Response.Redirect("~/Home.aspx");
                return;
            }

            if (!IsPostBack)
                LblName.Text = user.FirstName;
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}