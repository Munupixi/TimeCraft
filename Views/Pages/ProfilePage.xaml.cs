using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TimeCraft.ViewModels.Pages;

namespace TimeCraft
{
    public partial class ProfilePage : Page
    {
        public ProfilePage(User user)
        {
            InitializeComponent();
            DataContext = new ProfilePageViewModel(user);
        }
    }
}