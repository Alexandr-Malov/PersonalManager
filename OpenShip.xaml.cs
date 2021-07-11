using PersonalManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для OpenShip.xaml
    /// </summary>
    public partial class OpenShip : Window
    {
        private static readonly string PATH = $"{Environment.CurrentDirectory}\\UsersDate\\{IsValidData.User_Id}\\ShipList.json";
        private BindingList<ShipModel> _shipData;
        private FileDBService _filedbservice = new FileDBService(PATH);
        public OpenShip()
        {
            InitializeComponent();
        }

        protected void OnListChanged(object sender, ListChangedEventArgs args)
        {
            try
            {
                _filedbservice.SaveData(_shipData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _shipData = _filedbservice.LoadData<ShipModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
            ShipGrid.ItemsSource = _shipData;
            _shipData.ListChanged += new ListChangedEventHandler(OnListChanged); ;
        }
    }
}
