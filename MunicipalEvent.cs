using System;

namespace MunicipalServicesApp
{
    public class MunicipalEvent : IComparable<MunicipalEvent>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Organizer { get; set; }
        public int Priority { get; set; }

        public MunicipalEvent(int id, string title, string description, string category, DateTime eventDate, string location, string organizer, int priority = 1)
        {
            Id = id;
            Title = title;
            Description = description;
            Category = category;
            EventDate = eventDate;
            Location = location;
            Organizer = organizer;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"{Title} - {EventDate:yyyy-MM-dd} at {Location}";
        }

        public int CompareTo(MunicipalEvent other)
        {
            if (this.Priority != other.Priority)
                return this.Priority.CompareTo(other.Priority);
            return this.EventDate.CompareTo(other.EventDate);
        }
    }
}