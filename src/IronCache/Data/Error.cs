//-----------------------------------------------------------------------
// <copyright file="IronWorker.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is 
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Error
    {
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}