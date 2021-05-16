using System.Data.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PersonalManager
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsValidData check = new IsValidData(LoginBox.Text, EmailBox.Text, PasswordBox.Text, PasswordBox2.Text);
            if (check.IsValidRegistration())
            {
                NavigationService.Navigate(new Login());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }
    }
}
