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
using GalaSoft.MvvmLight.Command;

namespace TimeCraft.ViewModels.Pages
{
    internal class TaskListPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataBaseContent _context;
        private ObservableCollection<Task> _tasks = new ObservableCollection<Task>();
        private ObservableCollection<TaskUserControl> _taskUserControls = new ObservableCollection<TaskUserControl>();
        private DateTime _date;
        private string _search = "";
        private bool _isDone;
        private Visibility _noSelectedMessageVisibility;

        public ICommand ClearCommand { get; private set; }

        public TaskListPageViewModel()
        {
            ClearCommand = new RelayCommand(ClearExecute);

            TaskViewModel.TasksUpdated += HandleTasksUpdated;

            _context = new DataBaseContent();
            _tasks = new ObservableCollection<Task>(TaskViewModel.GetAll(User.ActiveUser.UserId));

            UpdateTasksView();
        }

        private void HandleTasksUpdated(object sender, EventArgs e)
        {
            UpdateTasksView();
        }

        public void UpdateTasksView()
        {
            _tasks = new ObservableCollection<Task>(TaskViewModel.GetAll(User.ActiveUser.UserId));
            _taskUserControls.Clear();
            foreach (Task _task in _tasks)
            {
                if (!_task.Title.ToLower().Contains(Search.ToLower()))
                {
                    continue;
                }
                if (Date != _task.StartDate && Date != new DateTime(1, 1, 1))
                {
                    continue;
                }
                if (!IsDone && _task.IsDone == true)
                {
                    continue;
                }
                _taskUserControls.Add(new TaskUserControl(_task));
            }

            Tasks = _taskUserControls;
            NoSelectedMessageVisibility =
                _tasks.Count == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        public ObservableCollection<TaskUserControl> Tasks
        {
            get { return _taskUserControls; }
            set
            {
                if (_taskUserControls != value)
                {
                    _taskUserControls = value;
                    OnPropertyChanged("Tasks");
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

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone != value)
                {
                    _isDone = value;
                    OnPropertyChanged("IsDone");
                }
            }
        }

        public void ClearExecute()
        {
            Search = String.Empty;
            Date = new DateTime(1, 1, 1);
            IsDone = false;
        }

        public void ExecuteCreateTask()
        {
            new CreateEditTaskWindow().Show();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateTasksView();
        }
    }
}