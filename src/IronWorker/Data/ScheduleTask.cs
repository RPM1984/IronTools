//-----------------------------------------------------------------------
// <copyright file="IronWorker.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is 
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

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
        public string Id { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "last_run_time")]
        public DateTime? LastRunTime { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "msg")]
        [DefaultValue("")]
        public string Message { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "next_start")]
        public DateTime? NextStart { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "priority")]
        public int Priority { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "project_id")]
        public string ProjectId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "run_count")]
        [DefaultValue(0)]
        public int RunCount { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            PropertyName = "run_every")]
        public int RunEvery { get; set; }

        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "run_times")]
        public int RunTimes { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "start_at")]
        public string StartAt { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "status")]
        [DefaultValue("")]
        public string Status { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "payload")]
        public string Payload { get; set; }
    }
}