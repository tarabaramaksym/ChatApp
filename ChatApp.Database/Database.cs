using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.Database
{
    public class SQLDatabase
    {
        string        _connectionString;
        SqlConnection _connection;
        SqlDataReader _reader;
        SqlCommand    _command;

        public SQLDatabase()
        {
            _connectionString = @"Data Source=DESKTOP-GDQIAUT\SQLEXPRESS2019;Initial Catalog=ChatApp;User ID=sa;Password=Database24MT";
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }
        public List<T> SelectAll<T>()
        {
            string sql = "SELECT * FROM Users";
            _command = new SqlCommand(sql, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                MessageBox.Show(_reader.GetValue(0).ToString() + _reader.GetValue(1).ToString());
            }
            return new List<T>();
        }
        ~SQLDatabase()
        {
            _connection.Close();
        }

        public User SelectOneUser(string username)
        {
            // TODO htmlspecialchar to protect from injections
            string sql = "SELECT * FROM Users WHERE Username = '" + username + "'";
            _command = new SqlCommand(sql, _connection);
            _reader = _command.ExecuteReader();
            User user = _reader.Read() ? new User(_reader.GetString(1), _reader.GetString(2), _reader.GetString(3)) : null;
            _reader.Close();
            return user;
        }
        public bool Insert(IModel model)
        {
            if (model.GetType() == typeof(User))
                if (SelectOneUser((model as User).Username) != null)
                    return false;

            string sql = model.GetInsert();
            _command = new SqlCommand(sql, _connection);
            _reader = _command.ExecuteReader();
            return true;
        }
    }
}
