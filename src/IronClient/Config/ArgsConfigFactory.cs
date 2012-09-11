﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronIO.Config
{
    internal class ArgsConfigFactory : ConfigurationFactory
    {
        private ConfigurationFactory configFactory;
        private string name;
        private string version;
        private string product;
        private string host;
        private string projectId;
        private string token;
        private string protocol;
        private int apiVersion;
        private int port;

        public ArgsConfigFactory(string name, string version, string product, string host, int port, string projectId, string token, string protocol, int apiVersion): this(new DefaultConfigurationFactory(), name, version,product,host, port,projectId,token,protocol,apiVersion)
        { }
        public ArgsConfigFactory(ConfigurationFactory configFactory, string name, string version, string product, string host, int port, string projectId, string token, string protocol, int apiVersion)
        {
            // TODO: Complete member initialization
            this.configFactory = configFactory;
            this.name = name;
            this.version = version;
            this.product = product;
            this.host = host;
            this.projectId = projectId;
            this.token = token;
            this.protocol = protocol;
            this.apiVersion = apiVersion;
            this.port = port;
        }
        public override Configuration GetConfiguartion()
        {
            Configuration baseConfig =  configFactory.GetConfiguartion();
            Configuration argsConfig = new Configuration()
            {
                ApiVersion = this.apiVersion,
                ClientVersion = this.version,
                Host = this.host,
                Port = this.port,
                Protocol = this.protocol,
                ProjectId = this.projectId,
                Token = this.token,
                Name = this.name
            };
            AutoMapper.Mapper.Map<Configuration, Configuration>(argsConfig, baseConfig);
            return baseConfig;
        }
    }
}