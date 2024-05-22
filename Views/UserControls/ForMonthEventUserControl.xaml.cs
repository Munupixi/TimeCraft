using System;
using System.Windows.Controls;
using System.Windows.Input;
using TimeCraft.ViewModels.UserControls;

namespace TimeCraft
{
    public partial class ForMonthEventUserControl : UserControl
    {
        public ForMonthEventUserControl(DateTime date)
        {
            InitializeComponent();
            DataContext = new ForMonthEventUserControlViewModel(date);
        }
        public void Update(DateTime date)
        {
            DataContext = new ForMonthEventUserControlViewModel(date);
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ForMonthEventUserControlViewModel viewModel)   // Нажатие на дату чтобы откарылосб расписание на выбранный день
            {
                viewModel.ExecuteOpenDay();
            }
        }
    }
}