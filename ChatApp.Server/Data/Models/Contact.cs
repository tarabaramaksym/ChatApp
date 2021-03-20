using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Server.Data.Models
{
    public class Contact : IModel
    {
        int _id;
        int _senderId;
        int _receiverId;

        public int SenderId { get => _senderId; set => _senderId = value; }
        public int ReceiverId { get => _receiverId; set => _receiverId = value; }
        public int Id { get => _id; set => _id = value; }

        public Contact(int senderId, int receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
        }

        public Contact()
        {
        }

        public string GetInsert()
        { 
            return $"INSERT INTO {GetTableName()} (SenderId, ReceiverId) VALUES ({SenderId}, {ReceiverId})";
        }

        public string GetTableName()
        {
            return "Contacts";
        }

        public void ParseSqlReader(SqlDataReader reader)
        {
            SenderId = reader.GetInt32(reader.GetOrdinal("SenderId"));
            ReceiverId = reader.GetInt32(reader.GetOrdinal("ReceiverId"));
        }
    }
}
