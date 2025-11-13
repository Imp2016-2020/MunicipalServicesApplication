using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class EventsForm : Form
    {
        // Advanced Data Structures
        private SortedDictionary<DateTime, List<MunicipalEvent>> eventsByDate;
        private Dictionary<string, List<MunicipalEvent>> eventsByCategory;
        private HashSet<string> eventCategories;
        private Queue<MunicipalEvent> eventNotificationQueue;
        private Stack<MunicipalEvent> recentlyViewedEvents;
        private PriorityQueue<MunicipalEvent> priorityEvents;
        private HashSet<string> userSearchHistory;

        private ListBox lstEvents;
        private TextBox txtSearch;
        private ComboBox cmbCategoryFilter;
        private DateTimePicker dtpDateFilter;
        private Button btnSearch;
        private Button btnBack;
        private Label lblRecommendations;
        private ListBox lstRecommendations;
        private Label lblTitle;
        private RichTextBox rtxtEventDetails;
        private Label lblEvents;
        private Button btnExit;
        private Button btnClearFilters;

        public EventsForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            try
        {
            this.lstEvents = new System.Windows.Forms.ListBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.dtpDateFilter = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblRecommendations = new System.Windows.Forms.Label();
            this.lstRecommendations = new System.Windows.Forms.ListBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.rtxtEventDetails = new System.Windows.Forms.RichTextBox();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.lblEvents = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstEvents
            // 
            this.lstEvents.FormattingEnabled = true;
            this.lstEvents.Location = new System.Drawing.Point(20, 114);
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.Size = new System.Drawing.Size(300, 160);
            this.lstEvents.TabIndex = 0;
            this.lstEvents.SelectedIndexChanged += new System.EventHandler(this.lstEvents_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(20, 40);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Search events...";
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(20, 70);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(150, 21);
            this.cmbCategoryFilter.TabIndex = 2;
            // 
            // dtpDateFilter
            // 
            this.dtpDateFilter.Location = new System.Drawing.Point(180, 70);
            this.dtpDateFilter.Name = "dtpDateFilter";
            this.dtpDateFilter.Size = new System.Drawing.Size(140, 20);
            this.dtpDateFilter.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(230, 38);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(530, 406);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 30);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "Back to Main Menu";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblRecommendations
            // 
            this.lblRecommendations.AutoSize = true;
            this.lblRecommendations.Location = new System.Drawing.Point(347, 98);
            this.lblRecommendations.Name = "lblRecommendations";
            this.lblRecommendations.Size = new System.Drawing.Size(119, 13);
            this.lblRecommendations.TabIndex = 6;
            this.lblRecommendations.Text = "Recommended For You";
            // 
            // lstRecommendations
            // 
            this.lstRecommendations.FormattingEnabled = true;
            this.lstRecommendations.Location = new System.Drawing.Point(350, 114);
            this.lstRecommendations.Name = "lstRecommendations";
            this.lstRecommendations.Size = new System.Drawing.Size(300, 160);
            this.lstRecommendations.TabIndex = 7;
            this.lstRecommendations.SelectedIndexChanged += new System.EventHandler(this.lstRecommendations_SelectedIndexChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(294, 24);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Local Events & Announcements";
            // 
            // rtxtEventDetails
            // 
            this.rtxtEventDetails.Location = new System.Drawing.Point(20, 280);
            this.rtxtEventDetails.Name = "rtxtEventDetails";
            this.rtxtEventDetails.ReadOnly = true;
            this.rtxtEventDetails.Size = new System.Drawing.Size(630, 100);
            this.rtxtEventDetails.TabIndex = 9;
            this.rtxtEventDetails.Text = "Select an event to view details...";
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(330, 68);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(90, 23);
            this.btnClearFilters.TabIndex = 10;
            this.btnClearFilters.Text = "Clear Filters";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // lblEvents
            // 
            this.lblEvents.AutoSize = true;
            this.lblEvents.Location = new System.Drawing.Point(20, 97);
            this.lblEvents.Name = "lblEvents";
            this.lblEvents.Size = new System.Drawing.Size(40, 13);
            this.lblEvents.TabIndex = 11;
            this.lblEvents.Text = "Events";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(24, 407);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 29);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // EventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblEvents);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.rtxtEventDetails);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstRecommendations);
            this.Controls.Add(this.lblRecommendations);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpDateFilter);
            this.Controls.Add(this.cmbCategoryFilter);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lstEvents);
            this.Name = "EventsForm";
            this.Text = "Local Events and Announcements";
            this.Load += new System.EventHandler(this.EventsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating form controls: {ex.Message}", "Control Creation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void EventsForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeDataStructures();
                LoadSampleEvents();
                PopulateCategoryFilter();
                DisplayAllEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form data: {ex.Message}", "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDataStructures()
        {
            try 
            {
            eventsByDate = new SortedDictionary<DateTime, List<MunicipalEvent>>();
            eventsByCategory = new Dictionary<string, List<MunicipalEvent>>();
            eventCategories = new HashSet<string>();
            eventNotificationQueue = new Queue<MunicipalEvent>();
            recentlyViewedEvents = new Stack<MunicipalEvent>();
            priorityEvents = new PriorityQueue<MunicipalEvent>();
            userSearchHistory = new HashSet<string>();
        }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize data structures: {ex.Message}", ex);
            }
        }
        private void LoadSampleEvents()
        {
            try
            { 
            var events = new List<MunicipalEvent>
            {
                new MunicipalEvent(1, "Community Clean-up", "Annual neighborhood clean-up event", "Environment", DateTime.Now.AddDays(2), "City Park", "Municipal Council", 1),
                new MunicipalEvent(2, "Budget Meeting", "Public meeting to discuss municipal budget", "Government", DateTime.Now.AddDays(5), "Town Hall", "Finance Department", 2),
                new MunicipalEvent(3, "Sports Tournament", "Inter-community sports competition", "Sports", DateTime.Now.AddDays(7), "Sports Complex", "Recreation Dept", 3),
                new MunicipalEvent(4, "Art Exhibition", "Local artists showcase their work", "Arts", DateTime.Now.AddDays(10), "Art Gallery", "Cultural Affairs", 2),
                new MunicipalEvent(5, "Emergency Drill", "Public safety emergency preparedness drill", "Safety", DateTime.Now.AddDays(3), "Various Locations", "Emergency Services", 1),
                new MunicipalEvent(6, "Farmers Market", "Weekly local produce market", "Commerce", DateTime.Now.AddDays(1), "Main Square", "Economic Development", 3),
                new MunicipalEvent(7, "Youth Workshop", "Career development workshop for youth", "Education", DateTime.Now.AddDays(14), "Community Center", "Youth Development", 2)
            };

            foreach (var eventItem in events)
                {
                    if (eventItem == null)
                    {
                        MessageBox.Show("Invalid event data encountered.", "Data Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    try

                    {
                if (!eventsByDate.ContainsKey(eventItem.EventDate.Date))
                    eventsByDate[eventItem.EventDate.Date] = new List<MunicipalEvent>();
                eventsByDate[eventItem.EventDate.Date].Add(eventItem);

                if (!eventsByCategory.ContainsKey(eventItem.Category))
                    eventsByCategory[eventItem.Category] = new List<MunicipalEvent>();
                eventsByCategory[eventItem.Category].Add(eventItem);

                eventCategories.Add(eventItem.Category);
                priorityEvents.Enqueue(eventItem);
                eventNotificationQueue.Enqueue(eventItem);
                        if (priorityEvents != null)
                            priorityEvents.Enqueue(eventItem);

                        if (eventNotificationQueue != null)
                            eventNotificationQueue.Enqueue(eventItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding event '{eventItem.Title}': {ex.Message}", "Event Addition Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load sample events: {ex.Message}", ex);
            }
        }

        private void PopulateCategoryFilter()
        {
            try
            {
                if (cmbCategoryFilter == null)
                {
                    MessageBox.Show("Category filter combo box is not available.", "Control Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                cmbCategoryFilter.Items.Clear();
                cmbCategoryFilter.Items.Add("All Categories");
                if (eventCategories != null)
                {
                    foreach (string category in eventCategories.OrderBy(c => c))
                    {
                        if (!string.IsNullOrEmpty(category))
                            cmbCategoryFilter.Items.Add(category);
                    }
                }

                if (cmbCategoryFilter.Items.Count > 0)
                    cmbCategoryFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating category filter: {ex.Message}", "Filter Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayAllEvents()
        {
            try
            {
                if (lstEvents == null)
                {
                    MessageBox.Show("Events list box is not available.", "Control Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lstEvents.Items.Clear();
                if (eventsByDate == null)
                {
                    MessageBox.Show("Events data is not initialized.", "Data Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (var dateGroup in eventsByDate)
                {
                    if (dateGroup.Value != null)
                    {
                        foreach (var eventItem in dateGroup.Value.OrderBy(e => e.EventDate))
                        {
                            if (eventItem != null)
                                lstEvents.Items.Add(eventItem);
                        }
                    }
                }

                GenerateRecommendations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying events: {ex.Message}", "Display Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchEvents()
        {
            try
            {
                if (lstEvents == null || txtSearch == null || cmbCategoryFilter == null || dtpDateFilter == null)
                {
                    MessageBox.Show("Required controls are not available.", "Control Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string searchTerm = txtSearch.Text.ToLower() ?? string.Empty;
                string selectedCategory = cmbCategoryFilter.SelectedItem?.ToString();
                DateTime selectedDate = dtpDateFilter.Value.Date;

                // Add to search history if valid search term
                if (!string.IsNullOrWhiteSpace(searchTerm) && searchTerm != "search events...")
                    userSearchHistory.Add(searchTerm);

                lstEvents.Items.Clear();
                List<MunicipalEvent> filteredEvents = new List<MunicipalEvent>();

                // Fix 1: Handle category filtering properly
                if (selectedCategory != "All Categories" && !string.IsNullOrEmpty(selectedCategory))
                {
                    // Fix 2: Check if category exists before accessing
                    if (eventsByCategory.ContainsKey(selectedCategory))
                    {
                        filteredEvents = new List<MunicipalEvent>(eventsByCategory[selectedCategory]);
                    }
                    else
                    {
                        // If category doesn't exist, start with empty list
                        filteredEvents = new List<MunicipalEvent>();
                    }
                }
                else
                {
                    // Fix 3: Handle "All Categories" case - get all events from all categories
                    filteredEvents = eventsByCategory.Values.SelectMany(x => x).ToList();
                }

                // Fix 4: Apply search term filtering if provided
                if (!string.IsNullOrWhiteSpace(searchTerm) && searchTerm != "search events...")
                {
                    filteredEvents = filteredEvents.Where(e =>
                        e.Title.ToLower().Contains(searchTerm) ||
                        e.Description.ToLower().Contains(searchTerm) ||
                        e.Location.ToLower().Contains(searchTerm))
                        .ToList();
                }

                // Fix 5: Apply date filtering (events on or after selected date)
                filteredEvents = filteredEvents.Where(e => e.EventDate.Date >= selectedDate).ToList();

                // Fix 6: Add ordered events to list
                foreach (var eventItem in filteredEvents.OrderBy(e => e.EventDate))
                {
                    lstEvents.Items.Add(eventItem);
                }

                // Fix 7: Only add to recently viewed if there are results
                if (filteredEvents.Count > 0)
                {
                    recentlyViewedEvents.Push(filteredEvents[0]);
                }

                GenerateRecommendations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching events: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateRecommendations()
        {
            {
                try
                {
                    if (lstRecommendations == null)
                    {
                        MessageBox.Show("Recommendations list box is not available.", "Control Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    lstRecommendations.Items.Clear();

                    if (userSearchHistory.Count == 0 && recentlyViewedEvents.Count == 0)
                    {
                        var recommendations = new List<MunicipalEvent>();
                        var tempPriorityQueue = new PriorityQueue<MunicipalEvent>();

                        foreach (var dateGroup in eventsByDate)
                            foreach (var eventItem in dateGroup.Value)
                                tempPriorityQueue.Enqueue(eventItem);

                        // FIXED: Use IsEmpty property instead of IsEmpty() method
                        for (int i = 0; i < 3 && !tempPriorityQueue.IsEmpty; i++)
                            recommendations.Add(tempPriorityQueue.Dequeue());

                        foreach (var recommendation in recommendations)
                            lstRecommendations.Items.Add(recommendation);
                    }
                    else
                    {
                        var recommendations = new List<MunicipalEvent>();

                        foreach (string searchTerm in userSearchHistory)
                        {
                            var matchingEvents = eventsByDate.Values.SelectMany(x => x)
                                .Where(e => e.Title.ToLower().Contains(searchTerm) || e.Description.ToLower().Contains(searchTerm) || e.Category.ToLower().Contains(searchTerm))
                                .OrderBy(e => e.EventDate).Take(2);
                            recommendations.AddRange(matchingEvents);
                        }

                        var viewedCategories = recentlyViewedEvents.Take(3).Select(e => e.Category).Distinct();
                        foreach (string category in viewedCategories)
                        {
                            if (eventsByCategory.ContainsKey(category))
                                recommendations.AddRange(eventsByCategory[category].OrderBy(e => e.EventDate).Take(2));
                        }

                        var finalRecommendations = recommendations.Distinct().OrderBy(e => e.EventDate).Take(5);
                        foreach (var recommendation in finalRecommendations)
                            lstRecommendations.Items.Add(recommendation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating recommendations: {ex.Message}", "Recommendation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void DisplayEventDetails()
        {
            try
            {
                if (rtxtEventDetails == null)
                {
                    MessageBox.Show("Event details text box is not available.", "Control Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lstEvents.SelectedItem is MunicipalEvent selectedEvent)
            {
                rtxtEventDetails.Text = $@"Title: {selectedEvent.Title}
                Category: {selectedEvent.Category}
                Date: {selectedEvent.EventDate:yyyy-MM-dd HH:mm}
                Location: {selectedEvent.Location}
                Organizer: {selectedEvent.Organizer}

                Description:
                {selectedEvent.Description}";

                    if (recentlyViewedEvents != null)
                        recentlyViewedEvents.Push(selectedEvent);
                }
                else
                {
                    rtxtEventDetails.Text = "Select an event to view details...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying event details: {ex.Message}", "Display Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtxtEventDetails.Text = "Error loading event details.";
            }
        }

        private void DisplayRecommendedEventDetails()
        {
            try
            {
                if (rtxtEventDetails == null)
                {
                    MessageBox.Show("Event details text box is not available.", "Control Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lstRecommendations.SelectedItem is MunicipalEvent selectedEvent)
            {
                rtxtEventDetails.Text = $@"[RECOMMENDED] 
                Title: {selectedEvent.Title}
                Category: {selectedEvent.Category}
                Date: {selectedEvent.EventDate:yyyy-MM-dd HH:mm}
                Location: {selectedEvent.Location}
                Organizer: {selectedEvent.Organizer}

                Description:
{selectedEvent.Description}";

                    if (recentlyViewedEvents != null)
                        recentlyViewedEvents.Push(selectedEvent);
                }
                else
                {
                    rtxtEventDetails.Text = "Select a recommended event to view details...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying recommended event details: {ex.Message}", "Display Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtxtEventDetails.Text = "Error loading event details.";
            }
        }

        private void ClearFilters()
        {
            try
            {
                if (txtSearch == null || cmbCategoryFilter == null || dtpDateFilter == null)
                {
                    MessageBox.Show("Required controls are not available.", "Control Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                txtSearch.Text = "Search events...";
                cmbCategoryFilter.SelectedIndex = 0;
                dtpDateFilter.Value = DateTime.Now;
                DisplayAllEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing filters: {ex.Message}", "Filter Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing search: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void btnSearch_Click(object sender, EventArgs e) => SearchEvents();
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                new MainMenu().Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to main menu: {ex.Message}", "Navigation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstEvents_SelectedIndexChanged(object sender, EventArgs e) => DisplayEventDetails();
        private void lstRecommendations_SelectedIndexChanged(object sender, EventArgs e) => DisplayRecommendedEventDetails();
        private void btnClearFilters_Click(object sender, EventArgs e) => ClearFilters();

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}