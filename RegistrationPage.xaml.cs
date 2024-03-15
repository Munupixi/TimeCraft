using System.Windows;
using System.Windows.Controls;

namespace TimeCraft
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public bool RegistrationEnabled = false;
        

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationEnabled == true)
            {
                string name = NameTextBox.Text;
                int age = int.Parse(AgeTextBox.Text);
                string password = PasswordTextBox.Text;
                string passwordChek = PasswordAgainTextBox.Text;
            }
            else
            {
                MessageBox.Show("Подвердите согласие на обработку данных!");
            }
        }

        private void ConfirmProgramPolicyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RegistrationEnabled = true;
        }
    }
}