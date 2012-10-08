using Newtonsoft.Json;

namespace IronIO.Data
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Error
    {
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}