using System;
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

        public CreateEditEventWindow(Event _event, bool isReadOnly = false)
        {
            InitializeComponent();
            if (isReadOnly)
            {
                TitleTextBox.IsEnabled = LocationTextBox.IsEnabled =
                    StartDateDatePicker.IsEnabled = StartTimeTextBox.IsEnabled =
                    EndDateDatePicker.IsEnabled = EndTimeTextBox.IsEnabled =
                    CategoryComboBox.IsEnabled = PriorityComboBox.IsEnabled =
                    DressCodeComboBox.IsEnabled = ParticipantsDataGrid.IsEnabled =
                    DescriptionRichTextBox.IsEnabled = ClearParticipantsButton.IsEnabled
                    = AddParticipantButton.IsEnabled = CancelButton.IsEnabled =
                    CreateButton.IsEnabled = false;
            }
            DataContext = new CreateEditEventWindowViewModel(_event);
        }
        public CreateEditEventWindow(DateTime selectedDate)
        {
            InitializeComponent();
            DataContext = new CreateEditEventWindowViewModel(selectedDate);
        }
    }
}