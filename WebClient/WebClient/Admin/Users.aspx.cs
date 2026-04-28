using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.QueueServiceRef;

namespace WebClient.Admin
{
    public partial class Users : Page
    {
        private Service1Client _client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null) { Response.Redirect("~/Login.aspx"); return; }
            if (!IsPostBack) LoadUsers();
        }

        private void LoadUsers()
        {
            GvUsers.DataSource = _client.GetAllUsers();
            GvUsers.DataBind();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _client.AddUser(new User
                {
                    FirstName = TxtFirstName.Text.Trim(),
                    LastName = TxtLastName.Text.Trim(),
                    Username = TxtUsername.Text.Trim(),
                    Password = TxtPassword.Text.Trim(),
                    Phone = TxtPhone.Text.Trim(),
                    Role = DdlRole.SelectedValue
                });
                ClearForm();
                LoadUsers();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _client.UpdateUser(new User
                {
                    UserId = (int)ViewState["EditId"],
                    FirstName = TxtFirstName.Text.Trim(),
                    LastName = TxtLastName.Text.Trim(),
                    Username = TxtUsername.Text.Trim(),
                    Password = TxtPassword.Text.Trim(),
                    Phone = TxtPhone.Text.Trim(),
                    Role = DdlRole.SelectedValue
                });
                ClearForm();
                LoadUsers();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        protected void GvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DeleteRow")
            {
                _client.DeleteUser(id);
                LoadUsers();
            }
            else if (e.CommandName == "EditRow")
            {
                User u = _client.GetUserById(id);
                TxtFirstName.Text = u.FirstName;
                TxtLastName.Text = u.LastName;
                TxtUsername.Text = u.Username;
                TxtPassword.Text = u.Password;
                TxtPhone.Text = u.Phone;
                DdlRole.SelectedValue = u.Role;
                ViewState["EditId"] = id;
                BtnAdd.Visible = false;
                BtnUpdate.Visible = true;
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            TxtFirstName.Text = TxtLastName.Text = TxtUsername.Text =
            TxtPassword.Text = TxtPhone.Text = "";
            DdlRole.SelectedIndex = 0;
            BtnAdd.Visible = true;
            BtnUpdate.Visible = false;
            ViewState["EditId"] = null;
        }

        private void ShowError(string msg) { LblError.Text = msg; LblError.Visible = true; }
    }
}