using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using TimeCraft.ViewModels.UserControls;
using TimeCraft.ViewModels.Windows;
using TimeCraft.Views.UserControls;

namespace TimeCraft.ViewModels.Pages
{
    internal class MonthlySchedulePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataBaseContent _context;
        private ForMonthEventUserControl[] _forMonthEventUserControls = new ForMonthEventUserControl[5 * 7];
        //private Dictionary<int, ForMonthEventUserControl> _forMonthEventUserControls =
        //    new Dictionary<int, ForMonthEventUserControl>();

        private DateTime _selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private string _search;
        private string _yearAndMonth;

        public ICommand DailyPageCommand { get; private set; }
        public ICommand WeeklyPageCommand { get; private set; }
        public ICommand YearlyPageCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand TaskListCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand TodayCommand { get; private set; }
        public ICommand NextCommand { get; private set; }

        public MonthlySchedulePageViewModel()
        {
            DailyPageCommand = new RelayCommand(NavigateToDailyPage);
            WeeklyPageCommand = new RelayCommand(NavigateToWeeklyPage);
            YearlyPageCommand = new RelayCommand(NavigateToYearlyPage);
            ProfileCommand = new RelayCommand(NavigateProfilePage);
            SettingsCommand = new RelayCommand(NavigateToSettingsPage);
            TaskListCommand = new RelayCommand(NavigateToTaskListPage);
            PreviousCommand = new RelayCommand(PreviousExecute);
            TodayCommand = new RelayCommand(TodayExecute);
            NextCommand = new RelayCommand(NextExecute);

            EventViewModel.EventsUpdated += HandleEventsUpdated;

            DateTime date = _selectedDate;
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            for (int day = 0; day < _forMonthEventUserControls.Length; day++)
            {
                _forMonthEventUserControls[day] = new ForMonthEventUserControl(date);
                date = date.AddDays(1);
            }

            _context = new DataBaseContent();
            UpdateEventsView();
        }

        private void HandleEventsUpdated(object sender, EventArgs e)
        {
            UpdateEventsView();
        }

        public void UpdateEventsView()
        {
            DateTime date = _selectedDate;
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }
            for (int day = 0; day < _forMonthEventUserControls.Length; day++)
            {
                _forMonthEventUserControls[day].Update(date);
                date = date.AddDays(1);
            }
            YearAndMonth = $"{_selectedDate.Year}, {_selectedDate.Month}";
        }


        public IEnumerable<ForMonthEventUserControl> Events => _forMonthEventUserControls;

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

        public string YearAndMonth
        {
            get { return _yearAndMonth; }
            set
            {
                if (_yearAndMonth != value)
                {
                    _yearAndMonth = value;
                }
            }
        }

        private void NavigateToDailyPage()
        {
            MainWindowViewModel.Frame.Content = new DailySchedulePage();
        }

        private void NavigateToWeeklyPage()
        {
            MainWindowViewModel.Frame.Content = new WeeklySchedulePage();
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
            if (_selectedDate != new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
            {
                _selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
        }

        private void PreviousExecute()
        {
            _selectedDate = _selectedDate.AddMonths(- 1);
        }

        private void NextExecute()
        {
            _selectedDate = _selectedDate.AddMonths(+ 1);
            OnPropertyChanged("Next");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateEventsView();
        }
    }
}