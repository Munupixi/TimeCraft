using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Linq;

namespace TimeCraft.ViewModels
{
    internal class EventViewModel : INotifyPropertyChanged
    {
        private Event _event;
        public event PropertyChangedEventHandler PropertyChanged;

        public EventViewModel(Event @event)
        {
            _event = @event;
        }
        public void Delete()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                if (db.Event.Find(_event.EventId) != null)
                {
                    db.Event.Remove(_event);
                    db.SaveChanges();
                    return;
                }
                throw new Exception("Возникли проблемы с удалением мероприятия");
            }
        }

        public static void Delete(int eventId)
        {
            using (DataBaseContent db = new DataBaseContent())
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
            using (DataBaseContent db = new DataBaseContent())
            {
                db.Event.Add(_event);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                try
                {
                    db.Event.Update(_event);
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
            using (DataBaseContent db = new DataBaseContent())
            {
                return (db.Event.Max(u => (int?)u.EventId) ?? 0) + 1;
            }
        }

        public static Event Get(int eventId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.FirstOrDefault(e => e.EventId == eventId);
            }
        }

        public static Event Get(string title)
        {
            using (DataBaseContent db = new DataBaseContent())
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
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.FirstOrDefault(e =>
                    (e.StartDate < endDateTime && e.EndDate > startDateTime) || // Если начало события в пределах диапазона или конец события в пределах диапазона
                    (e.StartDate >= startDateTime && e.EndDate <= endDateTime) || // Если событие полностью внутри указанного диапазона
                    (e.StartDate <= startDateTime && e.EndDate >= endDateTime));  // Если указанный диапазон полностью внутри события
            }
        }

        public bool IsTitleUnique()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return !db.Event.Any(e => e.Title == _event.Title);
            }
        }

        public bool IsEventExists(int eventId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.Any(e => e.EventId == eventId);
            }
        }
    }
}