using System;
using System.ServiceModel;
using System.Windows;
using WcfServiceLibrary;

namespace HOST
{
    public partial class MainWindow : Window
    {
        private ServiceHost _host;

        public MainWindow()
        {
            InitializeComponent();
            StartService();
        }

        private void StartService()
        {
            try
            {
                _host = new ServiceHost(typeof(Service1));
                _host.Open();
                StatusText.Text = "✅ Service is running!";
            }
            catch (Exception ex)
            {
                StatusText.Text = "❌ Failed to start service";
                MessageBox.Show(ex.Message, "Error");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                if (_host != null)
                    _host.Close();
            }
            catch { }

            base.OnClosed(e);
        }
    }
}