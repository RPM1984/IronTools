using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronIO.Config
{
    abstract class ConfigurationFactory
    {
        public virtual Configuration GetConfiguartion();
    }
}
