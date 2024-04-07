using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TimeCraft
{
    internal class User : IEntity
    {
        public static User ActiveUser { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public User(int userId, string login, string password,
            int age, string name = "guest", string surname = null,
            string patronymic = null)
        {
            UserId = userId;
            Login = login;
            Password = password;
            Age = age;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public static bool IsLoginCorrect(string login)
        {
            if (login.Contains('@') &&
                login.Contains('.') && login.Length > 5)
            {
                byte at = (byte)login.IndexOf('@');
                byte dot = (byte)login.IndexOf('.');

                if (at > 0 && dot > at + 1 && dot < login.Length - 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsLoginUnique(string login)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return !db.User.Any(u => u.Login == login);
            }
        }

        public static int GetNewId()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return (db.User.Max(u => (int?)u.UserId) ?? 0) + 1;
            }
        }

        public static bool IsPasswordCorrect(string password)
        {
            string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^])(?=.*[A-Z]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }

        public static bool IsAgeCorrect(string age)
        {
            if (int.TryParse(age, out int ageValue))
            {
                return ageValue >= 4 && ageValue <= 120 ? true : false;
            }
            return false;
        }

        public void Delete()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                if (db.User.Find(UserId) != null)
                {
                    db.User.Remove(this);
                    db.SaveChanges();
                    return;
                }
                throw new Exception("Возникли проблемы с удалением пользователя");
            }
        }

        public void Delete(int userId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                User user = db.User.Find(userId);
                if (user != null)
                {
                    db.User.Remove(user);
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
                db.User.Add(this);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                try
                {
                    db.User.Update(this);
                }
                catch
                {
                    throw new Exception("Неудалось сохранить изменения");
                }
                db.SaveChanges();
            }
        }

        public static User Get(string login)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.User.FirstOrDefault(u => u.Login == login);
            }
        }

        public static User Get(int userId)
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.User.FirstOrDefault(u => u.UserId == userId);
            }
        }

        public bool IsFreeTime(DateTime? startDate, TimeSpan? startTime, DateTime? endDate, TimeSpan? endTime)
        {
            if (startDate == null || endDate == null || startTime == null || endTime == null) { return false; }
            DateTime? startDateTime = startDate + startTime;
            DateTime? endDateTime = endDate + endTime;
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.Count(e =>
                    (e.StartDate < endDateTime && e.EndDate > startDateTime) || // Если начало события в пределах диапазона или конец события в пределах диапазона
                    (e.StartDate >= startDateTime && e.EndDate <= endDateTime) || // Если событие полностью внутри указанного диапазона
                    (e.StartDate <= startDateTime && e.EndDate >= endDateTime)) == 0; // Если указанный диапазон полностью внутри события
            }
        }
    }
}