using ChatApp.SharedLib.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.SharedLib.Models
{
    [Serializable]
    public class Contact : IModel
    {
        int _id;
        string _username;
        string _name;

        public int Id { get => _id; set => _id = value; }
        public string Username { get => _username; set => _username = value; }
        public string Name { get => _name; set => _name = value; }

        public void ParseSqlReader(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Name = reader.GetString(reader.GetOrdinal("Name"));
            Username = reader.GetString(reader.GetOrdinal("Username"));
        }

    }
}
