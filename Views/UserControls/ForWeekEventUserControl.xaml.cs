using System.Windows.Controls;
using System.Windows.Input;
using TimeCraft.ViewModels.UserControls;

namespace TimeCraft
{
    public partial class ForWeekEventUserControl : UserControl
    {
        public ForWeekEventUserControl(Event _event)
        {
            InitializeComponent();
            DataContext = new ForWeekEventUserControlViewModel(_event);
        }

        private void UserControl_MouseLeftButtonDown(
            object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ForWeekEventUserControlViewModel viewModel)
            {
                viewModel.ExecuteOpen();
            }
        }
    }
}