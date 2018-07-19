using System;
using Persitence;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace DAL
{
    public class itemDAL
    {
        private string query;
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public static item Getitem(MySqlDataReader reader)
        {
            item item = new item();
            item.item_id = reader.GetInt32("item_id");
            item.item_name = reader.GetString("item_name");
            item.unit_price = reader.GetDecimal("unit_price");
            item.quantity = reader.GetInt16("quantity");
            return item;
        }
        public itemDAL()
        {
            connection = DbConfiguration.OpenConnection();
        }
        public item Getitembyid(int Item_id)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = @"select item_id, item_name,unit_price,quantity from item where item_id = "+Item_id+" ;";
            MySqlCommand command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            item item = null;
            if (reader.Read())
            {
                item = Getitem(reader);
            }
            reader.Close();
            connection.Close();
            return item;
        }
        
    }
}