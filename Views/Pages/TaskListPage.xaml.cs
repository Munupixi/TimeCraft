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
using TimeCraft.ViewModels.Pages;

namespace TimeCraft.Views.Pages
{
    public partial class TaskListPage : Page
    {
        public TaskListPage()
        {
            InitializeComponent();
            this.DataContext = new TaskListPageViewModel();
        }

        private void MainListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is TaskListPageViewModel viewModel)
            {
                viewModel.ExecuteCreateTask();
            }
        }
    }
