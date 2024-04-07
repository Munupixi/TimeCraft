using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TimeCraft
{
    internal class LoginToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Brushes.Red;
            return !User.IsLoginUnique(value.ToString()) ? Brushes.Green : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}