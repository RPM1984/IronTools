//-----------------------------------------------------------------------
// <copyright file="EnvConfigFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Config
{
    using System;

    internal class EnvConfigFactory : ConfigurationFactory
    {
        #region Fields

        private ConfigurationFactory configurationFactory;

        #endregion Fields

        #region Constructors

        public EnvConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
        }

        public EnvConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }

        #endregion Constructors

        #region Methods

        public override Configuration GetConfiguartion()
        {
            var envConfig = ConfigFromEnv();
            var baseConfig = configurationFactory.GetConfiguartion();
            AutoMapper.Mapper.Map<Configuration, Configuration>(envConfig, baseConfig);
            return baseConfig;
        }

        private Configuration ConfigFromEnv()
        {
            Configuration config = new Configuration();
            var product = "iron";

            foreach (var propertyInfo in config.GetType().GetProperties(System.Reflection.BindingFlags.SetProperty))
            {
                var key = String.Format("{0}_{1}", product, propertyInfo.Name);
                var value = System.Environment.GetEnvironmentVariable(key);
                if (!String.IsNullOrEmpty(value))
                {
                    propertyInfo.SetValue(config, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                }
            }
            return config;
        }

        #endregion Methods
    }
}