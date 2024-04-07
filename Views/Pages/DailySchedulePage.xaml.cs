using System.Windows.Controls;

namespace TimeCraft
{
    public partial class DailySchedule : Page
    {
        public DailySchedule()
        {
            InitializeComponent();

            MainListView.Items.Clear();
            //MainListView.Items.Add(new Task());
        }
    }
}