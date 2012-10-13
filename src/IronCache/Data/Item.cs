//-----------------------------------------------------------------------
// <copyright file="IronWorker.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is 
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel;
using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    internal class Item<T>
    {
        /// <summary>
        /// The item’s data
        /// </summary>
        [JsonProperty("value", Required = Required.Always)]
        public T Body { get; set; }

        /// <summary>
        ///  How long in seconds to keep the item in the cache before it is deleted. Default is 604,800 seconds (7 days). Maximum is 2,592,000 seconds (30 days).
        /// </summary>
        [JsonProperty("expires_in")]
        public long? ExpiresIn { get; set; }

        /// <summary>
        ///  If set to true, only set the item if the item is already in the cache. If the item is not in the cache, do not create it.
        /// </summary>
        [JsonProperty("replace")]
        [DefaultValue(false)]
        public bool Replace { get; set; }

        /// <summary>
        ///  If set to true, only set the item if the item is not already in the cache. If the item is in the cache, do not overwrite it.
        /// </summary>
        [JsonProperty("add")]
        [DefaultValue(false)]
        public bool Add { get; set; }
    }
}