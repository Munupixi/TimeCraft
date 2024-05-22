using System;
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

        private void MondayListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is WeeklySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteCreateEvent(DayOfWeek.Monday);
            }
        }

        private void TuesdayListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is WeeklySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteCreateEvent(DayOfWeek.Tuesday);
            }
        }

        private void WednesdayListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is WeeklySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteCreateEvent(DayOfWeek.Wednesday);
            }
        }

        private void ThursdayListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is WeeklySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteCreateEvent(DayOfWeek.Thursday);
            }
        }

        private void FridayListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is WeeklySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteCreateEvent(DayOfWeek.Friday);
            }
        }

        private void SaturdayListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is WeeklySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteCreateEvent(DayOfWeek.Saturday);
            }
        }

        private void SundayListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is WeeklySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteCreateEvent(DayOfWeek.Sunday);
            }
        }
    }
}