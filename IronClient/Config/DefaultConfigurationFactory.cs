using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronIO.Config
{
    internal class DefaultConfigurationFactory : ConfigurationFactory
    {
        public override Configuration GetConfiguartion()
        {
            return new Configuration()
            {
                Protocol = "https",
                Port = 443,
            };
        }
    }
}
