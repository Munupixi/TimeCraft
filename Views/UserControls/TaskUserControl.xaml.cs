using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeCraft.ViewModels.UserControls;

namespace TimeCraft.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TaskUserControl.xaml
    /// </summary>
    public partial class TaskUserControl : UserControl
    {
        public TaskUserControl(Task _task)
        {
            InitializeComponent();
            DataContext = new TaskUserControlViewModel(_task);
        }

        private void IsDoneCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is TaskUserControlViewModel viewModel)
            {
                viewModel.IsDoneChanged();
            }
        }
    }
}