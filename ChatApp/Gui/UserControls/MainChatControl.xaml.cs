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

        List<ContactControl> contacts = new List<ContactControl>();
        public int Id { get; set; }
        public MainChatControl()
        {
            InitializeComponent();


            // seed
            ContactControl control = new ContactControl();
            control.NameTextBlock.Text = "John Doe";
            ContactsStackPanel.Children.Add(control);
            control = new ContactControl();
            ContactsStackPanel.Children.Add(control);
            control.NameTextBlock.Text = "Jane Doe";
            
        }

        private void ContactsStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Contacts.FillContacts(sender as StackPanel,Id);
        }
    }
}
