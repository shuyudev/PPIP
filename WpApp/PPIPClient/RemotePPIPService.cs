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
        private const string NotifyUploadCompleteFormat = @"task/uploadComplete/{0}?phoneId={1}&blobName={2}";
        private const string CompleteTaskFormat = @"task/update/{0}?status={1}";

        public async Task<string> Register(string deviceName, string registerKey)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriBase);

            string uri = string.Format(RegisterFormat, registerKey, deviceName);

            HttpResponseMessage response = await client.PostAsync(uri, null);
            var content = await response.Content.ReadAsStringAsync();

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

        public async Task<List<TaskDetail>> PullTask(string deviceId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriBase);

            string uri = string.Format(PullTaskFormat, deviceId);

            HttpResponseMessage response = await client.PostAsync(uri, null);
            var content = await response.Content.ReadAsStringAsync();

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

        public async Task<ResponseBase> CompleteTask(string deviceId, string taskId, DataContract.DeviceTaskStatus status)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriBase);

            string uri = string.Format(CompleteTaskFormat, taskId, status);

            HttpResponseMessage response = await client.PostAsync(uri, null);

            return response.StatusCode == System.Net.HttpStatusCode.OK ?
                new ResponseBase() { ResponseStatus = Status.Succeed }
                : new ResponseBase() { ResponseStatus = Status.Failed };
        }


        public async Task<ResponseBase> NotifyUploadComplete(string deviceId, int taskId, string blobName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UriBase);

            string uri = string.Format(NotifyUploadCompleteFormat, taskId, deviceId, blobName);

            HttpResponseMessage response = await client.PostAsync(uri, null);

            return response.StatusCode == System.Net.HttpStatusCode.OK ? 
                new ResponseBase() { ResponseStatus = Status.Succeed } 
                : new ResponseBase() { ResponseStatus = Status.Failed };
        }
    }
}
