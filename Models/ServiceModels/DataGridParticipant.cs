using System.Collections.Generic;
using System.Linq;
using TimeCraft.ViewModels;

namespace TimeCraft
{
    internal class DataGridParticipant
    {
        public string Login { get; set; }
        public string Role { get; set; }

        public DataGridParticipant(string login = "Логин", string role = "Нет")
        {
            Login = login;
            Role = role;
        }
    }
}