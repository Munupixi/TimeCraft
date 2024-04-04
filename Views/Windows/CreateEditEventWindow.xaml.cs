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
        public CreateEditEventWindow()
        {
            InitializeComponent();
            DataContext = new CreateEditEventWindowViewModel();
        }
    }
}