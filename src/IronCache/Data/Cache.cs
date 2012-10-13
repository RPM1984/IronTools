//-----------------------------------------------------------------------
// <copyright file="Cache.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is 
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Iron.io Cache
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Cache
    {
        /// <summary>
        /// Gets or sets the project identifier
        /// </summary>
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the cache name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}