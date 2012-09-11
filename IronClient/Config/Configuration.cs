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
        public string Host { get; set; }
        [DefaultValue("https")]
        public string Protocol { get; set; }
        [DefaultValue(443)]
        public int Port { get; set; }
        [DefaultValue(1)]
        public int ApiVersion { get; set; }
        public string ProjectId { get; set; }
        public string Token { get; set; }

        public string ClientVersion { get; set; }
    }
}
