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
using TimeCraft.Views.Pages;

namespace TimeCraft.ViewModels.Pages
{
    internal class WeeklySchedulePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataBaseContent _context;
        private Dictionary<DayOfWeek, ObservableCollection<ForWeekEventUserControl>> _forWeekEventUserControls =
            new Dictionary<DayOfWeek, ObservableCollection<ForWeekEventUserControl>>();
        private DateTime _selectedFirstDayOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
        private string _search;
        private string _yearAndWeek;

        public ICommand DailyPageCommand { get; private set; }
        public ICommand MonthlyPageCommand { get; private set; }
        public ICommand YearlyPageCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }
        public ICommand TaskListCommand { get; private set; }
        public ICommand InvitationListCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand TodayCommand { get; private set; }
        public ICommand NextCommand { get; private set; }

        public WeeklySchedulePageViewModel()
        {
            MessageBox.Show("Подождите, операция выполняется.\nВ среднем это занимает 5 секунд");
            DailyPageCommand = new RelayCommand(NavigateToDailyPage);
            MonthlyPageCommand = new RelayCommand(NavigateToMonthlyPage);
            YearlyPageCommand = new RelayCommand(NavigateToYearlyPage);
            ProfileCommand = new RelayCommand(NavigateProfilePage);
            TaskListCommand = new RelayCommand(NavigateToTaskListPage);
            InvitationListCommand = new RelayCommand(NavigateToInvitationListPage);
            PreviousCommand = new RelayCommand(PreviousExecute);
            TodayCommand = new RelayCommand(TodayExecute);
            NextCommand = new RelayCommand(NextExecute);

            EventViewModel.EventsUpdated += HandleEventsUpdated;

            _context = new DataBaseContent();
            foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                _forWeekEventUserControls.Add(dayOfWeek, new ObservableCollection<ForWeekEventUserControl>());
            }
            UpdateEventsView();
        }
        private void HandleEventsUpdated(object sender, EventArgs e)
        {
            UpdateEventsView();
        }

        public void UpdateEventsView()
        {
            foreach (var collection in _forWeekEventUserControls.Values)
            {
                collection.Clear();
            }
            foreach (KeyValuePair<DayOfWeek, List<Event>> eventsForDay in EventViewModel.GetFilterEventsBySearch(
                EventViewModel.GetAllMineAndInvitedByDateForWeek(
                User.ActiveUser.UserId, _selectedFirstDayOfWeek), Search))
            {
                foreach (Event _event in eventsForDay.Value)
                {
                    _forWeekEventUserControls[eventsForDay.Key].Add(new ForWeekEventUserControl(_event));
                }
            }

            int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(_selectedFirstDayOfWeek,
                CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            YearAndWeek = $"{_selectedFirstDayOfWeek.Year}, {weekNumber} неделя";
            //MondayEvents = _forWeekEventUserControls[DayOfWeek.Monday];
            //TuesdayEvents = _forWeekEventUserControls[DayOfWeek.Tuesday];
            //WednesdayEvents = _forWeekEventUserControls[DayOfWeek.Wednesday];
            //ThursdayEvents = _forWeekEventUserControls[DayOfWeek.Thursday];
            //FridayEvents = _forWeekEventUserControls[DayOfWeek.Friday];
            //SaturdayEvents = _forWeekEventUserControls[DayOfWeek.Saturday];
            //SundayEvents = _forWeekEventUserControls[DayOfWeek.Sunday];
        }
        public ObservableCollection<ForWeekEventUserControl> MondayEvents
        {
            get { return _forWeekEventUserControls[DayOfWeek.Monday]; }
            set
            {
                if (_forWeekEventUserControls[DayOfWeek.Monday] != value)
                {
                    _forWeekEventUserControls[DayOfWeek.Monday] = value;
                    OnPropertyChanged(nameof(MondayEvents));
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
                    OnPropertyChanged(nameof(TuesdayEvents));
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
                    OnPropertyChanged(nameof(WednesdayEvents));
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
                    OnPropertyChanged(nameof(ThursdayEvents));
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
                    OnPropertyChanged(nameof(FridayEvents));
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
                    OnPropertyChanged(nameof(SaturdayEvents));
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
                    OnPropertyChanged(nameof(SundayEvents));
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

        public int Monday => _selectedFirstDayOfWeek.Day;
        public int Tuesday => _selectedFirstDayOfWeek.Day + 1;
        public int Wednesday => _selectedFirstDayOfWeek.Day + 2;
        public int Thursday => _selectedFirstDayOfWeek.Day + 3;
        public int Friday => _selectedFirstDayOfWeek.Day + 4;
        public int Saturday => _selectedFirstDayOfWeek.Day + 5;
        public int Sunday => _selectedFirstDayOfWeek.Day + 6;

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

        private void NavigateToTaskListPage()
        {
            MainWindowViewModel.Frame.Content = new TaskListPage();
        }
        private void NavigateToInvitationListPage()
        {
            MainWindowViewModel.Frame.Content = new InvitationsListPage();
        }
        private void UpdateDayProperties()
        {
            OnPropertyChanged(nameof(Monday));
            OnPropertyChanged(nameof(Tuesday));
            OnPropertyChanged(nameof(Wednesday));
            OnPropertyChanged(nameof(Thursday));
            OnPropertyChanged(nameof(Friday));
            OnPropertyChanged(nameof(Saturday));
            OnPropertyChanged(nameof(Sunday));
        }

        private void TodayExecute()
        {
            _selectedFirstDayOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            OnPropertyChanged("Today");
            UpdateDayProperties();
        }

        private void PreviousExecute()
        {
            _selectedFirstDayOfWeek = _selectedFirstDayOfWeek.AddDays(- 7);
            OnPropertyChanged("Previous");
            UpdateDayProperties();
        }

        private void NextExecute()
        {
            _selectedFirstDayOfWeek = _selectedFirstDayOfWeek.AddDays(+ 7);
            OnPropertyChanged("Next");
            UpdateDayProperties();
        }
        public void ExecuteCreateEvent(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    new CreateEditEventWindow(_selectedFirstDayOfWeek).Show();
                    break;
                case DayOfWeek.Tuesday:
                    new CreateEditEventWindow(_selectedFirstDayOfWeek.AddDays(1)).Show();
                    break;
                case DayOfWeek.Wednesday:
                    new CreateEditEventWindow(_selectedFirstDayOfWeek.AddDays(2)).Show();
                    break;
                case DayOfWeek.Thursday:
                    new CreateEditEventWindow(_selectedFirstDayOfWeek.AddDays(3)).Show();
                    break;
                case DayOfWeek.Friday:
                    new CreateEditEventWindow(_selectedFirstDayOfWeek.AddDays(4)).Show();
                    break;
                case DayOfWeek.Saturday:
                    new CreateEditEventWindow(_selectedFirstDayOfWeek.AddDays(5)).Show();
                    break;
                case DayOfWeek.Sunday:
                    new CreateEditEventWindow(_selectedFirstDayOfWeek.AddDays(6)).Show();
                    break;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateEventsView();
        }
    }
}
