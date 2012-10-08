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