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
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public ContactControl()
        {
            InitializeComponent();
            
        }
        private bool _selected;

        public bool Selected { set
            {
                _selected = value;
                if(_selected)
                    MainGrid.Background = new SolidColorBrush(Color.FromRgb(32, 74, 78));
                else
                    MainGrid.Background = Brushes.Transparent;

            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e) {
            if (!_selected)
                MainGrid.Background = new SolidColorBrush(Color.FromRgb(32, 74, 78));
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!_selected)
                MainGrid.Background = Brushes.Transparent;
        }
        /*private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Selected = true;
            Main*Grid.Background = new SolidColorBrush(Color.FromRgb(32, 74, 78));
        }*/
    }
}
