namespace IronIO.Config
{
    internal class CacheConfigFactory : ConfigurationFactory
    {
        private ConfigurationFactory configurationFactory;

        public CacheConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
        }

        public CacheConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }

        public override Configuration GetConfiguartion()
        {
            Configuration config = configurationFactory.GetConfiguartion();
            config.Host = "cache-aws-us-east-1.iron.io";
            config.ApiVersion = 1;

            return config;
        }
    }
}