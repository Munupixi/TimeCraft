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
using TimeCraft.ViewModels.Models.ServiceModelsViewModels;

namespace TimeCraft.ViewModels.Windows
{
    internal class CreateEditTaskWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isEdit = false;

        private ObservableCollection<string> categories;

        private Task _task;

        private TaskViewModel _taskViewModel;

        private string errorMessage;

        public CreateEditTaskWindowViewModel()
        {
            SetUp();
            this._task = new Task(TaskViewModel.GetNewId(), "Новое напоминание", User.ActiveUser.UserId,
                "Описание", DateTime.Now.AddDays(1), TimeSpan.Parse((DateTime.Now).ToString("HH:mm")));
            _taskViewModel = new TaskViewModel(_task);
        }

        public CreateEditTaskWindowViewModel(Task task)
        {
            SetUp();
            isEdit = true;
            this._task = task;
            _taskViewModel = new TaskViewModel(task);
        }

        private void SetUp()
        {
            CreateCommand = new RelayCommand(CreateExecute, CanCreateExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            DecreaseRepeatCommand = new RelayCommand(DecreaseRepeatExecute);
            IncreaseRepeatCommand = new RelayCommand<object>(IncreaseRepeatExecute);
            categories = new ObservableCollection<string>(CategoryViewModel.GetAllTitles());
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }

        public string Title
        {
            get { return _task.Title; }
            set
            {
                if (_task.Title != value)
                {
                    _task.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public DateTime StartDate
        {
            get { return _task.StartDate; }
            set
            {
                if (_task.StartDate != value)
                {
                    _task.StartDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DateTime EndDate
        {
            get { return _task.EndDate.HasValue ? _task.EndDate.Value : DateTime.MinValue; }
            set
            {
                if (_task.EndDate != value)
                {
                    _task.EndDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public string StartTime
        {
            get
            {
                if (TimeSpan.TryParse(_task.StartTime.ToString(), out _))
                {
                    return _task.StartTime.ToString().
                    Substring(0, _task.StartTime.ToString().Length - 3);
                }
                return _task.StartTime.ToString();
            }
            set
            {
                if (_task.StartTime.ToString() != value)
                {
                    if (TimeSpan.TryParse(value, out TimeSpan startTime))
                    {
                        _task.StartTime = startTime;
                        OnPropertyChanged("StartTime");
                    }
                }
            }
        }

        public string EndTime
        {
            get
            {
                if (TimeSpan.TryParse(_task.EndTime.ToString(), out _))
                {
                    return _task.EndTime.ToString().
                    Substring(0, _task.EndTime.ToString().Length - 3);
                }
                return _task.EndTime.ToString();
            }
            set
            {
                if (_task.EndTime.ToString() != value)
                {
                    if (TimeSpan.TryParse(value, out TimeSpan endTime))
                    {
                        _task.EndTime = endTime;
                        OnPropertyChanged("EndTime");
                    }
                }
            }
        }

        public string Repeat
        {
            get
            {
                return _task.Repeat.ToString();
            }
            set
            {
                if (_task.Repeat.ToString() != value)
                {
                    if (Int16.TryParse(value, out short repeat))
                    {
                        _task.Repeat = repeat;
                        OnPropertyChanged("Repeat");
                    }
                }
            }
        }

        public int Category
        {
            get { return _task.IdCategory - 1; }
            set
            {
                if (_task.IdCategory != value)
                {
                    _task.IdCategory = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        public PriorityEnum Priority
        {
            get { return (PriorityEnum)_task.Priority; }
            set
            {
                if ((PriorityEnum)_task.Priority != value)
                {
                    _task.Priority = value;
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }

        public string Description
        {
            get { return _task.Description; }
            set
            {
                if (_task.Description != value)
                {
                    _task.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public ObservableCollection<string> Categories
        {
            get { return categories; }
            set
            {
                categories = new ObservableCollection<string>(CategoryViewModel.GetAllTitles());
                OnPropertyChanged(nameof(Categories));
            }
        }

        public Array Priorities
        {
            get { return Enum.GetValues(typeof(PriorityEnum)); }
        }

        public ICommand CreateCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand DecreaseRepeatCommand { get; private set; }
        public ICommand IncreaseRepeatCommand { get; private set; }

        private void CreateExecute()
        {
            if (isEdit)
            {
                _taskViewModel.Update();
            }
            else
            {
                _taskViewModel.Add();
            }
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }

        private bool CanCreateExecute()
        {
            //if (!Event.IsTimeCorrect(StartTime) || !Event.IsTimeCorrect(EndTime) ||
            //    !Event.IsStartDateCorrect(StartDate) ||
            //    !Event.IsEndDateCorrect(StartDate, StartTime, EndDate, EndTime))
            //{
            //    ErrorMessage = "Неверный формат времени";
            //    return false;
            //}

            //if (!Event.IsTitleCorrect(Title))
            //{
            //    ErrorMessage = "Заполните поле названия";
            //    return false;
            //}
            //if (!_taskViewModel.IsTitleUnique() &&
            //    EventViewModel.Get(Title).EventId != _task.EventId)
            //{
            //    ErrorMessage = "Мероприятие с этим названием уже существует";
            //    return false;
            //}
            //if (!DataGridParticipantViewModel.IsAllParticipantsExists(AddParticipants.ToList()))
            //{
            //    ErrorMessage = "Не все указанные участники найдены в системы";
            //    return false;
            //}
            ////Условие проверяет, доступно ли выбранное время для создания события
            ////(через метод IsFreeTime) или
            ////существует ли уже событие с таким временным диапазоном (через метод Event.Get),
            ////при условии редактирования.
            //if (!new UserViewModel(User.ActiveUser).IsFreeTime(_task))
            //{
            //    ErrorMessage = "Данные время у вас занято меропритием:\n" +
            //        EventViewModel.Get(_task.StartDate, _task.StartTime, _task.EndDate, _task.EndTime).Title;
            //    return false;
            //}
            //ErrorMessage = string.Empty;
            return true;
        }

        private void IncreaseRepeatExecute(object obj)
        {
            _task.Repeat++;
        }

        private void DecreaseRepeatExecute()
        {
            _task.Repeat = Convert.ToInt16((_task.Repeat != 0) ? _task.Repeat - 1 : 0);
        }

        private void CancelExecute()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _taskViewModel = new TaskViewModel(_task);
            ((RelayCommand)CreateCommand).RaiseCanExecuteChanged();
        }
    }
}