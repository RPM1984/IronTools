//-----------------------------------------------------------------------
// <copyright file="CodeInfo.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Code information class
    /// </summary>
    [JsonObject]
    public class CodeInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the code identifier
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the latest change value
        /// </summary>
        [JsonProperty(PropertyName = "latest_change")]
        public int LatestChange
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the most recent checksum
        /// </summary>
        [JsonProperty(PropertyName = "latest_checksum")]
        public string LatestChecksum
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the latest history identifier
        /// </summary>
        [JsonProperty(PropertyName = "latest_history_id")]
        public string LatestHistoryId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the code
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the project identifier
        /// </summary>
        [JsonProperty(PropertyName = "project_id")]
        public string ProjectId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the revision number
        /// </summary>
        [JsonProperty(PropertyName = "rev")]
        public int Revision
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the runtime of the code
        /// </summary>
        [JsonProperty(PropertyName = "runtime")]
        public string Runtime
        {
            get; set;
        }

        #endregion Properties
    }
}