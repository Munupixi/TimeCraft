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
            User user = User.GetUserByLogin(LoginTextBox.Text);
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