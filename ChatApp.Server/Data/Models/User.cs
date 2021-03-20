using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Data.Models
{
    public class User : IModel
    {
        int _id;
        string _username;
        string _password;
        string _name;

        public User()
        {

        }
        public User(string username, string password, string name)
        {
            Username = username;
            Password = password;
            Name = name;
        }

        public User(string username, string name, int id)
        {
            Username = username;
            Name = name;
            Id = id;
        }

        public User(string username, string password, string name, int id)
        {
            Username = username;
            Password = password;
            Name = name;
            Id = id;
        }

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string Name { get => _name; set => _name = value; }
        public int Id { get => _id; set => _id = value; }

        public string GetInsert()
        {
            return $"INSERT INTO {GetTableName()} (Username,Name,Password) VALUES ('{Username}','{Name}','{Password}')";
        }

        public string GetTableName()
        {
            return "Users";
        }

        public void ParseSqlReader(SqlDataReader reader)
        {
            Id = int.Parse(reader.GetString(reader.GetOrdinal("Id")));
            Username = reader.GetString(reader.GetOrdinal("Username"));
            Name = reader.GetString(reader.GetOrdinal("Name"));
            Password = reader.GetString(reader.GetOrdinal("Password"));
        }
    }
}
