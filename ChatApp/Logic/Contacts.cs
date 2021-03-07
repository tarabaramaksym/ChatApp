using ChatApp.Gui;
using ChatApp.Networking;
using ChatApp.SharedLib.Enums;
using ChatApp.SharedLib.Models;
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

        static List<Contact> _yourContacts;

        internal static void FillContacts(Panel pnl)
        {
            _yourContacts = Client.Request.Contacts(UserInfo.Id);
            foreach (Contact item in _yourContacts)
            {
                ContactControl contact = new ContactControl();
                contact.NameTextBlock.Text = item.Name;
                contact.NameTextBlock.Tag = item.Username;
                pnl.Children.Add(contact);
            }  
        }

        internal static void SearchContacts(Panel pnl,string search)
        {
            List<Contact> searchResult = Client.Request.SearchContacts(search);
            foreach (Contact item in searchResult)
            {
                ContactControl contact = new ContactControl();
                contact.NameTextBlock.Text = item.Name;
                contact.NameTextBlock.Tag = item.Username;
                pnl.Children.Add(contact);
            }
        }

    }
}
