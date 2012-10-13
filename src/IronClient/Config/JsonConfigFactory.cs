//-----------------------------------------------------------------------
// <copyright file="JsonConfigFactory.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO.Config
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    internal class JsonConfigFactory : ConfigurationFactory
    {
        #region Fields

        private ConfigurationFactory configurationFactory;
        private string filePath;

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Methods

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

        #endregion Methods
    }
}