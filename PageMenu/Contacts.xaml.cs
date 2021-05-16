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
using PersonalManager.Models;

namespace PersonalManager
{
    /// <summary>
    /// Логика взаимодействия для Contacts.xaml
    /// </summary>
    public partial class Contacts : Page
    {
        public Contacts()
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

        private readonly string PATH = $"{Environment.CurrentDirectory}\\{IsValidData.User_Id}\\contacts.json";
        private BindingList<ContactsModel> _contactData;
        private FileDBService _filedbservice;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _filedbservice = new FileDBService(PATH);
            try
            {
                _contactData = _filedbservice.LoadData<ContactsModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
            ContactGrid.ItemsSource = _contactData;
            _contactData.ListChanged += new ListChangedEventHandler(OnListChanged);
        }

        protected void OnListChanged(object sender, ListChangedEventArgs args)
        {
            try
            {
                _filedbservice.SaveData(_contactData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Contact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                int a = ContactGrid.SelectedIndex;
                if (_contactData.Count == ContactGrid.SelectedIndex++)
                {
                    MessageBox.Show("Выбрана пустая строка!", "Удаление");
                    return;
                }

                if (_contactData.Count == 1)
                {
                    return;
                }
                else
                {
                    _contactData.RemoveAt(a);
                }
            }
        }
    }
}
