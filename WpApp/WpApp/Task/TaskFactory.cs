using DataContract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WpApp.Task
{
    internal class TaskFactory
    {
        internal static ITask CreateTask(TaskDetail task)
        {
            if(task.Type == TaskType.download)
            {
                string connectionstring = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", task.TaskInfo.StorageAccountName, task.TaskInfo.StorageAccountKey);
                string container = task.TaskInfo.BlobContainer;
                string file = task.TaskInfo.BlobName;
                string taskId = task.Id;

                return new DownloadTaskWorker(connectionstring, container, file, AppCache.DeviceId, taskId);
            }

            if (task.Type == TaskType.upload)
            {
                string connectionstring = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", task.TaskInfo.StorageAccountName, task.TaskInfo.StorageAccountKey);
                string container = task.TaskInfo.BlobContainer;

                ApplicationData.Current.LocalSettings.Values[AppCache.UploadTaskName] = JsonConvert.SerializeObject(task);

                return new UploadTaskWorker(connectionstring, container, AppCache.DeviceId, task.Id);
            }

            return null;
        }
    }
}
