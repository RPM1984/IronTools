//-----------------------------------------------------------------------
// <copyright file="Item.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Data
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    /// <summary>
    /// Item class for serializing data for to IronCache
    /// </summary>
    /// <typeparam name="T">Type of object to be stored</typeparam>
    [JsonObject(MemberSerialization.OptOut)]
    internal class Item<T>
    {
        #region Properties

        /// <summary>
        ///  Gets or sets a value indicating whether to set the item if the item is not already in the cache
        /// </summary>
        [JsonProperty("add")]
        [DefaultValue(false)]
        public bool Add
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the item’s data
        /// </summary>
        [JsonProperty("value", Required = Required.Always)]
        public T Body
        {
            get; set;
        }

        /// <summary>
        ///  Gets or sets how long in seconds to keep the item in the cache before it is deleted. Default is 604,800 seconds (7 days). Maximum is 2,592,000 seconds (30 days).
        /// </summary>
        [JsonProperty("expires_in")]
        public long? ExpiresIn
        {
            get; set;
        }

        /// <summary>
        ///  Gets or sets a value indicating whether to only set the item if the item is already in the cache.
        /// </summary>
        [JsonProperty("replace")]
        [DefaultValue(false)]
        public bool Replace
        {
            get; set;
        }

        #endregion Properties
    }
}