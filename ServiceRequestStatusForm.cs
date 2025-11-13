using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ServiceRequestStatusForm : Form
    {
        // Advanced Data Structures for Part 3
        private BinarySearchTree<ServiceRequest> _requestsBST;
        private Dictionary<string, ServiceRequest> _requestsDictionary;
        private ServiceRequestPriorityQueue _priorityQueue;
        private Graph _serviceRequestGraph;
        private MinHeap<ServiceRequest> _statusHeap;

        // UI Controls
        private ListBox _lstRequests;
        private TextBox _txtSearchRequest;
        private Button _btnSearch;
        private Button _btnBack;
        private RichTextBox _rtxtRequestDetails;
        private ComboBox _cmbStatusFilter;
        private ComboBox _cmbPriorityFilter;
        private Label _lblTitle;
        private Button _btnUpdateStatus;
        private Button _btnGenerateReport;
        private Label _lblGraphInfo;
        private Label _lblSearch;
        private Label _lblStatusFilter;
        private Label _lblPriorityFilter;

        public ServiceRequestStatusForm()
        {
            InitializeForm();
            InitializeAdvancedDataStructures();
            LoadSampleRequests();
            DisplayAllRequests();
        }
        private void InitializeForm()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            // Form Properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Service Request Status";
            this.ClientSize = new Size(800, 600);
            this.BackColor = SystemColors.ButtonShadow;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Title Label
            _lblTitle = new Label();
            _lblTitle.Location = new Point(20, 15);
            _lblTitle.Size = new Size(400, 30);
            _lblTitle.Text = "Service Request Status Tracking";
            _lblTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            _lblTitle.ForeColor = Color.DarkBlue;
            this.Controls.Add(_lblTitle);

            // Search and Filter GroupBox
            GroupBox grpSearchFilter = new GroupBox();
            grpSearchFilter.Location = new Point(20, 50);
            grpSearchFilter.Size = new Size(760, 60);
            grpSearchFilter.Text = "Search & Filter";
            grpSearchFilter.Font = new Font("Arial", 8, FontStyle.Bold);

            // Search Label
            _lblSearch = new Label();
            _lblSearch.Location = new Point(10, 25);
            _lblSearch.Size = new Size(65, 20);
            _lblSearch.Text = "Search ID:";
            _lblSearch.TextAlign = ContentAlignment.MiddleLeft;
            grpSearchFilter.Controls.Add(_lblSearch);

            // Search TextBox
            _txtSearchRequest = new TextBox();
            _txtSearchRequest.Location = new Point(80, 25);
            _txtSearchRequest.Size = new Size(100, 20);
            _txtSearchRequest.Text = "REQ001";
            grpSearchFilter.Controls.Add(_txtSearchRequest);

            // Search Button
            _btnSearch = new Button();
            _btnSearch.Location = new Point(185, 24);
            _btnSearch.Size = new Size(60, 23);
            _btnSearch.Text = "Search";
            _btnSearch.BackColor = Color.LightBlue;
            _btnSearch.Click += SearchButton_Click;
            grpSearchFilter.Controls.Add(_btnSearch);

            // Status Filter Label
            _lblStatusFilter = new Label();
            _lblStatusFilter.Location = new Point(260, 25);
            _lblStatusFilter.Size = new Size(45, 20);
            _lblStatusFilter.Text = "Status:";
            _lblStatusFilter.TextAlign = ContentAlignment.MiddleLeft;
            grpSearchFilter.Controls.Add(_lblStatusFilter);

            // Status Filter ComboBox
            _cmbStatusFilter = new ComboBox();
            _cmbStatusFilter.Location = new Point(310, 25);
            _cmbStatusFilter.Size = new Size(100, 21);
            _cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            _cmbStatusFilter.Items.AddRange(new object[] { "All Status", "Submitted", "In Progress", "Resolved", "Closed" });
            _cmbStatusFilter.SelectedIndex = 0;
            _cmbStatusFilter.SelectedIndexChanged += FilterChanged;
            grpSearchFilter.Controls.Add(_cmbStatusFilter);

            // Priority Filter Label
            _lblPriorityFilter = new Label();
            _lblPriorityFilter.Location = new Point(420, 25);
            _lblPriorityFilter.Size = new Size(50, 20);
            _lblPriorityFilter.Text = "Priority:";
            _lblPriorityFilter.TextAlign = ContentAlignment.MiddleLeft;
            grpSearchFilter.Controls.Add(_lblPriorityFilter);

            // Priority Filter ComboBox
            _cmbPriorityFilter = new ComboBox();
            _cmbPriorityFilter.Location = new Point(475, 25);
            _cmbPriorityFilter.Size = new Size(100, 21);
            _cmbPriorityFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            _cmbPriorityFilter.Items.AddRange(new object[] { "All Priorities", "High", "Medium", "Low" });
            _cmbPriorityFilter.SelectedIndex = 0;
            _cmbPriorityFilter.SelectedIndexChanged += FilterChanged;
            grpSearchFilter.Controls.Add(_cmbPriorityFilter);

            this.Controls.Add(grpSearchFilter);

            // Requests ListBox
            _lstRequests = new ListBox();
            _lstRequests.Location = new Point(20, 130);
            _lstRequests.Size = new Size(400, 300);
            _lstRequests.Font = new Font("Consolas", 9);
            _lstRequests.SelectedIndexChanged += RequestsList_SelectedIndexChanged;
            this.Controls.Add(_lstRequests);

            // Request Details RichTextBox
            _rtxtRequestDetails = new RichTextBox();
            _rtxtRequestDetails.Location = new Point(430, 130);
            _rtxtRequestDetails.Size = new Size(350, 250);
            _rtxtRequestDetails.ReadOnly = true;
            _rtxtRequestDetails.Font = new Font("Consolas", 9);
            _rtxtRequestDetails.BackColor = Color.White;
            this.Controls.Add(_rtxtRequestDetails);

            // Action Buttons GroupBox
            GroupBox grpActions = new GroupBox();
            grpActions.Location = new Point(430, 390);
            grpActions.Size = new Size(350, 50);
            grpActions.Text = "Actions";
            grpActions.Font = new Font("Arial", 8, FontStyle.Bold);

            // Update Status Button
            _btnUpdateStatus = new Button();
            _btnUpdateStatus.Location = new Point(20, 20);
            _btnUpdateStatus.Size = new Size(120, 25);
            _btnUpdateStatus.Text = "Update Status";
            _btnUpdateStatus.BackColor = Color.LightGreen;
            _btnUpdateStatus.Click += UpdateStatusButton_Click;
            grpActions.Controls.Add(_btnUpdateStatus);

            // Generate Report Button
            _btnGenerateReport = new Button();
            _btnGenerateReport.Location = new Point(150, 20);
            _btnGenerateReport.Size = new Size(120, 25);
            _btnGenerateReport.Text = "Generate Report";
            _btnGenerateReport.BackColor = Color.LightYellow;
            _btnGenerateReport.Click += GenerateReportButton_Click;
            grpActions.Controls.Add(_btnGenerateReport);

            this.Controls.Add(grpActions);

            // Graph Analysis GroupBox
            GroupBox grpAnalysis = new GroupBox();
            grpAnalysis.Location = new Point(20, 440);
            grpAnalysis.Size = new Size(400, 120);
            grpAnalysis.Text = "Graph Analysis";
            grpAnalysis.Font = new Font("Arial", 8, FontStyle.Bold);

            // Graph Info Label
            _lblGraphInfo = new Label();
            _lblGraphInfo.Location = new Point(10, 20);
            _lblGraphInfo.Size = new Size(380, 90);
            _lblGraphInfo.BackColor = Color.White;
            _lblGraphInfo.Font = new Font("Consolas", 8);
            _lblGraphInfo.Text = "Graph Analysis will appear here...\n\nUse Generate Report to see detailed analysis.";
            grpAnalysis.Controls.Add(_lblGraphInfo);

            this.Controls.Add(grpAnalysis);

            // Back Button
            _btnBack = new Button();
            _btnBack.Location = new Point(660, 520);
            _btnBack.Size = new Size(120, 30);
            _btnBack.Text = "Back to Main";
            _btnBack.BackColor = Color.LightCoral;
            _btnBack.Click += BackButton_Click;
            this.Controls.Add(_btnBack);

            this.ResumeLayout(false);
        }


        private void InitializeAdvancedDataStructures()
        {
            _requestsBST = new BinarySearchTree<ServiceRequest>();
            _requestsDictionary = new Dictionary<string, ServiceRequest>();
            _priorityQueue = new ServiceRequestPriorityQueue();
            _serviceRequestGraph = new Graph();
            _statusHeap = new MinHeap<ServiceRequest>();
        }

        private void LoadSampleRequests()
        {
            try
            {
                var sampleRequests = new List<ServiceRequest>
                {
                    new ServiceRequest("REQ001", "Pothole Repair", "Roads", "Large pothole on Main Street causing traffic issues",
                                     "123 Main Street", "John Smith", "john@email.com", 1),
                    new ServiceRequest("REQ002", "Street Light Out", "Utilities", "Street light not working for 3 days",
                                     "456 Oak Avenue", "Sarah Johnson", "sarah@email.com", 2),
                    new ServiceRequest("REQ003", "Water Leak", "Water", "Water leaking from fire hydrant near park",
                                     "789 Pine Road", "Mike Brown", "mike@email.com", 1),
                    new ServiceRequest("REQ004", "Illegal Dumping", "Sanitation", "Garbage dumped in public park area",
                                     "City Park Area", "Emily Davis", "emily@email.com", 3),
                    new ServiceRequest("REQ005", "Traffic Signal Issue", "Traffic", "Malfunctioning traffic light at busy intersection",
                                     "Main & 1st Intersection", "Robert Wilson", "robert@email.com", 1)
                };

                // Update some statuses for variety
                sampleRequests[1].Status = "In Progress";
                sampleRequests[1].AssignedDepartment = "Electrical Department";

                sampleRequests[3].Status = "Resolved";
                sampleRequests[3].DateResolved = DateTime.Now.AddDays(-1);
                sampleRequests[3].AssignedDepartment = "Sanitation Department";

                sampleRequests[4].Status = "In Progress";
                sampleRequests[4].AssignedDepartment = "Traffic Department";

                foreach (var request in sampleRequests)
                {
                    AddRequestToStructures(request);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sample requests: {ex.Message}", "Initialization Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRequestToStructures(ServiceRequest request)
        {
            try
            {
                // Binary Search Tree
                _requestsBST.Insert(request);

                // Dictionary for quick lookup
                _requestsDictionary[request.RequestId] = request;

                // Priority Queue
                _priorityQueue.Enqueue(request);

                // Graph - create relationships
                _serviceRequestGraph.AddRequest(request);

                // Heap for status management
                _statusHeap.Insert(request);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding request to data structures: {ex.Message}", "Data Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DisplayAllRequests()
        {
            try
            {
                _lstRequests.Items.Clear();
                var allRequests = _requestsBST.InOrderTraversal();

                foreach (var request in allRequests)
                {
                    string statusIcon = GetStatusIcon(request.Status);
                    string priorityIcon = GetPriorityIcon(request.Priority);

                    _lstRequests.Items.Add($"{statusIcon} {priorityIcon} {request.RequestId}: {request.Title}");
                }

                UpdateGraphAnalysis();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying requests: {ex.Message}", "Display Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetStatusIcon(string status)
        {
            if (status == "Submitted") return "⏳";
            if (status == "In Progress") return "🔧";
            if (status == "Resolved") return "✅";
            if (status == "Closed") return "🔒";
            return "📋";
        }

        private string GetPriorityIcon(int priority)
        {
            if (priority == 1) return "🔴";
            if (priority == 2) return "🟡";
            if (priority == 3) return "🟢";
            return "⚪";
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            try
            {
                _lstRequests.Items.Clear();
                var filteredRequests = _requestsBST.InOrderTraversal();

                // Apply status filter
                string statusFilter = _cmbStatusFilter.SelectedItem?.ToString() ?? "All Status";
                if (statusFilter != "All Status")
                {
                    filteredRequests = filteredRequests.Where(r => r.Status == statusFilter).ToList();
                }

                // Apply priority filter
                string priorityFilter = _cmbPriorityFilter.SelectedItem?.ToString() ?? "All Priorities";
                if (priorityFilter != "All Priorities")
                {
                    int priorityValue = priorityFilter == "High" ? 1 : priorityFilter == "Medium" ? 2 : 3;
                    filteredRequests = filteredRequests.Where(r => r.Priority == priorityValue).ToList();
                }

                foreach (var request in filteredRequests)
                {
                    string statusIcon = GetStatusIcon(request.Status);
                    string priorityIcon = GetPriorityIcon(request.Priority);

                    _lstRequests.Items.Add($"{statusIcon} {priorityIcon} {request.RequestId}: {request.Title}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Filter Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RequestsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_lstRequests.SelectedIndex >= 0)
                {
                    string selectedText = _lstRequests.SelectedItem.ToString();
                    string requestId = selectedText.Split(':')[0].Split(' ').Last();

                    if (_requestsDictionary.ContainsKey(requestId))
                    {
                        var selectedRequest = _requestsDictionary[requestId];
                        DisplayRequestDetails(selectedRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting request: {ex.Message}", "Selection Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayRequestDetails(ServiceRequest request)
        {
            try
            {
                string priorityText = request.Priority == 1 ? "HIGH 🔴" :
                                    request.Priority == 2 ? "MEDIUM 🟡" : "LOW 🟢";

                _rtxtRequestDetails.Text = $@"
REQUEST ID: {request.RequestId}
TITLE: {request.Title}
CATEGORY: {request.Category}
STATUS: {request.Status}
PRIORITY: {priorityText}

LOCATION: {request.Location}
DATE SUBMITTED: {request.DateSubmitted:yyyy-MM-dd HH:mm}

DESCRIPTION:
{request.Description}

CITIZEN INFORMATION:
Name: {request.CitizenName}
Contact: {request.ContactInfo}

ASSIGNMENT:
Department: {request.AssignedDepartment ?? "Not assigned"}
Date Resolved: {request.DateResolved?.ToString("yyyy-MM-dd") ?? "Not resolved"}

Last Updated: {DateTime.Now:yyyy-MM-dd HH:mm}
";
            }
            catch (Exception ex)
            {
                _rtxtRequestDetails.Text = $"Error loading request details: {ex.Message}";
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string searchId = _txtSearchRequest.Text.Trim().ToUpper();
                if (string.IsNullOrWhiteSpace(searchId))
                {
                    MessageBox.Show("Please enter a Request ID to search.", "Search Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_requestsDictionary.ContainsKey(searchId))
                {
                    var request = _requestsDictionary[searchId];
                    DisplayRequestDetails(request);

                    // Highlight in list
                    for (int i = 0; i < _lstRequests.Items.Count; i++)
                    {
                        if (_lstRequests.Items[i].ToString().Contains(searchId))
                        {
                            _lstRequests.SelectedIndex = i;
                            break;
                        }
                    }

                    MessageBox.Show($"Request {searchId} found!", "Search Successful",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Request ID '{searchId}' not found.", "Search Result",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Search Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_lstRequests.SelectedIndex >= 0)
                {
                    string selectedText = _lstRequests.SelectedItem.ToString();
                    string requestId = selectedText.Split(':')[0].Split(' ').Last();

                    if (_requestsDictionary.ContainsKey(requestId))
                    {
                        var selectedRequest = _requestsDictionary[requestId];
                        var statusForm = new StatusUpdateForm(selectedRequest);

                        if (statusForm.ShowDialog() == DialogResult.OK)
                        {
                            // Refresh display
                            DisplayAllRequests();
                            DisplayRequestDetails(selectedRequest);
                            UpdateGraphAnalysis();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a request to update.", "Selection Required",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}", "Update Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReportButton_Click(object sender, EventArgs e)
        {
            GenerateComprehensiveReport();
        }

        private void GenerateComprehensiveReport()
        {
            try
            {
                var report = new StringBuilder();
                report.AppendLine("SERVICE REQUEST ANALYSIS REPORT");
                report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}");
                report.AppendLine("=================================");

                var allRequests = _requestsBST.InOrderTraversal();

                // 1. Tree-based analysis
                report.AppendLine("\n1. BINARY SEARCH TREE ANALYSIS");
                report.AppendLine($"   Total Requests: {allRequests.Count}");
                report.AppendLine($"   Tree Height: {CalculateTreeHeight()} (estimated)");

                // 2. Heap-based priority analysis
                report.AppendLine("\n2. PRIORITY ANALYSIS");
                var highPriority = allRequests.Count(r => r.Priority == 1);
                var mediumPriority = allRequests.Count(r => r.Priority == 2);
                var lowPriority = allRequests.Count(r => r.Priority == 3);
                report.AppendLine($"   High Priority: {highPriority}");
                report.AppendLine($"   Medium Priority: {mediumPriority}");
                report.AppendLine($"   Low Priority: {lowPriority}");

                // 3. Graph analysis
                report.AppendLine("\n3. GRAPH ANALYSIS");
                report.AppendLine(_serviceRequestGraph.GetAnalysisReport());

                // 4. Status distribution
                report.AppendLine("\n4. STATUS DISTRIBUTION");
                var statusGroups = allRequests.GroupBy(r => r.Status)
                                            .OrderByDescending(g => g.Count());

                foreach (var group in statusGroups)
                {
                    report.AppendLine($"   {group.Key}: {group.Count()} requests");
                }

                _rtxtRequestDetails.Text = report.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Report Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CalculateTreeHeight()
        {
            // Simplified tree height calculation
            var allRequests = _requestsBST.InOrderTraversal();
            return (int)Math.Ceiling(Math.Log(allRequests.Count + 1, 2));
        }

        private void UpdateGraphAnalysis()
        {
            try
            {
                _lblGraphInfo.Text = _serviceRequestGraph.GetVisualization();
            }
            catch (Exception ex)
            {
                _lblGraphInfo.Text = $"Error updating graph analysis: {ex.Message}";
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                new MainMenu().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating back: {ex.Message}", "Navigation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}