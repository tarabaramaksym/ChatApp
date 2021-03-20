using System;
using System.Text;
using Newtonsoft.Json;
using ChatApp.SharedLib.Enums;

namespace ChatApp.SharedLib.Messages
{

    

    // ASK_AUTHORIZATION - send login and password, wait for an answer
    // ASK_REGISTRATION - send login, password and username, wait for an answer
    // SEND_MESSAGE - send target username and message, async

    [Serializable]
    public class MessageToServer
    {
        CommandToServer _command;
        // TODO implement a way to dispatch a message on server side because this is terrible!!!!!
        string _password;
        string _username;
        string _name;
        int _id;
        int _targetId;
        string _newMessage;
        
        

        public CommandToServer Command { get => _command; set => _command = value; }
        public string Password { get => _password; set => _password = value; }
        public string Username { get => _username; set => _username = value; }
        public string Name { get => _name; set => _name = value; }
        public string NewMessage { get => _newMessage; set => _newMessage = value; }
        public int Id { get => _id; set => _id = value; }
        public int TargetId { get => _targetId; set => _targetId = value; }

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
