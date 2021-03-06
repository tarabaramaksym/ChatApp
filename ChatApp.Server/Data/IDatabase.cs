using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Server.Data.Models;
using ChatApp.SharedLib.Enums;
using ChatApp.SharedLib.Messages;

namespace ChatApp.Server.Data
{
    interface IDatabase
    {
        List<T> SelectAll<T>() where T : IModel, new();
        User SelectOneUser(string username);
        Contact SelectContact(int id1, int id2);
        T SelectOne<T>(int id) where T : IModel, new();
        bool Insert(IModel model);
        List<SharedLib.Models.Contact> SelectContacts(int id);
        List<SharedLib.Models.Contact> SearchUsers(string name);
        List<SharedLib.Models.Message> SelectMessages(int id,int targetId);
    }
}
