//-----------------------------------------------------------------------
// <copyright file="QueueInfo.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Data
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// A queue for use with Iron.io Message Queue
    /// </summary>
    [Serializable]
    [JsonObject]
    public class QueueInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the queue identifier
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the queue name
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the project identifier to which the queue belongs
        /// </summary>
        [JsonProperty("project_id")]
        public string ProjectId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the project identifier to which the queue belongs
        /// </summary>
        [JsonProperty("push_type")]
        public string PushType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the project identifier to which the queue belongs
        /// </summary>
        [JsonProperty("retries")]
        public int Retries
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the project identifier to which the queue belongs
        /// </summary>
        [JsonProperty("retries_delay")]
        public int RetriesDelay
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the project identifier to which the queue belongs
        /// </summary>
        [JsonProperty("size")]
        public int Size
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the project identifier to which the queue belongs
        /// </summary>
        [JsonProperty("subscribers")]
        public Subscriber[] Subscribers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the project identifier to which the queue belongs
        /// </summary>
        [JsonProperty("total_messages")]
        public int TotalMessages
        {
            get;
            set;
        }

        #endregion Properties
    }
}