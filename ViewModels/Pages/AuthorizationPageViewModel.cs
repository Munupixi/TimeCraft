using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft.ViewModels.Pages
{
    internal class AuthorizationPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataBaseContent db = new DataBaseContent();
        private string login;
        private string password;
        private string errorMessage;

        public ICommand AuthorizationCommand { get; private set; }
        public ICommand RegistrationCommand { get; private set; }

        public AuthorizationPageViewModel()
        {
            AuthorizationCommand = new RelayCommand(AuthorizationExecute, CanAuthorizationExecute);
            RegistrationCommand = new RelayCommand(RegistrationExecute);
        }

        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
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

        private void RegistrationExecute()
        {
            MainWindowViewModel.Frame.Content = new RegistrationPage();
        }

        private bool CanAuthorizationExecute()
        {
            if (login == "1") // must be delete
            { // must be delete
                ErrorMessage = string.Empty; // must be delete
                return true; // must be delete
            } // must be delete
            User user = UserViewModel.Get(login);
            if (user == null)
            {
                ErrorMessage = "Пользователь с данным логином не найден";
                return false;
            }
            if (user.Password != password)
            {
                ErrorMessage = "Пароль не верен";
                return false;
            }
            ErrorMessage = string.Empty;
            return true;
        }

        private void AuthorizationExecute()
        {
            if (login == "1") // must be delete
            { // must be delete
                User _user = UserViewModel.Get("1"); // must be delete
                if (_user == null) // must be delete
                { // must be delete
                    _user = new User(UserViewModel.GetNewId(), "1", "1", 120); // must be delete
                    new UserViewModel(_user).Add(); // must be delete
                } // must be delete
            } // must be delete
            User.ActiveUser = UserViewModel.Get(login);
            MainWindowViewModel.Frame.Content = new WeeklySchedule();
            new CreateEditEventWindow().Show();
            new CreateEditTaskWindow().Show();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ((RelayCommand)AuthorizationCommand).RaiseCanExecuteChanged();
        }
    }
}