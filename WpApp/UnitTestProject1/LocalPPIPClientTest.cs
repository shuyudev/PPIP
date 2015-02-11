using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceContract;

namespace UnitTestProject1
{
    [TestClass]
    public class LocalPPIPClientTest
    {
        [TestMethod]
        public void CanRegister()
        {
            Client client = new Client(false);

            var resp = client.Register("123", "1234");

            Assert.AreEqual(resp, "1234");
        }

    }
}
