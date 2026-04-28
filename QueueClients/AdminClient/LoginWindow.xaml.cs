using AdminClient.QueueServiceRef;
using System.Windows;

namespace AdminClient
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string password = TxtPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password", "Error");
                return;
            }

            try
            {
                Service1Client client = new Service1Client();
                User user = client.Login(username, password);

                if (user == null)
                {
                    MessageBox.Show("Invalid username or password", "Error");
                    return;
                }

                if (user.Role != "Admin")
                {
                    MessageBox.Show("Access denied – Admins only!", "Error");
                    return;
                }

                MainWindow main = new MainWindow(user);
                main.Show();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}