using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    internal class Participant
    {
        public int Id { get; set; }
        public int IdEvent { get; set; }
        public int IdUser { get; set; }
        public bool IsAccepted { get; set; }
        public string Role { get; set; }

        public Participant(int id, int idEvent, int idUser,
            bool isAccepted = false, string role = null)
        {
            Id = id;
            IsAccepted = isAccepted;
            Role = role;
            IdEvent = idEvent;
            IdUser = idUser;
        }
    }
}