using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ChatApp.SharedLib.Messages;
using ChatApp.SharedLib.Enums;
using ChatApp.Server.Data.Models;
using ChatApp.Data.MicrosoftSQLServer;
using ChatApp.Server.Data;

namespace ChatApp.Server.Core
{
    class Commands
    {

        private readonly IDatabase _database;
        private readonly Dictionary<string, Socket> _connectedUsers;

        public Commands()
        {
            _connectedUsers = new Dictionary<string, Socket>();
            _database = new SQLDatabase();
        }


        internal void GetContacts(MessageToServer receivedMessage, Socket handler)
        {
            MessageFromServer message = new MessageFromServer
            {
                Users = _database.SelectContacts(receivedMessage.Id)
            };
            handler.Send(message.EncodeMessage());
        }

        internal void SendMessage(MessageToServer receivedMessage, Socket handler)
        {
            if(_database.SelectContact(receivedMessage.Id,receivedMessage.TargetId) == null)
            {
                _database.Insert(new Contact(receivedMessage.Id, receivedMessage.TargetId));
            }
            _database.Insert(new Message(receivedMessage.Id, receivedMessage.TargetId, receivedMessage.NewMessage));
        }

        internal void Register(MessageToServer receivedMessage, Socket handler)
        {
            User user = _database.SelectOneUser(receivedMessage.Username);
            if (user != null)
                Reject(handler);
            else
            {
                _connectedUsers.Add(receivedMessage.Username, handler);
                user = new User(receivedMessage.Username, receivedMessage.Password, receivedMessage.Username);
                _database.Insert(user);
                user = _database.SelectOneUser(user.Username);
                MessageFromServer message = new MessageFromServer()
                {
                    Command = CommandFromServer.ACCEPTED,
                    Id = user.Id,
                    Name = user.Name
                };
                handler.Send(message.EncodeMessage());
            }
        }

        internal void Authorize(MessageToServer receivedMessage, Socket handler)
        {
            User user = _database.SelectOneUser(receivedMessage.Username);
            if (user != null)
            {
                if (user.Password == receivedMessage.Password)
                {
                    _connectedUsers.Add(receivedMessage.Username, handler);
                    MessageFromServer message = new MessageFromServer()
                    {
                        Command = CommandFromServer.ACCEPTED,
                        Id = user.Id,
                        Name = user.Name
                    };
                    handler.Send(message.EncodeMessage());
                    return;
                }
            }
            Reject(handler);
        }

        internal void LoadMessages(MessageToServer receivedMessage, Socket handler)
        {
            MessageFromServer message = new MessageFromServer
            {
                Messages = _database.SelectMessages(receivedMessage.Id,receivedMessage.TargetId)
            };
            handler.Send(message.EncodeMessage());
        }

        internal void SearchContacts(MessageToServer receivedMessage, Socket handler)
        {
            MessageFromServer message = new MessageFromServer
            {
                Users = _database.SearchUsers(receivedMessage.Name)
            };
            handler.Send(message.EncodeMessage());
        }

        internal void Accept(Socket handler)
        {
            MessageFromServer message = new MessageFromServer()
            {
                Command = CommandFromServer.ACCEPTED
            };
            handler.Send(message.EncodeMessage());
        }

        internal void Reject(Socket handler)
        {
            MessageFromServer message = new MessageFromServer()
            {
                Command = CommandFromServer.REJECTED
            };
            handler.Send(message.EncodeMessage());
        }
    }
}
