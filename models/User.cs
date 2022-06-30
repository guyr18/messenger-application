using System;
using System.Collections.Generic;
using System.Text;

namespace messenger_app.models
{

    public class User
    {

        // Private variables
        #region
        private uint _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private bool _isMale;
        private Dictionary<uint, List<Message>> _messageHistory;
        #endregion

        // Accessors
        #region
        public uint ID { get => _id; } // Unsigned integer that represents the unique identifier for this user; primary key.
        public string FirstName { get => _firstName; } // String that represents the first name of this user.

        public string LastName { get => _lastName;  } // String that represents the last name of this user.

        public string Email { get => _email;  } // String that represents the email address of this user; primary key.

        public bool IsMale { get => _isMale;  } // Boolean variable that determines if this user is a male or not.
        #endregion

        // Accessors / Mutators
        public Dictionary<uint, List<Message>> MessageHistory { get => _messageHistory; set => _messageHistory
                 = value;
        }

        // Default constructor that initializes all properties for this class.
        public User(uint id, string fname, string lname, string email, bool isMale)
        {

            this._id = id;
            this._firstName = fname;
            this._lastName = lname;
            this._email = email;
            this._isMale = isMale;

        }
    }
}