using ChatApp.Networking.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.Networking.Client
{
    public static class Client
    {
        static IPEndPoint _remoteEP;
        static Socket _sender;

        const int PORT = 11001;

        static Client()
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11001 on the local computer.  
            _remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1")/*IPAddress.Parse("178.151.85.223")*/, PORT);

            // Create a TCP/IP  socket.  
            _sender = new Socket(_remoteEP.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
        }
        public static async void StartReceiving()
        {
            await Task.Run(() =>
            {
                byte[] bytes = new byte[1024];
                while (true)
                {
                    int bytesRec = _sender.Receive(bytes);
                    //Console.WriteLine(Encoding.UTF8.GetString(bytes, 0, bytesRec));
                   
                }
            });
        }
        public static void CloseConnection()
        {
            // TODO send message on server to close connection
            // TODO IDisposable
            _sender.Shutdown(SocketShutdown.Both);
            _sender.Close();
        }
        public static bool RequestAuthentication(string username,string password)
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
        public static void StartClient()
        {
            byte[] bytes = new byte[1024];
            try
            {
                try
                {
                    _sender.Connect(_remoteEP);
                    // TODO implement logging?
                }
                catch (ArgumentNullException ane)
                {
                    //Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    //Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    // Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                // Console.WriteLine(e.ToString());
            }
        }
    }
}
