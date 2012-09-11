using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronIO.Config
{
    internal class MQConfigFactory : ConfigurationFactory
    {
        private ConfigurationFactory configurationFactory;
        public MQConfigFactory() : this ( new DefaultConfigurationFactory())
        {

        }
        public MQConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }
        public override Configuration GetConfiguartion()
        {
            Configuration config = configurationFactory.GetConfiguartion();
            config.Host = "mq-aws-us-east-1.iron.io";
            config.ApiVersion = 1;

            return config;
        }
    }
}
