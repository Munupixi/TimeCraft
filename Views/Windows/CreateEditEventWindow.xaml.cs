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

        public CreateEditEventWindow(Event _event)
        {
            InitializeComponent();
            DataContext = new CreateEditEventWindowViewModel(_event);
        }
    }
}