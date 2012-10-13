//-----------------------------------------------------------------------
// <copyright file="Error.cs" company="Oscar Deits">
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
    /// Cache error 
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