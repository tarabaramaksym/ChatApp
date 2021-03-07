using ChatApp.SharedLib.Enums;
using ChatApp.SharedLib.Messages;
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
        Socket _sender;

        public Requests(Socket sender)
        {
            _sender = sender;
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
            bytes = new byte[1024];
            _sender.Receive(bytes);
            MessageFromServer received = MessageFromServer.DecodeMessage(bytes);
            if (received.Command == CommandFromServer.ACCEPTED)
                return true;
            else
                return false;
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
            bytes = new byte[1024];
            _sender.Receive(bytes);
            MessageFromServer received = MessageFromServer.DecodeMessage(bytes);
            if (received.Command == CommandFromServer.ACCEPTED)
                return true;
            else
                return false;
        }

        public List<(string, ContactStatus)> Contacts(int id)
        {
            MessageToServer message = new MessageToServer
            {
                Command = CommandToServer.GET_CONTACTS,
                Id = id
            };
            byte[] bytes = message.EncodeMessage();
            _sender.Send(bytes);
            bytes = new byte[1024];
            _sender.Receive(bytes);
            MessageFromServer received = MessageFromServer.DecodeMessage(bytes);
            return received.Usernames;
        }
    }
}
