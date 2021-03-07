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
            {
                Storyboard shake = (Storyboard)UsernameTextBox.Resources["TextBoxShakeStoryboard"];
                Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, UsernameTextBox);
                shake.Begin();
            }
            if (PasswordPasswordBox.Password == "")
            {
                Storyboard shake = (Storyboard)PasswordPasswordBox.Resources["PasswordBoxShakeStoryboard"];
                Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, PasswordPasswordBox);
                shake.Begin();
            }
            if(ConfirmPasswordPasswordBox.Password == "")
            {
                Storyboard shake = (Storyboard)PasswordPasswordBox.Resources["PasswordBoxShakeStoryboard"];
                Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, ConfirmPasswordPasswordBox);
                shake.Begin();
            }

            if (PasswordPasswordBox.Password != "" && UsernameTextBox.Text != "" && ConfirmPasswordPasswordBox.Password != "")
            {
                if(PasswordPasswordBox.Password != ConfirmPasswordPasswordBox.Password)
                {
                    // TODO: When passwords are not the same show it using WPF not MessageBox;
                    MessageBox.Show("Passwords are not the same!");
                }
                else
                {
                    if (Login.Register(UsernameTextBox.Text, PasswordPasswordBox.Password))
                        (this.Parent as ContentControl).Content = new MainChatControl();
                    else
                    {
                        MessageBox.Show("User with this username already exists!");
                    }
                }
            }
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as ContentControl).Content = new LoginControl();
        }
    }
}
