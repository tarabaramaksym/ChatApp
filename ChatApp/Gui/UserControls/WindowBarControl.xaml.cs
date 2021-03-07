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
    /// Interaction logic for WindowBarControl.xaml
    /// </summary>
    public partial class WindowBarControl : UserControl
    {
        public WindowBarControl()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
            {
                Application.Current.MainWindow.DragMove();
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
            };
        }

        public void Close(object sender, EventArgs e) => Application.Current.MainWindow.Close();
        public void Minimize(object sender, EventArgs e) => Application.Current.MainWindow.WindowState = WindowState.Minimized;
        public void Maximize(object sender, EventArgs e) => Application.Current.MainWindow.WindowState = WindowState.Maximized;
    }
}
