using DataContract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceContract;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class RemotePPIPServiceTest
    {
        [TestMethod]
        public void CanRegister()
        {
            Client client = new Client(false);
            var deviceId = client.Register("miliu-lumia-123", "2028");

            Assert.AreEqual("9", deviceId);
        }

        [TestMethod]
        public void CanPullTask()
        {
            Client client = new Client(false);
            List<TaskDetail> result = client.PullTask("9");

            Assert.IsNotNull(result);
        }

    }
}
