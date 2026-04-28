using RegularClient.QueueServiceRef;
using System.Windows;

namespace RegularClient
{
    public partial class MyTicketWindow : Window
    {
        private Service1Client _client = new Service1Client();
        private User _user;
        private QueueTicket _ticket;

        public MyTicketWindow(User user)
        {
            InitializeComponent();
            _user = user;
            LoadTicket();
        }

        private void LoadTicket()
        {
            try
            {
                _ticket = _client.GetActiveTicketByUser(_user.UserId);

                if (_ticket == null)
                {
                    TxtTicketNumber.Text = "---";
                    TxtStatus.Text = "No active ticket";
                    TxtPosition.Text = "";
                    return;
                }

                int position = _client.GetPositionInQueue(_ticket.TicketId, _ticket.BranchId, _ticket.ServiceId);

                TxtTicketNumber.Text = $"#{_ticket.TicketNumber}";
                TxtStatus.Text = $"Status: {_ticket.Status}";
                TxtPosition.Text = position > 0
                    ? $"Position in queue: {position}"
                    : "You are next! 🎉";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => LoadTicket();

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_ticket == null)
            {
                MessageBox.Show("You have no active ticket to cancel", "Info");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure you want to cancel ticket #{_ticket.TicketNumber}?",
                "Confirm Cancel",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _client.CloseTicket(_ticket.TicketId, _user.UserId);
                    MessageBox.Show("Ticket cancelled successfully!", "Success");
                    LoadTicket();
                }
                catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}