//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Data
{
    using System;
    using System.ComponentModel;

    using Newtonsoft.Json;

    /// <summary>
    /// A message for use with Iron.io Message Queue
    /// </summary>
    [Serializable]
    [JsonObject]
    public class Message
    {
        #region Properties

        /// <summary>
        /// Gets or sets the message body
        /// </summary>
        [JsonProperty("body")]
        public string Body
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the message delivery delay in seconds
        /// </summary>
        [DefaultValue(0)]
        [JsonProperty("delay")]
        public long Delay
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the expire time of the message in seconds
        /// </summary>
        [DefaultValue(0)]
        [JsonProperty("expires_in")]
        public long ExpiresIn
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the message identifier
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the message timeout in seconds
        /// </summary>
        [DefaultValue(0)]
        [JsonProperty("timeout")]
        public long Timeout
        {
            get; set;
        }

        #endregion Properties
    }
}