using System.Collections.Generic;
using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject]
    internal class QueueResponse
    {
        [JsonProperty(PropertyName="msg")]
        public string Message { get; set; }

        [JsonProperty(PropertyName="tasks")]
        public List<TaskInfo> Tasks { get; set; }
    }
}