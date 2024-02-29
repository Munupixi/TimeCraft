using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    public enum Priority
    {
        High,
        Medium,
        Low
    }

    internal class Task
    {
        const string DateFormat = "yyyy-MM-dd";
        const string timeFormat = "HH:mm";
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public short Repeat { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public Priority Priority { get; set; }
        public bool IsDone { get; set; }
        public int IdCategory { get; set; }
        public int IdUser { get; set; }

        public Task(int id, string title, int idUser, string description = null,
            DateTime startDate = default, TimeSpan startTime = default,
            short repeat = -1, DateTime endDate = default, TimeSpan endTime = default,
            Priority priority = Priority.Medium, bool isDone = false, int idCategory = -1)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            StartTime = startTime;
            Repeat = repeat;
            EndDate = endDate;
            EndTime = endTime;
            Priority = priority;
            IsDone = isDone;
            IdCategory = idCategory;
            IdUser = idUser;
        }
    }
}