using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Networking.Client;

namespace ChatApp.Logic
{
    public static class Login
    {
        public static bool Hello()
        {
            return true;
        }
        public static bool Authenticate(string username,string password)
        {
            return Client.RequestAuthentication(username, password);
        }
    }
}
