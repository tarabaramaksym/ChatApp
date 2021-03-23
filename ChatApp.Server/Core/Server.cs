using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ChatApp.Data;
using ChatApp.SharedLib.Messages;
using ChatApp.Server.Data.Models;
using ChatApp.SharedLib.Enums;

namespace ChatApp.Server.Core
{
    public class Server
    {
        Commands _commands;
        
        public Server()
        {
            _commands = new Commands();
            StartListening();
        }
        public async void StartReceiving(Socket handler)
        {
            await Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        try { 
                            byte[] bytes = new Byte[3072];
                            int bytesRec = handler.Receive(bytes);
                            MessageToServer receivedMessage = MessageToServer.DecodeMessage(bytes);

                            switch (receivedMessage.Command)
                            {
                                case CommandToServer.ASK_AUTHORIZATION:
                                    _commands.Authorize(receivedMessage, handler);
                                    break;
                                case CommandToServer.ASK_REGISTRATION:
                                    _commands.Register(receivedMessage, handler);
                                    break;
                                case CommandToServer.SEND_MESSAGE:
                                    _commands.SendMessage(receivedMessage, handler);
                                    break;
                                case CommandToServer.GET_CONTACTS:
                                    _commands.GetContacts(receivedMessage, handler);
                                    break;
                                case CommandToServer.SEARCH_CONTACTS:
                                    _commands.SearchContacts(receivedMessage, handler);
                                    break;
                                case CommandToServer.GET_MESSAGES:
                                    _commands.LoadMessages(receivedMessage, handler);
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                catch(Exception)
                {
                    // user disconnected?
                }
            });
        }

        public void StartListening()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 11001 );

            // TCP/IP pocket 
            Socket listener = new Socket(endPoint.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // Bind
                listener.Bind(endPoint);

                listener.Listen(10);

                // start listening
                while (true)
                {
                    try
                    {
                        Socket handler = listener.Accept();
                        Console.WriteLine("{0} :: CONNECTED", handler.RemoteEndPoint);
                        StartReceiving(handler);

                        // TODO: handle disconnect
                        //handler.Shutdown(SocketShutdown.Both);
                        // handler.Close();
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine(e.ToString());
                    }
                }

            }
            catch (Exception)
            {
                //Console.WriteLine(e.ToString());
            }
        }
    }
}
