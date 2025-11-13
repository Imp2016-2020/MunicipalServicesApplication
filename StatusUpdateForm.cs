using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public class StatusUpdateForm : Form
    {
        private ServiceRequest _request;
        private ComboBox _cmbStatus;
        private TextBox _txtNotes;
        private Button _btnSave;
        private Button _btnCancel;
        private Label _lblStatus;
        private Label _lblNotes;

        public StatusUpdateForm(ServiceRequest requestToUpdate)
        {
            _request = requestToUpdate;
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Form setup
            this.Text = "Update Request Status";
            this.ClientSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = SystemColors.ButtonShadow;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Status Label
            _lblStatus = new Label();
            _lblStatus.Location = new Point(20, 20);
            _lblStatus.Size = new Size(150, 20);
            _lblStatus.Text = "Select New Status:";
            _lblStatus.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(_lblStatus);

            // Status ComboBox
            _cmbStatus = new ComboBox();
            _cmbStatus.Location = new Point(20, 45);
            _cmbStatus.Size = new Size(200, 21);
            _cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            _cmbStatus.Items.AddRange(new object[] { "Submitted", "In Progress", "Resolved", "Closed" });
            _cmbStatus.SelectedItem = _request.Status;
            this.Controls.Add(_cmbStatus);

            // Notes Label
            _lblNotes = new Label();
            _lblNotes.Location = new Point(20, 80);
            _lblNotes.Size = new Size(150, 20);
            _lblNotes.Text = "Update Notes:";
            _lblNotes.Font = new Font("Arial", 9, FontStyle.Bold);
            this.Controls.Add(_lblNotes);

            // Notes TextBox
            _txtNotes = new TextBox();
            _txtNotes.Location = new Point(20, 105);
            _txtNotes.Size = new Size(350, 100);
            _txtNotes.Multiline = true;
            _txtNotes.ScrollBars = ScrollBars.Vertical;
            _txtNotes.Text = "Enter update notes here...";
            this.Controls.Add(_txtNotes);

            // Save Button
            _btnSave = new Button();
            _btnSave.Location = new Point(200, 220);
            _btnSave.Size = new Size(80, 30);
            _btnSave.Text = "Save";
            _btnSave.BackColor = Color.LightGreen;
            _btnSave.Click += SaveButton_Click;
            this.Controls.Add(_btnSave);

            // Cancel Button
            _btnCancel = new Button();
            _btnCancel.Location = new Point(290, 220);
            _btnCancel.Size = new Size(80, 30);
            _btnCancel.Text = "Cancel";
            _btnCancel.BackColor = Color.LightCoral;
            _btnCancel.Click += CancelButton_Click;
            this.Controls.Add(_btnCancel);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _request.Status = _cmbStatus.SelectedItem.ToString();

            // Update resolution date if applicable
            if (_request.Status == "Resolved" || _request.Status == "Closed")
            {
                _request.DateResolved = DateTime.Now;
            }

            // Update assigned department based on status
            if (_request.Status == "In Progress" && string.IsNullOrEmpty(_request.AssignedDepartment))
            {
                _request.AssignedDepartment = GetDepartmentForCategory(_request.Category);
            }

            MessageBox.Show($"Request {_request.RequestId} status updated to: {_request.Status}",
                          "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private string GetDepartmentForCategory(string category)
        {
            if (category == "Roads" || category == "Potholes")
                return "Roads Department";
            else if (category == "Utilities" || category == "Streetlights")
                return "Electrical Department";
            else if (category == "Water" || category == "Sewage")
                return "Water Department";
            else if (category == "Sanitation")
                return "Sanitation Department";
            else if (category == "Traffic")
                return "Traffic Department";
            else
                return "General Services Department";
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}