using AdminClient.QueueServiceRef;
using System.Windows;
using System.Windows.Controls;

namespace AdminClient
{
    public partial class ManageUsersWindow : Window
    {
        private Service1Client _client = new Service1Client();
        private User _selected;

        public ManageUsersWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            DgUsers.ItemsSource = _client.GetAllUsers();
        }

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = DgUsers.SelectedItem as User;
            if (_selected == null) return;
            TxtFirstName.Text = _selected.FirstName;
            TxtLastName.Text = _selected.LastName;
            TxtUsername.Text = _selected.Username;
            TxtPhone.Text = _selected.Phone;
            CmbRole.Text = _selected.Role;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User u = new User
                {
                    FirstName = TxtFirstName.Text.Trim(),
                    LastName = TxtLastName.Text.Trim(),
                    Username = TxtUsername.Text.Trim(),
                    Password = TxtPassword.Password.Trim(),
                    Phone = TxtPhone.Text.Trim(),
                    Role = (CmbRole.SelectedItem as ComboBoxItem)?.Content.ToString()
                };
                _client.Register(u);
                MessageBox.Show("User added!", "Success");
                LoadUsers();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Select a user first", "Error"); return; }
            try
            {
                _selected.FirstName = TxtFirstName.Text.Trim();
                _selected.LastName = TxtLastName.Text.Trim();
                _selected.Username = TxtUsername.Text.Trim();
                _selected.Phone = TxtPhone.Text.Trim();
                _selected.Role = (CmbRole.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (!string.IsNullOrWhiteSpace(TxtPassword.Password))
                    _selected.Password = TxtPassword.Password.Trim();
                _client.UpdateUser(_selected);
                MessageBox.Show("User updated!", "Success");
                LoadUsers();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Select a user first", "Error"); return; }
            try
            {
                _client.DeleteUser(_selected.UserId);
                MessageBox.Show("User deleted!", "Success");
                LoadUsers();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
