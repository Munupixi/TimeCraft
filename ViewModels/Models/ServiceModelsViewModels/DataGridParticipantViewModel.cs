using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft.ViewModels.Models.ServiceModelsViewModels
{
    internal class DataGridParticipantViewModel : INotifyPropertyChanged
    {
        private DataGridParticipant _dataGridParticipant;

        public DataGridParticipantViewModel(DataGridParticipant dataGridParticipant)
        {
            _dataGridParticipant = dataGridParticipant;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static bool IsAllParticipantsExists(List<DataGridParticipant> addParticipants)
        {
            foreach (DataGridParticipant addParticipant in addParticipants)
            {
                if (UserViewModel.IsLoginUnique(addParticipant.Login))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<DataGridParticipant> ConvertParticipants(List<Participant> participants)
        {
            return participants.Select(participant =>
                new DataGridParticipant(UserViewModel.Get(participant.IdUser).Login, participant.Role))
                .ToList();
        }
    }
}
