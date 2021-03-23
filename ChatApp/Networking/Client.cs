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
        static readonly IPEndPoint _remoteEP;
        static readonly Socket     _sender;
        static readonly Requests   _request;

        const int PORT = 11001;

        public static Requests Request { get => _request;}

        static Client()
        {
            _remoteEP = new IPEndPoint(IPAddress.Parse("192.168.1.104")/*IPAddress.Parse("178.151.85.223")*/, PORT);
            _sender = new Socket(_remoteEP.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            _request = new Requests(_sender);
        }

        public static async void StartReceiving()
        {
            await Task.Run(() =>
            {
                byte[] bytes = new byte[2048];
                while (true)
                {
                    try
                    {
                        int bytesRec = _sender.Receive(bytes);
                    }
                    catch(Exception)
                    {

                    }
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
            try
            {
                try
                {
                    _sender.Connect(_remoteEP);
                    // TODO implement logging?
                }
                catch (ArgumentNullException)
                {
                    //Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException)
                {
                    //Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception)
                {
                    // Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception)
            {
                // Console.WriteLine(e.ToString());
            }
        }
    }
}
