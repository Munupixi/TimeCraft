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
        private ObservableCollection<AddParticipant> addParticipants = 
            new ObservableCollection<AddParticipant>();
        private ObservableCollection<string> categories;

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
            addParticipants.Add(new AddParticipant("login1", "role1"));
            AddParticipants.Add(new AddParticipant("login2", "role2"));
            AddParticipants.Add(new AddParticipant("login1", "role1"));
            AddParticipants.Add(new AddParticipant("login2", "role2"));
            categories = new ObservableCollection<string>(TimeCraft.Category.GetAllTitles());
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

        public ObservableCollection<AddParticipant> AddParticipants
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
            // Ваша логика для создания события
        }

        private bool CanCreateExecute()
        {
            // Ваша логика проверки, может ли команда быть выполнена
            return true;
        }

        private void CancelExecute()
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (window != null)
            {
                window.Close();
            }
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
