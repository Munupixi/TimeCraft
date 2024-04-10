using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TimeCraft
{
    public class User
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
    }
}