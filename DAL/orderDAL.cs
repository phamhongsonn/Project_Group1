using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL
{
    public class orderDAL
    {
        public bool CreateOrder(order order)
        {
            if (order == null || order.itemlist == null || order.itemlist.Count == 0)
            {
                return false;
            }
            bool result = true;
            MySqlConnection connection = DbConfiguration.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            
            command.CommandText = @"lock table orders write, orderdetail write;";
            command.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            command.Transaction = trans;
            MySqlDataReader reader = null;
            try
            {
                
                command.CommandText = "insert into orders(user_name) values (@Name);";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Name", "Pham Hong Son");
                command.ExecuteNonQuery();
                
                command.CommandText = "Select LAST_INSERT_ID() as order_id;";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    order.order_id = reader.GetInt32("order_id");
                }
                reader.Close();
                
                foreach (item item in order.itemlist)
                {
                    if (item.item_id == null || item.quantity <= 0)
                    {
                        throw new Exception("Not Exists Item");
                    }
                    command.CommandText = "select unit_price, item_name from item where item_id=@itemID;";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@itemID", item.item_id);
                    reader = command.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new Exception("Not Exists Item");
                    }
                    item.unit_price = reader.GetDecimal("unit_price");
                    item.item_name = reader.GetString("item_name");
                    reader.Close();
                    command.CommandText = @"insert into orderdetail(order_id,item_id,price,amount,item_name) 
                    values (" + order.order_id + "," + item.item_id + "," + item.unit_price + "," + item.quantity + ",'" + item.item_name + "');";
                    command.ExecuteNonQuery();
                }
                trans.Commit();
                result = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
                try
                {
                    trans.Rollback();
                }
                catch
                {
                }
            }
            finally
            {
                
                command.CommandText = "unlock tables;";
                command.ExecuteNonQuery();
                connection.Close();

            }
            return result;
        }
        public order GetLastOrder(int? id)
        {
            MySqlConnection connection = DbConfiguration.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            MySqlDataReader reader = null;
            command.CommandText = "select max(order_id) from orders";
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32("max(order_id)");
            }
            reader.Close();
            command.CommandText = "select order_date,user_name from orders where order_id = " + id + ";";
            reader = command.ExecuteReader();
            order order = null;
            if (reader.Read())
            {
                order = GetOrder(reader);
            }
            reader.Close();
            connection.Close();
            return order;
        }
        public List<orderdetail> GetLastOrderDetail(int? id)
        {
            MySqlConnection connection = DbConfiguration.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            MySqlDataReader reader = null;
            command.CommandText = "Select max(order_id) from orderdetail;";
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                id = reader.GetInt32("max(order_id)");
            }
            reader.Close();
            command.CommandText = "select order_id,item_id,item_name,price,amount from orderdetail where order_id = " + id + ";";
            return GetLastOrderDetailInfo(command);
        }
        private List<orderdetail> GetLastOrderDetailInfo(MySqlCommand command)
        {
            MySqlConnection connection = DbConfiguration.OpenConnection();
            MySqlDataReader reader = null;
            List<orderdetail> list = new List<orderdetail>();
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                list.Add(GetOrderDetail(reader));
            }
            reader.Close();
            connection.Close();
            return list;
        }
        private orderdetail  GetOrderDetail(MySqlDataReader reader)
        {
            orderdetail detail = new orderdetail();
            detail.item_id = reader.GetInt32("item_id");
            detail.item_name = reader.GetString("item_name");
            detail.price = reader.GetDecimal("price");
            detail.amount = reader.GetInt32("amount");
            detail.order_id =reader.GetInt32("order_id");
            return detail;
        }
        
        private order GetOrder(MySqlDataReader reader)
        {
            order order = new order();
            order.order_date = reader.GetDateTime("order_date");
            order.user_name = reader.GetString("user_name");
            return order;
        }

    }
}