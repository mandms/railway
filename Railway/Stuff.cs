using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Railway
{
    public class StuffObject
    {
        public string trainId { get; set; }
        public string shift { get; set; }
        public string stuffId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string comment { get; set; }
    }
    class Stuff
    {
        private MySqlConnection Conn;

        public Stuff()
        {
            string ConnectionStr = "host=localhost;port=3306;user=root;password=admin123;database=railway;sslmode=none;";
            Conn = new MySqlConnection(ConnectionStr);
        }
        public DataTable Employees()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select id, full_name from stuff";
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
        public DataTable All()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select train_stuff.id as 'Идентификатор', train.`number` as 'Номер поезда', shift as 'Рабочая смена', " +
                    "`stuff`.full_name as 'ФИО работника', `start_date` as 'Дата начала смены', `end_date` as 'Дата окончания смены', " +
                    "`comment` as 'Комментарий' from train_stuff " +
                    "inner join stuff on stuff.id = train_stuff.stuff " +
                    "inner join train on train.id = train_stuff.train ";
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

        public DataTable AllWhere(string id)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select train_stuff.id, train.`number` as 'Номер поезда', shift as 'Рабочая смена', " +
                    "`stuff`.full_name as 'ФИО работника', `start_date` as 'Дата начала смены', `end_date` as 'Дата окончания смены', " +
                    "`comment` as 'Комментарий' from train_stuff " +
                    "inner join stuff on stuff.id = train_stuff.stuff " +
                    "inner join train on train.id = train_stuff.train " +
                    "where train_stuff.user = @id";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@id", id);

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

        public void CreateStuffInfo(string trainId, string shift, string stuffId, string start_date, string end_date, string comment)
        {
            try
            {
                string cmdText = "INSERT INTO `train_stuff`(train, shift, stuff, start_date, end_date, comment) " +
                    "VALUES (@trainId, @shift, @stuff, @start_date, @end_date, @comment)";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@trainId", trainId);
                cmd.Parameters.AddWithValue("@shift", shift);
                cmd.Parameters.AddWithValue("@stuff", stuffId);
                cmd.Parameters.AddWithValue("@start_date", start_date);
                cmd.Parameters.AddWithValue("@end_date", end_date);
                cmd.Parameters.AddWithValue("@comment", comment);

                Conn.Open();
                if ((int)cmd.ExecuteNonQuery() < 0)
                {
                    throw new Exception("При добавлении произошла ошибка");
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

        public StuffObject GetStuffInfoById(string Id)
        {
            StuffObject stuff = null;
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select * from train_stuff where id = @id";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@id", Id);

                Conn.Open();
                MySqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    stuff = new StuffObject
                    {
                        trainId = Reader.GetString(1),
                        shift = Reader.GetString(2),
                        stuffId = Reader.GetString(3),
                        startDate = Reader.GetDateTime(4),
                        endDate = Reader.GetDateTime(5),
                        comment = Reader[6].ToString()
                    };
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
            return stuff;
        }

        public void ChangeStuffInfo(string id, string trainId, string shift, string stuffId, string start_date, string end_date, string comment)
        {
            try
            {
                string cmdText = "update `train_stuff` set " +
                    "train = @trainId, " +
                    "shift = @shift, " +
                    "stuff = @stuff, " +
                    "start_date = @start_date, " +
                    "end_date = @end_date, " +
                    "comment = @comment " +
                    "where id = @id";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@trainId", trainId);
                cmd.Parameters.AddWithValue("@shift", shift);
                cmd.Parameters.AddWithValue("@stuff", stuffId);
                cmd.Parameters.AddWithValue("@start_date", start_date);
                cmd.Parameters.AddWithValue("@end_date", end_date);
                cmd.Parameters.AddWithValue("@comment", comment);
                cmd.Parameters.AddWithValue("@id", id);

                Conn.Open();
                if ((int)cmd.ExecuteNonQuery() < 0)
                {
                    throw new Exception("При изменении произошла ошибка");
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

        public void DeleteStuffInfo(string Id)
        {
            try
            {
                string cmdText = "delete from `train_stuff` where id = @id";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@id", Id);

                Conn.Open();
                if ((int)cmd.ExecuteNonQuery() < 0)
                {
                    throw new Exception("При удалении произошла ошибка");
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
    }
}
