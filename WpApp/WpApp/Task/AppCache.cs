using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WpApp.Task
{
    public static class AppCache
    {
        public const string DeviceIdName = "DeviceId";
        public const string UploadedFilesName = "UploadedFiles";
        public const string UploadTaskName = "UploadTask";

        public static string DeviceId = "";
        public static string UploadedFiles = "";
        public static string UploadTask = "";

        public static bool DevelopMode = false;


        public static void ClearCache()
        {
            DeviceId = "";
            UploadedFiles = "";
            UploadTask = "";

            ApplicationData.Current.LocalSettings.Values[DeviceIdName] = "";
            ApplicationData.Current.LocalSettings.Values[UploadedFilesName] = "";
            ApplicationData.Current.LocalSettings.Values[UploadTaskName] = "";
        }
    }
}
