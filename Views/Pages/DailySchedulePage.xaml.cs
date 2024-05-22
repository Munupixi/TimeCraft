using System;
using System.Windows.Controls;
using TimeCraft.ViewModels.Pages;

namespace TimeCraft
{
    public partial class DailySchedulePage : Page
    {
        public DailySchedulePage()
        {
            InitializeComponent();
            this.DataContext = new DailySchedulePageViewModel();
        }

        public DailySchedulePage(DateTime date)
        {
            InitializeComponent();
            this.DataContext = new DailySchedulePageViewModel(date);
        }

        private void MainListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is DailySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteCreateEvent();
            }
        }
    }
}