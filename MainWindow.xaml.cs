using System;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegistrationPage regis = new RegistrationPage();  
            MainFrame.Navigate(regis);

            using (AppDBContent db = new AppDBContent())
            {

                User user1 = new User(83, "To4m4", "werg", 5, "we","Xs","s");
                User user2 = new User(49, "To44d", "werginia", 8, "we", "Xs", "s");

                db.User.Add(user1);
                db.User.Add(user2);
                db.SaveChanges();

                var users = db.User;
                foreach (User u in users)
                {
                    MessageBox.Show(u.UserId +" "+ u.Name + " "+ u.Age);
                }
            }
        }
    }
}