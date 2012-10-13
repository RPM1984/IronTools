//-----------------------------------------------------------------------
// <copyright file="CacheConfigFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Config
{
    /// <summary>
    /// Configuration factory for creating default configurations for Iron.io cache
    /// </summary>
    internal class CacheConfigFactory : ConfigurationFactory
    {
        /// <summary>
        /// Configuration factory to wrap.
        /// </summary>
        private ConfigurationFactory configurationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheConfigFactory" /> class.
        /// </summary>
        public CacheConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheConfigFactory" /> class.
        /// </summary>
        /// <param name="configuarationFactory">Configuration factory to wrap</param>
        public CacheConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }

        /// <summary>
        /// Creates a default Configuration for Iron.io cache
        /// </summary>
        /// <returns>Configuration for use with Iron.io cache</returns>
        public override Configuration GetConfiguartion()
        {
            Configuration config = this.configurationFactory.GetConfiguartion();
            config.Host = "cache-aws-us-east-1.iron.io";
            config.ApiVersion = 1;

            return config;
        }
    }
}