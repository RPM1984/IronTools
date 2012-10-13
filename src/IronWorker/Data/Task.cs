using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

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
    public class Task
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("code_id")]
        public string CodeId { get; set; }

        [JsonProperty("status")]
        public StatusEnum Status { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("code_name")]
        public string CodeName { get; set; }

        [JsonProperty("start_time")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("run_times")]
        public int RunTimes { get; set; }

        [JsonProperty("timeout")]
        public int Timeout { get; set; }

        [DefaultValue(3600)]
        [JsonProperty("percent")]
        public int percent { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }

        [DefaultValue(0)]
        [JsonProperty("priority")]
        public int Priority { get; set; }

        [DefaultValue(0)]
        [JsonProperty("delay")]
        public int Delay { get; set; }
    }
}