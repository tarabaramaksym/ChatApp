﻿using System;
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
using ChatApp.Networking.Client;
namespace ChatApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Client.StartClient();
        }
        private void GetLeftBarWidth()
        {

        }
        public void CompleteAuthorization()
        {
            ContentArea.Content = new MainChatControl();
        }

        private void LoginControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
