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

        public static bool IsAllParticipantsExists(List<AddParticipant> addParticipants)
        {
            foreach (AddParticipant addParticipant in addParticipants)
            {
                if (User.IsLoginUnique(addParticipant.Login))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<AddParticipant> ConvertParticipants(List<Participant> participants)
        {
            return participants.Select(participant =>
                new AddParticipant(User.Get(participant.IdUser).Login, participant.Role))
                .ToList();
        }
    }
}