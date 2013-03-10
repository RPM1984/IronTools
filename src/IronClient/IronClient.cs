//-----------------------------------------------------------------------
// <copyright file="IronClient.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;

    using IronIO.Config;

    using Newtonsoft.Json;

    /// <summary>
    /// Iron.io client interface
    /// </summary>
    public class IronClient
    {
        #region Fields

        /// <summary>
        /// base url from which to build requests
        /// </summary>
        private string baseUrl;

        /// <summary>
        /// Random number generator used for exponential back off
        /// </summary>
        private Random rng;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IronClient" /> class.
        /// </summary>
        /// <param name="name">Client name</param>
        /// <param name="version">Version of the client</param>
        /// <param name="product">Iron.io product</param>
        /// <param name="host">hostname to use</param>
        /// <param name="port">port number</param>
        /// <param name="projectId">Project identifier available from the HUD</param>
        /// <param name="token">Token available from the HUD</param>
        /// <param name="protocol">Protocol e.g. http https</param>
        /// <param name="apiVersion">Version of the API to use (1,2)</param>
        /// <param name="configFile">Path to a specific JSON configuration file to load</param>
        public IronClient(string name, string version, string product, string host = null, int port = 0, string projectId = null, string token = null, string protocol = null, int apiVersion = 0, string configFile = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            AutoMapper.Mapper.CreateMap<Configuration, Configuration>().ForMember(c => c.Port, opt => opt.Condition(s => s.Port > 0))
                                                         .ForMember(c => c.ApiVersion, opt => opt.Condition(s => s.ApiVersion > 0))
                                                         .ForMember(c => c.Host, opt => opt.Condition(s => !string.IsNullOrEmpty(s.Host)))
                                                         .ForMember(c => c.ProjectId, opt => opt.Condition(s => !string.IsNullOrEmpty(s.ProjectId)))
                                                         .ForMember(c => c.Token, opt => opt.Condition(s => !string.IsNullOrEmpty(s.Token)))
                                                         .ForMember(c => c.Protocol, opt => opt.Condition(s => !string.IsNullOrEmpty(s.Protocol)));

            ConfigurationFactory configFactory = new DefaultConfigurationFactory();

            if (product == "iron_mq")
            {
                configFactory = new MQConfigFactory(configFactory);
            }

            if (product == "iron_worker")
            {
                configFactory = new WorkerConfigFactory(configFactory);
            }

            if (product == "iron_cache")
            {
                configFactory = new CacheConfigFactory(configFactory);
            }

            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                   Environment.OSVersion.Platform == PlatformID.MacOSX)
            ? Environment.GetEnvironmentVariable("HOME")
            : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            configFactory = new JsonConfigFactory(configFactory, System.IO.Path.Combine(homePath, ".iron.json"));
            configFactory = new EnvConfigFactory(configFactory);
            configFactory = new JsonConfigFactory(configFactory, "iron.json");
            configFactory = new JsonConfigFactory(configFactory, configFile);
            configFactory = new ArgsConfigFactory(configFactory, name, version, product, host, port, projectId, token, protocol, apiVersion);

            this.Config = configFactory.GetConfiguartion();

            if (string.IsNullOrEmpty(this.Config.ProjectId))
            {
                throw new Exception("No projectId set. projectId is a required field.");
            }

            if (string.IsNullOrEmpty(this.Config.Token))
            {
                throw new Exception("No token set. token is a required field.");
            }

            this.BuildHeaders();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the current configuration
        /// </summary>
        public Configuration Config
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current base url
        /// </summary>
        private string BaseUrl
        {
            get
            {
                return this.baseUrl ?? (this.baseUrl = string.Format(
                        "{0}://{1}:{2}/{3}/projects/{4}/",
                        this.Config.Protocol,
                        this.Config.Host,
                    this.Config.Port,
                    this.Config.ApiVersion,
                    this.Config.ProjectId));
            }
        }
        
        /// <summary>
        /// Gets or sets the http headers
        /// </summary>
        private NameValueCollection Headers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the random number generator
        /// </summary>
        private Random Rng
        {
            get { return this.rng ?? (this.rng = new Random()); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Issues a DELETE request
        /// </summary>
        /// <param name="url">Url to issues the request to</param>
        /// <param name="body">Data to send</param>
        /// <param name="headers">Additional header information</param>
        /// <param name="retry">Retry if unsuccessful</param>
        /// <returns>JSON response</returns>
        public string Delete(string url, string body = "", NameValueCollection headers = null, bool retry = true)
        {
            return this.Request(url, "DELETE", body: body, headers: headers, retry: retry);
        }

        /// <summary>
        /// Issues a GET request
        /// </summary>
        /// <param name="url">Url to issues the request to</param>
        /// <param name="headers">Additional header information</param>
        /// <param name="retry">Retry if unsuccessful</param>
        /// <returns>JSON response</returns>
        public string Get(string url, NameValueCollection headers = null, bool retry = true)
        {
            return this.Request(url, "GET", headers: headers, retry: retry);
        }

        /// <summary>
        /// Issues a POST request
        /// </summary>
        /// <param name="url">Url to issues the request to</param>
        /// <param name="body">Data to send</param>
        /// <param name="headers">Additional header information</param>
        /// <param name="retry">Retry if unsuccessful</param>
        /// <returns>JSON response</returns>
        public string Post(string url, string body = "", NameValueCollection headers = null, bool retry = true)
        {
            return this.Request(url, "POST", body: body, headers: headers, retry: retry);
        }

        /// <summary>
        /// Issues a PUT request
        /// </summary>
        /// <param name="url">Url to issues the request to</param>
        /// <param name="body">Data to send</param>
        /// <param name="headers">Additional header information</param>
        /// <param name="retry">Retry if unsuccessful</param>
        /// <returns>JSON response</returns>
        public string Put(string url, string body = "", NameValueCollection headers = null, bool retry = true)
        {
            return this.Request(url, "PUT", body: body, headers: headers, retry: retry);
        }

        /// <summary>
        /// Issues a generic request
        /// </summary>
        /// <param name="url">Url to issues the request to</param>
        /// <param name="method">Http METHOD to use</param>
        /// <param name="body">Data to send</param>
        /// <param name="headers">Additional header information</param>
        /// <param name="retry">Retry if unsuccessful</param>
        /// <returns>JSON response</returns>
        public string Request(string url, string method, string body = null, NameValueCollection headers = null, bool retry = true)
        {
            if (headers != null)
            {
                foreach (var key in this.Headers.Keys.OfType<string>().Except(headers.Keys.OfType<string>()))
                {
                    headers.Add(key, this.Headers[key]);
                }
            }
            else
            {
                headers = this.Headers;
            }

            url = this.BaseUrl + url;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.UserAgent = string.Format("{0} (version: {1})", this.Config.Name, this.Config.ClientVersion);
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
            int tries = 0, maxTries = 5;
            do
            {
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException we)
                {
                    if (we.Response != null)
                    {
                        response = (HttpWebResponse)we.Response;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            while (response.StatusCode == HttpStatusCode.ServiceUnavailable
                && retry
                && tries++ < maxTries
                && this.ExponentialBackoff(this.Rng, tries));

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

        /// <summary>
        /// Internal utility function to build headers
        /// </summary>
        private void BuildHeaders()
        {
            this.Headers = new NameValueCollection();
            this.Headers["Authorization"] = string.Format("OAuth {0}", this.Config.Token);
        }

        /// <summary>
        /// Implements exponential back off source: <seealso cref="http://aws.amazon.com/articles/1394"/>
        /// </summary>
        /// <param name="rng">Random number generator to use</param>
        /// <param name="tries">Current try count</param>
        /// <returns>Always true</returns>
        private bool ExponentialBackoff(Random rng, int tries)
        {
            // source: http://aws.amazon.com/articles/1394
            var timespan = rng.Next(Convert.ToInt32(Math.Pow(4, tries) * 100));
            System.Threading.Thread.Sleep(timespan);
            return true;
        }

        #endregion Methods
    }
}