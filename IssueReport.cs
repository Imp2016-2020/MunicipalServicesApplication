using System;

namespace MunicipalServicesApp
{
    public class IssueReport
    {
        // A unique identifier for each report
        public int Id { get; set; }

        // The location entered by the user
        public string Location { get; set; }

        // The category selected from the dropdown
        public string Category { get; set; }

        // The detailed description
        public string Description { get; set; }

        // The file path to the attached image/document
        public string AttachmentPath { get; set; }

        // The date and time the report was submitted
        public DateTime ReportDate { get; set; }

        // A status for the report (e.g., Submitted, In Progress, Resolved)
        public string Status { get; set; } = "Submitted"; // Default status

        // Constructor to easily create a new report
        public IssueReport(int id, string location, string category, string description, string attachmentPath)
        {
            Id = id;
            Location = location;
            Category = category;
            Description = description;
            AttachmentPath = attachmentPath;
            ReportDate = DateTime.Now;
        }
    }
}