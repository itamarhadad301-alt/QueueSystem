using RegularClient.QueueServiceRef;
using System.Windows;
using System.Windows.Controls;

namespace RegularClient
{
    public partial class TakeTicketWindow : Window
    {
        private Service1Client _client = new Service1Client();
        private User _user;

        public TakeTicketWindow(User user)
        {
            InitializeComponent();
            _user = user;
            LoadBranches();
        }

        private void LoadBranches()
        {
            CmbBranch.ItemsSource = _client.GetAllBranches();
            CmbBranch.DisplayMemberPath = "BranchName";
            CmbBranch.SelectedValuePath = "BranchId";
        }

        private void CmbBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBranch.SelectedValue == null) return;
            int branchId = (int)CmbBranch.SelectedValue;
            CmbService.ItemsSource = _client.GetServicesByBranch(branchId);
            CmbService.DisplayMemberPath = "ServiceName";
            CmbService.SelectedValuePath = "ServiceId";
        }

        private void BtnTake_Click(object sender, RoutedEventArgs e)
        {
            if (CmbBranch.SelectedValue == null || CmbService.SelectedValue == null)
            {
                MessageBox.Show("Please select a branch and service", "Error");
                return;
            }

            try
            {
                int branchId = (int)CmbBranch.SelectedValue;
                int serviceId = (int)CmbService.SelectedValue;

                QueueTicket ticket = _client.TakeTicket(_user.UserId, serviceId, branchId);
                MessageBox.Show($"Ticket #{ticket.TicketNumber} taken successfully!\nGo to My Ticket to track your position.", "Success ✅");

                MyTicketWindow w = new MyTicketWindow(_user);
                w.Show();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
