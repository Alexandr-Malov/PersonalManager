using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            id.Text = IsValidData.User_Id.ToString();
            login.Text = IsValidData.Login;
            firstname.Text = IsValidData.First_Name;
            lastname.Text = IsValidData.Last_Name;
            age.Text = IsValidData.Age.ToString();
            phonenumber.Text = IsValidData.Phone;
            email.Text = IsValidData.Email;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DataBase db = new DataBase();
            if (db.connection.State == System.Data.ConnectionState.Closed)
            {
                db.connection.Open();
            }
            string sqlExpression = $"UPDATE Users SET Login = N'{login.Text}',First_Name = N'{firstname.Text}',Last_Name = N'{lastname.Text}',Age = {Convert.ToInt32(age.Text)},[Phone Number] = N'{phonenumber.Text}',Email = N'{email.Text}' WHERE User_Id = {Convert.ToInt32(id.Text)}";
            var sql = new SqlCommand(sqlExpression, db.connection);
            sql.ExecuteNonQuery();
            IsValidData.ReadFileFromDatabase();
            Page_Loaded(sender,e);
        }
    }
}
