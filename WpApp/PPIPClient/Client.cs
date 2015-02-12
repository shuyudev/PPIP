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

        public async Task<string> Register(string deviceName, string registerKey)
        {
            return await service.Register(deviceName, registerKey);
        }

        public async Task<List<TaskDetail>> PullTask(string deviceId)
        {
            return await service.PullTask(deviceId);
        }

        public async Task<ResponseBase> CompleteTask(string deviceId, string taskId, DeviceTaskStatus status)
        {
            return await service.CompleteTask(deviceId, taskId, status);
        }


        public async Task<ResponseBase> NotifyUploadComplete(string deviceId, int taskId, string blobName)
        {
            return await service.NotifyUploadComplete(deviceId, taskId, blobName);
        }
    }
}
