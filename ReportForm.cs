using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ReportForm : Form
    {
        // This list will hold all reported issues. It's static so it persists between form instances.
        public static List<IssueReport> ReportedIssues = new List<IssueReport>();
        private static int nextReportId = 1; // To generate unique IDs

        public object MessageBoxButton { get; private set; }
        public object MessageBoxImage { get; private set; }

        public ReportForm()
        {
            InitializeComponent();
            UpdateProgressBar(); // Initialize progress bar on form load
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            // Create and configure an OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Documents|*.pdf;*.docx;*.txt|All Files|*.*";
                openFileDialog.Title = "Select a File to Attach";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file's path and display its name
                    lblAttachmentInfo.Text = Path.GetFileName(openFileDialog.FileName);
                    // Store the full path in the Tag property of the label for later use
                    lblAttachmentInfo.Tag = openFileDialog.FileName;
                }
                UpdateProgressBar(); // Update progress after attempting attachment
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 1. Basic Validation
            if (string.IsNullOrWhiteSpace(txtLocation.Text) ||
                string.IsNullOrWhiteSpace(cmbCategory.Text) ||
                string.IsNullOrWhiteSpace(rtxtDescription.Text))
            {
                MessageBox.Show("Please fill in all required fields: Location, Category, and Description.", "Incomplete Form", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Get the attachment path if one was selected
            string attachmentPath = (lblAttachmentInfo.Tag != null) ? lblAttachmentInfo.Tag.ToString() : string.Empty;

            // 3. Create a new IssueReport object and add it to the list
            IssueReport newReport = new IssueReport(
                id: nextReportId++,
                location: txtLocation.Text,
                category: cmbCategory.Text,
                description: rtxtDescription.Text,
                attachmentPath: attachmentPath
            );
            ReportedIssues.Add(newReport);

            // 4. Show success message with reference number (Key Engagement Feature: Proactive Feedback)
            MessageBox.Show($"Thank you! Your report has been submitted successfully.\n\nYour reference number is: #{newReport.Id}", "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 5. Reset the form for a new entry (optional)
            ClearForm();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Close this form and return to the main menu
            this.Close();
        }

        private void ClearForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtxtDescription.Clear();
            lblAttachmentInfo.Text = "";
            lblAttachmentInfo.Tag = null;
            progressBarReport.Value = 0;
        }

        // ENGAGEMENT FEATURE: Dynamic Progress Bar
        private void UpdateProgressBar()
        {
            int progress = 0;
            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) progress++;
            if (!string.IsNullOrWhiteSpace(cmbCategory.Text)) progress++;
            if (!string.IsNullOrWhiteSpace(rtxtDescription.Text)) progress++;
            if (lblAttachmentInfo.Tag != null) progress++;

            progressBarReport.Value = progress;
        }

        // Event handlers to update the progress bar as the user fills the form
        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }

        private void rtxtDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            // For Windows Forms
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Correct method for Windows Forms
            }
        }

    }
}