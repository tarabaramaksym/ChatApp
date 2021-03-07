using ChatApp.Logic;
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
        ContactControl selected;
        public MainChatControl()
        {
            InitializeComponent();
            selected = new ContactControl();
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
        }

        private void ContactsStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Contacts.FillContacts(sender as StackPanel);
        }

        private void ContactsStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ContactControl control = e.Source as ContactControl;
            if(control != null && selected != control)
            {
                // TODO: Load messages
                selected.Selected = false;
                control.Selected = true;
                selected = control;
                NameTextBlock.Text = selected.NameTextBlock.Text;
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(SearchTextBox.Text == "Search")
            {
                // TODO: If you are going to type search it's going to delete it on click.
                // Simple but pretty bad solution is to always remove text on GotFocus including when you already typed something.
                SearchTextBox.Text = "";
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                SearchTextBox.Text = "Search";
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchTextBox.Text != "" && SearchTextBox.Text != "Search")
            {
                SearchContactsScrollViewer.Visibility = Visibility.Visible;
                YourContactsScrollViewer.Visibility = Visibility.Hidden;
                SearchContactsStackPanel.Children.Clear();
                Contacts.SearchContacts(SearchContactsStackPanel,SearchTextBox.Text);
            }
            else
            {
                SearchContactsScrollViewer.Visibility = Visibility.Hidden;
                SearchContactsStackPanel.Children.Clear();
                YourContactsScrollViewer.Visibility = Visibility.Visible;
            }
        }
    }
}
