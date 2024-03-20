using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeCraft
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            NameTextBox.Text = User.ActiveUser.Name;
            SurnameTextBox.Text = User.ActiveUser.Surname;
            PatronymicTextBox.Text = User.ActiveUser.Patronymic;
            PasswordTextBox.Text = User.ActiveUser.Password;
            AgeTextBox.Text = User.ActiveUser.Age.ToString();
            LoginTextBox.Text = User.ActiveUser.Login;
        }
        private bool IsAllCorrect()
        {
            return (!string.IsNullOrEmpty(NameTextBox.Text) &&
                User.IsAgeCorrect(AgeTextBox.Text) &&
                User.IsLoginCorrect(LoginTextBox.Text) &&
                User.IsPasswordCorrect(PasswordTextBox.Text) &&
                PasswordTextBox.Text == PasswordAgainTextBox.Text);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAllCorrect())
            {
                MessageBox.Show("Не все поля заполнены корректно");
                return;
            }
            User.ActiveUser.Name = NameTextBox.Text;
            User.ActiveUser.Surname = SurnameTextBox.Text;
            User.ActiveUser.Patronymic = PatronymicTextBox.Text;
            User.ActiveUser.Password = PasswordTextBox.Text;
            User.ActiveUser.Age = Convert.ToInt32(AgeTextBox.Text);
            User.ActiveUser.Update();
            NavigationService.Navigate(new WeeklySchedule());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
