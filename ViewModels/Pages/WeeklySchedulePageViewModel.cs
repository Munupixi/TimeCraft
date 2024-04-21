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
using TimeCraft.Views.UserControls;
using GalaSoft.MvvmLight.Command;
using System.Globalization;

namespace TimeCraft.ViewModels.Pages
{
    internal class WeeklySchedulePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataBaseContent _context;
        private Dictionary<DayOfWeek, List<Event>> _eventsForWeek;
        private Dictionary<DayOfWeek, ObservableCollection<ForWeekEventUserControl>> _forWeekEventUserControls =
            new Dictionary<DayOfWeek, ObservableCollection<ForWeekEventUserControl>>();
        private DateTime _selectedFirstDayOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
        private string _search;
        private string _yearAndWeek;

        public ICommand DailyPageCommand { get; private set; }
        public ICommand MonthlyPageCommand { get; private set; }
        public ICommand YearlyPageCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand TaskListCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand TodayCommand { get; private set; }
        public ICommand NextCommand { get; private set; }

        public WeeklySchedulePageViewModel()
        {
            DailyPageCommand = new RelayCommand(NavigateToDailyPage);
            MonthlyPageCommand = new RelayCommand(NavigateToMonthlyPage);
            YearlyPageCommand = new RelayCommand(NavigateToYearlyPage);
            ProfileCommand = new RelayCommand(NavigateProfilePage);
            SettingsCommand = new RelayCommand(NavigateToSettingsPage);
            TaskListCommand = new RelayCommand(NavigateToTaskListPage);
            PreviousCommand = new RelayCommand(PreviousExecute);
            TodayCommand = new RelayCommand(TodayExecute);
            NextCommand = new RelayCommand(NextExecute);

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
            _eventsForWeek = EventViewModel.GetAllMineAndInvitedByDateForWeek(
                User.ActiveUser.UserId, _selectedFirstDayOfWeek);
            _forWeekEventUserControls.Clear();

            foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                _forWeekEventUserControls.Add(dayOfWeek, new ObservableCollection<ForWeekEventUserControl>());
            }
            foreach (KeyValuePair<DayOfWeek, List<Event>> eventsForDay in _eventsForWeek)
            {
                foreach (Event _event in eventsForDay.Value)
                {
                    _forWeekEventUserControls[eventsForDay.Key].Add(new ForWeekEventUserControl(_event));
                }
            }

            int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(_selectedFirstDayOfWeek,
                CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            YearAndWeek = $"{_selectedFirstDayOfWeek.Year}, {weekNumber} неделя";

            MondayEvents = _forWeekEventUserControls[DayOfWeek.Monday];
            TuesdayEvents = _forWeekEventUserControls[DayOfWeek.Tuesday];
            WednesdayEvents = _forWeekEventUserControls[DayOfWeek.Wednesday];
            ThursdayEvents = _forWeekEventUserControls[DayOfWeek.Thursday];
            FridayEvents = _forWeekEventUserControls[DayOfWeek.Friday];
            SaturdayEvents = _forWeekEventUserControls[DayOfWeek.Saturday];
            SundayEvents = _forWeekEventUserControls[DayOfWeek.Sunday];
        }
        public ObservableCollection<ForWeekEventUserControl> MondayEvents
        {
            get { return _forWeekEventUserControls[DayOfWeek.Monday]; }
            set
            {
                if (_forWeekEventUserControls[DayOfWeek.Monday] != value)
                {
                    _forWeekEventUserControls[DayOfWeek.Monday] = value;
                    OnPropertyChanged("MondayEvents");
                }
            }
        }
        public ObservableCollection<ForWeekEventUserControl> TuesdayEvents
        {
            get { return _forWeekEventUserControls[DayOfWeek.Tuesday]; }
            set
            {
                if (_forWeekEventUserControls[DayOfWeek.Tuesday] != value)
                {
                    _forWeekEventUserControls[DayOfWeek.Tuesday] = value;
                    OnPropertyChanged("TuesdayEvents");
                }
            }
        }
        public ObservableCollection<ForWeekEventUserControl> WednesdayEvents
        {
            get { return _forWeekEventUserControls[DayOfWeek.Wednesday]; }
            set
            {
                if (_forWeekEventUserControls[DayOfWeek.Wednesday] != value)
                {
                    _forWeekEventUserControls[DayOfWeek.Wednesday] = value;
                    OnPropertyChanged("WednesdayEvents");
                }
            }
        }
        public ObservableCollection<ForWeekEventUserControl> ThursdayEvents
        {
            get { return _forWeekEventUserControls[DayOfWeek.Thursday]; }
            set
            {
                if (_forWeekEventUserControls[DayOfWeek.Thursday] != value)
                {
                    _forWeekEventUserControls[DayOfWeek.Thursday] = value;
                    OnPropertyChanged("ThursdayEvents");
                }
            }
        }
        public ObservableCollection<ForWeekEventUserControl> FridayEvents
        {
            get { return _forWeekEventUserControls[DayOfWeek.Friday]; }
            set
            {
                if (_forWeekEventUserControls[DayOfWeek.Friday] != value)
                {
                    _forWeekEventUserControls[DayOfWeek.Friday] = value;
                    OnPropertyChanged("FridayEvents");
                }
            }
        }
        public ObservableCollection<ForWeekEventUserControl> SaturdayEvents
        {
            get { return _forWeekEventUserControls[DayOfWeek.Saturday]; }
            set
            {
                if (_forWeekEventUserControls[DayOfWeek.Saturday] != value)
                {
                    _forWeekEventUserControls[DayOfWeek.Saturday] = value;
                    OnPropertyChanged("SaturdayEvents");
                }
            }
        }
        public ObservableCollection<ForWeekEventUserControl> SundayEvents
        {
            get { return _forWeekEventUserControls[DayOfWeek.Sunday]; }
            set
            {
                if (_forWeekEventUserControls[DayOfWeek.Sunday] != value)
                {
                    _forWeekEventUserControls[DayOfWeek.Sunday] = value;
                    OnPropertyChanged("SundayEvents");
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
        public string YearAndWeek
        {
            get { return _yearAndWeek; }
            set
            {
                if (_yearAndWeek != value)
                {
                    _yearAndWeek = value;
                    OnPropertyChanged("YearAndWeek");
                }
            }
        }

        private void NavigateToDailyPage()
        {
            MainWindowViewModel.Frame.Content = new DailySchedulePage();
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
            _selectedFirstDayOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
        }

        private void PreviousExecute()
        {
            _selectedFirstDayOfWeek.AddDays(- 7);
        }

        private void NextExecute()
        {
            _selectedFirstDayOfWeek.AddDays(+ 7);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateEventsView();
        }
    }
}
