using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Railway
{
    public class UserObject
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string position { get; set; }
    }
    public class User
    {
        public static UserObject CurrentUser = null;

        private MySqlConnection Conn;

        public User()
        {
            string ConnectionStr = "host=localhost;port=3306;user=root;password=admin123;database=railway;sslmode=none;";
            Conn = new MySqlConnection(ConnectionStr);
        }
        public UserObject AuthorizationEmployee(string Username, string Password)
        {
            UserObject user = null;
            try
            {
                string cmdText = "SELECT stuff.id, full_name, position.name FROM `stuff`" +
                    "inner join position on position.id = stuff.position " +
                    "WHERE username = @Username AND password = @Password";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", Password);

                Conn.Open();
                MySqlDataReader Reader = cmd.ExecuteReader();
                if (!Reader.HasRows)
                {
                    throw new Exception("Пользователь не найден");
                } else
                {
                    while (Reader.Read())
                    {
                        user = new UserObject
                        {
                            id = Reader.GetString(0),
                            fullName = Reader.GetString(1),
                            position = Reader.GetString(2)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
            return user;
        }

        public string GEtMd5Harh(MD5 md5Hash, string input)
        {
            // Преобразуем входную строку в массив байт и вычисляем хэш.
            byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(input));

            // Создаем новый Stringbuilder (Изменяемую строку) для набора байт.
            StringBuilder sBuilder = new StringBuilder();

            // Преобразуем каждый байт хэша в шестнадцатеричную строку.
            for (int i = 0; i < data.Length; i++)
            {
                // Указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа.
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public void RegistrationEmployee(string FullName, string PositionId, string Username, string Password)
        {
            try
            {
                string cmdText = "INSERT INTO `stuff`(full_name, position, username, password) " +
                    "VALUES (@full_name, @position, @username, @password)";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@full_name", FullName);
                cmd.Parameters.AddWithValue("@position", PositionId);
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@password", Password);

                Conn.Open();
                if ((int)cmd.ExecuteNonQuery() < 0)
                {
                    throw new Exception("При регистрации произошла ошибка");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
        }

        public DataTable Positions()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select id, name from position";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                
                Conn.Open();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                mySqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
            return dataTable;
        }
    }
}
