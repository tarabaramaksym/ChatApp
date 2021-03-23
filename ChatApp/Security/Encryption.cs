using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Security
{
    class Encryption
    {
        public string Encrypt(string str) // => Encoding.UTF8.GetString(new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(str)));
        {
            using (SHA256 sha = SHA256Managed.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(str);
                byte[] hash = sha.ComputeHash(data);
                StringBuilder hex = new StringBuilder();
                foreach (var x in hash)
                {
                    hex.Append(string.Format("{0:x2}", x));
                }
                return hex.ToString();
            }
        }
    }
}
