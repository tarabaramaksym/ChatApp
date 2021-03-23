using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RegistrationControl.xaml
    /// </summary>
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "")
                ShakeUsernameTextBox();
            if (PasswordPasswordBox.Password == "")
                ShakePasswordPasswordBox();   
            if(ConfirmPasswordPasswordBox.Password == "")
                ShakeConfirmPasswordPasswordBox();

            if (PasswordPasswordBox.Password != "" && UsernameTextBox.Text != "" && ConfirmPasswordPasswordBox.Password != "")
            {
                if(PasswordPasswordBox.Password != ConfirmPasswordPasswordBox.Password)
                {
                    AnimateErrorMessage("Password do not match.");
                }
                else if(PasswordPasswordBox.Password.Length <= 8)
                {
                    AnimateErrorMessage("Password needs to be at least 8 symbols long.");
                }
                else if(Regex.Match(PasswordPasswordBox.Password, @"[^\u0000-\u024F]+", RegexOptions.None).Success)
                {
                    AnimateErrorMessage("Use only latin/numbers/special symbols.");
                }
                else
                {
                    if (Login.Register(UsernameTextBox.Text, PasswordPasswordBox.Password))
                        (this.Parent as ContentControl).Content = new MainChatControl();
                    else
                    {
                        AnimateErrorMessage("User with this username already exists.");
                    }
                }
            }
        }

        private void ShakePasswordPasswordBox()
        {
            Storyboard shake = (Storyboard)PasswordPasswordBox.Resources["PasswordBoxShakeStoryboard"];
            Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, PasswordPasswordBox);
            shake.Begin();
        }
        private void ShakeConfirmPasswordPasswordBox()
        {
            Storyboard shake = (Storyboard)PasswordPasswordBox.Resources["PasswordBoxShakeStoryboard"];
            Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, ConfirmPasswordPasswordBox);
            shake.Begin();
        }
        private void ShakeUsernameTextBox()
        {
            Storyboard shake = (Storyboard)UsernameTextBox.Resources["TextBoxShakeStoryboard"];
            Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, UsernameTextBox);
            shake.Begin();

        }
        private void AnimateErrorMessage(string message)
        {
            ErrorLabel.Content = message;
            Storyboard storyBoard = (Storyboard)ErrorLabel.Resources["LabelOpacity"];
            Storyboard.SetTarget(storyBoard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, ErrorLabel);
            storyBoard.Begin();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as ContentControl).Content = new LoginControl();
        }
    }
}
