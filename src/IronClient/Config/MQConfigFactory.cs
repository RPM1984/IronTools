//-----------------------------------------------------------------------
// <copyright file="MQConfigFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronIO.Config
{
    internal class MQConfigFactory : ConfigurationFactory
    {
        private ConfigurationFactory configurationFactory;

        public MQConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
        }

        public MQConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }

        public override Configuration GetConfiguartion()
        {
            Configuration config = this.configurationFactory.GetConfiguartion();
            config.Host = "mq-aws-us-east-1.iron.io";
            config.ApiVersion = 1;

            return config;
        }
    }
}