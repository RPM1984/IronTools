using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IronIO.Data
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusEnum
    {
        None = 0,
        Queued = 1,
        Running = 2,
        Complete = 4,
        Error = 8,
        Cancelled = 16,
        Killed = 32,
        Timeout = 64,
        All = Queued | Running | Complete | Error | Cancelled | Killed | Timeout
    }
    
    [JsonObject]
    public class TaskInfo
    {
        public string id { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public string code_id { get; set; }

        public StatusEnum status { get; set; }

        public string msg { get; set; }

        public string code_name { get; set; }

        public DateTime? start_time { get; set; }

        public DateTime? end_time { get; set; }

        public int duration { get; set; }

        public int run_times { get; set; }

        public int timeout { get; set; }

        public int percent { get; set; }

    }
}
