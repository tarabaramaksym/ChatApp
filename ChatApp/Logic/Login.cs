using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Networking;

namespace ChatApp.Logic
{
    public static class Login
    {

        public static bool Authenticate(string username,string password)
        {
            if(Client.Request.Authentication(username, password))
            {
                UserInfo.Username = username;
                return true;
            }
            return false;
        }

        public static bool Register(string username,string password)
        {
            return Client.Request.Registration(username, password);
        }

    }
}
