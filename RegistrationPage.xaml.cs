﻿using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TimeCraft
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public void ShowCorrectnessStatus(Control element, bool correctnessStatus)
        {
            if (!correctnessStatus)
            {
                element.BorderBrush = Brushes.Red;
                return;
            }
            element.BorderBrush = Brushes.Green;
        }

        private bool IsAllCorrect()
        {
            return (!string.IsNullOrEmpty(NameTextBox.Text) &&
                User.IsAgeCorrect(AgeTextBox.Text) &&
                User.IsLoginCorrect(LoginTextBox.Text) &&
                User.IsPasswordCorrect(PasswordTextBox.Text) &&
                PasswordTextBox.Text == PasswordAgainTextBox.Text);
            // и проверка на уникальность должна быть
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAllCorrect())
            {
                MessageBox.Show("Не все поля заполнены корректно");
                return;
            }
            new User(User.GetNewId(), LoginTextBox.Text,
                PasswordTextBox.Text,
                Convert.ToInt32(AgeTextBox.Text),
                NameTextBox.Text).Add();
            NavigationService.Navigate(new WeeklySchedule());
        }

        private void AuthorizationTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void ConfirmProgramPolicyCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ConfirmButton.IsEnabled = !ConfirmButton.IsEnabled;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowCorrectnessStatus(NameTextBox,
                !string.IsNullOrEmpty(NameTextBox.Text));
        }

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowCorrectnessStatus(AgeTextBox,
                User.IsAgeCorrect(AgeTextBox.Text));
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowCorrectnessStatus(LoginTextBox,
                User.IsLoginCorrect(LoginTextBox.Text));
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowCorrectnessStatus(PasswordTextBox,
                User.IsPasswordCorrect(PasswordTextBox.Text));
        }

        private void PasswordAgainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowCorrectnessStatus(PasswordAgainTextBox,
               PasswordTextBox.Text == PasswordAgainTextBox.Text);
        }
    }
}