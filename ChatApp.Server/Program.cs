using System;
using ChatApp.Server.Core;

namespace ChatApp.Server.Core
{
    class Program
    {
        public static int Main()
        {
            Server server = new Server();
            server.StartListening();
            return 0;
        }
    }
}
