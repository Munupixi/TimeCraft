using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft.ViewModels.Pages
{
    internal class RegistrationPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private User _user;

        public ICommand AuthorizationCommand { get; private set; }
        public ICommand RegistrationCommand { get; private set; }

        private string ageAsString;
        private string againPassword;

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

        public RegistrationPageViewModel()
        {
            AuthorizationCommand = new RelayCommand(RegistrationExecute, CanRegistrationExecute);
            RegistrationCommand = new RelayCommand(AuthorizationExecute);

            _user = new User(User.GetNewId(), "Логин", "Пароль", 4);
        }

        public RegistrationPageViewModel(User user)
        {
            AuthorizationCommand = new RelayCommand(RegistrationExecute, CanRegistrationExecute);
            RegistrationCommand = new RelayCommand(AuthorizationExecute);

            _user = user;
        }

        private void AuthorizationExecute()
        {
            MainWindowViewModel.Frame.Content = new AuthorizationPage();
        }

        private bool CanRegistrationExecute()
        {
            return (!string.IsNullOrEmpty(Name) &&
              User.IsAgeCorrect(AgeAsString) &&
              User.IsLoginCorrect(Login) &&
              User.IsPasswordCorrect(Password) &&
              Password == AgainPassword) &&
              User.IsLoginUnique(Login);
        }

        private void RegistrationExecute()
        {
            User.ActiveUser = _user;
            User.ActiveUser.Add();
            MainWindowViewModel.Frame.Content = new WeeklySchedule();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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