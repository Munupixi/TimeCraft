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
    public partial class InvitationUserControl : UserControl
    {
        public InvitationUserControl(Participant participant)
        {
            InitializeComponent();
            if (participant.IsAccepted)
            {
                AcceptButton.Visibility = Visibility.Hidden;
            }
            else
            {
                DeclineButton.Visibility = Visibility.Hidden;
            }
            DataContext = new InvitationUserControlViewModel(participant);
        }
    }
}
