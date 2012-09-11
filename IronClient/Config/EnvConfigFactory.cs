using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronIO.Config
{
    public class EnvConfigFactory : ConfigurationFactory
    {

        private ConfigurationFactory configurationFactory;
        public EnvConfigFactory() : this ( new DefaultConfigurationFactory())
        {

        }
        public EnvConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }
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
                var key = String.Format("{0}_{1}",product,propertyInfo.Name);
                var value = System.Environment.GetEnvironmentVariable(key);
                if (!String.IsNullOrEmpty(value))
                {
                    propertyInfo.SetValue(config, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                }
            }
            return config;
        }
    }
}
