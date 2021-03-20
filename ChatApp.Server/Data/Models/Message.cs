using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Data.Models
{
    class Message : IModel
    {

        private int _senderId;
        private int _receiverId;
        private string _message;

        public Message(int senderId, int receiverId, string message)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            MessageProperty = message;
        }

        public int SenderId { get => _senderId; set => _senderId = value; }
        public int ReceiverId { get => _receiverId; set => _receiverId = value; }
        public string MessageProperty { get => _message; set => _message = value; }

        public string GetInsert()
        {
            return $"INSERT INTO {GetTableName()} VALUES ({SenderId}, {ReceiverId},'{MessageProperty}', '{DateTime.Now}')";
        }

        public string GetTableName()
        {
            return "Messages";
        }

        public void ParseSqlReader(SqlDataReader reader)
        {
            SenderId = reader.GetInt32(reader.GetOrdinal("SenderId"));
            ReceiverId = reader.GetInt32(reader.GetOrdinal("ReceiverId"));
            MessageProperty = reader.GetString(reader.GetOrdinal("Message"));
        }
    }
}
