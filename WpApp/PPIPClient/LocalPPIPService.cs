using DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    public class LocalPPIPService : IPPIPProtocol
    {
        public string Register(string deviceName, string registerKey)
        {
            return registerKey;
        }

        public List<TaskDetail> PullTask(string deviceId)
        {
            return new List<TaskDetail>() { new TaskDetail() };
        }

        public ResponseBase CompleteTask(string deviceId, string taskId, DeviceTaskStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
