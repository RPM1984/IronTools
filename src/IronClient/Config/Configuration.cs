//-----------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Config
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    [JsonObject]
    public class Configuration
    {
        #region Properties

        [DefaultValue(1)]
        [JsonProperty("api_version")]
        public int ApiVersion
        {
            get; set;
        }

        [JsonProperty("version")]
        public string ClientVersion
        {
            get; set;
        }

        [JsonProperty("host")]
        public string Host
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        [DefaultValue(443)]
        [JsonProperty("port")]
        public int Port
        {
            get; set;
        }

        [JsonProperty("project_id")]
        public string ProjectId
        {
            get; set;
        }

        [DefaultValue("https")]
        [JsonProperty("protocol")]
        public string Protocol
        {
            get; set;
        }

        [JsonProperty("token")]
        public string Token
        {
            get; set;
        }

        #endregion Properties
    }
}