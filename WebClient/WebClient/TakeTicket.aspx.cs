using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.QueueServiceRef;

namespace WebClient
{
    public partial class TakeTicket : Page
    {
        private Service1Client _client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadBranches();
        }

        private void LoadBranches()
        {
            DdlBranch.DataSource = _client.GetAllBranches();
            DdlBranch.DataTextField = "BranchName";
            DdlBranch.DataValueField = "BranchId";
            DdlBranch.DataBind();
            DdlBranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            DdlService.Items.Clear();
            DdlService.Items.Insert(0, new ListItem("-- Select Service --", "0"));
        }

        protected void DdlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlService.Items.Clear();

            if (DdlBranch.SelectedValue == "0")
            {
                DdlService.Items.Insert(0, new ListItem("-- Select Service --", "0"));
                return;
            }

            int branchId = int.Parse(DdlBranch.SelectedValue);
            DdlService.DataSource = _client.GetServicesByBranch(branchId);
            DdlService.DataTextField = "ServiceName";
            DdlService.DataValueField = "ServiceId";
            DdlService.DataBind();
            DdlService.Items.Insert(0, new ListItem("-- Select Service --", "0"));
        }

        protected void BtnTake_Click(object sender, EventArgs e)
        {
            if (DdlBranch.SelectedValue == "0" || DdlService.SelectedValue == "0")
            {
                LblError.Text = "Please select a branch and service";
                LblError.Visible = true;
                return;
            }

            try
            {
                User user = (User)Session["User"];
                int branchId = int.Parse(DdlBranch.SelectedValue);
                int serviceId = int.Parse(DdlService.SelectedValue);

                QueueTicket ticket = _client.TakeTicket(user.UserId, serviceId, branchId);
                Session["Ticket"] = ticket;
                Response.Redirect("~/MyTicket.aspx");
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message;
                LblError.Visible = true;
            }
        }
    }
}