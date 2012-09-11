using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace IronIO.Config
{
    [JsonObject]
    public class Configuration
    {
        public string Name { get; set; }
        
        [JsonProperty("host")]
        public string Host { get; set; }
        
        [DefaultValue("https")]
        [JsonProperty("protocol")]
        public string Protocol { get; set; }
        
        [DefaultValue(443)]
        [JsonProperty("port")]
        public int Port { get; set; }
        
        [DefaultValue(1)]
        [JsonProperty("api_version")]
        public int ApiVersion { get; set; }
        
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("version")]
        public string ClientVersion { get; set; }
    }
}
