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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatApp.Logic;

namespace ChatApp.Gui
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "")
            {
                Storyboard shake = (Storyboard)UsernameTextBox.Resources["TextBoxShakeStoryboard"];
                Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, UsernameTextBox);
                shake.Begin();
            }
            if(PasswordPasswordBox.Password == "")
            {
                Storyboard shake = (Storyboard)PasswordPasswordBox.Resources["PasswordBoxShakeStoryboard"];
                Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, PasswordPasswordBox);
                shake.Begin();
            }
            if(PasswordPasswordBox.Password != "" && UsernameTextBox.Text != "")
            {
                if (Login.Authenticate(UsernameTextBox.Text, PasswordPasswordBox.Password)){
                    // proceed to the next form
                    // TODO Remember Me button
                    (this.Parent as ContentControl).Content = new MainChatControl();
                }
                else
                {
                    Storyboard storyBoard = (Storyboard)IncorrectPassOrUsernameLabel.Resources["LabelOpacity"];
                    Storyboard.SetTarget(storyBoard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, IncorrectPassOrUsernameLabel);
                    storyBoard.Begin();
                }
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as ContentControl).Content = new RegistrationControl();
        }
    }
}
