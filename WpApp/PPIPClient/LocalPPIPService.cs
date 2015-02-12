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
        public async Task<string> Register(string deviceName, string registerKey)
        {
            return registerKey;
        }

        public async Task<List<TaskDetail>> PullTask(string deviceId)
        {
            return new List<TaskDetail>() 
            { 
                new TaskDetail()
                {
                    Type = TaskType.Upload,
                    TaskInfo = new TaskInfo()
                    {
                        BlobContainer = "miliutest",
                        BlobName = "test.jpg",
                        StorageAccountName = "userfeedback",
                        StorageAccountKey = "CRXei1BvPbtegj+0AfY5D3HlLGdSZ4XsBxIOG2PFPwJ7nVb14JrJnyueBG9ilkF36kYX8V4WJ3oeIteoVI9Jhg=="
                    }
                }
            };
        }

        public async Task<ResponseBase> CompleteTask(string deviceId, string taskId, DeviceTaskStatus status)
        {
            throw new NotImplementedException();
        }


        public async Task<ResponseBase> NotifyUploadComplete(string deviceId, int taskId, string blobName)
        {
            return new ResponseBase() { ResponseStatus = Status.Succeed };
        }
    }
}
