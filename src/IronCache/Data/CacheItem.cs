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

    [JsonObject(MemberSerialization.OptOut)]
    internal class CacheItem<T>
    {
        #region Properties

        public string Cache
        {
            get; set;
        }

        [JsonProperty("cas")]
        public long CheckAndSet
        {
            get; set;
        }

        public string Key
        {
            get; set;
        }

        public T Value
        {
            get; set;
        }

        #endregion Properties
    }
}