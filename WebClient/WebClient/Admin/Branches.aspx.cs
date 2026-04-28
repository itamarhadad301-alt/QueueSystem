using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.QueueServiceRef;

namespace WebClient.Admin
{
    public partial class Branches : Page
    {
        private Service1Client _client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null) { Response.Redirect("~/Login.aspx"); return; }
            if (!IsPostBack) LoadBranches();
        }

        private void LoadBranches()
        {
            GvBranches.DataSource = _client.GetAllBranches();
            GvBranches.DataBind();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _client.AddBranch(new Branch
                {
                    BranchName = TxtBranchName.Text.Trim(),
                    Address = TxtLocation.Text.Trim()
                });
                ClearForm();
                LoadBranches();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _client.UpdateBranch(new Branch
                {
                    BranchId = (int)ViewState["EditId"],
                    BranchName = TxtBranchName.Text.Trim(),
                    Address = TxtLocation.Text.Trim()
                });
                ClearForm();
                LoadBranches();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        protected void GvBranches_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DeleteRow")
            {
                _client.DeleteBranch(id);
                LoadBranches();
            }
            else if (e.CommandName == "EditRow")
            {
                var branches = _client.GetAllBranches();
                foreach (var b in branches)
                {
                    if (b.BranchId == id)
                    {
                        TxtBranchName.Text = b.BranchName;
                        TxtLocation.Text = b.Address;
                        ViewState["EditId"] = id;
                        BtnAdd.Visible = false;
                        BtnUpdate.Visible = true;
                        break;
                    }
                }
            }
        }

        protected void BtnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            TxtBranchName.Text = "";
            TxtLocation.Text = "";
            BtnAdd.Visible = true;
            BtnUpdate.Visible = false;
            ViewState["EditId"] = null;
        }

        private void ShowError(string msg)
        {
            LblError.Text = msg;
            LblError.Visible = true;
        }
    }
}