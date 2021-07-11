using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace PersonalManager
{
    class DataBase
    {
        
        public DataBase()
        {

        }
#pragma warning disable IDE0044 // Добавить модификатор только для чтения
        static string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\source\repos\PersonalManager\Data\PersonalManager.mdf;Integrated Security=True";
#pragma warning restore IDE0044 // Добавить модификатор только для чтения
        public SqlConnection connection = new SqlConnection(conStr);
        public void Open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void Close()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public void State()
        {
            MessageBox.Show("" + connection.State);
        }
    }
}
