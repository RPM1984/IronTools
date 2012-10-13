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

    /// <summary>
    /// Worker statuses
    /// </summary>
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusEnum
    {
        /// <summary>
        /// None status
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Queued status
        /// </summary>
        Queued = 1,

        /// <summary>
        /// Running status
        /// </summary>
        Running = 2,

        /// <summary>
        /// Complete status
        /// </summary>
        Complete = 4,

        /// <summary>
        /// Error status
        /// </summary>
        Error = 8,

        /// <summary>
        /// Cancelled status
        /// </summary>
        Cancelled = 16,

        /// <summary>
        /// Killed status
        /// </summary>
        Killed = 32,

        /// <summary>
        /// Timed out status
        /// </summary>
        Timeout = 64,

        /// <summary>
        /// All statuses
        /// </summary>
        All = Queued | Running | Complete | Error | Cancelled | Killed | Timeout
    }

    #endregion Enumerations

    /// <summary>
    /// Iron.io IronWorker Task
    /// </summary>
    [JsonObject]
    public class Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the code identifier
        /// </summary>
        [JsonProperty("code_id")]
        public string CodeId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the code name
        /// </summary>
        [JsonProperty("code_name")]
        public string CodeName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the creation time of the task
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the delay in seconds
        /// </summary>
        [DefaultValue(0)]
        [JsonProperty("delay")]
        public int Delay
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the duration in seconds
        /// </summary>
        [JsonProperty("duration")]
        public int Duration
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the end time
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime? EndTime
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the task's unique identifier
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        [JsonProperty("msg")]
        public string Message
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the JSON Payload
        /// </summary>
        [JsonProperty("payload")]
        public string Payload
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the percent complete
        /// </summary>
        [DefaultValue(3600)]
        [JsonProperty("percent")]
        public int Percent
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the priority of the task
        /// </summary>
        [DefaultValue(0)]
        [JsonProperty("priority")]
        public int Priority
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the number of times the task has run
        /// </summary>
        [JsonProperty("run_times")]
        public int RunTimes
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the start time of the task
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime? StartTime
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current status of the task
        /// </summary>
        [JsonProperty("status")]
        public StatusEnum Status
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating the task timeout
        /// </summary>
        [JsonProperty("timeout")]
        public int Timeout
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating the last time the task was updated
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt
        {
            get; set;
        }

        #endregion Properties
    }
}