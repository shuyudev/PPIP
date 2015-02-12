using DataContract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceContract;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class LocalPPIPClientTest
    {
        [TestMethod]
        public async void CanRegister()
        {
            Client client = new Client(true);
            var deviceId = await client.Register("miliu-lumia-123", "5678");

            Assert.AreEqual(deviceId, "5678");
        }

        [TestMethod]
        public async void CanPullTask()
        {
            Client client = new Client(true);
            List<TaskDetail> result = await client.PullTask("test");

            Assert.IsNotNull(result);
        }

    }
}
