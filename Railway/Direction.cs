using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Railway
{
    public class DirectionObject
    {
        public string trainID { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime arrivalDate { get; set; }
    }
    public class Direction
    {
        private MySqlConnection Conn;
        public Direction()
        {
            string ConnectionStr = "host=localhost;port=3306;user=root;password=admin123;database=railway;sslmode=none;";
            Conn = new MySqlConnection(ConnectionStr);
        }

        public DataTable All()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select " +
                    "directions.id, " +
                    "train.number as `Номер поезда`, " +
                    "`from` as `Откуда`, " +
                    "`to` as `Куда`, " +
                    "departure_date as `Дата и время отправки`, " +
                    "arrival_date as `Дата и время прибытия` " +
                    "from directions " +
                    "inner join train on train.id = directions.train";
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

        public void Create(string trainId, string from, string to, string departure_date, string arrival_date)
        {
            try
            {
                string cmdText = "INSERT INTO `directions`(train, `from`, `to`, departure_date, arrival_date) " +
                    "VALUES (@trainId, @from, @to, @departure_date, @arrival_date)";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@trainId", trainId);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                cmd.Parameters.AddWithValue("@departure_date", departure_date);
                cmd.Parameters.AddWithValue("@arrival_date", arrival_date);

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

        public void Change(string id, string trainId, string from, string to, string departure_date, string arrival_date)
        {
            try
            {
                string cmdText = "update `directions` set " +
                    "train = @trainId, " +
                    "`from` = @from, " +
                    "`to` = @to, " +
                    "departure_date = @departureDate, " +
                    "arrival_date = @arrivalDate " +
                    "where directions.id = @id";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@trainId", trainId);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                cmd.Parameters.AddWithValue("@departureDate", departure_date);
                cmd.Parameters.AddWithValue("@arrivalDate", arrival_date);
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

        public void Delete(string Id)
        {
            try
            {
                string cmdText = "delete from `directions` where id = @id";
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

        public DirectionObject GetById(string Id)
        {
            DirectionObject direction = null;
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select train.id, `from`, `to`, departure_date, arrival_date from directions " +
                    "inner join train on train.id = directions.train where directions.id = @id";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                Conn.Open();
                MySqlDataReader Reader = cmd.ExecuteReader();
                while(Reader.Read())
                {
                    direction = new DirectionObject
                    {
                        trainID = Reader.GetString(0),
                        from = Reader.GetString(1),
                        to = Reader.GetString(2),
                        departureDate = Reader.GetDateTime(3),
                        arrivalDate = Reader.GetDateTime(4)
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
            return direction;
        }

        public DataTable Trains()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select id, `number` from train";
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
