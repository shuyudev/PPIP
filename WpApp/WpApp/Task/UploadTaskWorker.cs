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
using Windows.UI.Popups;

namespace WpApp.Task
{
    public class UploadTaskWorker : ITask
    {
        private CloudStorageAccount StorageAccount;
        private string Container;
        private string DeviceId;
        private string TaskId;

        public UploadTaskWorker(string connectionString, string container, string deviceId, string taskId)
        {
            StorageAccount = CloudStorageAccount.Parse(connectionString);
            Container = container;
            DeviceId = deviceId;
            TaskId = taskId;
        }

        public async void Execute()
        {
            try
            {
                Dictionary<StorageFile, bool> totalFiles = await LoadFilesFromStorage();
                totalFiles.
                SkipUploadedFiles(totalFiles);

                StringBuilder toBeUploadedFiles = new StringBuilder();
                var fileList = totalFiles.Where(kvp => !kvp.Value);

                foreach (var file in fileList)
                {
                    UploadFile(file.Key);
                    toBeUploadedFiles.Append(file.Key.Name);
                    toBeUploadedFiles.Append(",");
                    break;
                }

                SaveUploadedFiles(toBeUploadedFiles);
            }
            catch(Exception e) { }
        }

        private void SaveUploadedFiles(StringBuilder toBeUploadedFiles)
        {
            AppCache.UploadedFiles = AppCache.UploadedFiles + toBeUploadedFiles.ToString();
            ApplicationData.Current.LocalSettings.Values[AppCache.UploadedFilesName] = AppCache.UploadedFiles;
        }

        private async void UploadFile(StorageFile file)
        {
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(Container);

            var blob = container.GetBlockBlobReference(file.Name);

            await blob.UploadFromFileAsync(file);

            //complete job
            Client client = new Client(AppCache.DevelopMode);

            var response = client.CompleteUploadTask(DeviceId, TaskId, file.Name);

            if(response.ResponseStatus == Status.Succeed)
            {
                MessageDialog msgBox = new MessageDialog("Uploaded successfully!");
                msgBox.ShowAsync().GetResults();
            }
            else
            {
                MessageDialog msgBox = new MessageDialog("Uploaded failed, please check network and try again!");
                msgBox.ShowAsync().GetResults();
            }
        }

        private void SkipUploadedFiles(Dictionary<StorageFile, bool> totalFiles)
        {
            string uploadedFiles = AppCache.UploadedFiles;

            for(int i = 0; i < totalFiles.Count; i++)
            {
                var file = totalFiles.ElementAt(i);
                if(uploadedFiles.Contains(file.Key.Name))
                {
                    totalFiles[file.Key] = true;
                }
            }
        }

        private async Task<Dictionary<StorageFile, bool>> LoadFilesFromStorage()
        {
            StorageFolder dataFolder = KnownFolders.CameraRoll;
            var files = await dataFolder.GetFilesAsync();

            Dictionary<StorageFile, bool> result = new Dictionary<StorageFile, bool>();
            foreach(var file in files)
            {
                result.Add(file, false);
            }

            return result;
        }
    }
}
