using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronIO.Config
{
    internal class WorkerConfigFactory : ConfigurationFactory
    {
        private ConfigurationFactory configurationFactory;
        public WorkerConfigFactory()
            : this(new DefaultConfigurationFactory())
        {

        }
        public WorkerConfigFactory(ConfigurationFactory configuarationFactory)
        {
            this.configurationFactory = configuarationFactory;
        }
        public override Configuration GetConfiguartion()
        {
            Configuration config = configurationFactory.GetConfiguartion();
            config.Host = "worker-aws-us-east-1.iron.io";
            config.ApiVersion = 2;

            return config;
        }
    }
}
