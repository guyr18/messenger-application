using System.Windows.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Threading;
using System;
using messenger_app.models;
using messenger_app.db;

namespace messenger_app
{
    /// <summary>
    /// Interaction logic for Conversation.xaml
    /// </summary>
    public partial class Conversation : Window
    {

        public readonly Color BLUE_FILL = Color.FromRgb(10, 122, 255); // Default blue RGB Color.
        public readonly Color WHITE_FILL = Color.FromRgb(255, 255, 255); // Default white RGB color.
        public const string PLACEHOLDER_MESSAGE = "Type a message.."; // String placeholder for typing / sending a message.
        private int _insertedMessageCount = 0; // An integer representing the number of messages inserted; this assists in rendering UI components.
        private User _me; // A reference to the User model that is currently logged.
        private User _target; // A reference to the User model that @see _me is interacting with.
        private SQLConnectionWrapper _wrapper; // A SQLConnectionWrapper object that represents the active connection to the Microsoft SQL Server instance.
        private TimeSpan _lastQueriedTime = TimeSpan.Parse("00:00:00"); // a TimeSpan object that assists in determining if a new message has been detected.

        // A three-parameter constructor that initializes @see _me, @see _target, and @see _wrapper.
        public Conversation(ref User me, ref User target, ref SQLConnectionWrapper wrapper)
        {

            InitializeComponent();
            Debug.WriteLine(me.Email);
            Debug.WriteLine(target.Email);
            this.Title = string.Format("Messenger App - Conversation [{0} -> {1}]", me.Email, target.Email);
            this.textWelcome.Content = string.Format("Welcome, {0}", me.FirstName);
            this.textRecipient.Content = string.Format("{0} {1}", target.FirstName, target.LastName);
            this._me = me;
            this._target = target;
            this._wrapper = wrapper;
            RenderMessages();
            Debug.WriteLine(string.Format("Last queried time: {0}", _lastQueriedTime));

            // Spawn a worker thread to constantly monitor database for new messages regarding this conversation.
            Thread messageCheckerThread = new Thread(monitorMessageHandler);
            messageCheckerThread.SetApartmentState(ApartmentState.STA);
            messageCheckerThread.Start();

        }

        // MonitorMessageHandler() is a callback function that is spawned by a daemon thread. It is intended to detect any new, incoming messages
        // from the target user and report them to the user interface.
        private void monitorMessageHandler()
        {

            int tempMessageCount = 0;

            while(true)
            {

                string newMessageQuery = string.Format("SELECT * FROM [messages].[Table] WHERE sender_id={0} AND receiver_id={1} AND CAST(time_created AS time) > CAST('{2}' as time)", _target.ID, _me.ID, _lastQueriedTime);
                var data = _wrapper.query(newMessageQuery);

                if(data.ContainsKey("id"))
                {

                    Debug.WriteLine("New data available.");
                    int relationsReturned = data["id"].Count;

                    for(int i = 0; i < relationsReturned; i++)
                    {

                        TimeSpan temp = TimeSpan.Parse(data["time_created"][i].ToString());
                        _lastQueriedTime = temp > _lastQueriedTime ? temp : _lastQueriedTime;
                        tempMessageCount++;

                        this.Dispatcher.Invoke(() =>
                        {

                            string content = string.Format("{0} says: {1}", _target.FirstName, data["content"][i].ToString());
                            DrawChatBox(content, 0, tempMessageCount * 20, WHITE_FILL);
                            textInfo.Content = "";

                        });
                    }
                }

                Thread.CurrentThread.Join(3000);

            }
        }

        // BuildTargetMessages(data) takes a reference of type List<Message> and builds this List for manipulation in the calling function.
        // This list represents messages that the target has sent to the currently logged in user.
        private void BuildTargetMessages(ref List<Message> data)
        {

            string targetMessageQuery = string.Format("SELECT * FROM [messages].[Table] WHERE sender_id={0} and receiver_id={1}", _target.ID, _me.ID);
            var messageData = _wrapper.query(targetMessageQuery);
            int relationsReturned = messageData.ContainsKey("id") ? messageData["id"].Count : 0;

            for (int i = 0; i < relationsReturned; i++)
            {

                uint id = System.Convert.ToUInt32(messageData["id"][i]);
                string content = messageData["content"][i].ToString();
                uint sId = System.Convert.ToUInt32(messageData["sender_id"][i]);
                uint rId = System.Convert.ToUInt32(messageData["receiver_id"][i]);
                string timeCreated = messageData["time_created"][i].ToString();
                data.Add(new Message(id, content, sId, rId, timeCreated));

            }
        }

        // DrawChatBox(content, X, Y, fillColor) renders a ChatBox user control to the user interface. 
        // The ChatBox is assigned a message of content at coordinates (X, Y) with a background color
        // of fillColor. This ChatBox user control instance is added to the chatStackPanel.
        private void DrawChatBox(string content, double X, double Y, Color fillColor)
        {

            ChatBox cb = new ChatBox(content, X, Y, fillColor, fillColor == BLUE_FILL);
            this.chatStackPanel.Children.Add(cb);

        }

        // RenderMessages() renders the chat history between the current user and the target user. This method takes into
        // account the timestamps of which messages were sent and as a result requires sorting.
        // Time complexity: O(max(NLogN, MLogM)) where N is the number of messages that the user has sent and M is
        // the total number of messages sent by the target.
        private void RenderMessages()
        {

            List<Message> messagesToRender = _me.MessageHistory.ContainsKey(_target.ID) ? _me.MessageHistory[_target.ID] : null;
            List<Message> targetMessagesToRender = new List<Message>();
            BuildTargetMessages(ref targetMessagesToRender);

            int myMessageIndex = 0;
            int targetMessageIndex = 0;
            int myMessagesUpperBound = messagesToRender == null ? 0 : messagesToRender.Count;
            int targetMessagesUpperBound = targetMessagesToRender.Count;

            try
            {

                if (messagesToRender != null)
                {

                    messagesToRender.Sort();

                }

            }
            catch (Exception ex) { }

            try
            {

                targetMessagesToRender.Sort();

            }
            catch (Exception ex) { }
            

            while(myMessageIndex < myMessagesUpperBound && targetMessageIndex < targetMessagesUpperBound)
            {

                int posY = myMessageIndex + targetMessageIndex == 0 ? 0 : 20;
                if (messagesToRender[myMessageIndex].ID < targetMessagesToRender[targetMessageIndex].ID)
                {

                    // Draw component for my message; it occurred first.
                    DrawChatBox(string.Format("{0} says: {1}", _me.FirstName, messagesToRender[myMessageIndex].Content), 0, posY, BLUE_FILL);
                    TimeSpan temp = TimeSpan.Parse(messagesToRender[myMessageIndex].TimeCreated);
                    _lastQueriedTime = temp > _lastQueriedTime ? temp : _lastQueriedTime;
                    myMessageIndex++;

                }
                else
                {

                    // Draw component for target message; it occurred first.
                    DrawChatBox(string.Format("{0} says: {1}", _target.FirstName, targetMessagesToRender[targetMessageIndex].Content), 0, posY, WHITE_FILL);
                    TimeSpan temp = TimeSpan.Parse(targetMessagesToRender[targetMessageIndex].TimeCreated);
                    _lastQueriedTime = temp > _lastQueriedTime ? temp : _lastQueriedTime;
                    targetMessageIndex++;

                }
            }

            while(myMessageIndex < myMessagesUpperBound)
            {

                // Draw component for my message; it occurred first.
                int posY = myMessageIndex + targetMessageIndex == 0 ? 0 : 20;
                TimeSpan temp = TimeSpan.Parse(messagesToRender[myMessageIndex].TimeCreated);
                _lastQueriedTime = temp > _lastQueriedTime ? temp : _lastQueriedTime;
                DrawChatBox(string.Format("{0} says: {1}", _me.FirstName, messagesToRender[myMessageIndex].Content), 0, posY, BLUE_FILL);
                myMessageIndex++;

            }

            while (targetMessageIndex < targetMessagesUpperBound)
            {

                // Draw component for my message; it occurred first.
                int posY = myMessageIndex + targetMessageIndex == 0 ? 0 : 20;
                DrawChatBox(string.Format("{0} says: {1}", _target.FirstName, targetMessagesToRender[targetMessageIndex].Content), 0, posY, WHITE_FILL);
                TimeSpan temp = TimeSpan.Parse(targetMessagesToRender[targetMessageIndex].TimeCreated);
                _lastQueriedTime = temp > _lastQueriedTime ? temp : _lastQueriedTime;
                targetMessageIndex++;

            }

            if(myMessageIndex == 0 && targetMessageIndex == 0)
            {

                textInfo.Content = string.Format("You and {0} have not shared any messages. Get a conversation started!", _target.FirstName);

            }
            else
            {

                textInfo.Content = "";
                _insertedMessageCount = targetMessagesUpperBound + myMessagesUpperBound;

            }
        }

        private void handleTypeFocus(object sender, RoutedEventArgs e)
        {

            if (this.textMessage.Text == PLACEHOLDER_MESSAGE)
            {

                this.textMessage.Text = "";

            }
        }

        private void handleTypeFocusLost(object sender, RoutedEventArgs e)
        {

            if(this.textMessage.Text == "")
            {

                this.textMessage.Text = PLACEHOLDER_MESSAGE;

            }
        }

        // RunMessageInsertThread() is a daemon thread spawned when a message is sent. It acquires a mutex to ensure that it
        // is the only thread entering the critical section (modifying the database concurrently), updates the UI, makes
        // database modifications, and releases the acquired lock.
        private void runMessageInsertThread()
        {

            Mutex myMutex = new Mutex();
            myMutex.WaitOne();

            this.Dispatcher.Invoke(() =>
            {

                string fullMessage = string.Format("{0} says: {1}", _me.FirstName, this.textMessage.Text);
                string message = this.textMessage.Text;
                DateTime dt = DateTime.Now;
                string timeCreated = new TimeSpan(dt.Hour, dt.Minute, dt.Second).ToString();
                this.textMessage.Text = "";
                this.textInfo.Content = "";
                string insertMessageQuery = string.Format("INSERT INTO [messages].[Table] (content, sender_id, receiver_id, time_created) VALUES ('{0}', {1}, {2}, '{3}')", message, _me.ID, _target.ID, timeCreated);
                _wrapper.insert(insertMessageQuery);
                DrawChatBox(fullMessage, 0, _insertedMessageCount == 0 ? 10 : 20, BLUE_FILL);
                _insertedMessageCount++;

            });

            myMutex.ReleaseMutex();

        }

        private void handleSendClick(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Send message clicked.");
            
            if(this.textMessage.Text != "" && this.textMessage.Text != PLACEHOLDER_MESSAGE)
            {

                Thread insertWorkerThread = new Thread(runMessageInsertThread);
                insertWorkerThread.Start();

            }
        }
    }
}
