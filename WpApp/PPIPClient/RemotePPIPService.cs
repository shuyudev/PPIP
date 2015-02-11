using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    public class RemotePPIPService : IPPIPProtocol
    {
        private const string UriBase = @"http://ppip.cloudapp.net:1337/";
        public string Register(string deviceName, string registerKey)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriBase);

            var result = client.GetAsync(@"phone/register?token=1234&deviceName=nexus5").Result;

            return null;
        }

        public List<DataContract.TaskDetail> PullTask(string deviceId)
        {
            throw new NotImplementedException();
        }

        public DataContract.ResponseBase CompleteTask(string deviceId, string taskId, DataContract.DeviceTaskStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
