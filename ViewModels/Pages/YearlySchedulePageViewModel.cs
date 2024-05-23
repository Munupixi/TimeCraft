using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows.Input;
using TimeCraft.ViewModels.Windows;
using TimeCraft.Views.Pages;

namespace TimeCraft.ViewModels.Pages
{
    internal class YearlySchedulePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataBaseContent _context;
        private DateTime _selectedDate = new DateTime(DateTime.Now.Year, 1, 1);
        private string _year;

        public ICommand DailyPageCommand { get; private set; }
        public ICommand WeeklyPageCommand { get; private set; }
        public ICommand MontlyPageCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }
        public ICommand TaskListCommand { get; private set; }
        public ICommand InvitationListCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand TodayCommand { get; private set; }
        public ICommand NextCommand { get; private set; }

        public YearlySchedulePageViewModel()
        {
            DailyPageCommand = new RelayCommand(NavigateToDailyPage);
            WeeklyPageCommand = new RelayCommand(NavigateToWeeklyPage);
            MontlyPageCommand = new RelayCommand(NavigateToMonthlyPage);
            ProfileCommand = new RelayCommand(NavigateProfilePage);
            TaskListCommand = new RelayCommand(NavigateToTaskListPage);
            InvitationListCommand = new RelayCommand(NavigateToInvitationListPage);
            PreviousCommand = new RelayCommand(PreviousExecute);
            TodayCommand = new RelayCommand(TodayExecute);
            NextCommand = new RelayCommand(NextExecute);

            _context = new DataBaseContent();
            UpdateEventsView();
        }

        public DateTime January
        {
            get { return _selectedDate.AddMonths(0); }
        }
        public DateTime February
        {
            get { return _selectedDate.AddMonths(1); }
        }
        public DateTime March
        {
            get { return _selectedDate.AddMonths(2); }
        }
        public DateTime April
        {
            get { return _selectedDate.AddMonths(3); }
        }
        public DateTime May
        {
            get { return _selectedDate.AddMonths(4); }
        }
        public DateTime June
        {
            get { return _selectedDate.AddMonths(5); }
        }
        public DateTime July
        {
            get { return _selectedDate.AddMonths(6); }
        }
        public DateTime August
        {
            get { return _selectedDate.AddMonths(7); }
        }
        public DateTime September
        {
            get { return _selectedDate.AddMonths(8); }
        }
        public DateTime October
        {
            get { return _selectedDate.AddMonths(9); }
        }
        public DateTime November
        {
            get { return _selectedDate.AddMonths(10); }
        }
        public DateTime December
        {
            get { return _selectedDate.AddMonths(11); }
        }

        public void UpdateEventsView()
        {
            _year = _selectedDate.Year.ToString();
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(January));
            OnPropertyChanged(nameof(February));
            OnPropertyChanged(nameof(March));
            OnPropertyChanged(nameof(April));
            OnPropertyChanged(nameof(May));
            OnPropertyChanged(nameof(June));
            OnPropertyChanged(nameof(July));
            OnPropertyChanged(nameof(August));
            OnPropertyChanged(nameof(September));
            OnPropertyChanged(nameof(October));
            OnPropertyChanged(nameof(November));
            OnPropertyChanged(nameof(December));
        }

        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
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

        private void NavigateToMonthlyPage()
        {
            MainWindowViewModel.Frame.Content = new MonthlySchedulePage();
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
        private void TodayExecute()
        {
            if (_selectedDate != new DateTime(DateTime.Now.Year, 1, 1))
            {
                _selectedDate = new DateTime(DateTime.Now.Year, 1, 1);
                UpdateEventsView();
            }
        }

        private void PreviousExecute()
        {
            _selectedDate = _selectedDate.AddYears(-1);
            UpdateEventsView();
        }

        private void NextExecute()
        {
            _selectedDate = _selectedDate.AddYears(+1);
            UpdateEventsView();
        }

        public void ExecuteOpenMonth(string month)
        {
            switch (month)
            {
                case "January":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate);
                    break;
                case "February":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(1));
                    break;
                case "March":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(2));
                    break;
                case "April":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(3));
                    break;
                case "May":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(4));
                    break;
                case "June":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(5));
                    break;
                case "July":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(6));
                    break;
                case "August":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(7));
                    break;
                case "September":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(8));
                    break;
                case "October":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(9));
                    break;
                case "November":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(10));
                    break;
                case "December":
                    MainWindowViewModel.Frame.Content = new MonthlySchedulePage(_selectedDate.AddMonths(11));
                    break;
            }


        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}