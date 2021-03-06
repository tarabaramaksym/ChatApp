using ChatApp.Logic;
using ChatApp.SharedLib.Enums;
using ChatApp.SharedLib.Messages;
using ChatApp.SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Networking
{
    public class Requests
    {
        readonly Socket _sender;

        public Requests(Socket sender)
        {
            _sender = sender;
        }

        internal List<Message> GetMessages(int id)
        {
            MessageToServer message = new MessageToServer
            {
                Command = CommandToServer.GET_MESSAGES,
                Id = UserInfo.Id,
                TargetId = id
            };
            byte[] bytes = message.EncodeMessage();
            _sender.Send(bytes);
            bytes = new byte[3072];
            _sender.Receive(bytes);
            MessageFromServer received = MessageFromServer.DecodeMessage(bytes);
            return received.Messages;
        }

        internal void SendMessage(string text, int id)
        {
            MessageToServer message = new MessageToServer
            {
                Command = CommandToServer.SEND_MESSAGE,
                Id = UserInfo.Id,
                TargetId = id,
                NewMessage = text,
            };
            byte[] bytes = message.EncodeMessage();
            _sender.Send(bytes);
        }

        public bool Authentication(string username, string password)
        {
            MessageToServer message = new MessageToServer
            {
                Command = CommandToServer.ASK_AUTHORIZATION,
                Username = username,
                Password = password
            };
            byte[] bytes = message.EncodeMessage();
            _sender.Send(bytes);
            bytes = new byte[3072];
            _sender.Receive(bytes);
            MessageFromServer received = MessageFromServer.DecodeMessage(bytes);
            if (received.Command == CommandFromServer.ACCEPTED)
            {
                UserInfo.Id = received.Id;
                UserInfo.Name = received.Name;
                UserInfo.Username = username;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Registration(string username, string password)
        {
            MessageToServer message = new MessageToServer
            {
                Command = CommandToServer.ASK_REGISTRATION,
                Username = username,
                Password = password
            };
            byte[] bytes = message.EncodeMessage();
            _sender.Send(bytes);
            bytes = new byte[3072];
            _sender.Receive(bytes);
            MessageFromServer received = MessageFromServer.DecodeMessage(bytes);
            if (received.Command == CommandFromServer.ACCEPTED)
            {
                UserInfo.Id = received.Id;
                UserInfo.Name = received.Name;
                UserInfo.Username = username;
                return true;
            }
            else
            {
                return false;
            }
                
        }

        public List<Contact> Contacts(int id)
        {
            MessageToServer message = new MessageToServer
            {
                Command = CommandToServer.GET_CONTACTS,
                Id = id
            };
            byte[] bytes = message.EncodeMessage();
            _sender.Send(bytes);
            bytes = new byte[3072];
            _sender.Receive(bytes);
            MessageFromServer received = MessageFromServer.DecodeMessage(bytes);
            return received.Users;
        }
        public List<Contact> SearchContacts(string search)
        {
            MessageToServer message = new MessageToServer
            {
                Command = CommandToServer.SEARCH_CONTACTS,
                Name = search
            };
            byte[] bytes = message.EncodeMessage();
            _sender.Send(bytes);
            bytes = new byte[3072];
            _sender.Receive(bytes);
            MessageFromServer received = MessageFromServer.DecodeMessage(bytes);
            return received.Users;
        }
    }
}
