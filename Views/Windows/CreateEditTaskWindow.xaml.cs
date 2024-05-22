using System.Windows;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft
{
    public partial class CreateEditTaskWindow : Window
    {
        public CreateEditTaskWindow()
        {
            InitializeComponent();
            DataContext = new CreateEditTaskWindowViewModel();
        }
        public CreateEditTaskWindow(Task task)
        {
            InitializeComponent();
            DataContext = new CreateEditTaskWindowViewModel(task);
        }
    }
}