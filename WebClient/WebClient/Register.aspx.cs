using System;
using System.Web.UI;
using WebClient.QueueServiceRef;

namespace WebClient
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFirstName.Text) ||
                string.IsNullOrWhiteSpace(TxtLastName.Text) ||
                string.IsNullOrWhiteSpace(TxtUsername.Text) ||
                string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                LblError.Text = "Please fill all required fields";
                LblError.Visible = true;
                return;
            }

            try
            {
                Service1Client client = new Service1Client();
                User u = new User
                {
                    FirstName = TxtFirstName.Text.Trim(),
                    LastName = TxtLastName.Text.Trim(),
                    Username = TxtUsername.Text.Trim(),
                    Password = TxtPassword.Text.Trim(),
                    Phone = TxtPhone.Text.Trim(),
                    Role = "User"
                };
                client.Register(u);
                Response.Redirect("~/Login.aspx");
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message;
                LblError.Visible = true;
            }
        }
    }
}