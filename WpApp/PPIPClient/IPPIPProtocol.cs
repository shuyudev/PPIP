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
        string Register(string deviceName, string registerKey);
        List<TaskDetail> PullTask(string deviceId);
        ResponseBase CompleteTask(string deviceId, string taskId);
        ResponseBase CompleteUploadTask(string deviceId, string taskId, string blobPath);
    }
}
