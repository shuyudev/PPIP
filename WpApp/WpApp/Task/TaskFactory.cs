using DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpApp.Task
{
    internal class TaskFactory
    {
        internal static ITask CreateTask(TaskDetail task)
        {
            string connectionstring = "DefaultEndpointsProtocol=https;AccountName=userfeedback;AccountKey=CRXei1BvPbtegj+0AfY5D3HlLGdSZ4XsBxIOG2PFPwJ7nVb14JrJnyueBG9ilkF36kYX8V4WJ3oeIteoVI9Jhg==";
            string container = "maggie";
            List<string> fileList = new List<string>() { "test.jpg" };
            return new DownloadTaskWorker(connectionstring, container, fileList);
        }
    }
}
