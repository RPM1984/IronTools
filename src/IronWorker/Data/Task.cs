//-----------------------------------------------------------------------
// <copyright file="Task.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Data
{
    using System;
    using System.ComponentModel;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #region Enumerations

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

    #endregion Enumerations

    [JsonObject]
    public class Task
    {
        #region Properties

        [JsonProperty("code_id")]
        public string CodeId
        {
            get; set;
        }

        [JsonProperty("code_name")]
        public string CodeName
        {
            get; set;
        }

        [JsonProperty("created_at")]
        public DateTime CreatedAt
        {
            get; set;
        }

        [DefaultValue(0)]
        [JsonProperty("delay")]
        public int Delay
        {
            get; set;
        }

        [JsonProperty("duration")]
        public int Duration
        {
            get; set;
        }

        [JsonProperty("end_time")]
        public DateTime? EndTime
        {
            get; set;
        }

        [JsonProperty("id")]
        public string Id
        {
            get; set;
        }

        [JsonProperty("msg")]
        public string Message
        {
            get; set;
        }

        [JsonProperty("payload")]
        public string Payload
        {
            get; set;
        }

        [DefaultValue(3600)]
        [JsonProperty("percent")]
        public int Percent
        {
            get; set;
        }

        [DefaultValue(0)]
        [JsonProperty("priority")]
        public int Priority
        {
            get; set;
        }

        [JsonProperty("run_times")]
        public int RunTimes
        {
            get; set;
        }

        [JsonProperty("start_time")]
        public DateTime? StartTime
        {
            get; set;
        }

        [JsonProperty("status")]
        public StatusEnum Status
        {
            get; set;
        }

        [JsonProperty("timeout")]
        public int Timeout
        {
            get; set;
        }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt
        {
            get; set;
        }

        #endregion Properties
    }
}