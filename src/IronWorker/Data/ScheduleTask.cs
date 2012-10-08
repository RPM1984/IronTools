using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject]
    public class ScheduleTask
    {
        public string code_name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "created_at")]
        public DateTime? created_at { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "end_at")]
        public DateTime? end_at { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "last_run_time")]
        public DateTime? last_run_time { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "msg")]
        [DefaultValue("")]
        public string msg { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "next_start")]
        public DateTime? next_start { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "priority")]
        public int priority { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "project_id")]
        public string project_id { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "run_count")]
        [DefaultValue(0)]
        public int run_count { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            PropertyName = "run_every")]
        public int run_every { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "run_times")]
        public int run_times { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "start_at")]
        public string start_at { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "status")]
        [DefaultValue("")]
        public string status { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "updated_at")]
        public DateTime? updated_at { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "payload")]
        public string payload { get; set; }
    }
}