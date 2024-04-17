using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TimeCraft.ViewModels.Windows;
using TimeCraft.Views.UserControls;

namespace TimeCraft.ViewModels.Pages
{
    internal class DailySchedulePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataBaseContent _context;
        private List<Event> _events;
        private ObservableCollection<ForDayEventUserControl> _forDayEventUserControls = new ObservableCollection<ForDayEventUserControl>();
        private DateTime _selectedDate = DateTime.Now;

        public ICommand WeeklyCommand { get; private set; }
        public ICommand MonthlyCommand { get; private set; }
        public ICommand YearlyCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand TaskListCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand TodayCommand { get; private set; }
        public ICommand NextCommand { get; private set; }

        public DailySchedulePageViewModel()
        {
            WeeklyCommand = new RelayCommand(NavigateToWeeklyPage);
            MonthlyCommand = new RelayCommand(NavigateToMonthlyPage);
            YearlyCommand = new RelayCommand(NavigateToYearlyPage);
            ProfileCommand = new RelayCommand(NavigateProfilePage);
            SettingsCommand = new RelayCommand(NavigateToSettingsPage);
            TaskListCommand = new RelayCommand(NavigateToTaskListPage);
            PreviousCommand = new RelayCommand(TodayExecute);
            TodayCommand = new RelayCommand(PreviousExecute);
            NextCommand = new RelayCommand(NextExecute);

            NoSelectedMessageVisibility = Visibility.Hidden;

            _context = new DataBaseContent();
            UpdateProductsView();
        }

        public void UpdateProductsView()
        {
            _events = EventViewModel.GetAllMineAndInvitedByDate(
                User.ActiveUser.UserId, SelectedDate);
            _forDayEventUserControls.Clear();
            foreach (Event _event in _events)
            {
                _forDayEventUserControls.Add(new ForDayEventUserControl(_event));
            }
            Events = _forDayEventUserControls;
        }

        public ObservableCollection<ForDayEventUserControl> Events
        {
            get { return _forDayEventUserControls; }
            set
            {
                if (_forDayEventUserControls != value)
                {
                    _forDayEventUserControls = value;
                    OnPropertyChanged("Events");
                }
            }
        }
        public Visibility NoSelectedMessageVisibility { get;  set; }
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }

        private void NavigateToWeeklyPage()
        {
            throw new NotImplementedException();
        }

        private void NavigateToMonthlyPage()
        {
            throw new NotImplementedException();
        }

        private void NavigateToYearlyPage()
        {
            throw new NotImplementedException();
        }

        private void NavigateProfilePage()
        {
            throw new NotImplementedException();
        }

        private void NavigateToSettingsPage()
        {
            throw new NotImplementedException();
        }

        private void NavigateToTaskListPage()
        {
            throw new NotImplementedException();
        }

        private void TodayExecute()
        {
            throw new NotImplementedException();
        }

        private void PreviousExecute()
        {
            throw new NotImplementedException();
        }

        private void NextExecute()
        {
            throw new NotImplementedException();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateProductsView();
        }
    }
}