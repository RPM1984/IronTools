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

    public class IronMQ
    {
        #region Fields

        private static readonly string QueueCore = "queues";

        private IronClient client;
        private string name;
        private JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.None, DefaultValueHandling = DefaultValueHandling.Ignore };

        #endregion Fields

        #region Constructors

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
        /// <param name="projectId"></param>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public static IList<string> Queues(string projectId = null, string token = null,int page = 0, int perPage = 30)
        {
            var client = new IronClient("IronWorker .NET", "0.1", "iron_mq", projectId: projectId, token: token);
            var url = string.Format("{0}?page={1}&per_page={2}",QueueCore,page,perPage);

            var json = client.Get(url);
            var template = new[] { new { id = string.Empty, name = string.Empty, projectId = string.Empty } };
            var queues = JsonConvert.DeserializeAnonymousType(json, template);
            return queues.Select(q => q.name).ToList();
        }

        /// <summary>
        /// Clears a Queue regardless of message status
        /// </summary>
        /// <exception cref="System.Web.HttpException">Thown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Clear()
        {
            string emptyJsonObject = "{}";
            var url = string.Join("/", QueueCore, name, "clear");

            var response = client.Post(url, emptyJsonObject);
            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(response, settings);
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
            var url = string.Format("{0}/{1}", QueueCore, name);
            var json = client.Get(url);
            var template = new { size = 0 };
            var queue = JsonConvert.DeserializeAnonymousType(json, template);
            return queue.size;
        }

        /// <summary>
        /// Delete a message from the queue
        /// </summary>
        /// <param name="id">Message Identifier</param>
        /// <exception cref="System.Web.HttpException">Thown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Delete(string id)
        {
            client.Delete("queues/" + name + "/messages/" + id);
        }

        /// <summary>
        /// Delete a message from the queue
        /// </summary>
        /// <param name="msg">Message to be deleted</param>
        /// <exception cref="System.Web.HttpException">Thown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Delete(Message msg)
        {
            Delete(msg.Id);
        }

        /// <summary>
        /// Retrieves a Message from the queue. If there are no items on the queue, an HTTPException is thrown.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Web.HttpException">Thown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public Message Get()
        {
            var url = String.Join("/", QueueCore, name, "messages");
            string json = client.Get(url);
            var queueResp = JsonConvert.DeserializeObject<Dictionary<string, Message[]>>(json, settings);
            if (queueResp.ContainsKey("messages"))
                return queueResp["messages"][0];
            return null;
        }

        /// <summary>
        /// Retrieves up to "max" messages from the queue
        /// </summary>
        /// <param name="max">the count of messages to return, default is 1</param>
        /// <returns>An IList of messages</returns>
        /// <exception cref="System.Web.HttpException">Thown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public IList<Message> Get(int max = 1)
        {
            string json = client.Get(string.Format("queues/{0}/messages?n={1}", name, max));
            var queueResp = JsonConvert.DeserializeObject<Dictionary<string, Message[]>>(json, settings);
            if (queueResp.ContainsKey("messages"))
                return queueResp["messages"];
            return new List<Message>();
        }

        /// <summary>
        /// Pushes a message onto the queue with a timeout
        /// </summary>
        /// <param name="msg">Message to be pushed.</param>
        /// <param name="timeout">The timeout of the message to push.</param>
        /// <exception cref="System.Web.HttpException">Thown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Push(string msg, long timeout = 0)
        {
            Push(new string[] { msg }, timeout);
        }

        /// <summary>
        /// Pushes messages onto the queue with an optional timeout
        /// </summary>
        /// <param name="msg">Messages to be pushed.</param>
        /// <param name="timeout">The timeout of the messages to push.</param>
        /// <exception cref="System.Web.HttpException">Thown if the IronMQ service returns a status other than 200 OK. </exception>
        /// <exception cref="System.IO.IOException">Thrown if there is an error accessing the IronMQ server.</exception>
        public void Push(IEnumerable<string> msgs, long timeout = 0, long delay = 0, long expires_in = 0)
        {
            var json = JsonConvert.SerializeObject(new Dictionary<string, Message[]>()
            {
                {"messages" , msgs.Select(msg => new Message() { Body = msg, Timeout = timeout, Delay = delay, ExpiresIn = expires_in }).ToArray()}
            }, settings);
            client.Post("queues/" + name + "/messages", json
               );
        }

        #endregion Methods
    }
}