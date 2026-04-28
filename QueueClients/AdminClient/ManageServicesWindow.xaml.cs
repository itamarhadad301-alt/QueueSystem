using AdminClient.QueueServiceRef;
using System.Windows;
using System.Windows.Controls;

namespace AdminClient
{
    public partial class ManageServicesWindow : Window
    {
        private Service1Client _client = new Service1Client();
        private Service _selected;

        public ManageServicesWindow()
        {
            InitializeComponent();
            LoadBranches();
            LoadServices();
        }

        private void LoadBranches()
        {
            CmbBranch.ItemsSource = _client.GetAllBranches();
            CmbBranch.DisplayMemberPath = "BranchName";
            CmbBranch.SelectedValuePath = "BranchId";
        }

        private void LoadServices()
        {
            DgServices.ItemsSource = _client.GetAllServices();
        }

        private void DgServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = DgServices.SelectedItem as Service;
            if (_selected == null) return;
            TxtServiceName.Text = _selected.ServiceName;
            CmbBranch.SelectedValue = _selected.BranchId;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Service s = new Service
                {
                    ServiceName = TxtServiceName.Text.Trim(),
                    BranchId = (int)CmbBranch.SelectedValue
                };
                _client.AddService(s);
                MessageBox.Show("Service added!", "Success");
                LoadServices();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Select a service first", "Error"); return; }
            try
            {
                _selected.ServiceName = TxtServiceName.Text.Trim();
                _selected.BranchId = (int)CmbBranch.SelectedValue;
                _client.UpdateService(_selected);
                MessageBox.Show("Service updated!", "Success");
                LoadServices();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Select a service first", "Error"); return; }
            try
            {
                _client.DeleteService(_selected.ServiceId);
                MessageBox.Show("Service deleted!", "Success");
                LoadServices();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
