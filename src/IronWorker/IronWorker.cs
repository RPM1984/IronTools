//-----------------------------------------------------------------------
// <copyright file="IronWorker.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is 
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using IronIO.Data;
    using Newtonsoft.Json;

    /// <summary>
    /// Iron Worker API interface
    /// </summary>
    public class IronWorker
    {
        #region Fields

        #region Cores

        /// <summary>
        /// Code endpoint
        /// </summary>
        private static readonly string CodeCore = "codes";

        /// <summary>
        /// Schedule endpoint
        /// </summary>
        private static readonly string ScheduleCore = "schedules";

        /// <summary>
        /// Tasks endpoint
        /// </summary>
        private static readonly string TaskCore = "tasks";

        #endregion Cores

        /// <summary>
        /// Iron.io API client interface
        /// </summary>
        private IronClient client;

        #endregion Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="IronWorker" /> class.
        /// </summary>
        /// <param name="projectId">Project identifier available from the HUD</param>
        /// <param name="token">Token available from the HUD</param>
        public IronWorker(string projectId = null, string token = null)
        {
            this.client = new IronClient("IronWorker .NET", "0.1", "iron_worker", projectId: projectId, token: token);
        }

        #region Tasks

        /// <summary>
        /// Cancels a task
        /// </summary>
        /// <param name="id">Task identifier</param>
        /// <returns>Success of cancelation</returns>
        public bool Cancel(string id)
        {
            var url = string.Format("{0}/{1}/cancel", TaskCore, id);
            var response = this.client.Post(url);
            var status = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            return status.ContainsKey("msg") && status["msg"] == "Cancelled";
        }

        /// <summary>
        /// Get a log
        /// </summary>
        /// <param name="id">Log identifier</param>
        /// <returns>log body</returns>
        public string Log(string id)
        {
            var url = string.Format("{0}/{1}/log", TaskCore, id);
            var response = this.client.Get(url);
            return response;
        }

        /// <summary>
        /// Enqueue an IEnumerable of Task
        /// </summary>
        /// <param name="tasks">Tasks to be enqueued</param>
        /// <returns>Task identifiers</returns>
        public IList<string> Queue(IEnumerable<Task> tasks)
        {
            var body = JsonConvert.SerializeObject(new
            {
                tasks = tasks.ToArray()
            });
            string url = TaskCore;
            var response = this.client.Post(url, body: body);
            var template = new { msg = string.Empty, tasks = new[] { new { id = string.Empty } } };
            var result = JsonConvert.DeserializeAnonymousType(response, template).tasks.Select(t => t.id).ToList();
            return result;
        }

        /// <summary>
        /// Enqueue a single task
        /// </summary>
        /// <param name="code_name">Code name of the task</param>
        /// <param name="payload">JSON payload to be sent to the task</param>
        /// <param name="priority">Task priority</param>
        /// <param name="timeout">Task timeout</param>
        /// <param name="delay">Delay in seconds before executing the task</param>
        /// <returns>Task identifier</returns>
        public IList<string> Queue(string code_name, string payload, int priority = 0, int timeout = 3600, int delay = 0)
        {
            var tasks = new Task[]
                {
                    new Task()
                        {
                            CodeName = code_name,
                            Payload = payload,
                            Priority = priority,
                            Timeout = timeout,
                            Delay = delay
                        }
                };
            return this.Queue(tasks);
        }

        /// <summary>
        /// Get a Task
        /// </summary>
        /// <param name="id">Task identifier</param>
        /// <returns>An Iron.io Task</returns>
        public Task Task(string id)
        {
            var url = string.Format("{0}/{1}", TaskCore, id);

            var response = this.client.Get(url);

            var taskInfo = JsonConvert.DeserializeObject<Task>(response);

            return taskInfo;
        }

        /// <summary>
        /// Gets a paged list of Tasks
        /// </summary>
        /// <param name="page">Zero based page index</param>
        /// <param name="per_page">Number of results per page</param>
        /// <param name="statusFilter">Only lists tasks that match this status filter</param>
        /// <param name="from_time">Lower bound of the time of the task</param>
        /// <param name="to_time">Upper bound of the time of the task</param>
        /// <returns>Tasks that meet the criteria</returns>
        public IList<Task> Tasks(int page = 0, int per_page = 30, StatusEnum statusFilter = StatusEnum.All, DateTime? from_time = null, DateTime? to_time = null)
        {
            StringBuilder queryParameters = new StringBuilder();

            if (page != 0)
            {
                queryParameters.AppendFormat("page={0}&", page);
            }

            if (per_page != 30)
            {
                queryParameters.AppendFormat("per_page={0}&", per_page);
            }

            if (!statusFilter.HasFlag(StatusEnum.All))
            {
                var statusQueryParams = Enum.GetNames(typeof(StatusEnum))
                .Where(status =>
                    statusFilter.HasFlag((StatusEnum)Enum.Parse(typeof(StatusEnum), status)))
                    .Select(status => status.ToLower())
                    .Select(status => string.Format("{0}=1", status));

                var statusFilterQuery = string.Join("&", statusQueryParams);

                queryParameters.Append(statusFilterQuery);
            }

            if (from_time.HasValue)
            {
                queryParameters.AppendFormat("from_time={0}", (from_time.Value - new DateTime(1970, 1, 1)).Seconds);
            }

            if (to_time.HasValue)
            {
                queryParameters.AppendFormat("to_time={0}", (to_time.Value - new DateTime(1970, 1, 1)).Seconds);
            }

            var url = string.Format("{0}?{1}", TaskCore, queryParameters.ToString());

            var json = this.client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, Task[]>>(json);
            Task[] tasks;
            if (d.TryGetValue("tasks", out tasks))
            {
                return tasks;
            }

            return new Task[0];
        }

        #endregion Tasks

        #region Code Packages

        /// <summary>
        /// Gets information on a code object
        /// </summary>
        /// <param name="id">Code identifier</param>
        /// <returns>Information regarding a code package</returns>
        public CodeInfo Code(string id)
        {
            var url = string.Format("{0}/{1}", CodeCore, id);
            var json = this.client.Get(url);
            var codeInfo = JsonConvert.DeserializeObject<CodeInfo>(json);
            return codeInfo;
        }

        /// <summary>
        /// Gets a paged list of the revisions of a code object
        /// </summary>
        /// <param name="id">Code identifier</param>
        /// <param name="page">Zero based page index</param>
        /// <param name="per_page">Number of results per page</param>
        /// <returns>Revisions of a code object</returns>
        public IList<CodeInfo> CodeRevisions(string id, int page = 0, int per_page = 30)
        {
            var url = string.Format("{0}/{1}/revisions?page={2}&per_page={3}", CodeCore, id, page, per_page);
            var json = this.client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, CodeInfo[]>>(json);
            CodeInfo[] revisions;
            if (d.TryGetValue("revisions", out revisions))
            {
                return revisions;
            }

            return new CodeInfo[0];
        }

        /// <summary>
        /// Gets a paged list of code objects
        /// </summary>
        /// <param name="page">Zero based page index</param>
        /// <param name="per_page">Number of results per page</param>
        /// <returns>A paged list of code objects</returns>
        public IList<CodeInfo> Codes(int page = 0, int per_page = 30)
        {
            var url = string.Format("{0}?page={1}&per_page={2}", CodeCore, page, per_page);
            var json = this.client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, CodeInfo[]>>(json);
            CodeInfo[] codes;
            if (d.TryGetValue("codes", out codes))
            {
                return codes;
            }

            return new CodeInfo[0];
        }

        /// <summary>
        /// Deletes a code object
        /// </summary>
        /// <param name="id">Code identifier</param>
        public void DeleteCode(string id)
        {
            var url = string.Format("{0}/{1}", CodeCore, id);
            var json = this.client.Delete(url);
            var msg = JsonConvert.DeserializeObject(json);
        }

        #endregion Code Packages

        #region Schedule Tasks

        /// <summary>
        /// Cancels a schedule
        /// </summary>
        /// <param name="id">Schedule identifier</param>
        public void CancelSchedule(string id)
        {
            var url = string.Format("{0}/{1}/cancel", ScheduleCore, id);

            var response = this.client.Post(url);
        }

        /// <summary>
        /// Gets a Schedule
        /// </summary>
        /// <param name="id">Schedule identifier</param>
        /// <returns>A Task Schedule</returns>
        public ScheduleTask Schedule(string id)
        {
            var url = string.Format("{0}/{1}", ScheduleCore, id);

            var response = this.client.Get(url);

            var scheduleInfo = JsonConvert.DeserializeObject<ScheduleTask>(response);

            return scheduleInfo;
        }

        /// <summary>
        /// Gets a paged list of Schedules
        /// </summary>
        /// <param name="page">Zero based page index</param>
        /// <param name="per_page">Number of results per page</param>
        /// <returns>A paged list of Schedules</returns>
        public IList<ScheduleTask> Schedules(int page = 0, int per_page = 30)
        {
            var url = string.Format("{0}?page={1}&per_page={2}", ScheduleCore, page, per_page);
            var json = this.client.Get(url);
            var d = JsonConvert.DeserializeObject<Dictionary<string, ScheduleTask[]>>(json);
            ScheduleTask[] schedules;
            if (d.TryGetValue("schedules", out schedules))
            {
                return schedules;
            }

            return new ScheduleTask[0];
        }

        /// <summary>
        /// Schedules tasks
        /// </summary>
        /// <param name="schedules">Tasks to schedule</param>
        /// <returns>Schedule identifiers</returns>
        public IList<string> ScheduleWorker(params ScheduleTask[] schedules)
        {
            // Validate the shedules
            foreach (var schedule in schedules)
            {
                schedule.Payload = schedule.Payload ?? "{}";
            }

            var url = ScheduleCore;
            Dictionary<string, IList<ScheduleTask>> d = new Dictionary<string, IList<ScheduleTask>>();
            d["schedules"] = schedules;
            var json = JsonConvert.SerializeObject(d);
            var responseJson = this.client.Post(url, body: json);
            var template = new { msg = string.Empty, schedules = new[] { new { id = string.Empty } } };
            var result = JsonConvert.DeserializeAnonymousType(responseJson, template).schedules.Select(s => s.id).ToList();
            return result;
        }

        #endregion Schedule Tasks
    }
}