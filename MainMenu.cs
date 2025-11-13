using System;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            // Instance of the ReportForm
            ReportForm reportForm = new ReportForm();
            // Show the ReportForm. Using ShowDialog() makes it modal (user must close it to return).
            reportForm.ShowDialog();
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            EventsForm eventsForm = new EventsForm();
            eventsForm.Show();
            this.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void btnRequestStatus_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceRequestStatusForm statusForm = new ServiceRequestStatusForm();
                statusForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Service Request Status: {ex.Message}",
                              "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}