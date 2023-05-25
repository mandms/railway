using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Railway
{
    class Train
    {
        private MySqlConnection Conn;

        public Train()
        {
            string ConnectionStr = "host=localhost;port=3306;user=root;password=admin123;database=railway;sslmode=none;";
            Conn = new MySqlConnection(ConnectionStr);
        }

        public DataTable All()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select train.id, number as 'Номер поезда', " +
                    "wagons_quantity as 'Кол-во вагонов', train_type.name as 'Тип поезда' from train " +
                    "inner join train_type on train_type.id = train.type";
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

        public DataTable AllWhere(string typeId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select train.id, number as 'Номер поезда', " +
                    "wagons_quantity as 'Кол-во вагонов', train_type.name as 'Тип поезда' from train " +
                    "inner join train_type on train_type.id = train.type where train_type.id = @id";
                MySqlCommand cmd = new MySqlCommand(cmdText, Conn);
                cmd.Parameters.AddWithValue("@id", typeId);

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

        public DataTable TrainTypes()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string cmdText = "select id, name from train_type";
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
