using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeCraft.ViewModels
{
    internal class UserViewModel: DependencyObject
    {
        public string Login
        {
            get { return (string)GetValue(LoginProperty); }
            set { SetValue(LoginProperty, value); }
        }

        public static readonly DependencyProperty LoginProperty =
            DependencyProperty.Register("Login",
                typeof(string), typeof(UserViewModel), 
                new PropertyMetadata(string.Empty));
    }
}
