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
        string _senderId;
        string _receiverId;
        bool _accepted;

        public string SenderId { get => _senderId; set => _senderId = value; }
        public string ReceiverId { get => _receiverId; set => _receiverId = value; }
        public bool Accepted { get => _accepted; set => _accepted = value; }
        public int Id { get => _id; set => _id = value; }

        public Contact(int id,string senderId, string receiverId, bool accepted)
        {
            Id = id;
            SenderId = senderId;
            ReceiverId = receiverId;
            Accepted = accepted;
        }

        public string GetInsert()
        {
            int accepted = Accepted ? 1 : 0;
            return $"INSERT INTO {GetTableName()} (SenderId, ReceiverId, Accepted) VALUES ({SenderId}, {ReceiverId}, {accepted})";
        }

        public string GetTableName()
        {
            return "Contacts";
        }

        public void ParseSqlReader(SqlDataReader reader)
        {
            SenderId = reader.GetString(reader.GetOrdinal("SenderId"));
            ReceiverId = reader.GetString(reader.GetOrdinal("ReceiverId"));
            Accepted = int.Parse(reader.GetString(reader.GetOrdinal("Accepted"))) == 1 ? true : false;
        }
    }
}
