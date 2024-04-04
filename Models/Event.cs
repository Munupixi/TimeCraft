using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    public partial class Event : Activity, IEntity
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
            EndTime = startTime;
            EndDate = endDate;
            EndTime = endTime;
            Location = location;
            DressCode = dressCode;
            Priority = priority;
            CategoryId = categoryId;
            UserId = userId;
        }

        public void Delete()
        {
            using (AppDBContent db = new AppDBContent())
            {
                if (db.Event.Find(EventId) != null)
                {
                    db.Event.Remove(this);
                    db.SaveChanges();
                    return;
                }
                throw new Exception("Возникли проблемы с удалением мероприятия");
            }
        }

        public void Delete(int eventId)
        {
            using (AppDBContent db = new AppDBContent())
            {
                Event _event = db.Event.Find(eventId);
                if (_event != null)
                {
                    db.Event.Remove(_event);
                    db.SaveChanges();
                    return;
                }
                throw new Exception("Возникли проблемы с удалением пользователя");
            }
        }

        public void Add()
        {
            using (AppDBContent db = new AppDBContent())
            {
                db.Event.Add(this);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (AppDBContent db = new AppDBContent())
            {
                try
                {
                    db.Event.Update(this);
                }
                catch
                {
                    throw new Exception("Неудалось сохранить изменения");
                }
                db.SaveChanges();
            }
        }

        public static int GetNewId()
        {
            using (AppDBContent db = new AppDBContent())
            {
                return (db.Event.Max(u => (int?)u.EventId) ?? 0) + 1;
            }
        }
        public static Event Get(int eventId)
        {
            using (AppDBContent db = new AppDBContent())
            {
                return db.Event.FirstOrDefault(e => e.EventId == eventId);
            }
        }
        public static Event Get(string title)
        {
            using (AppDBContent db = new AppDBContent())
            {
                return db.Event.FirstOrDefault(e => e.Title == title);
            }
        }
        public static Event Get(DateTime? startDate, TimeSpan? startTime, DateTime? endDate, TimeSpan? endTime)
        {
            if (startDate == null || endDate == null || startTime == null || endTime == null)
            {
                return null;
            }

            DateTime? startDateTime = startDate + startTime;
            DateTime? endDateTime = endDate + endTime;
            using (AppDBContent db = new AppDBContent())
            {
                return db.Event.FirstOrDefault(e =>
                    (e.StartDate < endDateTime && e.EndDate > startDateTime) || // Если начало события в пределах диапазона или конец события в пределах диапазона
                    (e.StartDate >= startDateTime && e.EndDate <= endDateTime) || // Если событие полностью внутри указанного диапазона
                    (e.StartDate <= startDateTime && e.EndDate >= endDateTime));  // Если указанный диапазон полностью внутри события
            }
        }

        public static bool IsTitleUnique(string title)
        {
            using (AppDBContent db = new AppDBContent())
            {
                return !db.Event.Any(e => e.Title == title);
            }
        }
        public bool IsEventExists(int eventId)
        {
            using (AppDBContent db = new AppDBContent())
            {
                return db.Event.Any(e => e.EventId == eventId);
            }
        }
    }
}