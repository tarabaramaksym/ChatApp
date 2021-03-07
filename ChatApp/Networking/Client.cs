using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ChatApp.SharedLib.Messages;
using ChatApp.SharedLib.Enums;
using System.Collections.Generic;

namespace ChatApp.Networking
{
    public static class Client
    {
        static IPEndPoint _remoteEP;
        static Socket     _sender;
        static Requests   _request;

        const int PORT = 11001;

        public static Requests Request { get => _request;}

        static Client()
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11001 on the local computer.  
            _remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1")/*IPAddress.Parse("178.151.85.223")*/, PORT);
            // Create a TCP/IP  socket.  
            _sender = new Socket(_remoteEP.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            _request = new Requests(_sender);
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
            // TODO send message to server to close connection?
            // TODO IDisposable
            _sender.Shutdown(SocketShutdown.Both);
            _sender.Close();
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
