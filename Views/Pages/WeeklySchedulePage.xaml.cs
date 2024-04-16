using System.Windows.Controls;

namespace TimeCraft
{
    public partial class WeeklySchedule : Page
    {
        public WeeklySchedule()
        {
            InitializeComponent();
            MondayListView.Items.Add(new ForWeekEventUserControl());
            MondayListView.Items.Add(new ForWeekEventUserControl());
            MondayListView.Items.Add(new ForWeekEventUserControl());
            MondayListView.Items.Add(new ForWeekEventUserControl());
            MondayListView.Items.Add(new ForWeekEventUserControl());
        }
    }
}