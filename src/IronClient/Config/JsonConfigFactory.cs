using System;
using System.IO;
using Newtonsoft.Json;

namespace IronIO.Config
{
    internal class JsonConfigFactory : ConfigurationFactory
    {
        private ConfigurationFactory configurationFactory;
        private string filePath;

        public JsonConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
        }

        public JsonConfigFactory(ConfigurationFactory configurationFactory)
            : this(configurationFactory, "~/.iron.json")
        {
        }

        public JsonConfigFactory(string filePath)
            : this(new DefaultConfigurationFactory(), filePath)
        {
        }

        public JsonConfigFactory(ConfigurationFactory configurationFactory, string filePath)
        {
            this.configurationFactory = configurationFactory;
            this.filePath = filePath;
        }

        public override Configuration GetConfiguartion()
        {
            var fileConfig = ConfigFromFile(filePath);
            var baseConfig = configurationFactory.GetConfiguartion();

            AutoMapper.Mapper.Map<Configuration, Configuration>(fileConfig, baseConfig);
            return baseConfig;
        }

        private Configuration ConfigFromFile(string path)
        {
            if (String.IsNullOrEmpty(path)) return new Configuration();

            path = Path.GetFullPath(path);
            if (!File.Exists(path)) return new Configuration();

            string json = File.ReadAllText(path);
            Configuration config = JsonConvert.DeserializeObject<Configuration>(json);
            return config;
        }
    }
}