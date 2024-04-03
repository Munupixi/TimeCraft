using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace TimeCraft.ViewModels.Windows
{
    internal class CreateEditEventWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Event _event = new Event(Event.GetNewId(),
            "Новое мероприятие", User.ActiveUser.UserId, "Описание",
            DateTime.Now.AddDays(1),
            TimeSpan.Parse((DateTime.Now).ToString("HH:mm")));

        public CreateEditEventWindowViewModel()
        {
            CreateCommand = new RelayCommand(CreateExecute, CanCreateExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            AddParticipantCommand = new RelayCommand(AddParticipantExecute);
            ClearParticipantsCommand = new RelayCommand(ClearParticipantsExecute);
            DeleteParticipantCommand = new RelayCommand(DeleteParticipantExecute);
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
        public string StartTime
        {
            get { return _event.StartTime.ToString(); }
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
        public string EndTime
        {
            get { return _event.EndTime.ToString(); }
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


        public ICommand CreateCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand AddParticipantCommand { get; private set; }
        public ICommand ClearParticipantsCommand { get; private set; }
        public ICommand DeleteParticipantCommand { get; private set; }

        private void CreateExecute()
        {
            // Ваша логика для создания события
        }

        private bool CanCreateExecute()
        {
            // Ваша логика проверки, может ли команда быть выполнена
            return true;
        }

        private void CancelExecute()
        {
            // Ваша логика для отмены операции
        }

        private void AddParticipantExecute( )
        {
            // Ваша логика для добавления участника
        }

        private void ClearParticipantsExecute()
        {
            // Ваша логика для очистки списка участников
        }

        private void DeleteParticipantExecute()
        {
            // Ваша логика для удаления участника
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
