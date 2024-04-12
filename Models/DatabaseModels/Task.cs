using System;

namespace TimeCraft
{
    internal class Task : Activity
    {
        private const string DateFormat = "yyyy-MM-dd";
        private const string timeFormat = "HH:mm";
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public short? Repeat { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }
        public PriorityEnum Priority { get; set; }
        public bool IsDone { get; set; }
        public int IdCategory { get; set; }
        public int IdUser { get; set; }

        public Task(int taskId, string title, int idUser, string description = null,
            DateTime? startDate = default, TimeSpan? startTime = default,
            short? repeat = 0, DateTime? endDate = default, TimeSpan? endTime = default,
            PriorityEnum priority = PriorityEnum.Средний, bool isDone = false, int idCategory = -1)
        {
            TaskId = taskId;
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