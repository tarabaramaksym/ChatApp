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
        T SelectOne<T>(int id) where T : IModel, new();
        bool Insert(IModel model);
        List<SharedLib.Models.Contact> SelectContacts(int id);
        List<SharedLib.Models.Contact> SearchUsers(string name);
    }
}
