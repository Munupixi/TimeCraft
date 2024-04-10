using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace TimeCraft.ViewModels
{
    internal class UserViewModel : INotifyPropertyChanged
    {
        private User _user;
        private string ageAsString;

        public event PropertyChangedEventHandler PropertyChanged;

        public UserViewModel(User user)
        {
            _user = user;
        }

        public string Name
        {
            get { return _user.Name; }
            set
            {
                if (_user.Name != value)
                {
                    _user.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Surname
        {
            get { return _user.Surname; }
            set
            {
                if (_user.Surname != value)
                {
                    _user.Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public string Patronymic
        {
            get { return _user.Patronymic; }
            set
            {
                if (_user.Patronymic != value)
                {
                    _user.Patronymic = value;
                    OnPropertyChanged("Patronymic");
                }
            }
        }

        public string Age
        {
            get { return ageAsString; }
            set
            {
                ageAsString = value;
                if (int.TryParse(ageAsString, out int _age))
                {
                    _user.Age = _age;
                    OnPropertyChanged("Age");
                }
                OnPropertyChanged("ageAsString");
            }
        }

        public int UserId
        {
            get { return _user.UserId; }
            set
            {
                if (_user.UserId != value)
                {
                    _user.UserId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        public string Login
        {
            get { return _user.Login; }
            set
            {
                if (_user.Login != value)
                {
                    _user.Login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return _user.Password; }
            set
            {
                if (_user.Password != value)
                {
                    _user.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public bool IsLoginCorrect()
        {
            string login = _user.Login;
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

        public bool IsLoginUnique()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return !db.User.Any(u => u.Login == _user.Login);
            }
        }

        public static int GetNewId()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return (db.User.Max(u => (int?)u.UserId) ?? 0) + 1;
            }
        }

        public bool IsPasswordCorrect()
        {
            string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^])(?=.*[A-Z]).{6,}$";
            return Regex.IsMatch(_user.Password, pattern);
        }

        public bool IsAgeCorrect()
        {
            if (int.TryParse(ageAsString, out int _age))
            {
                return _age >= 4 && _age <= 120 ? true : false;
            }
            return false;
        }

        public void Delete()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                if (db.User.Find(UserId) != null)
                {
                    db.User.Remove(_user);
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
                db.User.Add(_user);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                try
                {
                    db.User.Update(_user);
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

        public bool IsFreeTime(Event _event)
        {
            if (_event.StartDate == null || _event.EndDate == null || _event.StartTime == null || _event.EndTime == null) { return false; }
            DateTime? startDateTime = _event.StartDate + _event.StartTime;
            DateTime? endDateTime = _event.EndDate + _event.EndTime;
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Event.Count(e =>
                    (e.StartDate < endDateTime && e.EndDate > startDateTime) || // Если начало события в пределах диапазона или конец события в пределах диапазона
                    (e.StartDate >= startDateTime && e.EndDate <= endDateTime) || // Если событие полностью внутри указанного диапазона
                    (e.StartDate <= startDateTime && e.EndDate >= endDateTime)) == 0; // Если указанный диапазон полностью внутри события
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}