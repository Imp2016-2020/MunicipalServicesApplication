namespace MunicipalServicesApp
{
    partial class MainMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.btnRequestStatus = new System.Windows.Forms.Button();
            this.toolTipFutureFeature = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnReportIssues
            // 
            this.btnReportIssues.Location = new System.Drawing.Point(100, 50);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(200, 40);
            this.btnReportIssues.TabIndex = 0;
            this.btnReportIssues.Text = "Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = true;
            this.btnReportIssues.Click += new System.EventHandler(this.btnReportIssues_Click);
            // 
            // btnLocalEvents
            // 
            this.btnLocalEvents.Location = new System.Drawing.Point(100, 110);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(200, 40);
            this.btnLocalEvents.TabIndex = 1;
            this.btnLocalEvents.Text = "Local Events and Announcements";
            this.toolTipFutureFeature.SetToolTip(this.btnLocalEvents, "Thi feature is cooming soon! Check back later for local events and announcements." +
        "");
            this.btnLocalEvents.UseVisualStyleBackColor = true;
            this.btnLocalEvents.Click += new System.EventHandler(this.btnLocalEvents_Click);
            // 
            // btnRequestStatus
            // 
            this.btnRequestStatus.Location = new System.Drawing.Point(100, 170);
            this.btnRequestStatus.Name = "btnRequestStatus";
            this.btnRequestStatus.Size = new System.Drawing.Size(200, 40);
            this.btnRequestStatus.TabIndex = 2;
            this.btnRequestStatus.Text = "Service Request Status";
            this.toolTipFutureFeature.SetToolTip(this.btnRequestStatus, "This feature is coming soon! You will soon be able to check the status of your se" +
        "rvice request here.");
            this.btnRequestStatus.UseVisualStyleBackColor = true;
            this.btnRequestStatus.Click += new System.EventHandler(this.btnRequestStatus_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.btnRequestStatus);
            this.Controls.Add(this.btnLocalEvents);
            this.Controls.Add(this.btnReportIssues);
            this.Name = "MainMenu";
            this.Text = "Municipal Services - Main Menu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReportIssues;
        private System.Windows.Forms.Button btnLocalEvents;
        private System.Windows.Forms.Button btnRequestStatus;
        private System.Windows.Forms.ToolTip toolTipFutureFeature;
    }
}