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

namespace TimeCraft
{
    public partial class MainWindow : Window
    {
        private readonly AppDBContent appDBContent = new AppDBContent();
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Category.CreateAllCategories();
            }
            catch { }

            //CreateEditEventWindow createEditEventWindow = new CreateEditEventWindow();
            //createEditEventWindow.Show();

            //CreateEditTaskWindow createEditTaskWindow = new CreateEditTaskWindow();
            //createEditTaskWindow.Show();
           MainFrame.Navigate(new AuthorizationPage());
        }
    }
}