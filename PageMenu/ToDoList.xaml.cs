using Newtonsoft.Json;
using PersonalManager.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PersonalManager
{
    /// <summary>
    /// Логика взаимодействия для ToDoList.xaml
    /// </summary>
    public partial class ToDoList : Page
    {
        //TODO:переделать систему сохранения информации
        //TODO:поставить шифрование на сохраненные данные
        private readonly string PATH = $"{Environment.CurrentDirectory}\\UsersDate\\{IsValidData.User_Id}\\todolist.json";//путь сохранения
        private BindingList<TodoModel> _todoData;//данные таблицы
        private FileDBService _filedbservice;//класс для работы с данными таблицы

        public ToDoList()
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _filedbservice = new FileDBService(PATH);//назначается путь загрузки и сохранения файла
            try
            {
                _todoData =_filedbservice.LoadData<TodoModel>();//происходит загрузка таблицы данных
            }
             catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();//при возникновении ошибки загрузки,приложение закрывается
            }
            TodoGrid.ItemsSource = _todoData;//происходит собственно заполнение таблицы загруженными данными
            var SOB = new ListChangedEventHandler(OnListChanged);//создан метод
            _todoData.ListChanged += SOB;//привязка метода к событию


        }

        protected void OnListChanged(object sender, ListChangedEventArgs args)
        {
            try
            {
                _filedbservice.SaveData(_todoData);//происходит сохранение данных при изменении таблицы
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TodoGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                int a = TodoGrid.SelectedIndex;
                if (_todoData.Count == TodoGrid.SelectedIndex++)
                {
                    MessageBox.Show("Выбрана пустая строка!","Удаление");
                    return;
                }

                if (_todoData.Count == 1)
                {
                    return;
                }
                else
                {
                    _todoData.RemoveAt(a);
                }
            }
        }

    }
}
