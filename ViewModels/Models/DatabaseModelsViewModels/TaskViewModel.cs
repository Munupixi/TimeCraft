using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace TimeCraft.ViewModels
{
    internal class TaskViewModel : Activity, INotifyPropertyChanged
    {
        private Task _task;

        public event PropertyChangedEventHandler PropertyChanged;
        public static event EventHandler TasksUpdated;

        public TaskViewModel(Task task)
        {
            _task = task;
        }
        public static bool IsEndDateCorrect(DateTime startDate, string startTime, DateTime endDate, string endTime, short? repeat)
        {
            return true;
            //return repeat == 0 ||
            //    (Activity.IsEndDateCorrect(startDate, startTime, endDate, endTime) &&
            // (startDate.AddDays(repeat ?? 0) <= endDate ||
            // (TimeSpan.Parse(startTime) < TimeSpan.Parse(endTime) &&
            // startDate.AddDays(repeat ?? 0) == endDate)));
        }
        public bool IsTitleUnique()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return !db.Task.Any(t => t.Title == _task.Title);
            }
        }
        public void Delete()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                if (db.Task.Find(_task.TaskId) != null)
                {
                    db.Task.Remove(_task);
                    db.SaveChanges();
                    TasksUpdated?.Invoke(this, EventArgs.Empty);
                    return;
                }
                throw new Exception("Возникли проблемы с удалением напоминания");
            }
        }

        public static void Delete(int taskId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                Task _task = db.Task.Find(taskId);
                if (_task != null)
                {
                    db.Task.Remove(_task);
                    db.SaveChanges();
                    TasksUpdated?.Invoke(null, EventArgs.Empty);
                    return;
                }
                throw new Exception("Возникли проблемы с удалением напоминания");
            }
        }

        public void Add()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                db.Task.Add(_task);
                db.SaveChanges();
                TasksUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Update()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                try
                {
                    db.Task.Update(_task);
                    TasksUpdated?.Invoke(this, EventArgs.Empty);
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
                return (db.Task.Max(t => (int?)t.TaskId) ?? 0) + 1;
            }
        }

        public static Task Get(int taskId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Task.FirstOrDefault(t => t.TaskId == taskId);
            }
        }

        public static Task Get(string title)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Task.FirstOrDefault(t => t.Title == title);
            }
        }
    }
}