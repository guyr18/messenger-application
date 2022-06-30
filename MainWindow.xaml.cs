using messenger_app.db;
using messenger_app.models;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;

namespace messenger_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public const string PLACEHOLDER_EMAIL = "example@yourdomain.com"; // Constant string representing the default value for the email user control component.
        public const string PLACEHOLDER_PWD = "<enter your password>"; // Constant string representing the default value for the password user control component
        public const string DEFAULT_DATA_SOURCE = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = messengerdb; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SQLConnectionWrapper _sqlWrapper; //  A SQLConnectionWrapper object that represents the active connection to the Microsoft SQL Server instance.

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Messenger App";
            this.textError.Content = "";
            _sqlWrapper = new SQLConnectionWrapper(DEFAULT_DATA_SOURCE, true);
         
        }

        private void handleGotFocusEmail(object sender, RoutedEventArgs e)
        {

            this.textEmail.Text = "";

        }

        private void handleGotFocusPwd(object sender, RoutedEventArgs e)
        {

            this.textPassword.Password = "";

        }

        private void handleLoginClick(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Login clicked.");
            string loginQuery = string.Format("SELECT * FROM [users].[Table] WHERE email='{0}' AND password='{1}'", this.textEmail.Text, this.textPassword.Password);
            Dictionary<string, List<object>> userData = _sqlWrapper.query(loginQuery);

            if(userData.Count == 0)
            {

                this.textError.Content = "Invalid email/password combination.";

            }
            else
            {

                // Login was successful. We also need to load corresponding message data.
                string messageQuery = string.Format("SELECT * FROM [messages].[Table] WHERE sender_id={0}", userData["id"][0]);
                Dictionary<string, List<object>> messageData = _sqlWrapper.query(messageQuery);

                // Build list of messages for this user.
                Dictionary<uint, List<Message>> messageMap = new Dictionary<uint, List<Message>>();
                int relationsReturned = messageData.ContainsKey("id") ? messageData["id"].Count : 0;

                for (int i = 0; i < relationsReturned; i++)
                {

                    uint id = System.Convert.ToUInt32(messageData["id"][i]);
                    string content = messageData["content"][i].ToString();
                    uint sId = System.Convert.ToUInt32(messageData["sender_id"][i]);
                    uint rId = System.Convert.ToUInt32(messageData["receiver_id"][i]);
                    string timeCreated = messageData["time_created"][i].ToString();

                    if (messageMap.ContainsKey(rId))
                    {

                        messageMap[rId].Add(new Message(id, content, sId, rId, timeCreated));

                    }
                    else
                    {

                        messageMap[rId] = new List<Message> { new Message(id, content, sId, rId, timeCreated) };

                    }
                }

                User activeUser = new User(System.Convert.ToUInt32(userData["id"][0]), userData["first_name"][0].ToString(), userData["last_name"][0].ToString(), userData["email"][0].ToString(), userData["gender"][0].ToString() == "1");
                activeUser.MessageHistory = messageMap;
                Dashboard dashboard = new Dashboard(ref activeUser, ref _sqlWrapper);
                dashboard.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                dashboard.Show();
                this.Close();
            }
        }

        private void handleLostFocusEmail(object sender, RoutedEventArgs e)
        {

            this.textEmail.Text = this.textEmail.Text == "" ? PLACEHOLDER_EMAIL : this.textEmail.Text;

        }

        private void handleLostFocusPwd(object sender, RoutedEventArgs e)
        {

            this.textPassword.Password = this.textPassword.Password == "" ? PLACEHOLDER_PWD : this.textPassword.Password;

        }

        private void handleRegisterClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Register reg = new Register(ref _sqlWrapper);
            reg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            reg.Show();
            this.Close();

        }
    }
}
