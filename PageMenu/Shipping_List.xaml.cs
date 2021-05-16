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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalManager
{
    /// <summary>
    /// Логика взаимодействия для Shipping_List.xaml
    /// </summary>
    public partial class Shipping_List : Page
    {
        private static readonly string PATH = $"{Environment.CurrentDirectory}\\{IsValidData.User_Id}\\ShipList.json";
        private BindingList<ShipModel> _shipData;
        private FileDBService _filedbservice = new FileDBService(PATH);
        public Shipping_List()
        {
            InitializeComponent();
            KeyGesture backKeyGesture = null;
            foreach (var gesture in NavigationCommands.BrowseBack.InputGestures)
            {
                KeyGesture keyGesture = gesture as KeyGesture;
                if ((keyGesture != null) &&
                   (keyGesture.Key == Key.Back) &&
                   (keyGesture.Modifiers == ModifierKeys.None))
                {
                    backKeyGesture = keyGesture;
                }
            }

            if (backKeyGesture != null)
            {
                NavigationCommands.BrowseBack.InputGestures.Remove(backKeyGesture);
            }
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

        private void ShipGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                int a = ShipGrid.SelectedIndex;
                if (_shipData.Count == ShipGrid.SelectedIndex++)
                {
                    MessageBox.Show("Выбрана пустая строка!", "Удаление");
                    return;
                }

                if (_shipData.Count == 1)
                {
                    return;
                }
                else
                {
                    _shipData.RemoveAt(a);
                }
            }
        }

        /// <summary>
        /// Кнопка для добавления заказа
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddShip addShip = new AddShip();
            addShip.ShowDialog();
            Page_Loaded(sender,e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenShip openShip = new OpenShip();
            openShip.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _shipData = _filedbservice.LoadData<ShipModel>();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
            ShipGrid.ItemsSource = _shipData;
            var SOB = new ListChangedEventHandler(OnListChanged);
            _shipData.ListChanged += new ListChangedEventHandler(OnListChanged);
        }
    }
}
