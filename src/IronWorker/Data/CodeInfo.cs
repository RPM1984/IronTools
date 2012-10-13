//-----------------------------------------------------------------------
// <copyright file="CodeInfo.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Data
{
    using Newtonsoft.Json;

    [JsonObject]
    public class CodeInfo
    {
        #region Properties

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get; set;
        }

        [JsonProperty(PropertyName = "latest_change")]
        public int LatestChange
        {
            get; set;
        }

        [JsonProperty(PropertyName = "latest_checksum")]
        public string LatestChecksum
        {
            get; set;
        }

        [JsonProperty(PropertyName = "latest_history_id")]
        public string LatestHistoryId
        {
            get; set;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get; set;
        }

        [JsonProperty(PropertyName = "project_id")]
        public string ProjectId
        {
            get; set;
        }

        [JsonProperty(PropertyName = "rev")]
        public int Revision
        {
            get; set;
        }

        [JsonProperty(PropertyName = "runtime")]
        public string Runtime
        {
            get; set;
        }

        #endregion Properties
    }
}