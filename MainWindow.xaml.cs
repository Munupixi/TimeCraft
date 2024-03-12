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
                // создаем два объекта User
                User user1 = new User(2, "Tom", "werg", 5);
                User user2 = new User(2, "Tod", "werginia", 8);

                // добавляем их в бд
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();

                // получаем объекты из бд и выводим на консоль
                var users = db.Users;
                Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    MessageBox.Show(u.Id + u.Name + u.Age);
                }
            }
        }
    }
}