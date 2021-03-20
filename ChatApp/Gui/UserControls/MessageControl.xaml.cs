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

namespace ChatApp.Gui.UserControls
{
    /// <summary>
    /// Interaction logic for MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        public MessageControl(MessageStatus status,string message,DateTime dateTime)
        {
            InitializeComponent();

            this.Margin = new Thickness(3);
            if(status == MessageStatus.RECEIVER)
            {
                this.HorizontalAlignment = HorizontalAlignment.Left;
                this.LeftMessage.Visibility = Visibility.Visible;
                LeftTimeTextBlock.Text = dateTime.ToShortTimeString();
                LeftMessageTextBlock.Text = message;
            }
            else
            {

                this.HorizontalAlignment = HorizontalAlignment.Right;
                this.RightMessage.Visibility = Visibility.Visible;
                RightTimeTextBlock.Text = dateTime.ToShortTimeString();
                RightMessageTextBlock.Text = message;
            }

            
            
        }
    }
}
