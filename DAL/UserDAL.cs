using System;
using Persitence;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class UserDal
    {
        private string query;
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public static users GetUser(MySqlDataReader reader)
        {
            users use = new users();
            use.user_name = reader.GetString("user_name");
            use.user_password = reader.GetString("user_password");
            return use;
        }
        public users Login(string user_name, string user_pass)
        {
            query = $"select * from users where user_name = '{user_name}' and user_password = '{user_pass}';";
            users use = new users();
            using (connection = DbConfiguration.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        use = GetUser(reader);
                    }
                }
            }
            return use;

        }
    }
}
