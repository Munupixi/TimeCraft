using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TimeCraft
{
    public class User : IEntity
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
            using (AppDBContent db = new AppDBContent())
            {
                return !db.User.Any(u => u.Login == login);
            }
        }

        public static int GetNewId()
        {
            using (AppDBContent db = new AppDBContent())
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
            using (AppDBContent db = new AppDBContent())
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
            using (AppDBContent db = new AppDBContent())
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
            using (AppDBContent db = new AppDBContent())
            {
                db.User.Add(this); 
                db.SaveChanges(); 
            }
        }


       

        public void Update()
        {
            using (AppDBContent db = new AppDBContent())
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
        public static User GetUserByLogin(string login)
        {
            using (AppDBContent db = new AppDBContent())
            {
                return db.User.FirstOrDefault(u => u.Login == login);
            }
        }
    }
}