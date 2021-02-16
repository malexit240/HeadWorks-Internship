using Microsoft.VisualStudio.TestTools.UnitTesting;
using HWInternshipProject.Models;

namespace HWITests
{
    [TestClass]
    public class TestUserModel
    {

        [TestInitialize]
        public void TestInit()
        {
            (new Context()).Clear();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            (new Context()).Clear();
        }

        [TestMethod]
        public void TestCreateUser()
        {
            var user = User.Create("Vasya", "admin");
            Assert.IsTrue(RFCHasher.Verify("admin", user.HashPassword));
            user = User.Create("Vasya", "admin");
            Assert.IsNull(user);
        }
    }
}
