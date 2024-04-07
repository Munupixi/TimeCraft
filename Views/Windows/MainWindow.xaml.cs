using System.Windows;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(MainFrame);
        }
    }
}