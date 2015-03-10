using DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    public class Client : IPPIPProtocol
    {
        private IPPIPProtocol service;

        public Client(bool isDevelop = false)
        {
            if (isDevelop)
            {
                service = new LocalPPIPService();
            }
            else
            {
                service = new RemotePPIPService();
            }
        }

        public string Register(string deviceName, string registerKey)
        {
            return service.Register(deviceName, registerKey);
        }

        public List<TaskDetail> PullTask(string deviceId)
        {
            return service.PullTask(deviceId);
        }

        public ResponseBase CompleteTask(string deviceId, string taskId)
        {
            return service.CompleteTask(deviceId, taskId);
        }

        public ResponseBase CompleteUploadTask(string deviceId, string taskId, string blobPath)
        {
            return service.CompleteUploadTask(deviceId, taskId, blobPath);
        }
    }
}
