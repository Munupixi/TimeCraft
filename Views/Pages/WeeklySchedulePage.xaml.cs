using System.Windows.Controls;

namespace TimeCraft
{
    public partial class WeeklySchedulePage : Page
    {
        public WeeklySchedulePage()
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