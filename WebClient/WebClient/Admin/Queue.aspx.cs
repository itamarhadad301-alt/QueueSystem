using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.QueueServiceRef;

namespace WebClient.Admin
{
    public partial class Queue : Page
    {
        private Service1Client _client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null) { Response.Redirect("~/Login.aspx"); return; }
            if (!IsPostBack)
            {
                LoadBranches();
            }
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

        protected void DdlService_SelectedIndexChanged(object sender, EventArgs e) => LoadQueue();

        protected void BtnRefresh_Click(object sender, EventArgs e) => LoadQueue();

        private void LoadQueue()
        {
            if (DdlBranch.SelectedValue == "0" || DdlService.SelectedValue == "0") return;
            int branchId = int.Parse(DdlBranch.SelectedValue);
            int serviceId = int.Parse(DdlService.SelectedValue);
            GvQueue.DataSource = _client.GetWaitingTickets(branchId, serviceId);
            GvQueue.DataBind();
        }

        protected void GvQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ticketId = int.Parse(GvQueue.SelectedRow.Cells[1].Text);
            Session["SelectedTicketId"] = ticketId;
        }

        protected void BtnCallNext_Click(object sender, EventArgs e)
        {
            if (DdlBranch.SelectedValue == "0" || DdlService.SelectedValue == "0") return;
            try
            {
                User admin = (User)Session["User"];
                int branchId = int.Parse(DdlBranch.SelectedValue);
                int serviceId = int.Parse(DdlService.SelectedValue);
                _client.CallNextTicket(branchId, serviceId, admin.UserId);
                ShowMsg("Next ticket called! ✅");
                LoadQueue();
            }
            catch (Exception ex) { ShowMsg(ex.Message); }
        }

        protected void BtnSkip_Click(object sender, EventArgs e)
        {
            if (Session["SelectedTicketId"] == null) { ShowMsg("Please select a ticket first"); return; }
            try
            {
                User admin = (User)Session["User"];
                int ticketId = (int)Session["SelectedTicketId"];
                _client.SkipTicket(ticketId, admin.UserId);
                ShowMsg("Ticket skipped ⏭");
                LoadQueue();
            }
            catch (Exception ex) { ShowMsg(ex.Message); }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            if (Session["SelectedTicketId"] == null) { ShowMsg("Please select a ticket first"); return; }
            try
            {
                User admin = (User)Session["User"];
                int ticketId = (int)Session["SelectedTicketId"];
                _client.CloseTicket(ticketId, admin.UserId);
                ShowMsg("Ticket closed ✅");
                Session["SelectedTicketId"] = null;
                LoadQueue();
            }
            catch (Exception ex) { ShowMsg(ex.Message); }
        }

        private void ShowMsg(string msg) { LblMsg.Text = msg; LblMsg.Visible = true; }
    }
}