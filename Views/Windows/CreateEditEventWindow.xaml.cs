using System.Windows;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft
{
    public partial class CreateEditEventWindow : Window
    {
        public CreateEditEventWindow()
        {
            InitializeComponent();
            DataContext = new CreateEditEventWindowViewModel();
        }
    }
}