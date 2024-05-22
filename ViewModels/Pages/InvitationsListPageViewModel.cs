using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TimeCraft.ViewModels.Windows;
using TimeCraft.Views.Pages;
using TimeCraft.Views.UserControls;

namespace TimeCraft.ViewModels.Pages
{
    internal class InvitationsListPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<InvitationUserControl> _invitationUserControls =
    new ObservableCollection<InvitationUserControl>();

        private DataBaseContent _context;

        public InvitationsListPageViewModel()
        {
            ParticipantViewModel.ParticipantsUpdated += HandleParticipantsUpdated;
            _context = new DataBaseContent();
            UpdateParticipantsView();
        }

        private void HandleParticipantsUpdated(object sender, EventArgs e)
        {
            UpdateParticipantsView();
        }

        public void UpdateParticipantsView()
        {
            _invitationUserControls.Clear();
            List<Participant> participants = ParticipantViewModel.GetAllParticipantByIdUser(User.ActiveUser.UserId); ;
            foreach (Participant participant in participants)
            {
                _invitationUserControls.Add(new InvitationUserControl(participant));
            }
        }

        public ObservableCollection<InvitationUserControl> Participants
        {
            get { return _invitationUserControls; }
            set
            {
                if (_invitationUserControls != value)
                {
                    _invitationUserControls = value;
                    OnPropertyChanged("Events");
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateParticipantsView();
        }
    }
}