using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft
{
    public partial class CreateEditEventWindow : Window
    {
        //private List<AddParticipant> addParticipants = new List<AddParticipant>();
        //private int idEvent = Event.GetNewId();
        //private bool isEdit = false;

        //Конструктор для создания мероприятия
        public CreateEditEventWindow()
        {
            InitializeComponent();
            DataContext = new CreateEditEventWindowViewModel();
            //SetUpComboBox();

            //StartDateDatePicker.SelectedDate = DateTime.Now.AddDays(1);
            //StartTimeTextBox.Text = (DateTime.Now).ToString("HH:mm");
        }

        ////Конструктор для редактирования мероприятия
        //public CreateEditEventWindow(Event _event)
        //{
        //    InitializeComponent();
        //    SetUpComboBox();

        //    idEvent = _event.EventId;
        //    TitleTextBox.Text = _event.Title;
        //    LocationTextBox.Text = _event.Location;
        //    StartDateDatePicker.SelectedDate = _event.StartDate;
        //    StartTimeTextBox.Text = _event.StartTime.ToString().Substring(
        //        0, _event.StartTime.ToString().Length - 3);
        //    EndDateDatePicker.SelectedDate = _event.EndDate;
        //    EndTimeTextBox.Text = _event.EndTime.ToString().Substring(
        //        0, _event.EndTime.ToString().Length - 3);
        //    DressCodeComboBox.SelectedIndex = (int)_event.DressCode;
        //    CategoryComboBox.SelectedIndex = _event.CategoryId;
        //    PriorityComboBox.SelectedIndex = (int)_event.Priority;
        //    DescriptionRichTextBox.Document.Blocks.Clear();
        //    DescriptionRichTextBox.AppendText(_event.Description);
        //    addParticipants = AddParticipant.ConvertParticipants(
        //        Participant.GetAllParticipantByIdEvent(_event.EventId));
        //    RefreshParticipantsDataGrid();
        //    ShowStartDateTimeEndDateTimeCorrectnessStatus();
        //    ShowCorrectnessStatus(TitleTextBox,
        //     Event.IsTitleCorrect(TitleTextBox.Text) &&
        //     (Event.IsTitleUnique(TitleTextBox.Text) ||
        //     Event.Get(TitleTextBox.Text).EventId == idEvent));
        //    isEdit = true;
        //}

        //private void SetUpComboBox()
        //{
        //    DressCodeComboBox.ItemsSource = Enum.GetNames(typeof(DressCodeEnum));
        //    DressCodeComboBox.SelectedIndex = 7;
        //    PriorityComboBox.ItemsSource = Enum.GetNames(typeof(PriorityEnum));
        //    PriorityComboBox.SelectedIndex = 2;

        //    CategoryComboBox.ItemsSource = Category.GetAllTitles();
        //    CategoryComboBox.SelectedIndex = 0;
        //}

        //public void ShowCorrectnessStatus(Control element, bool correctnessStatus)
        //{
        //    if (element == null) return;
        //    if (!correctnessStatus)
        //    {
        //        element.BorderBrush = Brushes.Red;
        //        return;
        //    }
        //    element.BorderBrush = Brushes.Green;
        //}

        //private bool IsAllCorrect()
        //{
        //    if (!Event.IsTimeCorrect(StartTimeTextBox.Text) ||
        //        !Event.IsTimeCorrect(EndTimeTextBox.Text) ||
        //        !Event.IsStartDateCorrect(StartDateDatePicker.SelectedDate) ||
        //        !Event.IsEndDateCorrect(
        //           StartDateDatePicker.SelectedDate, StartTimeTextBox.Text,
        //           EndDateDatePicker.SelectedDate, EndTimeTextBox.Text))
        //    {
        //        MessageBox.Show("Неверный формат времени");
        //        return false;
        //    }

        //    if (!Event.IsTitleCorrect(TitleTextBox.Text))
        //    {
        //        MessageBox.Show("Заполните поле названия");
        //        return false;
        //    }
        //    if (!Event.IsTitleUnique(TitleTextBox.Text) &&
        //        Event.Get(TitleTextBox.Text).EventId != idEvent)
        //    {
        //        MessageBox.Show("Мероприятие с этим названием уже существует");
        //        return false;
        //    }
        //    if (!AddParticipant.IsAllParticipantsExists(addParticipants))
        //    {
        //        MessageBox.Show("Не все указанные участники найдены в системы");
        //        return false;
        //    }
        //    //Условие проверяет, доступно ли выбранное время для создания события
        //    //(через метод IsFreeTime) или
        //    //существует ли уже событие с таким временным диапазоном (через метод Event.Get),
        //    //при условии редактирования.
        //    if (!User.ActiveUser.IsFreeTime(
        //        StartDateDatePicker.SelectedDate,
        //        TimeSpan.Parse(StartTimeTextBox.Text),
        //       EndDateDatePicker.SelectedDate,
        //       TimeSpan.Parse(EndTimeTextBox.Text)) &&
        //       Event.Get(
        //        StartDateDatePicker.SelectedDate,
        //        TimeSpan.Parse(StartTimeTextBox.Text),
        //       EndDateDatePicker.SelectedDate,
        //       TimeSpan.Parse(EndTimeTextBox.Text)).EventId != idEvent)
        //    {
        //        MessageBox.Show("Данные время у вас занято другим меропритием");
        //        return false;
        //    }
        //    return true;
        //}

        //private void ShowStartDateTimeEndDateTimeCorrectnessStatus()
        //{
        //    if (StartTimeTextBox == null || EndTimeTextBox == null ||
        //        StartDateDatePicker == null || EndDateDatePicker == null)
        //    {
        //        return;
        //    }
        //    ShowCorrectnessStatus(StartTimeTextBox,
        //       Event.IsTimeCorrect(StartTimeTextBox.Text));
        //    ShowCorrectnessStatus(EndTimeTextBox,
        //        Event.IsTimeCorrect(EndTimeTextBox.Text));
        //    ShowCorrectnessStatus(StartDateDatePicker,
        //       Event.IsStartDateCorrect(
        //           StartDateDatePicker.SelectedDate));
        //    ShowCorrectnessStatus(EndDateDatePicker,
        //       Event.IsEndDateCorrect(
        //           StartDateDatePicker.SelectedDate, StartTimeTextBox.Text,
        //           EndDateDatePicker.SelectedDate, EndTimeTextBox.Text));
        //}

        //private void StartTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    ShowStartDateTimeEndDateTimeCorrectnessStatus();
        //}

        //private void EndTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    ShowStartDateTimeEndDateTimeCorrectnessStatus();
        //}

        //private void StartDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ShowStartDateTimeEndDateTimeCorrectnessStatus();
        //}

        //private void EndDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ShowStartDateTimeEndDateTimeCorrectnessStatus();
        //}

        //private void RefreshParticipantsDataGrid()
        //{
        //    ParticipantsDataGrid.ItemsSource = null;
        //    ParticipantsDataGrid.ItemsSource = addParticipants;
        //    ParticipantsDataGrid.Items.Refresh();
        //}

        //private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        //{
        //    addParticipants.Add(new AddParticipant());
        //    RefreshParticipantsDataGrid();
        //}

        //private void ClearParticipantsButton_Click(object sender, RoutedEventArgs e)
        //{
        //    addParticipants = new List<AddParticipant>();
        //    RefreshParticipants DataGrid();
        //}

        //private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

        //private void CreateButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!IsAllCorrect())
        //    {
        //        return;
        //    }
        //    Event _event = new Event(idEvent, TitleTextBox.Text,
        //        User.ActiveUser.UserId,
        //        new TextRange(DescriptionRichTextBox.Document.ContentStart,
        //        DescriptionRichTextBox.Document.ContentEnd).Text,
        //        StartDateDatePicker.SelectedDate,
        //        TimeSpan.Parse(StartTimeTextBox.Text),
        //        EndDateDatePicker.SelectedDate,
        //        TimeSpan.Parse(EndTimeTextBox.Text),
        //        LocationTextBox.Text,
        //        (DressCodeEnum)Enum.Parse(typeof(DressCodeEnum), DressCodeComboBox.Text),
        //        (PriorityEnum)Enum.Parse(typeof(PriorityEnum), PriorityComboBox.Text),
        //        CategoryComboBox.SelectedIndex + 1);
        //    if (isEdit)
        //    {
        //        _event.Update();
        //        Participant.DeleteAllByEventId(_event.EventId);
        //    }
        //    else
        //    {
        //        _event.Add();
        //    }
        //    foreach (AddParticipant addParticipant in addParticipants)
        //    {
        //        new Participant(Participant.GetNewId(), idEvent,
        //            User.ActiveUser.UserId, false, addParticipant.Role).Add();
        //    }
        //    this.Close();
        //}

        //public static int GetIndexDataGridButton(object sender)
        //{
        //    FrameworkElement button = (FrameworkElement)sender;
        //    DataGridRow row = (DataGridRow)button.Tag;
        //    return row.GetIndex();
        //}

        //private void DeleteParticipantButton_Click(object sender, RoutedEventArgs e)
        //{
        //    addParticipants.RemoveAt(GetIndexDataGridButton(sender));
        //    RefreshParticipantsDataGrid();
        //}

        //private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    ShowCorrectnessStatus(TitleTextBox,
        //    Event.IsTitleCorrect(TitleTextBox.Text) &&
        //    (Event.IsTitleUnique(TitleTextBox.Text) ||
        //    Event.Get(TitleTextBox.Text).EventId == idEvent));
        //}
    }
}