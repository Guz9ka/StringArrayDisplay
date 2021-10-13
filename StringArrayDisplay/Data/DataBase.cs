using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace StringArrayDisplay.Data
{
    public class DataBase
    {
        private string tableName = "Strings";
        
        private MySqlConnection _sqlConnection;

        public DataBase()
        {
            var address = "sql11.freesqldatabase.com";
            var dbName = "sql11443667";
            var userName = "sql11443667";
            var password = "K8XN6kDiWy";
            var port = "3306";

            _sqlConnection = new MySqlConnection($"server={address};" +
                                                 $"database={dbName};" +
                                                 $"port={port};" +
                                                 $"uid={userName};" +
                                                 $"password={password};" + 
                                                 "SSL Mode=None");
        }
        
        public List<string> GetAllStringsList()
        {
            _sqlConnection.Open();
            
            var task = "SELECT * FROM Strings";
            var command = new MySqlCommand(task, _sqlConnection);
            
            var strings = new List<string>();
            
            using (DbDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cellValue = reader.GetString(0);
                        strings.Add(cellValue);
                    }
                }
            }
            
            _sqlConnection.Close();
            
            return strings; 
        }

        public void TryAddNewString(string stringToAdd)
        {
            _sqlConnection.Open();

            var task = $"INSERT INTO {tableName} VALUE ('{stringToAdd}')";
            var command = new MySqlCommand(task, _sqlConnection);

            command.ExecuteNonQuery();
            
            _sqlConnection.Close();
        }
    }
}
