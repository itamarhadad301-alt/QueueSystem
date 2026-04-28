using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.QueueServiceRef;

namespace WebClient.Admin
{
    public partial class Services : Page
    {
        private Service1Client _client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null) { Response.Redirect("~/Login.aspx"); return; }
            if (!IsPostBack)
            {
                LoadBranches();
                LoadServices();
            }
        }

        private void LoadBranches()
        {
            DdlBranch.DataSource = _client.GetAllBranches();
            DdlBranch.DataTextField = "BranchName";
            DdlBranch.DataValueField = "BranchId";
            DdlBranch.DataBind();
        }

        private void LoadServices()
        {
            GvServices.DataSource = _client.GetAllServices();
            GvServices.DataBind();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _client.AddService(new Service
                {
                    ServiceName = TxtServiceName.Text.Trim(),
                    BranchId = int.Parse(DdlBranch.SelectedValue),
                    AvgDuration = int.Parse(TxtAvgDuration.Text.Trim())
                });
                ClearForm();
                LoadServices();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _client.UpdateService(new Service
                {
                    ServiceId = (int)ViewState["EditId"],
                    ServiceName = TxtServiceName.Text.Trim(),
                    BranchId = int.Parse(DdlBranch.SelectedValue),
                    AvgDuration = int.Parse(TxtAvgDuration.Text.Trim())

                });
                ClearForm();
                LoadServices();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        protected void GvServices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DeleteRow")
            {
                _client.DeleteService(id);
                LoadServices();
            }
            else if (e.CommandName == "EditRow")
            {
                var services = _client.GetAllServices();
                foreach (var s in services)
                {
                    if (s.ServiceId == id)
                    {
                        TxtServiceName.Text = s.ServiceName;
                        TxtAvgDuration.Text = s.AvgDuration.ToString();

                        DdlBranch.SelectedValue = s.BranchId.ToString();
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
            TxtServiceName.Text = "";
            TxtAvgDuration.Text = "";

            BtnAdd.Visible = true;
            BtnUpdate.Visible = false;
            ViewState["EditId"] = null;
        }

        private void ShowError(string msg) { LblError.Text = msg; LblError.Visible = true; }
    }
}