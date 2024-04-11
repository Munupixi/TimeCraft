using System;
using System.ComponentModel;
using System.Linq;

namespace TimeCraft.ViewModels
{
    internal class TaskViewModel : INotifyPropertyChanged
    {
        private Task _task;

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskViewModel(Task task)
        {
            _task = task;
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
    }
}