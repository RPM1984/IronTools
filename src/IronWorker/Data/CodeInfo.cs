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
    [JsonObject]
    public class CodeInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "latest_change")]
        public int LatestChange { get; set; }

        [JsonProperty(PropertyName = "latest_checksum")]
        public string LatestChecksum { get; set; }

        [JsonProperty(PropertyName = "latest_history_id")]
        public string LatestHistoryId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "project_id")]
        public string ProjectId { get; set; }

        [JsonProperty(PropertyName = "rev")]
        public int Revision { get; set; }

        [JsonProperty(PropertyName = "runtime")]
        public string Runtime { get; set; }
    }
}