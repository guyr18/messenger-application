using System;
using System.Collections.Generic;
using System.Text;

namespace messenger_app.models
{
    public sealed class Message
    {

        // Private variables
        #region
        private uint _id;
        private string _content;
        private uint _senderId;
        private uint _receiverId;
        private string _timeCreated;
        #endregion

        // Accessors
        #region
        public uint ID { get => _id; } // Unsigned integer that represents the unique identifier for this message; primary key.
        public string Content { get => _content; } // String representing the content of this message.
        public uint SenderID { get => _senderId; } // Unsigned integer that represents the identifier of the user who sent this message.
        public uint ReceiverID { get => _receiverId; } // Unsigned integer that represents the identifier of the intended recipient of this message.

        public string TimeCreated { get => _timeCreated;  } // String representing the time that the message was sent / created; using hh:mm:ss format.
        #endregion

        // Default constructor that initializes all properties for this class.
        public Message(uint id, string content, uint sId, uint rId, string tc)
        {

            this._id = id;
            this._content = content;
            this._senderId = sId;
            this._receiverId = rId;
            this._timeCreated = tc;

        }

        public static bool operator <(Message m1, Message m2)
        {

            return m1.ID < m2.ID;

        }

        public static bool operator >(Message m1, Message m2)
        {

            return m1.ID > m2.ID;

        }
    }
}