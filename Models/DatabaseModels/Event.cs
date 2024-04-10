using System;
using System.Linq;

namespace TimeCraft
{
    public partial class Event : Activity
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Location { get; set; }
        public DressCodeEnum DressCode { get; set; }
        public PriorityEnum Priority { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public Event(int eventId, string title, int userId, string description = null,
            DateTime? startDate = default, TimeSpan? startTime = default,
            DateTime? endDate = default, TimeSpan? endTime = default,
            string location = null, DressCodeEnum dressCode = DressCodeEnum.НеИмеетЗначения,
            PriorityEnum priority = PriorityEnum.Средний, int categoryId = 1)
        {
            EventId = eventId;
            Title = title;
            Description = description;
            StartDate = startDate;
            StartTime = startTime;
            EndDate = endDate;
            EndTime = endTime;
            Location = location;
            DressCode = dressCode;
            Priority = priority;
            CategoryId = categoryId;
            UserId = userId;
        }

     
    }
}