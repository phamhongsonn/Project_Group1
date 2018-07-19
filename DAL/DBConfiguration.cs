using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;

namespace DAL
{
    public class DbConfiguration
    {
        private static string CONNECTION_STRING = "server=localhost;user id=root;password=24081999;port=3306;database=shopdb;SslMode=None";
        public static MySqlConnection OpenDefaultConnection()
        {
            try{
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = CONNECTION_STRING
                };
                connection.Open();
                return connection;
            }catch{
                return null;
            }
        }

        public static MySqlConnection OpenConnection()
        {
            try{
                string connectionString;
                FileStream fileStream = File.OpenRead("ConnectionString.txt");
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    connectionString = reader.ReadLine();
                }
                return OpenConnection(connectionString);
            }catch{
                return null;
            }
        }

        public static MySqlConnection OpenConnection(string connectionString)
        {
            try{
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connectionString
                };
                connection.Open();
                return connection;
            }catch{
                return null;
            }
        }
    }
}