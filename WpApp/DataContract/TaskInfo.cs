namespace DataContract
{
    public class TaskInfo
    {
        public string StorageAccountName { get; set; }
        public string StorageAccountKey { get; set; }
        public string BlobContainer { get; set; }
        public string BlobName { get; set; }
    }
}