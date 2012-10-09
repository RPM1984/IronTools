using System;
using Newtonsoft.Json;
using System.ComponentModel;

namespace IronIO.Data
{
    [Serializable]
    [JsonObject]
    public class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [DefaultValue(0)]
        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [DefaultValue(0)]
        [JsonProperty("delay")]
        public long Delay { get; set; }

        [DefaultValue(0)] 
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}
