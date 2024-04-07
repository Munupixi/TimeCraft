using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using TimeCraft.ViewModels.Pages;

namespace TimeCraft
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            DataContext = new AuthorizationPageViewModel();
        }
    }
}