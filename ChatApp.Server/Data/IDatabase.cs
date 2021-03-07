using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Server.Data.Models;
using ChatApp.SharedLib.Enums;

namespace ChatApp.Server.Data
{
    interface IDatabase
    {
        List<T> SelectAll<T>() where T : IModel, new();
        User SelectOneUser(string username);
        bool Insert(IModel model);
        List<(string, ContactStatus)> SelectContacts(int id);
    }
}
