using ChatApp.Gui.UserControls;
using ChatApp.Networking;
using ChatApp.SharedLib.Messages;
using ChatApp.SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChatApp.Logic
{
    public static class Chat
    {

        internal static void LoadMessages(Panel pnl,int targetId)
        {
            pnl.Children.Clear();
            List<Message> messages = Client.Request.GetMessages(targetId);
            foreach(var message in messages)
            {
                var msg = new MessageControl(message.Status,message.MessageProperty,message.DateTime);
                pnl.Children.Add(msg);
            }
        }

        internal static void SendMessage(string text, int id)
        {
            Client.Request.SendMessage(text, id);
        }

    }
}
