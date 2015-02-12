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
using DataContract;

namespace WpApp.Task
{
    public class DownloadTaskWorker : ITask
    {
        private CloudStorageAccount StorageAccount;
        private string Container;
        private int TaskId;
        private List<string> FileList;
        private const string Folder = "ShareFolder";

        public DownloadTaskWorker(string connectionString, string container, List<string> fileList, int id)
        {
            StorageAccount = CloudStorageAccount.Parse(connectionString);
            this.Container = container;
            this.FileList = fileList;
            TaskId = id;
        }

        public async void Execute()
        {
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(Container);
            StorageFolder dataFolder = KnownFolders.PicturesLibrary;

            foreach(var file in FileList)
            {
                var blob = container.GetBlockBlobReference(file);

                var windowsFile = await dataFolder.CreateFileAsync(file, CreationCollisionOption.ReplaceExisting);

                await blob.DownloadToFileAsync(windowsFile);

                Client client = new Client(Configurations.DevelopMode);

                var resp = await client.CompleteTask(Configurations.DeviceId, this.TaskId.ToString(), DeviceTaskStatus.succeeded);

                await System.Threading.Tasks.Task.Delay(1000);
                await Windows.System.Launcher.LaunchFileAsync(windowsFile);
            }
        }
    }
}
