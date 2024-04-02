using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace TimeCraft
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {

            //4 testin'
            if (LoginTextBox.Text == "1")
            {
                User _user = User.Get("1");
                if (_user == null)
                {
                    _user = new User(User.GetNewId(), "1", "1", 120);
                    _user.Add();
                }
                User.ActiveUser = _user;
                NavigationService.Navigate(new WeeklySchedule());
                new CreateEditEventWindow(Event.Get(1)).Show();
                return;
            }


            User user = User.Get(LoginTextBox.Text);
            if (user == null)
            {
                MessageBox.Show("Пользователь с данным логином не найден");
                return;
            }
            if (user.Password != PasswordTextBox.Text)
            {
                MessageBox.Show("Пароль не верен");
                return;
            }
            User.ActiveUser = user;
            NavigationService.Navigate(new WeeklySchedule());
        }

        private void RegistrationTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}