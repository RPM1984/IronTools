using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronIO.Data;
using Newtonsoft.Json;

namespace IronIO
{
    public class IronWorker
    {
        private IronClient _client;
        private string _taskCore = "tasks";

        public IronWorker(string projectId = null, string token = null)
        {
            _client = new IronClient("IronWorker .NET", "0.1", "iron_worker", projectId: projectId, token: token);
        }

        public IList<TaskInfo> Queue(IEnumerable<Task> tasks)
        {

            var body = JsonConvert.SerializeObject(new
            {
                tasks = tasks.ToArray()
            });
            string url = _taskCore;
            var response = _client.Post(url, body: body);
            var queueResponse = JsonConvert.DeserializeObject<QueueResponse>(response);
            return queueResponse.tasks;
        }
        public IList<TaskInfo> Queue(string code_name, string payload, int priority = 0, int timeout = 3600, int delay = 0)
        {
            var tasks = new Task[] 
                { 
                    new Task(){ 
                                code_name = code_name, 
                                payload = payload, 
                                priority = priority, 
                                timeout = timeout, 
                                delay = delay 
                            } 
                };
            return Queue(tasks);
        }

        public TaskInfo Task(string id)
        {
            var url = string.Format("{0}/{1}", _taskCore, id);

            var response = _client.Get(url);
            
            var taskInfo = JsonConvert.DeserializeObject<TaskInfo>(response);

            return taskInfo;
        }

        public string Log(string id)
        {
            var url = string.Format("{0}/{1}/log", _taskCore, id);
            var response = _client.Get(url);
            return response;
        }

        public bool Cancel(string id)
        {
            var url = string.Format("{0}/{1}/cancel", _taskCore, id);
            var response = _client.Post(url);
            var status = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            return status.ContainsKey("msg") && status["msg"] == "Cancelled";
        }
        public IList<TaskInfo> Tasks(int page = 0, int per_page = 30, StatusEnum statusFilter = StatusEnum.All, DateTime? from_time = null, DateTime? to_time = null)
        {
            StringBuilder queryParameters = new StringBuilder();

            if (page != 0)
                queryParameters.AppendFormat("page={0}&", page);
            if (per_page != 30)
                queryParameters.AppendFormat("per_page-{0}&", per_page);
            if (!statusFilter.HasFlag(StatusEnum.All))
            {
                var statusQueryParams = Enum.GetNames(typeof(StatusEnum))
                .Where(status =>
                    statusFilter.HasFlag((StatusEnum)Enum.Parse(typeof(StatusEnum), status)))
                    .Select(status => status.ToLower())
                    .Select(status => String.Format("{0}=1", status));

                var statusFilterQuery = String.Join("&", statusQueryParams);

                queryParameters.Append(statusFilterQuery);
            }
            if (from_time.HasValue)
                queryParameters.AppendFormat("from_time={0}", (from_time.Value - new DateTime(1970, 1, 1)).Seconds);
            if (to_time.HasValue)
                queryParameters.AppendFormat("to_time={0}", (to_time.Value - new DateTime(1970, 1, 1)).Seconds);

            var url = String.Format("{0}?{1}", _taskCore, queryParameters.ToString());

            var json = _client.Get(url);
            IList<TaskInfo> s = JsonConvert.DeserializeObject<TaskInfo[]>(json);
            return s;
        }

    }
}