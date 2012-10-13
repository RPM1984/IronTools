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

    /// <summary>
    /// Configuration factory for creating configurations from a JSON file
    /// </summary>
    internal class JsonConfigFactory : ConfigurationFactory
    {
        #region Fields
        /// <summary>
        /// wrapped configuration factory
        /// </summary>
        private ConfigurationFactory configurationFactory;

        /// <summary>
        /// JSON file path
        /// </summary>
        private string filePath;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfigFactory" /> class.    
        /// </summary>
        public JsonConfigFactory()
            : this(new DefaultConfigurationFactory())
        {
            // Empty constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfigFactory" /> class.    
        /// </summary>
        /// <param name="configurationFactory">Configuration factory to wrap</param>
        public JsonConfigFactory(ConfigurationFactory configurationFactory)
            : this(configurationFactory, "~/.iron.json")
        {
            // Empty constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfigFactory" /> class.    
        /// </summary>
        /// <param name="filePath">Path to a JSON file</param>
        public JsonConfigFactory(string filePath)
            : this(new DefaultConfigurationFactory(), filePath)
        {
            // Empty constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfigFactory" /> class.    
        /// </summary>
        /// <param name="configurationFactory">Configuration factory to wrap</param>
        /// <param name="filePath">Path to a JSON file</param>
        public JsonConfigFactory(ConfigurationFactory configurationFactory, string filePath)
        {
            this.configurationFactory = configurationFactory;
            this.filePath = filePath;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Constructs a Configuration
        /// </summary>
        /// <returns>A Configuration based on a JSON file</returns>
        public override Configuration GetConfiguartion()
        {
            var fileConfig = this.ConfigFromFile(this.filePath);
            var baseConfig = this.configurationFactory.GetConfiguartion();

            AutoMapper.Mapper.Map<Configuration, Configuration>(fileConfig, baseConfig);
            return baseConfig;
        }

        /// <summary>
        /// Utility function to create a Configuration from a JSON file
        /// </summary>
        /// <param name="path">JSON file path</param>
        /// <returns>A Configuration based on a JSON file</returns>
        private Configuration ConfigFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new Configuration();
            }

            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                return new Configuration();
            }

            string json = File.ReadAllText(path);
            Configuration config = JsonConvert.DeserializeObject<Configuration>(json);
            return config;
        }

        #endregion Methods
    }
}