using ChatApp.SharedLib.Messages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.SharedLib.Models
{
    [Serializable]
    public class Message : IModel
    {
        string _message;
        DateTime _dateTime;
        MessageStatus status;

        public string MessageProperty { get => _message; set => _message = value; }
        public DateTime DateTime { get => _dateTime; set => _dateTime = value; }
        public MessageStatus Status { get => status; set => status = value; }

        public void ParseSqlReader(SqlDataReader reader)
        {
            MessageProperty = reader.GetString(reader.GetOrdinal("Message"));
            DateTime = reader.GetDateTime(reader.GetOrdinal("Datetime"));
        }

    }
}
