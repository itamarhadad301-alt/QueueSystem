using AdminClient.QueueServiceRef;
using System.Windows;
using System.Windows.Controls;

namespace AdminClient
{
    public partial class CallQueueWindow : Window
    {
        private Service1Client _client = new Service1Client();
        private User _admin;
        private int _branchId;
        private int _serviceId;

        public CallQueueWindow(User admin)
        {
            InitializeComponent();
            _admin = admin;
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
            _branchId = (int)CmbBranch.SelectedValue;
            var services = _client.GetServicesByBranch(_branchId);
            CmbService.ItemsSource = services;
            CmbService.DisplayMemberPath = "ServiceName";
            CmbService.SelectedValuePath = "ServiceId";
        }

        private void CmbService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbService.SelectedValue == null) return;
            _serviceId = (int)CmbService.SelectedValue;
            LoadQueue();
        }

        private void LoadQueue()
        {
            DgQueue.ItemsSource = _client.GetWaitingTickets(_branchId, _serviceId);
        }

        private void BtnCallNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _client.CallNextTicket(_branchId, _serviceId, _admin.UserId);
                MessageBox.Show("Next ticket called!", "Success ✅");
                LoadQueue();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnSkip_Click(object sender, RoutedEventArgs e)
        {
            var selected = DgQueue.SelectedItem as QueueTicket;
            if (selected == null) { MessageBox.Show("Select a ticket first", "Error"); return; }
            try
            {
                _client.SkipTicket(selected.TicketId, _admin.UserId);
                MessageBox.Show("Ticket skipped!", "Success");
                LoadQueue();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var selected = DgQueue.SelectedItem as QueueTicket;
            if (selected == null) { MessageBox.Show("Select a ticket first", "Error"); return; }
            try
            {
                _client.CloseTicket(selected.TicketId, _admin.UserId);
                MessageBox.Show("Ticket closed!", "Success");
                LoadQueue();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => LoadQueue();
    }
}