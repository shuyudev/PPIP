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
            return new List<TaskDetail>() 
            { 
                new TaskDetail()
                {
                    Type = TaskType.Download,
                    TaskInfo = new DownloadTask()
                    {
                        BlobContainer = "miliutest",
                        BlobName = "test.jpg",
                        StorageAccountName = "userfeedback",
                        StorageAccountKey = "CRXei1BvPbtegj+0AfY5D3HlLGdSZ4XsBxIOG2PFPwJ7nVb14JrJnyueBG9ilkF36kYX8V4WJ3oeIteoVI9Jhg=="
                    }
                }
            };
        }

        public ResponseBase CompleteTask(string deviceId, string taskId, DeviceTaskStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
