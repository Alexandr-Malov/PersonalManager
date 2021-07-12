using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace PersonalManager
{
    class IsValidData
    {
        internal static int User_Id;
        internal static string Login;
        internal static string First_Name;
        internal static string Last_Name;
        internal static int Age;
        internal static string Phone;
        internal static string Email;
        internal static string Pass;
        internal static string Pass2;
        public IsValidData()
        {

        }
        /// <summary>
        /// Конструктор для входа в профиль
        /// </summary>
        /// <param name="login"></param>
        /// <param name="Password"></param>
        public IsValidData(string login, string Password)
        {
            Login = login;
            Pass = Password;
        }
        /// <summary>
        /// Конструктор для окна регистрации
        /// </summary>
        /// <param name="LoginBox"></param>
        /// <param name="EmailBox"></param>
        /// <param name="PasswordBox"></param>
        public IsValidData(string LoginBox, string EmailBox, string PasswordBox, string PasswordBox2)
        {
            Login = LoginBox;
            Email = EmailBox;
            Pass = PasswordBox;
            Pass2 = PasswordBox2;
        }

        public bool IsValidRegistration()
        {
            DataBase db = new DataBase();
            if (db.connection.State == System.Data.ConnectionState.Closed)
            {
                db.connection.Open();
            }
            string sqlExpression = $"SELECT * FROM Users WHERE Login = '{Login}'";
            SqlCommand sql = new SqlCommand(sqlExpression, db.connection);
            if (sql.ExecuteScalar() == null)
            {
                sqlExpression = $"SELECT * FROM Users WHERE Email = '{Email}'";
                sql = new SqlCommand(sqlExpression, db.connection);
                if (sql.ExecuteScalar() == null)
                {

                    if (IsValidEmail(Email))
                    {
                        if (Pass != "" && Pass2 != "")
                        {
                            if (Pass == Pass2)
                            {
                                string sqlUserid = "SELECT MAX(User_Id) FROM Users";
                                var sqlid = new SqlCommand(sqlUserid, db.connection);
                                User_Id = Convert.ToInt32(sqlid.ExecuteScalar());
                                User_Id++;
                                sqlExpression = $"INSERT INTO Users ([User_Id],[Login],[First_Name],[Last_Name],[Age],[Phone Number],[Email],[Password]) VALUES (@id,@login,@firstname,@lastname,@age,@numberphone,@email,@password)";
                                sql = new SqlCommand(sqlExpression, db.connection);

                                SqlParameter id = new SqlParameter("@id", User_Id);

                                sql.Parameters.Add(id);
                                SqlParameter loginParam = new SqlParameter("@login", Login);

                                sql.Parameters.Add(loginParam);

                                SqlParameter firstnameParam = new SqlParameter("@firstname", "unknown");

                                sql.Parameters.Add(firstnameParam);

                                SqlParameter lastnameParam = new SqlParameter("@lastname", "unknown");

                                sql.Parameters.Add(lastnameParam);

                                SqlParameter ageParam = new SqlParameter("@age", 10);

                                sql.Parameters.Add(ageParam);

                                SqlParameter numberphoneParam = new SqlParameter("@numberphone", "+79999999999");

                                sql.Parameters.Add(numberphoneParam);

                                SqlParameter emailParam = new SqlParameter("@email", Email);

                                sql.Parameters.Add(emailParam);

                                SqlParameter passwordParam = new SqlParameter("@password", Pass);

                                sql.Parameters.Add(passwordParam);

                                int number = sql.ExecuteNonQuery();
                                if (number == 0)
                                {
                                    MessageBox.Show("Регистрация непрошла!", "Регистрация");
                                    return false;
                                }
                                else
                                {
                                    MessageBox.Show("Регистрация прошла успешно!", "Регистрация");
                                    Directory.CreateDirectory($"{Environment.CurrentDirectory}\\UsersDate\\{IsValidData.User_Id}");
                                    return true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароли разные!", "Регистрация");
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поля с паролями неполны!", "Регистрация");
                            return false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Некорректный Email!", "Регистрация");
                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("Аккаунт с таким Email уже существует!", "Регистрация");
                    return false;
                }

            }
            else
            {
                MessageBox.Show("Такой логин уже существует!", "Регистрация");
                return false;

            }

        }
        public bool IsValidInput()
        {
            DataBase db = new DataBase();
            if (db.connection.State == System.Data.ConnectionState.Closed)
            {
                db.connection.Open();
            }
            string sqlExpression = $"SELECT * FROM Users WHERE Login = '{Login}' AND Password = '{Pass}' ";
            SqlCommand sql = new SqlCommand(sqlExpression, db.connection);
            if (sql.ExecuteScalar() != null)
            {
                ReadFileFromDatabase();
                db.Close();
                return true;
            }
            else
            {
                MessageBox.Show("Данные неверны!", "Вход в приложение");
                db.Close();
                return false;
            }
        }
        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static void ReadFileFromDatabase()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\source\repos\PersonalManager\Data\PersonalManager.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                string comm = $"SELECT * FROM Users WHERE Login = N'{Login}'";
                SqlCommand command = new SqlCommand(comm, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User_Id = reader.GetInt32(1);
                        Login = reader.GetString(2);
                        First_Name = reader.GetString(3);
                        Last_Name = reader.GetString(4);
                        Age = reader.GetInt32(5);
                        Phone = reader.GetString(6);
                        Email = reader.GetString(7);
                        Pass = reader.GetString(8);
                        break;
                    }
                }
            }
        }
    }
}
