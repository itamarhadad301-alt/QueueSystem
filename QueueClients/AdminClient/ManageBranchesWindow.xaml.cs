using AdminClient.QueueServiceRef;
using System.Windows;
using System.Windows.Controls;

namespace AdminClient
{
    public partial class ManageBranchesWindow : Window
    {
        private Service1Client _client = new Service1Client();
        private Branch _selected;

        public ManageBranchesWindow()
        {
            InitializeComponent();
            LoadBranches();
        }

        private void LoadBranches()
        {
            DgBranches.ItemsSource = _client.GetAllBranches();
        }

        private void DgBranches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = DgBranches.SelectedItem as Branch;
            if (_selected == null) return;
            TxtBranchName.Text = _selected.BranchName;
            TxtLocation.Text = _selected.Address;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Branch b = new Branch
                {
                    BranchName = TxtBranchName.Text.Trim(),
                    Address = TxtLocation.Text.Trim()
                };
                _client.AddBranch(b);
                MessageBox.Show("Branch added!", "Success");
                LoadBranches();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Select a branch first", "Error"); return; }
            try
            {
                _selected.BranchName = TxtBranchName.Text.Trim();
                _selected.Address = TxtLocation.Text.Trim();
                _client.UpdateBranch(_selected);
                MessageBox.Show("Branch updated!", "Success");
                LoadBranches();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Select a branch first", "Error"); return; }
            try
            {
                _client.DeleteBranch(_selected.BranchId);
                MessageBox.Show("Branch deleted!", "Success");
                LoadBranches();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
