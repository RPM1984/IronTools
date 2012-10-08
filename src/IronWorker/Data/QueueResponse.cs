using System.Collections.Generic;
using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject]
    internal class QueueResponse
    {
        public string msg { get; set; }

        public List<TaskInfo> tasks { get; set; }
    }
}