using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ChatApp.Server.Data.Models;
using ChatApp.Server.Data;
using ChatApp.SharedLib.Enums;

namespace ChatApp.Data.MicrosoftSQLServer
{
    public class SQLDatabase : IDisposable, IDatabase
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

        public List<T> SelectAll<T>() where T : IModel, new()
        {
            T obj = new T();
            var sql = "SELECT * FROM " + obj.GetTableName();
            _command = new SqlCommand(sql, _connection);
            _reader = _command.ExecuteReader();

            List<T> list = new List<T>();
            while (_reader.Read())
            {
                obj.ParseSqlReader(_reader);
                list.Add(obj); 
            }
            return list;
        }

        public User SelectOneUser(string username)
        {
            // TODO htmlspecialchar to protect from injections
            var sql = "SELECT * FROM Users WHERE Username = '" + username + "'";
            _command = new SqlCommand(sql, _connection);
            _reader = _command.ExecuteReader();
            User user = _reader.Read() ? 
                new User(
                    _reader.GetString(_reader.GetOrdinal("Username")),
                    _reader.GetString(_reader.GetOrdinal("Password")),
                    _reader.GetString(_reader.GetOrdinal("Name")),
                    _reader.GetInt32(_reader.GetOrdinal("Id"))) 
                : null;
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
            _reader.Close();
            return true;
        }

        public List<SharedLib.Models.Contact> SelectContacts(int id)
        {
            List<SharedLib.Models.Contact> contacts = new List<SharedLib.Models.Contact>();

            var yourRequestsSQL = $"SELECT u.Id, u.Name,u.Username FROM Contacts LEFT JOIN Users u ON u.Id = ReceiverId WHERE SenderId = {id} AND Accepted = 0";
            var requestsToYouSQL = $"SELECT  u.Id, u.Name,u.Username FROM Contacts LEFT JOIN Users u ON u.Id = SenderId WHERE ReceiverId = {id} AND Accepted = 0";
            var acceptedContactsSQL = $"SELECT u.Id, u.Name,u.Username FROM Contacts LEFT JOIN Users u ON u.Id = ReceiverId WHERE SenderId = {id} AND Accepted = 1";

            GetContacts(yourRequestsSQL, ContactStatus.YOUR_REQUESTS, ref contacts);
            GetContacts(requestsToYouSQL, ContactStatus.REQUESTS_TO_YOU, ref contacts);
            GetContacts(acceptedContactsSQL, ContactStatus.ACCEPTED, ref contacts);
            acceptedContactsSQL = $"SELECT u.Id, u.Name,u.Username FROM Contacts LEFT JOIN Users u ON u.Id = SenderId WHERE ReceiverId = {id} AND Accepted = 1";
            GetContacts(acceptedContactsSQL, ContactStatus.ACCEPTED, ref contacts);

            return contacts;
        }

        private void GetContacts(string sql,ContactStatus status, ref List<SharedLib.Models.Contact> contacts)
        {
            _command = new SqlCommand(sql, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                var c = new SharedLib.Models.Contact();
                c.ParseSqlReader(_reader);
                contacts.Add(c);
            }
                
            _reader.Close();
        }

        public List<SharedLib.Models.Contact> SearchUsers(string name)
        {
            List<SharedLib.Models.Contact> contacts = new List<SharedLib.Models.Contact>();
            var sql = $"SELECT * FROM Users WHERE Username like '%{name}%' OR Name like '%{name}%'";
            _command = new SqlCommand(sql, _connection);
            _reader = _command.ExecuteReader();
            while (_reader.Read())
            {
                var c = new SharedLib.Models.Contact();
                c.ParseSqlReader(_reader);
                contacts.Add(c);
            }
            _reader.Close();
            return contacts;
        }

        public T SelectOne<T>(int id) where T : IModel, new()
        {
            T obj = new T();
            var sql = $"SELECT * FROM {new T().GetTableName()} WHERE Id = {id}";
            _command = new SqlCommand(sql, _connection);
            _reader = _command.ExecuteReader();
            if (_reader.Read())
            {
                obj.ParseSqlReader(_reader);
            }
            _reader.Close();
            return obj;
        }

        private bool _disposed = false;
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _connection.Close();
                _reader.Close();
            }
            _disposed = true;
        }

        

        
    }
}
