using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft.ViewModels.Pages
{
    internal class ProfilePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private User _user;
        private UserViewModel _userViewModel;

        public ICommand CancelCommand { get; private set; }
        public ICommand ApllyCommand { get; private set; }
        public ProfilePageViewModel(User user)
        {
            _user = user;
            ApllyCommand = new RelayCommand(ApllyExecute, CanApllyExecute);
            CancelCommand = new RelayCommand(CancelExecute);
        }

        private string ageAsString;
        private string againPassword;
        private string errorMessage = "";

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

        public string AgeAsString
        {
            get { return ageAsString; }
            set
            {
                if (ageAsString != value)
                {
                    ageAsString = value;
                    OnPropertyChanged("AgeAsString");
                }
            }
        }

        public string Login
        {
            get { return _user.Login; }
            set
            {
                if (_user.Login != value)
                {
                    _user.Login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return _user.Password; }
            set
            {
                if (_user.Password != value)
                {
                    _user.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string AgainPassword
        {
            get { return againPassword; }
            set
            {
                if (againPassword != value)
                {
                    againPassword = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Name
        {
            get { return _user.Name; }
            set
            {
                if (_user.Name != value)
                {
                    _user.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Surname
        {
            get { return _user.Surname; }
            set
            {
                if (_user.Surname != value)
                {
                    _user.Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
        public string Patronymic
        {
            get { return _user.Patronymic; }
            set
            {
                if (_user.Patronymic != value)
                {
                    _user.Patronymic = value;
                    OnPropertyChanged("Patronymic");
                }
            }
        }

        private void CancelExecute()
        {
            MainWindowViewModel.Frame.GoBack();
        }

        private bool CanApllyExecute()
        {
            if (string.IsNullOrEmpty(Name))
            {
                ErrorMessage = "Имя не может быть пустым";
                return false;
            }
            if (!UserViewModel.IsAgeCorrect(ageAsString))
            {
                ErrorMessage = "Возраст некоректен";
                return false;
            }
            else if (_user.Age != Convert.ToInt32(ageAsString))
            {
                _user.Age = Convert.ToInt32(ageAsString);
                OnPropertyChanged("Age");
            }
            if (!_userViewModel.IsPasswordCorrect())
            {
                ErrorMessage = "Пароль некоректен";
                return false;
            }
            if (Password != AgainPassword)
            {
                ErrorMessage = "Пароли несовпадают";
                return false;
            }
            ErrorMessage = "";
            return true;
        }

        private void ApllyExecute()
        {
            User.ActiveUser = _user;
            _userViewModel.Update();
            MainWindowViewModel.Frame.Content = new WeeklySchedule();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _userViewModel = new UserViewModel(_user);
            ((RelayCommand)ApllyCommand).RaiseCanExecuteChanged();
        }

    }
}
