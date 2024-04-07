using System.Windows.Controls;

namespace TimeCraft
{
    public partial class WeeklySchedule : Page
    {
        public WeeklySchedule()
        {
            InitializeComponent();
            MondayListView.Items.Add(new EventUserControl());
            MondayListView.Items.Add(new EventUserControl());
            MondayListView.Items.Add(new EventUserControl());
            MondayListView.Items.Add(new EventUserControl());
            MondayListView.Items.Add(new EventUserControl());
        }
    }
}