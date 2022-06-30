using System.Collections.Generic;
using System.Windows;
using System;
using System.Diagnostics;
using System.Threading;
using messenger_app.models;
using messenger_app.db;

namespace messenger_app
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {

        public const string PLACEHOLDER_SEARCH = "<enter an email address>"; // The default text string within the search bar.
        private User _activeUser;  // A reference to the User model that is currently logged in.
        private SQLConnectionWrapper _wrapper; // A SQLConnectionWrapper object that represents the active connection to the Microsoft SQL Server instance.
        private TimeSpan _lastQueriedTime = TimeSpan.Parse("00:00:00"); // a TimeSpan object that assists in determining if a new message has been detected.
        private Dictionary<uint, MailControl> _mailControls = new Dictionary<uint, MailControl>(); // A Dictionary object for storing references to MailControl instances.

        // A two-parameter constructor that accepts a User model object and a valid SQLConnectionWrapper for handling SQL Server queries.
        public Dashboard(ref User u, ref SQLConnectionWrapper wrapper)
        {
            InitializeComponent();
            this._activeUser = u;
            this._wrapper = wrapper;
            this.Title = "Messenger App - Dashboard";
            this.textWelcome.Content = string.Format("Welcome, {0}", u.FirstName);
            this.textMessageInfo.Content = u.MessageHistory.Count == 0 ? "No current messages. Search for a user or a friend to get started!" : "Looks like you've been pretty social lately! Ready to pick up where you left off?";
            RenderMessages();

            // Spawn a worker thread to check for any new messages to a particular conversation.
            Thread checkMessagesThread = new Thread(monitorMessages);
            checkMessagesThread.SetApartmentState(ApartmentState.STA);
            checkMessagesThread.Start();

        }

        // NavigateMainWindow() navigates the user to the main window.
        private void NavigateMainWindow()
        {

            MainWindow home = new MainWindow();
            home.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            home.Show();
            this.Close();
        }

        // MonitorMessages() is a callback function that is spawned by a daemon thread. It is intended to detect any new, incoming conversations
        // during this user session and to alert the user.
        private void monitorMessages()
        {

            int tempMessageCount = 0;

            while(true)
            {

                string newMessageQuery = string.Format("SELECT sender_id, time_created FROM [messages].[Table] WHERE receiver_id={0} AND CAST(time_created AS time) > CAST('{1}' as time)", _activeUser.ID, _lastQueriedTime);
                var data = _wrapper.query(newMessageQuery);

                if (data.ContainsKey("sender_id"))
                {

                    Debug.WriteLine("New data available.");
                    int relationsReturned = data["sender_id"].Count;

                    for (int i = 0; i < relationsReturned; i++)
                    {

                        TimeSpan temp = TimeSpan.Parse(data["time_created"][i].ToString());
                        uint useId = System.Convert.ToUInt32(data["sender_id"][i]);
                        _lastQueriedTime = temp > _lastQueriedTime ? temp : _lastQueriedTime;
                        tempMessageCount++;

                        this.Dispatcher.Invoke(() =>
                        {


                            if (_mailControls.ContainsKey(useId))
                            {

                                _mailControls[useId].IncomingMessages += 1;
                                string newContent = string.Format("{0} ({1})\t\t({2}) new messages", _mailControls[useId].FullName, _mailControls[useId].Email, _mailControls[useId].IncomingMessages.ToString());
                                _mailControls[useId].StringContent = newContent;

                            }
                            else
                            {

                                string userQuery = string.Format("SELECT id, first_name, last_name, email FROM [users].[Table] WHERE id={0}", useId);
                                var data = _wrapper.query(userQuery);
                                uint lookupId = System.Convert.ToUInt32(data["id"][0]);
                                string fullName = string.Format("{0} {1}", data["first_name"][0].ToString(), data["last_name"][0].ToString());
                                string lookupEmail = data["email"][0].ToString();
                                DrawMailControl(lookupId, fullName, lookupEmail, 0, 0);

                            }
                        });
                    }
                }

                Thread.CurrentThread.Join(3000);
            }
        }

        // DrawMailControl(id, name, email, X, Y) draws a MailControl component to @see myStackPanel. It cached as key-value pair
        // where id is the key that points to a MailControl instance. This instance has content composed of name and email. 
        // Additionally, it is located at coordinates (X, Y).
        private void DrawMailControl(uint id, string name, string email, double X, double Y)
        {

            MailControl mc = new MailControl(name, email, X, Y, ref _wrapper, this, ref _activeUser);
            myStackPanel.Children.Add(mc);
            
            if(!_mailControls.ContainsKey(id))
            {

                _mailControls[id] = mc;

            }
        }

        // RenderMessages() renders valid conversations to the user interface at instantiation time.
        private void RenderMessages()
        {


            string dbQuery = string.Format("SELECT time_created, sender_id, receiver_id from [messages].[Table] WHERE sender_id={0} OR receiver_id={0}", _activeUser.ID);
            var data = _wrapper.query(dbQuery);
            int relationsReturned = data.ContainsKey("sender_id") ? data["sender_id"].Count : 0;
            int count = 0;

            for(int i = 0; i < relationsReturned; i++)
            {

                uint sId = System.Convert.ToUInt32(data["sender_id"][i]);
                uint rId = System.Convert.ToUInt32(data["receiver_id"][i]);
                uint useId = sId != _activeUser.ID ? sId : rId;

                if(_mailControls.ContainsKey(useId)) { continue;  }

                TimeSpan tempTs = TimeSpan.Parse(data["time_created"][i].ToString());
                _lastQueriedTime = tempTs > _lastQueriedTime ? tempTs : _lastQueriedTime;
                string tempq = string.Format("SELECT id, first_name, last_name, email FROM [users].[Table] WHERE id={0}", useId);
                var data2 = _wrapper.query(tempq);
                uint id = System.Convert.ToUInt32(data2["id"][0]);
                string fullName = string.Format("{0} {1}", data2["first_name"][0].ToString(), data2["last_name"][0].ToString());
                string email = data2["email"][0].ToString();
                DrawMailControl(id, fullName, email, 0, count == 0 ? 10 : 20);
                count++;

            }
        }

        private void handleSearchGotFocus(object sender, RoutedEventArgs e)
        {

            this.textSearch.Text = this.textSearch.Text == PLACEHOLDER_SEARCH ? "" : this.textSearch.Text;

        }

        private void handleLostFocusSearch(object sender, RoutedEventArgs e)
        {

            //this.textSearch.Text = this.textSearch.Text.Length < 5 ? PLACEHOLDER_SEARCH : this.textSearch.Text;

        }

        private void handleSearchClick(object sender, RoutedEventArgs e)
        {

            int queryLength = this.textSearch.Text.Length;

            if (queryLength < 5 || this.textSearch.Text == PLACEHOLDER_SEARCH)
            {

                this.textSearch.Text = "You have entered an invalid query. Please check your input.";

            }
            else if (textSearch.Text == _activeUser.Email)
            {

                this.textSearch.Text = "Messages cannot be sent to yourself!";

            }
            else
            { 

                string dbQuery = string.Format("SELECT * FROM [users].[Table] WHERE email='{0}'", this.textSearch.Text);
                Dictionary<string, List<object>> userData = _wrapper.query(dbQuery);

                if (!userData.ContainsKey("id"))
                {

                    this.textSearch.Text = "No results found.";

                }
                else
                {

                    User recipient = new User(System.Convert.ToUInt32(userData["id"][0]), userData["first_name"][0].ToString(), userData["last_name"][0].ToString(), userData["email"][0].ToString(), userData["gender"][0].ToString() == "1");
                    Conversation conversation = new Conversation(ref _activeUser, ref recipient, ref _wrapper);
                    conversation.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    conversation.Show();
                    this.Close();

                }
            }
        }

        private void handleLogoutClick(object sender, RoutedEventArgs e)
        {

            NavigateMainWindow();

        }
    }
}
