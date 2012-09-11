using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronIO.Config
{
    internal abstract class ConfigurationFactory
    {
        public abstract Configuration GetConfiguartion();
    }
}
