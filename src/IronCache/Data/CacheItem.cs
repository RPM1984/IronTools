//-----------------------------------------------------------------------
// <copyright file="CacheItem.cs" company="Oscar Deits">
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
    /// Iron.io cache item
    /// </summary>
    /// <typeparam name="T">Type of the object cached</typeparam>
    [JsonObject(MemberSerialization.OptOut)]
    internal class CacheItem<T>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the cache name
        /// </summary>
        public string Cache
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the check and set identifier
        /// </summary>
        [JsonProperty("cas")]
        public long CheckAndSet
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the cache key
        /// </summary>
        public string Key
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the value of the cache item
        /// </summary>
        public T Value
        {
            get; set;
        }

        #endregion Properties
    }
}