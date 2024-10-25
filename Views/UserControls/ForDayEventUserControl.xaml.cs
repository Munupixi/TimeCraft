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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeCraft.ViewModels.UserControls;

namespace TimeCraft.Views.UserControls
{
    public partial class ForDayEventUserControl : UserControl
    {
        public ForDayEventUserControl(Event _event)
        {
            InitializeComponent();
            DataContext = new ForDayEventUserControlViewModel(_event);
        }

        private void UserControl_MouseLeftButtonDown(
            object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ForDayEventUserControlViewModel viewModel)
            {
                viewModel.ExecuteOpen();
            }
        }
    }
}