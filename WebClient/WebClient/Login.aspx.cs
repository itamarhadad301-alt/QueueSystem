using System;
using System.Web.UI;
using WebClient.QueueServiceRef;

namespace WebClient
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string password = TxtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                LblError.Text = "Please enter username and password";
                LblError.Visible = true;
                return;
            }

            try
            {
                Service1Client client = new Service1Client();
                User user = client.Login(username, password);

                if (user == null)
                {
                    LblError.Text = "Invalid username or password";
                    LblError.Visible = true;
                    return;
                }

                Session["User"] = user;

                if (user.Role == "Admin")
                    Response.Redirect("~/Admin/Dashboard.aspx");
                else
                    Response.Redirect("~/Home.aspx");
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message;
                LblError.Visible = true;
            }
        }
    }
}