using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft.ViewModels.UserControls
{
    internal class ForMonthEventUserControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime _date;
        private List<Event> _events;
        private DataBaseContent _context;

        public ForMonthEventUserControlViewModel(DateTime date)
        {
            _context = new DataBaseContent();
            _date = date;
            _events = EventViewModel.GetAllMineAndInvitedByDateForDay(User.ActiveUser.UserId, _date);

        }

        public int Day
        {
            get { return _date.Day; }
        }

        public int CountEvents
        {
            get { return _events.Count; }
        }

        public List<string> Events
        {
            get
            {
                return _events.Select(e => e.Title).ToList();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void ExecuteOpenDay()
        {
            MainWindowViewModel.Frame.Content = new DailySchedulePage(_date);
        }

        internal void ExecuteOpenEvent(int index)
        {
            new CreateEditEventWindow(_events[index]).Show();
        }
    }
}