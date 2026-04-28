using AdminClient.QueueServiceRef;
using System.Windows;

namespace AdminClient
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

        private void BtnBranches_Click(object sender, RoutedEventArgs e)
        {
            ManageBranchesWindow w = new ManageBranchesWindow();
            w.Show();
        }

        private void BtnServices_Click(object sender, RoutedEventArgs e)
        {
            ManageServicesWindow w = new ManageServicesWindow();
            w.Show();
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersWindow w = new ManageUsersWindow();
            w.Show();
        }

        private void BtnQueue_Click(object sender, RoutedEventArgs e)
        {
            CallQueueWindow w = new CallQueueWindow(_user);
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
