using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    internal class AddParticipant
    {
        public string Login { get; set; }
        public string Role { get; set; }

        public AddParticipant(string login = "Логин", string role = "Нет")
        {
            Login = login;
            Role = role;
        }
    }
}
