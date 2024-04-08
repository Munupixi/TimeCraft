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
            return (!string.IsNullOrEmpty(NameTextBox.Text) &&
      User.IsAgeCorrect(AgeTextBox.Text) &&
      User.IsLoginCorrect(LoginTextBox.Text) &&
      User.IsPasswordCorrect(PasswordTextBox.Text) &&
      PasswordTextBox.Text == PasswordAgainTextBox.Text) &&
      User.IsLoginUnique(LoginTextBox.Text);
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
            ((RelayCommand)AuthorizationCommand).RaiseCanExecuteChanged();
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