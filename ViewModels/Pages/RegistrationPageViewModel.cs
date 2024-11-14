using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows.Input;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft.ViewModels.Pages
{
    internal class RegistrationPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private User _user;
        private UserViewModel _userViewModel;

        public ICommand AuthorizationCommand { get; private set; }
        public ICommand RegistrationCommand { get; private set; }

        private string ageAsString;
        private string againPassword;
        private bool isConfirmProgramPolicy = false;
        private string errorMessage = "";

        public RegistrationPageViewModel()
        {
            RegistrationCommand = new RelayCommand(RegistrationExecute, CanRegistrationExecute);
            AuthorizationCommand = new RelayCommand(AuthorizationExecute);

            _user = new User(UserViewModel.GetNewId(), "Логин", "Пароль", -1);
            ageAsString = "Возраст";
            _userViewModel = new UserViewModel(_user);
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

        public bool ConfirmProgramPolicyCheck
        {
            get { return isConfirmProgramPolicy; }
            set
            {
                if (isConfirmProgramPolicy != value)
                {
                    isConfirmProgramPolicy = value;
                    OnPropertyChanged("ConfirmProgramPolicyCheck");
                }
            }
        }

        private void AuthorizationExecute()
        {
            MainWindowViewModel.Frame.Content = new AuthorizationPage();
        }

        public bool CanRegistrationExecute()
        {
            if (string.IsNullOrEmpty(Name))
            {
                ErrorMessage = "Имя не может быть пустым";
                return false;
            }
            if (Name.Length < 3)
            {
                ErrorMessage = "Имя не может быть короче 3х символов";
                return false;
            }
            if (Name.Length > 20)
            {
                ErrorMessage = "Имя не может быть длинее 20 символов";
                return false;
            }
            if (!UserViewModel.IsAgeCorrect(ageAsString))
            {
                ErrorMessage = "Вам должно быть не меньше 4 и не больше 120 лет";
                return false;
            }
            else if (_user.Age != Convert.ToInt32(ageAsString))
            {
                _user.Age = Convert.ToInt32(ageAsString);
                OnPropertyChanged("Age");
            }
            if (Login.Length > 50)
            {
                ErrorMessage = "Логин не может быть длинее 50 символов";
                return false;
            }
            if (!_userViewModel.IsLoginCorrect())
            {
                ErrorMessage = "Логин некоректен (Формат: XX@X.X)";
                return false;
            }
            if (Login.Length > 20)
            {
                ErrorMessage = "Пароль не может быть длинее 20 символов";
                return false;
            }
            if (!_userViewModel.IsPasswordCorrect())
            {
                ErrorMessage = "Пароль некоректен " +
                    "(Не менее 6 символов. Из них минимум: 1 - специальный 1 - цифра";
                return false;
            }
            if (Password != AgainPassword)
            {
                ErrorMessage = "Пароли несовпадают";
                return false;
            }
            if (!ConfirmProgramPolicyCheck)
            {
                ErrorMessage = "Не принята политика компании";
                return false;
            }
            if (!_userViewModel.IsLoginUnique())
            {
                ErrorMessage = "Логин не уникален";
                return false;
            }
            ErrorMessage = "";
            return true;
        }

        public void RegistrationExecute()
        {
            User.ActiveUser = _user;
            _userViewModel.Add();
            MainWindowViewModel.Frame.Content = new WeeklySchedulePage();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            _userViewModel = new UserViewModel(_user);
            ((RelayCommand)RegistrationCommand).RaiseCanExecuteChanged();
        }

        //public void ShowCorrectnessStatus(Control element, bool correctnessStatus)
        //{
        //    if (!correctnessStatus)
        //    {
        //        element.BorderBrush = Brushes.Red;
        //        return;
        //    }
        //    element.BorderBrush = Brushes.Green;
        //}
    }
}