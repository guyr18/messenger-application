using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace messenger_app
{
    /// <summary>
    /// Interaction logic for ChatBox.xaml
    /// </summary>
    public partial class ChatBox : UserControl
    {

        // Default constructor for a ChatBox user control that takes 5 parameters.
        // @param content: The message displayed by the control.
        // @param X: The X coordinate of this control.
        // @param Y: The Y coordinate of this control.
        // @param fillColor: The background color of this control.
        // @param isMe: A boolean variable indicating if this control represents a message
        //              that was sent by the user that is logged in on this application.
        public ChatBox(string content, double X, double Y, Color fillColor, bool isMe)
        {
            InitializeComponent();
            this.Margin = new Thickness(X, Y, 0, 0);
            this.Background = new SolidColorBrush(fillColor);
            this.textContent.Foreground = Brushes.Black;
            this.textContent.AppendText(content);

        }
    }
}
