using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
