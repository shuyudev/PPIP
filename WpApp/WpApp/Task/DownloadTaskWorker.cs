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

namespace WpApp.Task
{
    public class DownloadTaskWorker : ITask
    {
        private CloudStorageAccount StorageAccount;
        private string Container;
        private string File;

        public DownloadTaskWorker(string connectionString, string container, string file)
        {
            StorageAccount = CloudStorageAccount.Parse(connectionString);
            this.Container = container;
            this.File = file;
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

            await Launcher.LaunchFileAsync(windowsFile);
        }
    }
}
