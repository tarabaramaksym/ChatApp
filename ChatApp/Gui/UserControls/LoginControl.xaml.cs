using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
            bool isValid = true;
            if (UsernameTextBox.Text == "")
            {
                ShakeUsernameTextBox();
                isValid = false;
            }
            if(PasswordPasswordBox.Password == "")
            {
                ShakePasswordBox();
                isValid = false;
            }
            if(isValid)
            {
                if (Login.Authenticate(UsernameTextBox.Text, PasswordPasswordBox.Password)){
                    // TODO Remember Me button

                    // proceed to the next form
                    (this.Parent as ContentControl).Content = new MainChatControl();
                }
                else
                {
                    ShakeUsernameTextBox();
                    ShakePasswordBox();
                    Storyboard storyBoard = (Storyboard)IncorrectPassOrUsernameLabel.Resources["LabelOpacity"];
                    Storyboard.SetTarget(storyBoard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, IncorrectPassOrUsernameLabel);
                    storyBoard.Begin();
                }
            }
        }


        private void ShakeUsernameTextBox()
        {
            Storyboard shake = (Storyboard)UsernameTextBox.Resources["TextBoxShakeStoryboard"];
            Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, UsernameTextBox);
            shake.Begin();
        }
        private void ShakePasswordBox()
        {
            Storyboard shake = (Storyboard)PasswordPasswordBox.Resources["PasswordBoxShakeStoryboard"];
            Storyboard.SetTarget(shake.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, PasswordPasswordBox);
            shake.Begin();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as ContentControl).Content = new RegistrationControl();
        }
    }
}
