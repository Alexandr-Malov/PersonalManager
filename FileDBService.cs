using PersonalManager.Models;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;

namespace PersonalManager
{
    class FileDBService
    {
        public FileDBService()
        {
           
        }
        public FileDBService(string path)
        {
            PATH = path;
        }

        private readonly string PATH;

        /// <summary>
        /// Метод для загрузки таблицы
        /// </summary>
        /// <param name="Category">Категория данных
        /// TODOContacts
        /// NoteBook
        /// Diary
        /// ShipList
        /// </param>
        /// <returns>Возвращает BindingList<object></returns>
        public virtual BindingList<T> LoadData<T>()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<T>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<T>>(fileText);
            }
        }

        /// <summary>
        /// Метод для сохранения данных в базу данных
        /// </summary>
        /// <param name="Data">Binding List</param>
        /// <param name="Category">Категория данных
        /// TODOContacts
        /// NoteBook
        /// Diary
        /// ShipList
        /// </param>
        public virtual void SaveData(object Data)
        {
            using (StreamWriter writer = new StreamWriter(PATH))
            {
                var output = JsonConvert.SerializeObject(Data);
                writer.Write(output);
            }  
        }
    }
}
