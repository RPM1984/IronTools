using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronIO.Config;
namespace IronIO
{
    public class IronClient
    {
        public IronClient(string name, string version, string product, string host = null, int port = 0, string projectId = null, string token = null, string protocol = null, int apiVersion = 0, string configFile = null)
        {
            AutoMapper.Mapper.CreateMap<Configuration, Configuration>().ForMember(c => c.Port, opt => opt.Condition(s => s.Port > 0))
                                                         .ForMember(c => c.ApiVersion, opt => opt.Condition(s => s.ApiVersion > 0))
                                                         .ForMember(c => c.Host, opt => opt.Condition(s => !String.IsNullOrEmpty(s.Host)))
                                                         .ForMember(c => c.ProjectId, opt => opt.Condition(s => !String.IsNullOrEmpty(s.ProjectId)))
                                                         .ForMember(c => c.Token, opt => opt.Condition(s => !String.IsNullOrEmpty(s.Token)))
                                                         .ForMember(c => c.Protocol, opt => opt.Condition(s => !String.IsNullOrEmpty(s.Protocol)));

            ConfigurationFactory configFactory = new DefaultConfigurationFactory();

            if (product == "iron_mq")
                configFactory = new MQConfigFactory(configFactory);
            if (product == "iron_worker")
                configFactory = new WorkerConfigFactory(configFactory);
            if (product == "iron_cache")
                configFactory = new CacheConfigFactory(configFactory);

            configFactory = new JsonConfigFactory(configFactory,"~/.iron.json");
            configFactory = new EnvConfigFactory(configFactory);
            configFactory = new JsonConfigFactory(configFactory, "iron.json");
            configFactory = new JsonConfigFactory(configFactory, configFile);
            configFactory = new ArgsConfigFactory(configFactory, name, version, product, host, port, projectId, token, protocol, apiVersion);

            this.Config = configFactory.GetConfiguartion();

            if (String.IsNullOrEmpty(this.Config.ProjectId))
                throw new Exception("No projectId set. projectId is a required field.");

            if (String.IsNullOrEmpty(this.Config.Token))
                throw new Exception("No token set. token is a required field.");

        }

        public Configuration Config { get; set; }
    }
}
