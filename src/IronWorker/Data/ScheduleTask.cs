//-----------------------------------------------------------------------
// <copyright file="ScheduleTask.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Data
{
    using System;
    using System.ComponentModel;

    using Newtonsoft.Json;

    /// <summary>
    /// Represents a Task that repeats on a Schedule
    /// </summary>
    [JsonObject]
    public class ScheduleTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the code to schedule
        /// </summary>
        public string CodeName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the DateTime of the creation of the Schedule
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "created_at")]
        public DateTime? CreatedAt
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the DateTime of the End of the Schedule
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "end_at")]
        public DateTime? EndAt
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Schedule identifier
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "id")]
        public string Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the DateTime of the last run of the scheduled task
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "last_run_time")]
        public DateTime? LastRunTime
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a message returned by the Iron.io API
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "msg")]
        [DefaultValue("")]
        public string Message
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the DateTime of the next start of the schedule task
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "next_start")]
        public DateTime? NextStart
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the payload to be handed to the code
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "payload")]
        public string Payload
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the priority of the task
        /// </summary>
        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "priority")]
        public int Priority
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the project identifier
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "project_id")]
        public string ProjectId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the number of times the schedule has already run
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "run_count")]
        [DefaultValue(0)]
        public int RunCount
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the interval in seconds
        /// </summary>
        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            PropertyName = "run_every")]
        public int RunEvery
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the max number of times to run the task
        /// </summary>
        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "run_times")]
        public int RunTimes
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the DateTime to begin running the task
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "start_at")]
        public string StartAt
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current status of the Schedule
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "status")]
        [DefaultValue("")]
        public string Status
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the DateTime of the last update to the Schedule
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore, PropertyName = "updated_at")]
        public DateTime? UpdatedAt
        {
            get; set;
        }

        #endregion Properties
    }
}