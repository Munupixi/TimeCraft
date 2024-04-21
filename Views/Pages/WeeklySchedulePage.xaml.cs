using System.Windows.Controls;
using TimeCraft.ViewModels.Pages;

namespace TimeCraft
{
    public partial class WeeklySchedulePage : Page
    {
        public WeeklySchedulePage()
        {
            InitializeComponent();
            this.DataContext = new WeeklySchedulePageViewModel();
        }
    }
}