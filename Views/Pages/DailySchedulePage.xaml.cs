using System.Windows.Controls;
using TimeCraft.ViewModels.Pages;

namespace TimeCraft
{
    public partial class DailySchedule : Page
    {
        public DailySchedule()
        {
            InitializeComponent();
            this.DataContext = new DailySchedulePageViewModel();
        }
    }
}