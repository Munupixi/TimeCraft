using System.Collections.Generic;
using System.Linq;
using TimeCraft.ViewModels;

namespace TimeCraft
{
    internal class DataGridParticipantModel
    {
        public string Login { get; set; }
        public string Role { get; set; }

        public DataGridParticipantModel(string login = "Логин", string role = "Нет")
        {
            Login = login;
            Role = role;
        }

        public static bool IsAllParticipantsExists(List<DataGridParticipantModel> addParticipants)
        {
            foreach (DataGridParticipantModel addParticipant in addParticipants)
            {
                if (UserViewModel.IsLoginUnique(addParticipant.Login))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<DataGridParticipantModel> ConvertParticipants(List<Participant> participants)
        {
            return participants.Select(participant =>
                new DataGridParticipantModel(UserViewModel.Get(participant.IdUser).Login, participant.Role))
                .ToList();
        }
    }
}