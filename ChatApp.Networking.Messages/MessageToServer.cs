using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChatApp.Networking.Messages
{

    public enum CommandToServer
    {
        ASK_AUTHORIZATION = 0,
        ASK_REGISTRATION,
        SEND_MESSAGE,

    }

    // ASK_AUTHORIZATION - send login and password, wait for an answer
    // ASK_REGISTRATION - send login, password and username, wait for an answer
    // SEND_MESSAGE - send target username and message, async

    [Serializable]
    public class MessageToServer
    {
        CommandToServer _command;
        // TODO implement a way to dispatch a message on server side
        string _password;
        string _username;
        string _name;
        string _newMessage;
        string _toUsername;

        public CommandToServer Command { get => _command; set => _command = value; }
        public string Password { get => _password; set => _password = value; }
        public string Username { get => _username; set => _username = value; }
        public string Name { get => _name; set => _name = value; }
        public string NewMessage { get => _newMessage; set => _newMessage = value; }
        public string ToUsername { get => _toUsername; set => _toUsername = value; }


        public byte[] EncodeMessage()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        public static MessageToServer DecodeMessage(byte[] data)
        {
            return JsonConvert.DeserializeObject<MessageToServer>(Encoding.UTF8.GetString(data));
        }
    }
}
