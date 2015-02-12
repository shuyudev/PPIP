using DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    public interface IPPIPProtocol
    {
        Task<string> Register(string deviceName, string registerKey);
        Task<List<TaskDetail>> PullTask(string deviceId);
        Task<ResponseBase> CompleteTask(string deviceId, string taskId, DeviceTaskStatus status);

        Task<ResponseBase> NotifyUploadComplete(string deviceId, int taskId, string blobName);
    }
}
