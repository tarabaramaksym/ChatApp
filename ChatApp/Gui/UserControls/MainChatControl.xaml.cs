using ChatApp.Gui.UserControls;
using ChatApp.Logic;
using ChatApp.SharedLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp.Gui
{
    /// <summary>
    /// Interaction logic for MainChatControl.xaml
    /// </summary>
    public partial class MainChatControl : UserControl
    {
        ContactControl _selected;
        public MainChatControl()
        {
            InitializeComponent();
            _selected = new ContactControl();
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
        }

        private void ContactsStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Contacts.FillContacts(sender as StackPanel);
        }

        private void ContactsStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ContactControl control && _selected != control)
            {
                _selected.Selected = false;
                control.Selected = true;
                _selected = control;
                NameTextBlock.Text = _selected.NameTextBlock.Text;

                Chat.LoadMessages(this.MessageStackPanel,(int)_selected.Tag);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxLabel_TextChanged(sender, e);
            if(SearchTextBox.Text != "")
            {
                SearchContactsScrollViewer.Visibility = Visibility.Visible;
                YourContactsScrollViewer.Visibility = Visibility.Hidden;
                SearchContactsStackPanel.Children.Clear();
                Contacts.SearchContacts(SearchContactsStackPanel,SearchTextBox.Text);
            }
            else
            {
                SearchContactsScrollViewer.Visibility = Visibility.Hidden;
                YourContactsScrollViewer.Visibility = Visibility.Visible;
                SearchContactsStackPanel.Children.Clear();
            }
        }

        private void TextBoxLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Name == "ChatTextBox"){
                if (ChatTextBox.Text != "")
                {
                    PlaceholderChatLabel.Visibility = Visibility.Hidden;
                }
                else
                {
                    PlaceholderChatLabel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (SearchTextBox.Text != "")
                {
                    PlaceholderSearchLabel.Visibility = Visibility.Hidden;
                }
                else
                {
                    PlaceholderSearchLabel.Visibility = Visibility.Visible;
                }
            }
            
        }

        private void TextBoxLabel_GotFocus(object sender, RoutedEventArgs e)
        {
            if((sender as TextBox).Name == "ChatTextBox")
            {
                PlaceholderChatLabel.Foreground = new SolidColorBrush(Color.FromRgb(97, 97, 97));
            }
            else
            {
                PlaceholderSearchLabel.Foreground = new SolidColorBrush(Color.FromRgb(97, 97, 97));
            }
        }

        private void TextBoxLabel_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Name == "ChatTextBox")
            {
                PlaceholderChatLabel.Foreground = new SolidColorBrush(Color.FromRgb(213, 213, 213));
            }
            else
            {
                PlaceholderSearchLabel.Foreground = new SolidColorBrush(Color.FromRgb(213, 213, 213));
            }
        }

        private void SendMessage(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (ChatTextBox.Text != "" && _selected != null)
                {
                    Chat.SendMessage(ChatTextBox.Text, (int)_selected.Tag);
                    var msg = new MessageControl(MessageStatus.SENDER, ChatTextBox.Text, DateTime.Now);
                    MessageStackPanel.Children.Add(msg);
                    Contacts.FillContacts(ContactsStackPanel);
                }
            }
        }
    }
}
