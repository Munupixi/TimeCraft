using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeCraft
{
    internal class Participant
    {
        public int ParticipantId { get; set; }
        public int IdEvent { get; set; }
        public int IdUser { get; set; }
        public bool IsAccepted { get; set; }
        public string Role { get; set; }

        public Participant(int participantId, int idEvent, int idUser,
            bool isAccepted = false, string role = "Нет")
        {
            ParticipantId = participantId;
            IsAccepted = isAccepted;
            Role = role;
            IdEvent = idEvent;
            IdUser = idUser;
        }
    }
}