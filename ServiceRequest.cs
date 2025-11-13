using System;

namespace MunicipalServicesApp
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public string RequestId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Status { get; set; } // Submitted, In Progress, Resolved, Closed
        public int Priority { get; set; } // 1=High, 2=Medium, 3=Low
        public string CitizenName { get; set; }
        public string ContactInfo { get; set; }
        public string AssignedDepartment { get; set; }
        public DateTime? DateResolved { get; set; }

        public ServiceRequest(string requestId, string title, string category, string description,
                            string location, string citizenName, string contactInfo, int priority = 2)
        {
            RequestId = requestId;
            Title = title;
            Category = category;
            Description = description;
            Location = location;
            CitizenName = citizenName;
            ContactInfo = contactInfo;
            Priority = priority;
            DateSubmitted = DateTime.Now;
            Status = "Submitted";
        }

        public int CompareTo(ServiceRequest other)
        {
            if (this.Priority != other.Priority)
                return this.Priority.CompareTo(other.Priority);
            return this.DateSubmitted.CompareTo(other.DateSubmitted);
        }

        public override string ToString()
        {
            return $"{RequestId}: {Title} - {Status}";
        }
    }
}