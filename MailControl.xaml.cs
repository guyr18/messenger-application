using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using messenger_app.db;
using messenger_app.models;

namespace messenger_app
{
    /// <summary>
    /// Interaction logic for MailControl.xaml
    /// </summary>
    public partial class MailControl : UserControl
    {

        private string _fullName; // String representing the full name corresponding to this user control.
        private string _email; // String representing the email address corresponding to this user control.
        private SQLConnectionWrapper _wrapper; // A SQLConnectionWrapper object that represents the active connection to the Microsoft SQL Server instance.
        private Dashboard _window; // A reference to the Dashboard window.
        private User _activeUser; // A reference to the User model that is currently logged in.
        private int _incomingMessages = 0; // The number of logged incoming text messages since this session was launched.

        // Getter for @see _fullName.
        public string FullName { get => _fullName;  }

        // Getter for @see _email.
        public string Email { get => _email;  }

        // Default constructor for a MailControl user control. There are seven parameters:
        // @param fullName: The full name of the user represented by this user control.
        // @param email: The email address of the user represented by this user control.
        // @param X: The X coordinate of this user control.
        // @param Y: The Y coordinate of this user control.
        // @param wrapper: A SQLConnectionWrapper object that represents the active connection to the Microsoft SQL Server instance.
        // @param window: A reference to the Dashboard window.
        // @param activeUser: A reference to the User model that is currently logged in.
        public MailControl(string fullName, string email, double X, double Y, ref SQLConnectionWrapper wrapper, Dashboard window, ref User activeUser)
        {
            InitializeComponent();
            this._fullName = fullName;
            this._email = email;
            this._wrapper = wrapper;
            this._window = window;
            this._activeUser = activeUser;
            this.Margin = new Thickness(X, Y, 0, 0);
            buttonUI.Content = string.Format("{0} ({1})", fullName, email);
        }

        // Getter/setter for @see _incomingMessages.
        public int IncomingMessages { get => _incomingMessages; set => _incomingMessages = value; }

        // Getter/setter for the content represented by this MailControl instance.
        public string StringContent { get => buttonUI.Content.ToString(); set => buttonUI.Content = value; }
     
        private void handleMailClick(object sender, RoutedEventArgs e)
        {

            string dbQuery = string.Format("SELECT * FROM [users].[Table] WHERE email='{0}'", _email);
            Dictionary<string, List<object>> userData = _wrapper.query(dbQuery);
            User recipient = new User(System.Convert.ToUInt32(userData["id"][0]), userData["first_name"][0].ToString(), userData["last_name"][0].ToString(), userData["email"][0].ToString(), userData["gender"][0].ToString() == "1");
            Conversation conversation = new Conversation(ref _activeUser, ref recipient, ref _wrapper);
            conversation.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            conversation.Show();
            _window.Close();

        }
    }
}
