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
        #region Fields

        private static string codeCore = "codes";
        private IronClient client;
        private string scheduleCore = "schedules";
        private string taskCore = "tasks";

        #endregion Fields

        public IronWorker(string projectId = null, string token = null)
        {
            client = new IronClient("IronWorker .NET", "0.1", "iron_worker", projectId: projectId, token: token);
        }

        #region Tasks

        public bool Cancel(string id)
        {
            var url = string.Format("{0}/{1}/cancel", taskCore, id);
            var response = client.Post(url);
            var status = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            return status.ContainsKey("msg") && status["msg"] == "Cancelled";
        }

        public string Log(string id)
        {
            var url = string.Format("{0}/{1}/log", taskCore, id);
            var response = client.Get(url);
            return response;
        }

        public IList<string> Queue(IEnumerable<Task> tasks)
        {
            var body = JsonConvert.SerializeObject(new
            {
                tasks = tasks.ToArray()
            });
            string url = taskCore;
            var response = client.Post(url, body: body);
            var template = new { msg = string.Empty, tasks = new[] { new { id = string.Empty } } };
            var result = JsonConvert.DeserializeAnonymousType(response, template).tasks.Select(t => t.id).ToList();
            return result;
        }

        public IList<string> Queue(string code_name, string payload, int priority = 0, int timeout = 3600, int delay = 0)
        {
            var tasks = new Task[]
                {
                    new Task(){
                                CodeName = code_name,
                                Payload = payload,
                                Priority = priority,
                                Timeout = timeout,
                                Delay = delay
                            }
                };
            return Queue(tasks);
        }

        public Task Task(string id)
        {
            var url = string.Format("{0}/{1}", taskCore, id);

            var response = client.Get(url);

            var taskInfo = JsonConvert.DeserializeObject<Task>(response);

            return taskInfo;
        }

        public IList<Task> Tasks(int page = 0, int per_page = 30, StatusEnum statusFilter = StatusEnum.All, DateTime? from_time = null, DateTime? to_time = null)
        {
            StringBuilder queryParameters = new StringBuilder();

            if (page != 0)
                queryParameters.AppendFormat("page={0}&", page);
            if (per_page != 30)
                queryParameters.AppendFormat("per_page={0}&", per_page);
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

            var url = String.Format("{0}?{1}", taskCore, queryParameters.ToString());

            var json = client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, Task[]>>(json);
            Task[] tasks;
            if (d.TryGetValue("tasks", out tasks))
                return tasks;
            return new Task[0];
        }

        #endregion Tasks

        #region Code Packages

        public CodeInfo Code(string id)
        {
            var url = string.Format("{0}/{1}", codeCore, id);
            var json = client.Get(url);
            var codeInfo = JsonConvert.DeserializeObject<CodeInfo>(json);
            return codeInfo;
        }

        public IList<CodeInfo> CodeRevisions(string id, int page = 0, int per_page = 30)
        {
            var url = string.Format("{0}/{1}/revisions?page={2}&per_page={3}", codeCore, id, page, per_page);
            var json = client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, CodeInfo[]>>(json);
            CodeInfo[] revisions;
            if (d.TryGetValue("revisions", out revisions))
                return revisions;
            return new CodeInfo[0];
        }

        public IList<CodeInfo> Codes(int page = 0, int per_page = 30)
        {
            var url = string.Format("{0}?page={1}&per_page={2}", codeCore, page, per_page);
            var json = client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, CodeInfo[]>>(json);
            CodeInfo[] codes;
            if (d.TryGetValue("codes", out codes))
                return codes;
            return new CodeInfo[0];
        }

        public void DeleteCode(string id)
        {
            var url = string.Format("{0}/{1}", codeCore, id);
            var json = client.Delete(url);
            var msg = JsonConvert.DeserializeObject(json);
        }

        #endregion Code Packages

        #region Schedule Tasks

        public void CancelSchedule(string id)
        {
            var url = string.Format("{0}/{1}/cancel", scheduleCore, id);

            var response = client.Post(url);
        }

        public ScheduleTask Schedule(string id)
        {
            var url = string.Format("{0}/{1}", scheduleCore, id);

            var response = client.Get(url);

            var scheduleInfo = JsonConvert.DeserializeObject<ScheduleTask>(response);

            return scheduleInfo;
        }

        public IList<ScheduleTask> Schedules(int page = 0, int per_page = 30)
        {
            var url = String.Format("{0}?page={1}&per_page={2}", scheduleCore, page, per_page);
            var json = client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, ScheduleTask[]>>(json);
            ScheduleTask[] schedules;
            if (d.TryGetValue("schedules", out schedules))
                return schedules;
            return new ScheduleTask[0];
        }

        public IList<string> ScheduleWorker(params ScheduleTask[] schedules)
        {
            // Validate the shedules
            foreach (var schedule in schedules)
                schedule.Payload = schedule.Payload ?? "{}";

            var url = scheduleCore;
            Dictionary<string, IList<ScheduleTask>> d = new Dictionary<string, IList<ScheduleTask>>();
            d["schedules"] = schedules;
            var json = JsonConvert.SerializeObject(d);
            var responseJson = client.Post(url, body: json);
            var template = new { msg = string.Empty, schedules = new[] { new { id = string.Empty } } };
            var result = JsonConvert.DeserializeAnonymousType(responseJson, template).schedules.Select(s => s.id).ToList();
            return result;
        }

        #endregion Schedule Tasks
    }
}