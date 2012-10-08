using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject]
    public class ScheduleTask
    {
        public string payload { get; set; }

        public string name { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            PropertyName = "run_every")]
        public int run_every { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "end_at")]
        public DateTime? end_at { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "run_times")]
        public int run_times { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "priority")]
        public int priority { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "start_at")]
        public DateTime? start_at { get; set; }
    }
}