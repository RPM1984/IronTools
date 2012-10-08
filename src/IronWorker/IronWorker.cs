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

        #region Tasks

        public IList<TaskInfo> Queue(IEnumerable<Task> tasks)
        {
            var body = JsonConvert.SerializeObject(new
            {
                tasks = tasks.ToArray()
            });
            string url = _taskCore;
            var response = _client.Post(url, body: body);
            var queueResponse = JsonConvert.DeserializeObject<QueueResponse>(response);
            return queueResponse.Tasks;
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
            var d = JsonConvert.DeserializeObject<Dictionary<string, TaskInfo[]>>(json);
            TaskInfo[] tasks;
            if (d.TryGetValue("tasks", out tasks))
                return tasks;
            return new TaskInfo[0];
        }

        #endregion Tasks

        #region Code Packages

        private static string _codeCore = "codes";

        public IList<CodeInfo> Codes(int page = 0, int per_page = 30)
        {
            var url = string.Format("{0}?page={1}&per_page={2}", _codeCore, page, per_page);
            var json = _client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, CodeInfo[]>>(json);
            CodeInfo[] codes;
            if (d.TryGetValue("codes", out codes))
                return codes;
            return new CodeInfo[0];
        }

        public CodeInfo Code(string id)
        {
            var url = string.Format("{0}/{1}", _codeCore, id);
            var json = _client.Get(url);
            var codeInfo = JsonConvert.DeserializeObject<CodeInfo>(json);
            return codeInfo;
        }

        public void DeleteCode(string id)
        {
            var url = string.Format("{0}/{1}", _codeCore, id);
            var json = _client.Delete(url);
            var msg = JsonConvert.DeserializeObject(json);
        }

        public IList<CodeInfo> CodeRevisions(string id, int page = 0, int per_page = 30)
        {
            var url = string.Format("{0}/{1}/revisions?page={2}&per_page={3}", _codeCore, id, page, per_page);
            var json = _client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, CodeInfo[]>>(json);
            CodeInfo[] revisions;
            if (d.TryGetValue("revisions", out revisions))
                return revisions;
            return new CodeInfo[0];
        }

        #endregion Code Packages

        #region Schedule Tasks

        private string _scheduleCore = "schedules";

        public IList<ScheduleInfo> Schedules(int page = 0, int per_page = 30)
        {
            var url = String.Format("{0}?page={1}&per_page={2}", _scheduleCore, page, per_page);
            var json = _client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, ScheduleInfo[]>>(json);
            ScheduleInfo[] schedules;
            if (d.TryGetValue("schedules", out schedules))
                return schedules;
            return new ScheduleInfo[0];
        }

        public ScheduleInfo Schedule(string id)
        {
            throw new NotImplementedException();
        }

        public void CancelSchedule(string id)
        {
            throw new NotImplementedException();
        }

        public IList<ScheduleInfo> ScheduleWorker(params ScheduleTask[] schedules)
        {
            var url = _scheduleCore;
            Dictionary<string, IList<ScheduleTask>> d = new Dictionary<string, IList<ScheduleTask>>();
            d["schedules"] = schedules;
            var json = JsonConvert.SerializeObject(d);
            var responseJson = _client.Post(url, body: json);
            var response = JsonConvert.DeserializeObject(responseJson) as Dictionary<string, object>;
            var s = response["schedules"] as ScheduleInfo[];
            return s;
        }

        #endregion Schedule Tasks
    }
}