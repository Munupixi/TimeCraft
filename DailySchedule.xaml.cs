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

namespace TimeCraft
{
    public partial class DailySchedule : Page
    {
        public DailySchedule()
        {
            InitializeComponent();

            MainListView.Items.Clear();
            //MainListView.Items.Add(new Task());
        }
    }
}