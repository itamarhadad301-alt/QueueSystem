using RegularClient.QueueServiceRef;
using System.Windows;

namespace RegularClient
{
    public partial class MainWindow : Window
    {
        private User _user;

        public MainWindow(User user)
        {
            InitializeComponent();
            _user = user;
            TxtWelcome.Text = $"Welcome, {_user.FirstName}!";
        }

        private void BtnTakeTicket_Click(object sender, RoutedEventArgs e)
        {
            TakeTicketWindow w = new TakeTicketWindow(_user);
            w.Show();
        }

        private void BtnMyTicket_Click(object sender, RoutedEventArgs e)
        {
            MyTicketWindow w = new MyTicketWindow(_user);
            w.Show();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
