using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

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
