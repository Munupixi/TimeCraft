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
    internal class User : Entity
    {
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
        public bool IsEmailCorrect(string email)
        {
            if (email.Contains('@') &&
                email.Contains('.') && email.Length > 5)
            {
                byte at = (byte)email.IndexOf('@');
                byte dot = (byte)email.IndexOf('.');

                if (at > 0 && dot > at + 1 && dot < email.Length - 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsLoginUnique(string login, List<User> users)
        {
            foreach (User user in users)
            {
                if (user.Login == login)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsPasswordCorrect(string password)
        {
            string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^])(?=.*[A-Z]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }

        public void AddNewUserChek(string name, int age, string login, string patronymic)
        {
            IsAgeCorrect(age);



        }
        



        public bool IsAgeCorrect(int age)
        {
            return age >= 4 && age <= 120 ? true : false;
        }
    }
}