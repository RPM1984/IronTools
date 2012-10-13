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

    [Serializable]
    [JsonObject]
    public class Message
    {
        #region Properties

        [JsonProperty("body")]
        public string Body
        {
            get; set;
        }

        [DefaultValue(0)]
        [JsonProperty("delay")]
        public long Delay
        {
            get; set;
        }

        [DefaultValue(0)]
        [JsonProperty("expires_in")]
        public long ExpiresIn
        {
            get; set;
        }

        [JsonProperty("id")]
        public string Id
        {
            get; set;
        }

        [DefaultValue(0)]
        [JsonProperty("timeout")]
        public long Timeout
        {
            get; set;
        }

        #endregion Properties
    }
}