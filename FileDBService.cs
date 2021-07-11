using PersonalManager.Models;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;

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
            else
            {
                
                var reader = File.OpenText(PATH);
                var fileText = reader.ReadToEnd();
                if (fileText == "")
                {
                    return new BindingList<T>();
                }
                else
                {
                    reader.Dispose();
                    try
                    {
                        using (FileStream fileStream = new FileStream(PATH, FileMode.Open))
                        {
                            using (Aes aes = Aes.Create())
                            {
                                byte[] iv = new byte[aes.IV.Length];
                                int numBytesToRead = aes.IV.Length;
                                int numBytesRead = 0;
                                while (numBytesToRead > 0)
                                {
                                    int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                                    if (n == 0) break;

                                    numBytesRead += n;
                                    numBytesToRead -= n;
                                }

                                byte[] key =
                                {
                                       0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                                       0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                                    };

                                using (CryptoStream cryptoStream = new CryptoStream(
                                   fileStream,
                                   aes.CreateDecryptor(key, iv),
                                   CryptoStreamMode.Read))
                                {
                                    using (StreamReader decryptReader = new StreamReader(cryptoStream))
                                    {
                                        string decryptedMessage = decryptReader.ReadToEnd();
                                        return JsonConvert.DeserializeObject<BindingList<T>>(decryptedMessage);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex);
                        return null;
                    }
                }
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
        public virtual void SaveData(object Data)//сохраняет таблицу
        {
            var output = JsonConvert.SerializeObject(Data);
            try
            {
                using (FileStream fileStream = new FileStream(PATH, FileMode.OpenOrCreate))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] key =
                        {
                           0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                           0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                        };
                        aes.Key = key;

                        byte[] iv = aes.IV;
                        fileStream.Write(iv, 0, iv.Length);

                        using (CryptoStream cryptoStream = new CryptoStream(
                            fileStream,
                            aes.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            using (StreamWriter encryptWriter = new StreamWriter(cryptoStream))
                            {
                                encryptWriter.WriteLine(output);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The encryption failed.{ex}");
            }
        }
    }
}
