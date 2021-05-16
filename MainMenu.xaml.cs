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
using System.Windows.Shapes;

namespace PersonalManager
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            MenuFrame.Content = new Home_Page();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Content = new ToDoList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MenuFrame.Content = new Contacts();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MenuFrame.Content = new Shipping_List();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MenuFrame.Content = new Profile();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Threading.Dispatcher.ExitAllFrames();
        }
    }
}
