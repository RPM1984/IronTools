//-----------------------------------------------------------------------
// <copyright file="IronMQ.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using IronIO.Data;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Iron.io Message Queue API
    /// </summary>
    public class IronMQ
    {
        #region Fields

        /// <summary>
        /// Iron.io Message Queue API prefix
        /// </summary>
        private static readonly string QueueCore = "queues";

        /// <summary>
        /// Reference to the Iron.io client interface
        /// </summary>
        private IronClient client;

        /// <summary>
        /// Queue name
        /// </summary>
        private string name;

        /// <summary>
        /// JSON serialization settings
        /// </summary>
        private JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None, DefaultValueHandling = DefaultValueHandling.Ignore };

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IronMQ" /> class.
        /// </summary>
        /// <param name="name">Queue name</param>
        /// <param name="projectId">Project identifier available from the HUD</param>
        /// <param name="token">Token available from the HUD</param>
        public IronMQ(string name, string projectId = null, string token = null)
        {
            this.client = new IronClient("IronWorker .NET", "0.1", "iron_mq", projectId: projectId, token: token);
            this.name = name;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get a list of all queues in a project. By default, 30 queues are listed at a time. To see more, use the page parameter or the per_page parameter. Up to 100 queues may be listed on a single page.
        /// </summary>
        /// <param name="projectId">Project identifier available from the HUD</param>
        /// <param name="token">Token available from the HUD</param>
        /// <param name="page">Page index (zero based)</param>
        /// <param name="perPage">Number of results per page</param>
        /// <returns>An IList of queue names</returns>
        public static IList<string> Queues(string projectId = null, string token = null, int page = 0, int perPage = 30)
        {
            var client = new IronClient("IronWorker .NET", "0.1", "iron_mq", projectId: projectId, token: token);
            var url = string.Format("{0}?page={1}&per_page={2}", QueueCore, page, perPage);

            var json = client.Get(url);
            var template = new[] { new { id = string.Empty, name = string.Empty, projectId = string.Empty } };
            var queues = JsonConvert.DeserializeAnonymousType(json, template);
            return queues.Select(q => q.name).ToList();
        }

        /// <summary>
        /// Clears a Queue regardless of message status
        /// </summary>
        /// <exception cref="System.Web.HttpException">Thrown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Clear()
        {
            string emptyJsonObject = "{}";
            var url = string.Join("/", QueueCore, this.name, "clear");

            var response = this.client.Post(url, emptyJsonObject);
            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(response, this.settings);
            if (responseObject["msg"] != "Cleared")
            {
                throw new Exception(string.Format("Unknown response from REST Endpoint : {0}", response));
            }
        }

        /// <summary>
        /// Gets the size of the queue
        /// </summary>
        /// <returns>Number of messages still in the queue</returns>
        public int Count()
        {
            var url = string.Format("{0}/{1}", QueueCore, this.name);
            var json = this.client.Get(url);
            var template = new { size = 0 };
            var queue = JsonConvert.DeserializeAnonymousType(json, template);
            return queue.size;
        }

        /// <summary>
        /// Delete a message from the queue
        /// </summary>
        /// <param name="id">Message Identifier</param>
        /// <exception cref="System.Web.HttpException">Thrown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Delete(string id)
        {
            this.client.Delete("queues/" + this.name + "/messages/" + id);
        }

        /// <summary>
        /// Delete a message from the queue
        /// </summary>
        /// <param name="msg">Message to be deleted</param>
        /// <exception cref="System.Web.HttpException">Thrown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Delete(Message msg)
        {
            this.Delete(msg.Id);
        }

        /// <summary>
        /// Retrieves a Message from the queue. If there are no items on the queue, an HTTPException is thrown.
        /// </summary>
        /// <returns>A single message</returns>
        /// <exception cref="System.Web.HttpException">Thrown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public Message Get()
        {
            var url = string.Join("/", QueueCore, this.name, "messages");
            string json = this.client.Get(url);
            var queueResp = JsonConvert.DeserializeObject<Dictionary<string, Message[]>>(json, this.settings);
            return queueResp.ContainsKey("messages") ? queueResp["messages"][0] : null;
        }

        /// <summary>
        /// Retrieves up to "max" messages from the queue
        /// </summary>
        /// <param name="max">the count of messages to return, default is 1</param>
        /// <returns>An IList of messages</returns>
        /// <exception cref="System.Web.HttpException">Thrown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public IList<Message> Get(int max = 1)
        {
            string json = this.client.Get(string.Format("queues/{0}/messages?n={1}", this.name, max));
            var queueResp = JsonConvert.DeserializeObject<Dictionary<string, Message[]>>(json, this.settings);

            return queueResp.ContainsKey("messages") ? queueResp["messages"] : new Message[0];
        }

        /// <summary>
        /// Pushes a message onto the queue with a timeout
        /// </summary>
        /// <param name="msg">Message to be pushed.</param>
        /// <param name="timeout">The timeout of the message to push.</param>
        /// <exception cref="System.Web.HttpException">Thrown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Push(string msg, long timeout = 0)
        {
            this.Push(new string[] { msg }, timeout);
        }

        /// <summary>
        /// Pushes messages onto the queue with an optional timeout
        /// </summary>
        /// <param name="msgs">Messages to be pushed</param>
        /// <param name="timeout">The timeout of the messages to push.</param>
        /// <param name="delay">Delay in seconds</param>
        /// <param name="expires_in">Expires in seconds</param>
        /// <exception cref="System.Web.HttpException">Thrown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Push(IEnumerable<string> msgs, long timeout = 0, long delay = 0, long expires_in = 0)
        {
            var json = JsonConvert.SerializeObject(
                new Dictionary<string, Message[]>()
                {
                    { "messages", msgs.Select(msg => new Message() { Body = msg, Timeout = timeout, Delay = delay, ExpiresIn = expires_in }).ToArray() }
                },
                this.settings);

            this.client.Post("queues/" + this.name + "/messages", json);
        }

        #endregion Methods
    }
}