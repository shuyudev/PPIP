using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Windows.Storage;
using Windows.System;
using ServiceContract;

namespace WpApp.Task
{
    public class DownloadTaskWorker : ITask
    {
        private CloudStorageAccount StorageAccount;
        private string Container;
        private string File;
        private string DeviceId;
        private string TaskId;

        public DownloadTaskWorker(string connectionString, string container, string file, string deviceId, string taskId)
        {
            StorageAccount = CloudStorageAccount.Parse(connectionString);
            this.Container = container;
            this.File = file;
            this.DeviceId = deviceId;
            this.TaskId = taskId;
        }

        public async void Execute()
        {
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(Container);
            StorageFolder dataFolder = KnownFolders.PicturesLibrary;

            var blob = container.GetBlockBlobReference(File);

            var windowsFile = await dataFolder.CreateFileAsync(File, CreationCollisionOption.ReplaceExisting);

            await blob.DownloadToFileAsync(windowsFile);

            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5));

            Client client = new Client(AppCache.DevelopMode);

            var response = client.CompleteTask(DeviceId, TaskId);

            await Launcher.LaunchFileAsync(windowsFile);
        }
    }
}
