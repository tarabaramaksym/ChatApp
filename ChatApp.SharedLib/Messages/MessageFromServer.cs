using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ChatApp.SharedLib.Enums;
using ChatApp.SharedLib.Models;

namespace ChatApp.SharedLib.Messages
{
    
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
        int _id;
        string _name;
        public List<Models.Contact> Users;
        public List<Message> Messages;

        public CommandFromServer Command { get => _command; set => _command = value; }
        public string NewMessage { get => _newMessage; set => _newMessage = value; }
        public string FromUsername { get => _fromUsername; set => _fromUsername = value; }
        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }

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
