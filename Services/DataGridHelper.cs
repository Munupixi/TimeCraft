using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TimeCraft.Services
{
    public static class DataGridHelper
    {
        public static int GetRowIndex(object sender)
        {
            if (sender is Button button)
            {
                if (button.DataContext is DataGridRow row)
                {
                    return row.GetIndex();
                }
            }
            return -1;
        }
    }
}