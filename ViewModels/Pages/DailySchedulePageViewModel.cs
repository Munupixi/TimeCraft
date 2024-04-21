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
        private string _search;
        private Visibility _noSelectedMessageVisibility;

        public ICommand WeeklyPageCommand { get; private set; }
        public ICommand MonthlyPageCommand { get; private set; }
        public ICommand YearlyPageCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand TaskListCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand TodayCommand { get; private set; }
        public ICommand NextCommand { get; private set; }

        public DailySchedulePageViewModel()
        {
            WeeklyPageCommand = new RelayCommand(NavigateToWeeklyPage);
            MonthlyPageCommand = new RelayCommand(NavigateToMonthlyPage);
            YearlyPageCommand = new RelayCommand(NavigateToYearlyPage);
            ProfileCommand = new RelayCommand(NavigateProfilePage);
            SettingsCommand = new RelayCommand(NavigateToSettingsPage);
            TaskListCommand = new RelayCommand(NavigateToTaskListPage);
            PreviousCommand = new RelayCommand(PreviousExecute);
            TodayCommand = new RelayCommand(TodayExecute);
            NextCommand = new RelayCommand(NextExecute);

            NoSelectedMessageVisibility = Visibility.Hidden;

            EventViewModel.EventsUpdated += HandleEventsUpdated;

            _context = new DataBaseContent();
            UpdateEventsView();
        }

        private void HandleEventsUpdated(object sender, EventArgs e)
        {
            UpdateEventsView();
        }

        public void UpdateEventsView()
        {
            _events = EventViewModel.GetFilterEventsBySearch(
                EventViewModel.GetAllMineAndInvitedByDateForDay(
                User.ActiveUser.UserId, SelectedDate),
                Search);
            _forDayEventUserControls.Clear();
            foreach (Event _event in _events)
            {
                _forDayEventUserControls.Add(new ForDayEventUserControl(_event));
            }
            Events = _forDayEventUserControls;
            NoSelectedMessageVisibility =
                Events.Count == 0 ? Visibility.Visible : Visibility.Hidden;
  
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

        public Visibility NoSelectedMessageVisibility
        {
            get { return _noSelectedMessageVisibility; }
            set
            {
                if (_noSelectedMessageVisibility != value)
                {
                    _noSelectedMessageVisibility = value;
                    OnPropertyChanged("NoSelectedMessageVisibility");
                }
            }
        }
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

        public string Search
        {
            get { return _search; }
            set
            {
                if (_search != value)
                {
                    _search = value;
                    OnPropertyChanged("Search");
                }
            }
        }

        private void NavigateToWeeklyPage()
        {
            MainWindowViewModel.Frame.Content = new WeeklySchedulePage();
        }

        private void NavigateToMonthlyPage()
        {
            MainWindowViewModel.Frame.Content = new MonthlySchedulePage();
        }

        private void NavigateToYearlyPage()
        {
            MainWindowViewModel.Frame.Content = new YearlySchedulePage();
        }

        private void NavigateProfilePage()
        {
            MainWindowViewModel.Frame.Content = new ProfilePage(User.ActiveUser);
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
            SelectedDate = DateTime.Now;
        }

        private void PreviousExecute()
        {
            SelectedDate = SelectedDate.AddDays(-1);
        }

        private void NextExecute()
        {
            SelectedDate = SelectedDate.AddDays(+1);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateEventsView();
        }
    }
}