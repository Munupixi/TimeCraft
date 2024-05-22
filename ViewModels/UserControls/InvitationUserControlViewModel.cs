using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace TimeCraft.ViewModels.UserControls
{
    internal class InvitationUserControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Participant _participant;
        private Event _event;
        private DataBaseContent _context;

        public ICommand OpenCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }
        public ICommand DeclineCommand { get; private set; }

        public InvitationUserControlViewModel(Participant _participant)
        {
            this._participant = _participant;
            OpenCommand = new RelayCommand(OpenExecute);
            AcceptCommand = new RelayCommand(AcceptExecute);
            DeclineCommand = new RelayCommand(DeclineExecute);
            _context = new DataBaseContent();
            _event = EventViewModel.Get(_participant.IdEvent);
        }

        public string Title
        {
            get { return _event.Title; }
        }

        public string StartDate
        {
            get { return _event.StartDate.ToString(); }
        }

        public string StartTime
        {
            get { return _event.StartTime.ToString(); }
        }

        public string Role
        {
            get { return _participant.Role; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void OpenExecute()
        {
            new CreateEditEventWindow(_event, true).Show();
        }

        internal void AcceptExecute()
        {
            if (!new UserViewModel(User.ActiveUser).IsFreeTime(_event))
            {
                Event blockedEvent = EventViewModel.Get(_event.StartDate, _event.StartTime, _event.EndDate, _event.EndTime);
                if (blockedEvent.EventId != _event.EventId)
                {
                    MessageBox.Show("Данные время у вас занято меропритием:\n" + blockedEvent.Title);
                    return;
                }
            }
            _participant.IsAccepted = true;
            new ParticipantViewModel(_participant).Update();
        }

        internal void DeclineExecute()
        {
            new ParticipantViewModel(_participant).Delete();
        }
    }
}