//-----------------------------------------------------------------------
// <copyright file="EnvConfigFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Config
{
    using System;

    /// <summary>
    /// Configuration factory for creating configurations from environment variables
    /// </summary>
    internal class EnvConfigFactory : ConfigurationFactory
    {
        #region Fields

        /// <summary>
        /// wrapped configuration factory
        /// </summary>
        private ConfigurationFactory configurationFactory;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvConfigFactory" /> class.    
        /// </summary>
        public EnvConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
            // Empty constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvConfigFactory" /> class.
        /// </summary>
        /// <param name="configuarationFactory">Configuration factory to wrap</param>
        public EnvConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Constructs a Configuration.
        /// </summary>
        /// <returns>A Configuration based on environment variables</returns>
        public override Configuration GetConfiguartion()
        {
            var envConfig = this.ConfigFromEnv();
            var baseConfig = this.configurationFactory.GetConfiguartion();
            AutoMapper.Mapper.Map<Configuration, Configuration>(envConfig, baseConfig);
            return baseConfig;
        }

        /// <summary>
        /// Utility function to create a Configuration from environment variables
        /// </summary>
        /// <returns>A Configuration based on environment variables</returns>
        private Configuration ConfigFromEnv()
        {
            Configuration config = new Configuration();
            var product = "iron";

            foreach (var propertyInfo in config.GetType().GetProperties(System.Reflection.BindingFlags.SetProperty))
            {
                var key = string.Format("{0}_{1}", product, propertyInfo.Name);
                var value = System.Environment.GetEnvironmentVariable(key);
                if (!string.IsNullOrEmpty(value))
                {
                    propertyInfo.SetValue(config, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                }
            }

            return config;
        }

        #endregion Methods
    }
}