using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using messenger_app.models;

namespace messenger_app.tests.unit
{
    [TestClass]
    public sealed class MSTest_MessageModel
    {

        [TestMethod]
        public void TestMessageModelInit()
        {

            List<uint> ids = new List<uint> { 0, 1 };
            bool fromIdFound = false;
            bool bothFound = false;
            Message m1 = new Message(2, "Hello World", 0, 1, "00:00:05");
            Assert.AreEqual(2, m1.ID);
            Assert.AreEqual("Hello World", m1.Content);

            foreach(uint id in ids)
            {


                if(fromIdFound && id == m1.ReceiverID)
                {

                    bothFound = true;
                    break;

                }

                if(!fromIdFound && id == m1.SenderID)
                {

                    fromIdFound = true;

                }
            }

            Assert.AreEqual(true, bothFound); // We are simulating the idea that sender and receiver IDS must be valid and present in our database.
        }
    }
}
