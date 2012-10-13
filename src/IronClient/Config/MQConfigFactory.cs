//-----------------------------------------------------------------------
// <copyright file="MQConfigFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Config
{
    /// <summary>
    /// Configuration factory for creating Iron.io Message Queue client configurations
    /// </summary>
    internal class MQConfigFactory : ConfigurationFactory
    {
        /// <summary>
        /// ConfigurationFactory to wrap
        /// </summary>
        private ConfigurationFactory configurationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MQConfigFactory" /> class.
        /// </summary>
        public MQConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MQConfigFactory" /> class.
        /// </summary>
        /// <param name="configuarationFactory">ConfigurationFactory to wrap</param>
        public MQConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }

        /// <summary>
        /// Creates a Configuration for use with IronClient
        /// </summary>
        /// <returns>A configuration for use with IronClient</returns>
        public override Configuration GetConfiguartion()
        {
            Configuration config = this.configurationFactory.GetConfiguartion();
            config.Host = "mq-aws-us-east-1.iron.io";
            config.ApiVersion = 1;

            return config;
        }
    }
}