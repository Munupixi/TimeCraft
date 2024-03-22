using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TimeCraft
{
    public partial class CreateEditEventWindow : Window
    {
        private List<AddParticipant> addParticipants = new List<AddParticipant>();

        public CreateEditEventWindow()
        {
            InitializeComponent();
            DressCodeComboBox.ItemsSource = Enum.GetNames(typeof(DressCode));
            DressCodeComboBox.SelectedIndex = 7;
            PriorityComboBox.ItemsSource = Enum.GetNames(typeof(Priority));
            PriorityComboBox.SelectedIndex = 2;

            using (AppDBContent db = new AppDBContent())
            {
                CategoryComboBox.ItemsSource = db.Category.Select(c => c.Title).ToList();
            }
            CategoryComboBox.SelectedIndex = 0;
            StartDateDatePicker.SelectedDate = DateTime.Now.AddDays(1);
            StartTimeTextBox.Text = (DateTime.Now).ToString("HH:mm");
        }

        public void ShowCorrectnessStatus(Control element, bool correctnessStatus)
        {
            if (element == null) return;
            if (!correctnessStatus)
            {
                element.BorderBrush = Brushes.Red;
                return;
            }
            element.BorderBrush = Brushes.Green;
        }

        private bool IsAllCorrect()
        {
            if (Event.IsTimeCorrect(StartTimeTextBox.Text) &&
                Event.IsTimeCorrect(EndTimeTextBox.Text) &&
                Event.IsStartDateCorrect(  StartDateDatePicker.SelectedDate) &&
                Event.IsEndDateCorrect(
                   StartDateDatePicker.SelectedDate, StartTimeTextBox.Text,
                   EndDateDatePicker.SelectedDate, EndTimeTextBox.Text) &&
                   Event.IsTitleCorrect(TitleTextBox.Text) && 
                   Event.IsTitleUnique(TitleTextBox.Text) && IsAllParticipantsExists())
            {
                return true;
            }
            return false;
        }

        private void ShowStartDateTimeEndDateTimeCorrectnessStatus()
        {
            if (StartTimeTextBox == null || EndTimeTextBox == null ||
                StartDateDatePicker == null || EndDateDatePicker == null)
            {
                return;
            }
            ShowCorrectnessStatus(StartTimeTextBox,
               Event.IsTimeCorrect(StartTimeTextBox.Text));
            ShowCorrectnessStatus(EndTimeTextBox,
                Event.IsTimeCorrect(EndTimeTextBox.Text));
            ShowCorrectnessStatus(StartDateDatePicker,
               Event.IsStartDateCorrect(
                   StartDateDatePicker.SelectedDate));
            ShowCorrectnessStatus(EndDateDatePicker,
               Event.IsEndDateCorrect(
                   StartDateDatePicker.SelectedDate, StartTimeTextBox.Text,
                   EndDateDatePicker.SelectedDate, EndTimeTextBox.Text));
        }

        private void StartTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowStartDateTimeEndDateTimeCorrectnessStatus();
        }

        private void EndTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowStartDateTimeEndDateTimeCorrectnessStatus();
        }

        private void StartDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowStartDateTimeEndDateTimeCorrectnessStatus();
        }

        private void EndDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowStartDateTimeEndDateTimeCorrectnessStatus();
        }

        private void RefreshParticipantsDataGrid()
        {
            ParticipantsDataGrid.ItemsSource = null;
            ParticipantsDataGrid.ItemsSource = addParticipants;
            ParticipantsDataGrid.Items.Refresh();
        }
        private bool IsAllParticipantsExists()
        {
            foreach (AddParticipant addParticipant in addParticipants)
            {
                if (User.IsLoginUnique(addParticipant.Login))
                {
                    return false;
                }
            }
            return true;
        }

        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            addParticipants.Add(new AddParticipant());
            RefreshParticipantsDataGrid();
        }

        private void ClearParticipantsButton_Click(object sender, RoutedEventArgs e)
        {
            addParticipants = new List<AddParticipant>();
            RefreshParticipantsDataGrid();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAllCorrect())
            {
                MessageBox.Show("Не все поля заполнены корректно");
                return;
            }
            int idEvent = Event.GetNewId();
            Event _event = new Event(idEvent, TitleTextBox.Text,
                User.ActiveUser.UserId,
                new TextRange(DescriptionRichTextBox.Document.ContentStart,
                DescriptionRichTextBox.Document.ContentEnd).Text,
        StartDateDatePicker.SelectedDate,
        TimeSpan.Parse(StartTimeTextBox.Text),
        EndDateDatePicker.SelectedDate,
        TimeSpan.Parse(EndTimeTextBox.Text),
        LocationTextBox.Text,
     (DressCode)Enum.Parse(typeof(DressCode), DressCodeComboBox.Text),
     (Priority)Enum.Parse(typeof(Priority), PriorityComboBox.Text),
    CategoryComboBox.SelectedIndex + 1);
            _event.Add();

            foreach (AddParticipant addParticipant in addParticipants)
            {
                new Participant(Participant.GetNewId(), idEvent,
                    User.ActiveUser.UserId, false, addParticipant.Role).Add();
            }
            this.Close();
        }

        public static int GetIndexDataGridButton(object sender)
        {
            FrameworkElement button = (FrameworkElement)sender;
            DataGridRow row = (DataGridRow)button.Tag;
            return row.GetIndex();
        }

        private void DeleteParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            addParticipants.RemoveAt(GetIndexDataGridButton(sender));
            RefreshParticipantsDataGrid();
        }

        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowCorrectnessStatus(TitleTextBox,
            Event.IsTitleCorrect(TitleTextBox.Text) &&
            Event.IsTitleUnique(TitleTextBox.Text));
        }
    }
}