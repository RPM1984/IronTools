//-----------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Config
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    /// <summary>
    /// Configuration for use with IronClient
    /// </summary>
    [JsonObject]
    public class Configuration
    {
        #region Properties

        /// <summary>
        /// Gets or sets Iron.io API version
        /// </summary>
        [DefaultValue(1)]
        [JsonProperty("api_version")]
        public int ApiVersion
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the version of the client
        /// </summary>
        [JsonProperty("version")]
        public string ClientVersion
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the host name or address of the Iron.io API endpoint
        /// </summary>
        [JsonProperty("host")]
        public string Host
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the client
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the port on which to connect to the Iron.io API endpoint
        /// </summary>
        [DefaultValue(443)]
        [JsonProperty("port")]
        public int Port
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the project identifier available from the HUD
        /// </summary>
        [JsonProperty("project_id")]
        public string ProjectId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the transport protocol to use
        /// </summary>
        [DefaultValue("https")]
        [JsonProperty("protocol")]
        public string Protocol
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the token available from the HUD
        /// </summary>
        [JsonProperty("token")]
        public string Token
        {
            get; set;
        }

        #endregion Properties
    }
}