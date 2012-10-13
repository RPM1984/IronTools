//-----------------------------------------------------------------------
// <copyright file="ArgsConfigFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Config
{
    /// <summary>
    /// Configuration factory for building Configurations from passed in arguments
    /// </summary>
    internal class ArgsConfigFactory : ConfigurationFactory
    {
        /// <summary>
        /// Configuration to wrap
        /// </summary>
        private ConfigurationFactory configFactory;

        /// <summary>
        /// Name of the client
        /// </summary>
        private string name;

        /// <summary>
        /// Version of the client
        /// </summary>
        private string version;

        /// <summary>
        /// Iron.io product name
        /// </summary>
        private string product;

        /// <summary>
        /// Host name
        /// </summary>
        private string host;

        /// <summary>
        /// Project identifier available from the HUD
        /// </summary>
        private string projectId;

        /// <summary>
        /// Token available from the HUD
        /// </summary>
        private string token;

        /// <summary>
        /// Transport protocol
        /// </summary>
        private string protocol;

        /// <summary>
        /// Version of the Iron.io API to use
        /// </summary>
        private int apiVersion;

        /// <summary>
        /// Port number
        /// </summary>
        private int port;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgsConfigFactory" /> class.    
        /// </summary>
        /// <param name="name">Name of the client</param>
        /// <param name="version">Version of the client</param>
        /// <param name="product">Iron.io product name</param>
        /// <param name="host">Host name</param>
        /// <param name="port">Port number</param>
        /// <param name="projectId">Project identifier available from the HUD</param>
        /// <param name="token">Token available from the HUD</param>
        /// <param name="protocol">Transport protocol</param>
        /// <param name="apiVersion">Iron.io API version</param>
        public ArgsConfigFactory(string name, string version, string product, string host, int port, string projectId, string token, string protocol, int apiVersion)
            : this(new DefaultConfigurationFactory(), name, version, product, host, port, projectId, token, protocol, apiVersion)
        {
            // Empty constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgsConfigFactory" /> class.    
        /// </summary>
        /// <param name="configFactory">Configuration factory to wrap</param>
        /// <param name="name">Name of the client</param>
        /// <param name="version">Version of the client</param>
        /// <param name="product">Iron.io product name</param>
        /// <param name="host">Host name</param>
        /// <param name="port">Port number</param>
        /// <param name="projectId">Project identifier available from the HUD</param>
        /// <param name="token">Token available from the HUD</param>
        /// <param name="protocol">Transport protocol</param>
        /// <param name="apiVersion">Iron.io API version</param>
        public ArgsConfigFactory(ConfigurationFactory configFactory, string name, string version, string product, string host, int port, string projectId, string token, string protocol, int apiVersion)
        {
            // TODO: Complete member initialization
            this.configFactory = configFactory;
            this.name = name;
            this.version = version;
            this.product = product;
            this.host = host;
            this.projectId = projectId;
            this.token = token;
            this.protocol = protocol;
            this.apiVersion = apiVersion;
            this.port = port;
        }

        /// <summary>
        /// Creates a new Configuration from passed in arguments
        /// </summary>
        /// <returns>Argument based Configuration</returns>
        public override Configuration GetConfiguartion()
        {
            Configuration baseConfig = this.configFactory.GetConfiguartion();
            Configuration argsConfig = new Configuration()
            {
                ApiVersion = this.apiVersion,
                ClientVersion = this.version,
                Host = this.host,
                Port = this.port,
                Protocol = this.protocol,
                ProjectId = this.projectId,
                Token = this.token,
                Name = this.name
            };
            AutoMapper.Mapper.Map<Configuration, Configuration>(argsConfig, baseConfig);
            return baseConfig;
        }
    }
}