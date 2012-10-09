using System.ComponentModel;
using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject]
    public class Task
    {
        [JsonProperty("code_name")]
        public string CodeName { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }

        [DefaultValue(0)]
        [JsonProperty("priority")]
        public int Priority { get; set; }

        [DefaultValue(3600)]
        [JsonProperty("timeout")]
        public int Timout { get; set; }

        [DefaultValue(0)]
        public int Delay { get; set; }
    }
}