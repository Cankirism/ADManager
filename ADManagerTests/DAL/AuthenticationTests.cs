using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADManager.Tests
{
    [TestClass()]
    public class AuthenticationTests
    {
        [TestMethod()]
        public void IsAuthenticatedTest()
        {
            var auth = new  Authentication("admin user","admin pass");
            bool actual = auth.IsAuthenticated();
            Assert.AreEqual(true, actual);
        }
    }
}