﻿using System;
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
using System.Windows.Shapes;

namespace TimeCraft
{
    public partial class CreateEditEventWindow : Window
    {
        public CreateEditEventWindow()
        {
            InitializeComponent();
        }

        private void StartTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EndTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StartDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EndDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearParticipantsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Event _event = new Event(Event.GetNewId(), TitleTextBox.Text,
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
CategoryComboBox.SelectedIndex);
            _event.Add();
        }

        private void DeleteParticipantButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
