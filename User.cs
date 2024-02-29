using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    internal class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public User(int id, string login, string password,
            int age, string name = "guest", string surname = null,
            string patronymic = null)
        {
            Id = id;
            Login = login;
            Password = password;
            Age = age;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }
    }
}