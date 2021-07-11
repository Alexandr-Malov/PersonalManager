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
    /// Логика взаимодействия для AddShip.xaml
    /// </summary>
    public partial class AddShip : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\UsersDate\\{IsValidData.User_Id}\\ShipList.json";
        private BindingList<ShipModel> _shipData;
        private FileDBService _filedbservice;
        public AddShip()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _filedbservice = new FileDBService(PATH);
            try
            {
                _shipData = _filedbservice.LoadData<ShipModel>();
                var numberorder = _shipData.Count;
                numberorder++;
                _shipData.Add(new ShipModel { OrderNumber = numberorder, OrderedQuantity = Convert.ToInt32(orderedquantity.Text), OrderAmount = Convert.ToInt32(orderamount.Text), Manufacturer = manufacturer.Text, Nomenclature = nomenclature.Text, PriceOneProduct = Convert.ToInt32(priceoneproduct.Text) });
                _filedbservice.SaveData(_shipData);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
