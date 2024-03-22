using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
namespace TimeCraft
{

    internal class Event : Activity, IEntity
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Location { get; set; }
        public DressCode DressCode { get; set; }
        public Priority Priority { get; set; }
        public int IdCategory { get; set; }
        public int IdUser { get; set; }

        public Event(int eventId, string title, int idUser, string description = null,
            DateTime? startDate = default, TimeSpan? startTime = default,
            DateTime? endDate = default, TimeSpan? endTime = default,
            string location = null, DressCode dressCode = DressCode.NotMatter,
            Priority priority = Priority.Medium, int idCategory = -1)
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
            IdCategory = idCategory;
            IdUser = idUser;
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
    }
}