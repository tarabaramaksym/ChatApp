using ChatApp.Gui;
using ChatApp.Networking;
using ChatApp.SharedLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatApp.Logic
{
    public static class Contacts
    {
        internal static void FillContacts(Panel pnl,int id)
        {
            List<(string, ContactStatus)> list = Client.Request.Contacts(id);

            foreach((string,ContactStatus) item in list)
            {
                ContactControl contact = new ContactControl();
                contact.NameTextBlock.Text = item.Item1;
                switch (item.Item2)
                {
                    case ContactStatus.ACCEPTED: contact.MainGrid.Background = Brushes.Green;
                        break;
                    case ContactStatus.REQUESTS_TO_YOU:
                        contact.MainGrid.Background = Brushes.Yellow;
                        break;
                    case ContactStatus.YOUR_REQUESTS:
                        contact.MainGrid.Background = Brushes.Red;
                        break;
                    default:
                        break;
                }
                pnl.Children.Add(contact);
            }
            
        }
    }
}
