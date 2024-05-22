using System;
using System.Windows.Controls;
using TimeCraft.ViewModels.Pages;

namespace TimeCraft
{
    public partial class MonthlySchedulePage : Page
    {
        public MonthlySchedulePage()
        {
            InitializeComponent();
            this.DataContext = new MonthlySchedulePageViewModel();
        }
        public MonthlySchedulePage(DateTime selectedDate)
        {
            InitializeComponent();
            this.DataContext = new MonthlySchedulePageViewModel(selectedDate);
        }
    }
}