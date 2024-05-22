using System.Windows.Controls;
using TimeCraft.ViewModels.Pages;
using TimeCraft.ViewModels.UserControls;

namespace TimeCraft
{
    /// <summary>
    /// Логика взаимодействия для YearlySchedule.xaml
    /// </summary>
    public partial class YearlySchedulePage : Page
    {
        public YearlySchedulePage()
        {
            InitializeComponent();
            this.DataContext = new YearlySchedulePageViewModel();
        }

        private void JanuaryCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("January");
            }
        }

        private void FebruaryCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("February");
            }
        }

        private void MarchCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("March");
            }
        }

        private void AprilCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("April");
            }
        }

        private void MayCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("May");
            }
        }

        private void JuneCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("June");
            }
        }

        private void JulyCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("July");
            }
        }

        private void AugustCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("August");
            }
        }

        private void DecemberCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("December");
            }
        }

        private void NovemberCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("November");
            }
        }

        private void OctoberCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("October");
            }
        }

        private void SeptemberCalendar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is YearlySchedulePageViewModel viewModel)
            {
                viewModel.ExecuteOpenMonth("September");
            }
        }

    }
}