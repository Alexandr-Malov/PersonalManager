using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PersonalManager
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());//открываем окно регистрации 
        }

        private  void Button_Click_1(object sender, RoutedEventArgs e)
        {

            IsValidData check = new IsValidData(textBox.Text, passwordBox.Password);//textBox.Text,passwordBox.Password
            if (check.IsValidInput())
            {
                MainMenu mainMenu = new MainMenu();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Hide();
                mainMenu.Show();
            }
        }
    }
}
