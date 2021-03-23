using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Networking;
using ChatApp.Security;

namespace ChatApp.Logic
{
    public static class Login
    {

        public static bool Authenticate(string username,string password)
        {
            var encryption = new Encryption();
            if(Client.Request.Authentication(username,encryption.Encrypt(password)))
            {
                UserInfo.Username = username;
                return true;
            }
            return false;
        }

        public static bool Register(string username,string password)
        {
            var encryption = new Encryption();
            return Client.Request.Registration(username, encryption.Encrypt(password));
        }

    }
}
