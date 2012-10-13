//-----------------------------------------------------------------------
// <copyright file="Error.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an Error returned by Iron.io API
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Error
    {
        #region Properties

        /// <summary>
        /// Gets or sets the message from Iron.io
        /// </summary>
        [JsonProperty("msg")]
        public string Message
        {
            get; set;
        }

        #endregion Properties
    }
}