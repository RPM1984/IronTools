using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using IronIO.Config;
using Newtonsoft.Json;
namespace IronIO
{
    public class IronClient
    {
        public IronClient(string name, string version, string product, string host = null, int port = 0, string projectId = null, string token = null, string protocol = null, int apiVersion = 0, string configFile = null)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty");

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

            BuildHeaders();
        }

        public Configuration Config { get; set; }

        private NameValueCollection Headers { get; set; }
        private string baseUrl;
        private string BaseUrl
        {
            get
            {
                return baseUrl ?? ( baseUrl = String.Format("{0}://{1}:{2}/{3}/projects/{4}/", this.Config.Protocol, this.Config.Host,
                    this.Config.Port, this.Config.ApiVersion,this.Config.ProjectId));
            }
        }
        private void BuildHeaders()
        {
            this.Headers = new NameValueCollection();
            this.Headers["Accept"] = "application/json";
            this.Headers["User-Agent"] = String.Format("{0} (version: {1})",this.Config.Name,this.Config.ClientVersion);
            this.Headers["Authorization"] = String.Format("OAuth {0}", this.Config.Token);
        }
        public string Request(string url, string method, string body = "", NameValueCollection headers = null, bool retry = true)
        {
            if (headers != null)
                foreach (var key in this.Headers.Keys.OfType<string>().Except( headers.Keys.OfType<string>()))
                    headers.Add(key, this.Headers[key]);
            else
                headers = this.Headers;


            url = this.baseUrl + url;
            WebRequest request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Headers.Add(headers);
            request.Method = method;


            if (body != null)
            {
                using (System.IO.StreamWriter write = new System.IO.StreamWriter(request.GetRequestStream()))
                {
                    write.Write(body);
                    write.Flush();
                }
            }
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                if (we.Response != null)
                    response = (HttpWebResponse)we.Response;
                else
                    throw we;
            }
            string json = string.Empty;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                json = reader.ReadToEnd();
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Error error = JsonConvert.DeserializeObject<Error>(json);
                throw new System.Web.HttpException((int)response.StatusCode, error.Message);
            }
            return json;

        }

        public string Delete(string url, NameValueCollection headers = null, bool retry = true)
        {
            return Request(url, "DELETE", headers: headers, retry: retry);
        }

        public string Get(string url, NameValueCollection headers = null, bool retry = true)
        {
            return Request(url, "GET", headers: headers, retry: retry);
        }

        public string Post(string url, string body = "", NameValueCollection headers = null, bool retry = true)
        {
            return Request( url, "POST", body:body,headers:Headers,retry:retry);
        }
        public string Put(string url, string body = "", NameValueCollection headers = null, bool retry = true)
        {
            return Request(url, "Put", body: body, headers: Headers, retry: retry);
        }

    }
}
