using RegularClient.QueueServiceRef;
using System.Windows;


namespace RegularClient
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFirstName.Text) ||
                string.IsNullOrWhiteSpace(TxtLastName.Text) ||
                string.IsNullOrWhiteSpace(TxtUsername.Text) ||
                string.IsNullOrWhiteSpace(TxtPassword.Password))
            {
                MessageBox.Show("Please fill in all required fields", "Error");
                return;
            }

            User u = new User
            {
                FirstName = TxtFirstName.Text.Trim(),
                LastName = TxtLastName.Text.Trim(),
                Username = TxtUsername.Text.Trim(),
                Password = TxtPassword.Password.Trim(),
                Phone = TxtPhone.Text.Trim(),
                Email = TxtEmail.Text.Trim(),
                Role = "User"
            };

            Service1Client client = new Service1Client();
            client.Register(u);

            MessageBox.Show("Account created successfully! Please login.", "Success");
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}