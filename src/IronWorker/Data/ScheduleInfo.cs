using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject]
    public class ScheduleInfo
    {
        public string id { get; set; }

        public string created_at { get; set; }

        public string updated_at { get; set; }

        public string project_id { get; set; }

        public string msg { get; set; }

        public string status { get; set; }

        public string code_name { get; set; }

        public string start_at { get; set; }

        public string end_at { get; set; }

        public string next_start { get; set; }

        public string last_run_time { get; set; }

        public int run_times { get; set; }

        public int run_count { get; set; }
    }
}