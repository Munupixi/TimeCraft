using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    public enum DressCode
    {
        BlackTie,
        Cocktail,
        Business,
        Casual,
        Sporty,
        Beach,
        Costume,
        Other
    }

    internal class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public string DressCode { get; set; }
        public Priority Priority { get; set; }
        public int IdCategory { get; set; }
        public int IdUser { get; set; }

        public Event(int id, string title, int idUser, string description = null,
            DateTime startDate = default, TimeSpan startTime = default,
            DateTime endDate = default, TimeSpan endTime = default,
            string location = null, string dressCode = null,
            Priority priority = Priority.Medium, int idCategory = -1)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            StartTime = startTime;
            EndDate = endDate;
            EndTime = endTime;
            Location = location;
            DressCode = dressCode;
            Priority = priority;
            IdCategory = idCategory;
            IdUser = idUser;
        }
    }
}