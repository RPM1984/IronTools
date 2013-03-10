//-----------------------------------------------------------------------
// <copyright file="Subscriber.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Data
{
    using System;
    using System.ComponentModel;

    using Newtonsoft.Json;

    /// <summary>
    /// A subscriber for use with Iron.io Message Queue
    /// </summary>
    [Serializable]
    [JsonObject]
    public class Subscriber
    {
        #region Properties

        /// <summary>
        /// Gets or sets the url
        /// </summary>
        [JsonProperty("url")]
        public string Url
        {
            get;
            set;
        }

        #endregion Properties
    }
}