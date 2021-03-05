using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class User : IModel
    {
        const string TABLE_NAME = "Users";

        string _username;
        string _password;
        string _name;

        public User(string username, string password, string name)
        {
            Username = username;
            Password = password;
            Name = name;
        }

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string Name { get => _name; set => _name = value; }

        public string GetInsert()
        {
            return $"INSERT INTO {TABLE_NAME} (Username,Name,Password) VALUES ('{Username}','{Name}','{Password}')";
        }
    }
}
