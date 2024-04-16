using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand WeeklyCommand { get; private set; }
        public ICommand MonthlyCommand { get; private set; }
        public ICommand YearlyCommand { get; private set; }

        public DailySchedulePageViewModel()
        {
            WeeklyCommand = new RelayCommand(NavigateToWeeklyPage);
            MonthlyCommand = new RelayCommand(NavigateToMonthlyPage);
            YearlyCommand = new RelayCommand(NavigateToYearlyPage);

            _context = new DataBaseContent();
            UpdateProductsView();
        }

        public void UpdateProductsView()
        {
            _events = _context.Event.ToList();

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

        private void NavigateToYearlyPage()
        {
            throw new NotImplementedException();
        }

        private void NavigateToMonthlyPage()
        {
            throw new NotImplementedException();
        }

        private void NavigateToWeeklyPage()
        {
            throw new NotImplementedException();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}