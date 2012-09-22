using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject]
    public class CodeInfo
    {
        public string id { get; set; }
        public string project_id { get; set; }
        public string name { get; set; }
        public string runtime { get; set; }
        public string latest_checksum { get; set; }
        public int rev { get; set; }
        public string latest_history_id { get; set; }
        public int latest_change { get; set; }
    }
}
