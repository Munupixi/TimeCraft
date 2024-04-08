using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimeCraft.ViewModels.Pages;

namespace TimeCraft
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            DataContext = new RegistrationPageViewModel();
        }
    }
}