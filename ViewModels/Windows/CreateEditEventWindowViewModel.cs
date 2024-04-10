using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TimeCraft.ViewModels.Windows
{
    internal class CreateEditEventWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isEdit = false;

        private ObservableCollection<DataGridParticipantModel> addParticipants =
            new ObservableCollection<DataGridParticipantModel>();

        private ObservableCollection<string> categories;

        private Event _event = new Event(Event.GetNewId(),
            "Новое мероприятие", User.ActiveUser.UserId, "Описание",
            DateTime.Now.AddDays(1),
            TimeSpan.Parse((DateTime.Now).ToString("HH:mm")),
            DateTime.Now.AddDays(2),
            TimeSpan.Parse((DateTime.Now).ToString("HH:mm")),
            "Онлайн");

        private void SetUp()
        {
            CreateCommand = new RelayCommand(CreateExecute, CanCreateExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            AddParticipantCommand = new RelayCommand(AddParticipantExecute);
            ClearParticipantsCommand = new RelayCommand(ClearParticipantsExecute);
            DeleteParticipantCommand = new RelayCommand<object>(DeleteParticipantExecute);
            categories = new ObservableCollection<string>(TimeCraft.Category.GetAllTitles());
        }

        public CreateEditEventWindowViewModel()
        {
            SetUp();
        }

        public CreateEditEventWindowViewModel(Event _event)
        {
            SetUp();
            this._event = _event;
        }

        public string Title
        {
            get { return _event.Title; }
            set
            {
                if (_event.Title != value)
                {
                    _event.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Location
        {
            get { return _event.Location; }
            set
            {
                if (_event.Location != value)
                {
                    _event.Location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        public DateTime StartDate
        {
            get { return _event.StartDate.HasValue ? _event.StartDate.Value : DateTime.MinValue; }
            set
            {
                if (_event.StartDate != value)
                {
                    _event.StartDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DateTime EndDate
        {
            get { return _event.EndDate.HasValue ? _event.EndDate.Value : DateTime.MinValue; }
            set
            {
                if (_event.EndDate != value)
                {
                    _event.EndDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        public string StartTime
        {
            get
            {
                if (TimeSpan.TryParse(_event.StartTime.ToString(), out _))
                {
                    return _event.StartTime.ToString().
                    Substring(0, _event.StartTime.ToString().Length - 3);
                }
                return _event.StartTime.ToString();
            }
            set
            {
                if (_event.StartTime.ToString() != value)
                {
                    if (TimeSpan.TryParse(value, out TimeSpan startTime))
                    {
                        _event.StartTime = startTime;
                        OnPropertyChanged("StartTime");
                    }
                }
            }
        }

        public string EndTime
        {
            get
            {
                if (TimeSpan.TryParse(_event.EndTime.ToString(), out _))
                {
                    return _event.EndTime.ToString().
                    Substring(0, _event.EndTime.ToString().Length - 3);
                }
                return _event.EndTime.ToString();
            }
            set
            {
                if (_event.EndTime.ToString() != value)
                {
                    if (TimeSpan.TryParse(value, out TimeSpan endTime))
                    {
                        _event.EndTime = endTime;
                        OnPropertyChanged("EndTime");
                    }
                }
            }
        }

        public int Category
        {
            get { return _event.CategoryId; }
            set
            {
                if (_event.CategoryId != value)
                {
                    _event.CategoryId = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        public PriorityEnum Priority
        {
            get { return (PriorityEnum)_event.Priority; }
            set
            {
                if ((PriorityEnum)_event.Priority != value)
                {
                    _event.Priority = value;
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }

        public DressCodeEnum DressCode
        {
            get { return (DressCodeEnum)_event.DressCode; }
            set
            {
                if ((DressCodeEnum)_event.DressCode != value)
                {
                    _event.DressCode = value;
                    OnPropertyChanged(nameof(DressCode));
                }
            }
        }

        public string Description
        {
            get { return _event.Description; }
            set
            {
                if (_event.Description != value)
                {
                    _event.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public ObservableCollection<String> Categories
        {
            get { return categories; }
            set
            {
                categories = new ObservableCollection<string>(TimeCraft.Category.GetAllTitles());
                OnPropertyChanged(nameof(Categories));
            }
        }

        public Array Priorities
        {
            get { return Enum.GetValues(typeof(PriorityEnum)); }
        }

        public Array DressCodes
        {
            get { return Enum.GetValues(typeof(DressCodeEnum)); }
        }

        public ObservableCollection<DataGridParticipantModel> AddParticipants
        {
            get { return addParticipants; }
            set
            {
                addParticipants = value;
                OnPropertyChanged(nameof(AddParticipants));
            }
        }

        public string Role
        {
            set
            {
                Role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public string Login
        {
            set
            {
                Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public ICommand CreateCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand AddParticipantCommand { get; private set; }
        public ICommand ClearParticipantsCommand { get; private set; }
        public ICommand DeleteParticipantCommand { get; private set; }

        private void CreateExecute()
        {
            if (!CanCreateExecute())
            {
                return;
            }
            if (isEdit)
            {
                _event.Update();
                Participant.DeleteAllByEventId(_event.EventId);
            }
            else
            {
                _event.Add();
            }
            foreach (DataGridParticipantModel addParticipant in AddParticipants)
            {
                new Participant(Participant.GetNewId(), _event.EventId,
                    User.ActiveUser.UserId, false, addParticipant.Role).Add();
            }
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }

        private bool CanCreateExecute()
        {
            if (!Event.IsTimeCorrect(StartTime) || !Event.IsTimeCorrect(EndTime) ||
                !Event.IsStartDateCorrect(StartDate) ||
                !Event.IsEndDateCorrect(StartDate, StartTime, EndDate, EndTime))
            {
                //MessageBox.Show("Неверный формат времени");
                return false;
            }

            if (!Event.IsTitleCorrect(Title))
            {
                //MessageBox.Show("Заполните поле названия");
                return false;
            }
            if (!Event.IsTitleUnique(Title) &&
                Event.Get(Title).EventId != _event.EventId)
            {
                //MessageBox.Show("Мероприятие с этим названием уже существует");
                return false;
            }
            if (!DataGridParticipantModel.IsAllParticipantsExists(AddParticipants.ToList()))
            {
                //MessageBox.Show("Не все указанные участники найдены в системы");
                return false;
            }
            //Условие проверяет, доступно ли выбранное время для создания события
            //(через метод IsFreeTime) или
            //существует ли уже событие с таким временным диапазоном (через метод Event.Get),
            //при условии редактирования.
            if (!User.ActiveUser.IsFreeTime(
                StartDate, TimeSpan.Parse(StartTime), EndDate,
               TimeSpan.Parse(EndTime)) &&
               Event.Get(StartDate, TimeSpan.Parse(StartTime),
               EndDate, TimeSpan.Parse(EndTime)).EventId != _event.EventId)
            {
                //MessageBox.Show("Данные время у вас занято другим меропритием");
                return false;
            }
            return true;
        }

        private void CancelExecute()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }

        private void AddParticipantExecute()
        {
            AddParticipants.Add(new DataGridParticipantModel());
        }

        private void ClearParticipantsExecute()
        {
            AddParticipants = new ObservableCollection<DataGridParticipantModel>();
        }

        private void DeleteParticipantExecute(object participant)
        {
            if (participant is DataGridParticipantModel addParticipant)
            {
                AddParticipants.Remove(addParticipant);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ((RelayCommand)CreateCommand).RaiseCanExecuteChanged();
        }
    }
}