using RegularClient.QueueServiceRef;
using System.Windows;

namespace RegularClient
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

            Service1Client client = new Service1Client();
            User user = client.Login(username, password);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password", "Error");
                return;
            }

            if (user.Role != "User")
            {
                MessageBox.Show("This app is for regular users only", "Error");
                return;
            }

            // Login success → open main window
            MainWindow main = new MainWindow(user);
            main.Show();
            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Close();
        }
    }
}