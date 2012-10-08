using System.ComponentModel;
using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject]
    public class Task
    {
        public string code_name { get; set; }

        public string payload { get; set; }

        [DefaultValue(0)]
        public int priority { get; set; }

        [DefaultValue(3600)]
        public int timeout { get; set; }

        [DefaultValue(0)]
        public int delay { get; set; }
    }
}