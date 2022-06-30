using Microsoft.VisualStudio.TestTools.UnitTesting;
using messenger_app.models;

namespace messenger_app.tests.unit
{
    [TestClass]
    public sealed class MSTest_UserModel
    {

        [TestMethod]
        public void TestUserModelInit()
        {

            User u1 = new User(2, "Robert", "Guy", "guyr18@students.ecu.edu", true);
            Assert.AreEqual(2, u1.ID);
            Assert.AreEqual("Robert", u1.FirstName);
            Assert.AreEqual("Guy", u1.LastName);
            Assert.AreEqual("guyr18@students.ecu.edu", u1.Email);
            Assert.AreEqual(true, u1.IsMale);
            User u2 = new User(0, "Sarah", "Palin", "palin@domain.com", false);
            Assert.AreEqual(false, u2.IsMale);

        }
    }
}
