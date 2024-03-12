using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool IsEmailUnique(string email, List<User> users)
        {
            foreach (User user in users)
            {
                if (user.Login == email)
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

            //if (password.Length >= 6 &&
            //    (password.Contains('1') || password.Contains('1')
            //                            || password.Contains('2') ||
            //                            password.Contains('3') ||
            //                            password.Contains('4')
            //                            || password.Contains('5')
            //                            || password.Contains('6')
            //                            || password.Contains('7')
            //                            || password.Contains('8')
            //                            || password.Contains('9')
            //                            || password.Contains('0'))
            //                         && (password.Contains('!')
            //                             || password.Contains('@')
            //                             || password.Contains('#')
            //                             || password.Contains('$')
            //                             || password.Contains('%')
            //                             || password.Contains('^')))
            //{
            //    foreach (char i in password)
            //    {
            //        if (char.IsUpper(i))
            //        {
            //            return true;
            //        }
            //    }
            //}
            //return false;
        }

        //public bool IsAgeCorrect(int age)
        //{
        //    return ageValue >= 4 && ageValue <= 120 : false;
        //}
    }
}