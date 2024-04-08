using System.Windows.Controls;
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