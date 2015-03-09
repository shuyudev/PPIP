using DataContract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    public class RemotePPIPService : IPPIPProtocol
    {
        private const string UriBase = @"http://ppip.cloudapp.net:1337/";
        private const string RegisterFormat = @"phone/register?token={0}&deviceName={1}";
        private const string PullTaskFormat = @"task/fetch?phoneId={0}";
        private const string CompleteUploadTaskFormat = @"task/uploadComplete/{0}?phoneId={1}&blobName={2}";
        private const string CompleteDownloadTaskFormat = @"task/update/{0}?status={1}";

        public string Register(string deviceName, string registerKey)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriBase);

            string uri = string.Format(RegisterFormat, registerKey, deviceName);
            
            HttpResponseMessage response = client.GetAsync(uri).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            try
            {
                var result = JsonConvert.DeserializeObject<List<RegisterResponse>>(content);
                return result[0].Id;
            }
            catch 
            {
                return null;
            }
        }

        public List<TaskDetail> PullTask(string deviceId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriBase);

            string uri = string.Format(PullTaskFormat, deviceId);

            HttpResponseMessage response = client.GetAsync(uri).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            try
            {
                var result = JsonConvert.DeserializeObject<List<TaskDetail>>(content);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public DataContract.ResponseBase CompleteTask(string deviceId, string taskId, DataContract.DeviceTaskStatus status)
        {
            throw new NotImplementedException();
        }

        public ResponseBase CompleteUploadTask(string deviceId, string taskId, string blobPath)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriBase);

            string uri = string.Format(CompleteUploadTaskFormat, taskId, deviceId, blobPath);

            HttpResponseMessage response = client.GetAsync(uri).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            try
            {
                var result = JsonConvert.DeserializeObject<List<TaskDetail>>(content);
                if(result != null)
                {
                    return new ResponseBase() { ResponseStatus = Status.Succeed };
                }
            }
            catch
            {
            }

            return new ResponseBase() { ResponseStatus = Status.Failed };
        }
    }
}
