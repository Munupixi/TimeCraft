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

        private void UserControl_MouseLeftButtonDown(
            object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ForMonthEventUserControlViewModel viewModel)
            {
                viewModel.ExecuteOpenDay();
            }
        }

        private void нажатиеНаЭлементВlistView(
            object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ForMonthEventUserControlViewModel viewModel)
            {
                viewModel.ExecuteOpenEvent(_event);
            }
        }
    }
}