using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace ChatApp.Networking.Messages
{
    public enum CommandFromServer
    {
        REJECTED = 0,
        ACCEPTED,
        NEW_MESSAGE
    }

    // REJECT - rejected authorization or registration
    // ACCEPT - accepted authorization or registration
    // NEW_MESSAGE - receiving new message from another user

    // TODO Implement a way to dispatch a message

    [Serializable]
    public class MessageFromServer
    {
        CommandFromServer _command;
        string _newMessage;
        string _fromUsername;

        public CommandFromServer Command { get => _command; set => _command = value; }
        public string NewMessage { get => _newMessage; set => _newMessage = value; }
        public string FromUsername { get => _fromUsername; set => _fromUsername = value; }

        public byte[] EncodeMessage()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        public static MessageFromServer DecodeMessage(byte[] data)
        {
            return JsonConvert.DeserializeObject<MessageFromServer>(Encoding.UTF8.GetString(data));
        }
    }
}
