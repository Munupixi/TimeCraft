﻿using System.Windows.Controls;
using TimeCraft.ViewModels.Pages;

namespace TimeCraft
{
    public partial class DailySchedulePage : Page
    {
        public DailySchedulePage()
        {
            InitializeComponent();
            this.DataContext = new DailySchedulePageViewModel();
        }
    }
}