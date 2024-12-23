﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TimeCraft.ViewModels.Models.ServiceModelsViewModels;

namespace TimeCraft.ViewModels.Windows
{
    internal class CreateEditEventWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isEdit = false;

        private List<DataGridParticipant> addParticipants =
            new List<DataGridParticipant>();

        private ObservableCollection<string> categories;

        public Event _event;

        private EventViewModel _eventViewModel;

        private string errorMessage;

        private void SetUp()
        {
            CreateCommand = new RelayCommand(CreateExecute, CanCreateExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            AddParticipantCommand = new RelayCommand(AddParticipantExecute);
            ClearParticipantsCommand = new RelayCommand(ClearParticipantsExecute);
            DeleteParticipantCommand = new RelayCommand<object>(DeleteParticipantExecute);
            categories = new ObservableCollection<string>(CategoryViewModel.GetAllTitles());
        }

        public CreateEditEventWindowViewModel()
        {
            SetUp();
            this._event = new Event(EventViewModel.GetNewId(),
            "Новое мероприятие", User.ActiveUser.UserId, "Описание",
            DateTime.Now.AddDays(1),
            TimeSpan.Parse((DateTime.Now).ToString("HH:mm")),
            DateTime.Now.AddDays(2),
            TimeSpan.Parse((DateTime.Now).ToString("HH:mm")));
            _eventViewModel = new EventViewModel(_event);
        }
        public CreateEditEventWindowViewModel(DateTime selectedDate)
        {
            SetUp();
            this._event = new Event(EventViewModel.GetNewId(),
            "Новое мероприятие", User.ActiveUser.UserId, "Описание",
            selectedDate.Date,
            TimeSpan.Parse((selectedDate).ToString("HH:mm")),
            selectedDate.AddDays(1),
            TimeSpan.Parse((selectedDate).ToString("HH:mm")));
            _eventViewModel = new EventViewModel(_event);
        }
        public CreateEditEventWindowViewModel(Event _event)
        {
            SetUp();
            isEdit = true;
            this._event = _event;
            this.addParticipants = DataGridParticipantViewModel.ConvertParticipants(ParticipantViewModel.GetAllParticipantByIdEvent(_event.EventId));
            _eventViewModel = new EventViewModel(_event);
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
            get { return _event.StartDate; }
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
            get { return _event.CategoryId - 1; }
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

        public Array DressCodes
        {
            get { return Enum.GetValues(typeof(DressCodeEnum)); }
        }

        public List<DataGridParticipant> AddParticipants
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

        public void CreateExecute()
        {
            if (isEdit)
            {
                _eventViewModel.Update();
                ParticipantViewModel.DeleteAllByEventId(_event.EventId);
            }
            else
            {
                _eventViewModel.Add();
            }
            foreach (DataGridParticipant addParticipant in AddParticipants)
            {
                new ParticipantViewModel(new Participant(ParticipantViewModel.GetNewId(), _event.EventId,
                    UserViewModel.Get(addParticipant.Login).UserId, false, addParticipant.Role)).Add();
            }
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }

        public bool CanCreateExecute()
        {
            if (!Event.IsTimeCorrect(StartTime) || !Event.IsTimeCorrect(EndTime) ||
                !Event.IsStartDateCorrect(StartDate) ||
                !Event.IsEndDateCorrect(StartDate, StartTime, EndDate, EndTime))
            {
                ErrorMessage = "Неверный формат времени";
                return false;
            }

            if (!Event.IsTitleCorrect(Title))
            {
                ErrorMessage = "Заполните поле названия";
                return false;
            }
            if (!_eventViewModel.IsTitleUnique() &&
                EventViewModel.Get(Title).EventId != _event.EventId)
            {
                ErrorMessage = "Мероприятие с этим названием уже существует";
                return false;
            }
            if (!DataGridParticipantViewModel.IsAllParticipantsExists(AddParticipants.ToList()))
            {
                ErrorMessage = "Не все указанные участники найдены в системы";
                return false;
            }
            //Условие проверяет, доступно ли выбранное время для создания события
            //(через метод IsFreeTime) или
            //существует ли уже событие с таким временным диапазоном (через метод Event.Get),
            //при условии редактирования.
            if (!new UserViewModel(User.ActiveUser).IsFreeTime(_event))
            {
                Event blockedEvent = EventViewModel.Get(_event.StartDate, _event.StartTime, _event.EndDate, _event.EndTime);
                if (blockedEvent.EventId != _event.EventId)
                {
                    ErrorMessage = "Данные время у вас занято меропритием:\n" + blockedEvent.Title;
                    return false;
                }
            }
            ErrorMessage = string.Empty;
            return true;
        }

        private void CancelExecute()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive).Close();
        }

        private void AddParticipantExecute()
        {
            AddParticipants.Add(new DataGridParticipant());
        }

        private void ClearParticipantsExecute()
        {
            AddParticipants = new List<DataGridParticipant>();
        }

        private void DeleteParticipantExecute(object participant)
        {
            if (participant is DataGridParticipant addParticipant)
            {
                AddParticipants.Remove(addParticipant);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _eventViewModel = new EventViewModel(_event);
            ((RelayCommand)CreateCommand).RaiseCanExecuteChanged();
        }
    }
}