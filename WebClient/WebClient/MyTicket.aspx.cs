using System;
using System.Web.UI;
using WebClient.QueueServiceRef;

namespace WebClient
{
    public partial class MyTicket : Page
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
                LoadTicket();
        }

        private void LoadTicket()
        {
            User user = (User)Session["User"];
            QueueTicket ticket = _client.GetActiveTicketByUser(user.UserId);

            if (ticket == null)
            {
                PnlTicket.Visible = false;
                PnlNoTicket.Visible = true;
                return;
            }

            int position = _client.GetPositionInQueue(ticket.TicketId, ticket.BranchId, ticket.ServiceId);

            PnlTicket.Visible = true;
            PnlNoTicket.Visible = false;
            LblTicketNumber.Text = $"#{ticket.TicketNumber}";
            LblStatus.Text = $"Status: {ticket.Status}";
            LblPosition.Text = position > 0
                ? $"Position in queue: {position}"
                : "🎉 You are next!";

            Session["Ticket"] = ticket;
        }

        protected void BtnRefresh_Click(object sender, EventArgs e) => LoadTicket();

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                User user = (User)Session["User"];
                QueueTicket ticket = (QueueTicket)Session["Ticket"];

                _client.CloseTicket(ticket.TicketId, user.UserId);
                Session["Ticket"] = null;
                LoadTicket();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}');</script>");
            }
        }
    }
}