using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TimeCraft.ViewModels;
using TimeCraft.ViewModels.Pages;
using TimeCraft.ViewModels.UserControls;
using TimeCraft.Views.UserControls;

namespace TimeCraft.Views.Pages
{
    public partial class InvitationsListPage : Page
    {

        public InvitationsListPage()
        {
            InitializeComponent();
            this.DataContext = new InvitationsListPageViewModel();
        }
    }
}
