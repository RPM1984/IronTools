//-----------------------------------------------------------------------
// <copyright file="WorkerConfigFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Config
{
    /// <summary>
    /// ConfigurationFactory for creating configurations for use with Iron.io Worker
    /// </summary>
    internal class WorkerConfigFactory : ConfigurationFactory
    {
        /// <summary>
        /// ConfigurationFactory to wrap
        /// </summary>
        private ConfigurationFactory configurationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerConfigFactory" /> class.
        /// </summary>
        public WorkerConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerConfigFactory" /> class.
        /// </summary>
        /// <param name="configuarationFactory">ConfigurationFactory to wrap</param>
        public WorkerConfigFactory(ConfigurationFactory configuarationFactory)
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
            config.Host = "worker-aws-us-east-1.iron.io";
            config.ApiVersion = 2;

            return config;
        }
    }
}