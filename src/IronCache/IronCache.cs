//-----------------------------------------------------------------------
// <copyright file="IronCache.cs" company="Oscar Deits">
// Usage of the works is permitted provided that this instrument is
// retained with the works, so that any entity that uses the works is
// notified of this instrument.
// DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace IronIO
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using IronIO.Data;

    using Newtonsoft.Json;

    /// <summary>
    /// IronCache API
    /// </summary>
    public class IronCache
    {
        #region Fields

        /// <summary>
        /// Iron.io API product name
        /// </summary>
        private static readonly string CacheCore = "caches";

        /// <summary>
        /// Reference to Iron.io API client interface
        /// </summary>
        private IronClient client;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IronCache" /> class.
        /// </summary>
        /// <param name="projectId">Project Id obtained from the HUD</param>
        /// <param name="token">Token obtained from the HUD</param>
        public IronCache(string projectId = null, string token = null)
        {
            this.client = new IronClient("IronCache .NET", "0.2", "iron_cache", projectId: projectId, token: token);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///  Only set the item if the item is not already in the cache. If the item is in the cache, do not overwrite it.
        /// </summary>
        /// <typeparam name="T">Type of the object to add to the cache</typeparam>
        /// <param name="cache">Name of the cache</param>
        /// <param name="key">The key to store the item under in the cache</param>
        /// <param name="value">Item to be stored</param>
        /// <param name="expiresIn"> How long in seconds to keep the item in the cache before it is deleted. Default is 604,800 seconds (7 days). Maximum is 2,592,000 seconds (30 days).</param>
        public void Add<T>(string cache, string key, T value, int expiresIn = 0)
        {
            this.Put<T>(cache, key, value, true, false, expiresIn);
        }

        /// <summary>
        /// Get a list of caches available.
        /// </summary>
        /// <returns>Caches available</returns>
        public IList<Cache> Caches()
        {
            var response = this.client.Get(CacheCore);
            var caches = JsonConvert.DeserializeObject<List<Cache>>(response) ?? new List<Cache>();
            return caches as IList<Cache>;
        }

        /// <summary>
        /// Internal Get method
        /// </summary>
        /// <typeparam name="T">Type of object to get from the cache</typeparam>
        /// <param name="cache"> The name of the cache the item belongs to.</param>
        /// <param name="key">The key the item is stored under in the cache.</param>
        /// <param name="defaultValue">Value to return if not found</param>
        /// <returns>Value at the key or the default value if not found</returns>
        public T Get<T>(string cache, string key, T defaultValue = default(T))
        {
            string endpoint = string.Format("{0}/{1}/items/{2}", CacheCore, cache, HttpUtility.UrlPathEncode(key));
            try
            {
                var response = this.client.Get(endpoint);
                var item = JsonConvert.DeserializeObject<CacheItem<T>>(response);
                return item.Value;
            }
            catch (System.Web.HttpException we)
            {
                if (we.GetHttpCode() == 404)
                {
                    return defaultValue;
                }

                throw;
            }
        }

        /// <summary>
        /// Increment an existing integer
        /// </summary>
        /// <param name="cache">Cache Name</param>
        /// <param name="key">Key to lookup</param>
        /// <param name="amount">amount to increment (decrement) value at the specified key</param>
        /// <returns>value after increment</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">No key to increment</exception>
        /// <exception cref="System.InvalidOperationException">Bad Request: Invalid JSON (can't be parsed or has wrong types).</exception>
        public int Increment(string cache, string key, int amount)
        {
            string endpoint = string.Format("{0}/{1}/items/{2}/increment", CacheCore, cache, HttpUtility.UrlPathEncode(key));

            var body = "{" + string.Format("\"amount\": {0}", amount) + "}";
            try
            {
                var response = this.client.Post(endpoint, body);

                var increment = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                return int.Parse(increment["value"]);
            }
            catch (System.Web.HttpException e)
            {
                if (e.GetHttpCode() == 404)
                {
                    throw new KeyNotFoundException(e.Message);
                }

                if (e.GetHttpCode() == 400)
                {
                    throw new InvalidOperationException(e.Message);
                }

                throw;
            }
        }

        /// <summary>
        /// Puts a value into the cache for the provided key.
        /// </summary>
        /// <typeparam name="T">Type of the object to put in the cache</typeparam>
        /// <param name="cache">Name of the cache</param>
        /// <param name="key">The key to store the item under in the cache</param>
        /// <param name="value">Item to be stored</param>
        /// <param name="add">If set to true, only set the item if the item is not already in the cache. If the item is in the cache, do not overwrite it.</param>
        /// <param name="replace">If set to true, only set the item if the item is already in the cache. If the item is not in the cache, do not create it.</param>
        /// <param name="expiresIn"> How long in seconds to keep the item in the cache before it is deleted. Default is 604,800 seconds (7 days). Maximum is 2,592,000 seconds (30 days).</param>
        public void Put<T>(string cache, string key, T value, bool add = false, bool replace = false, int? expiresIn = null)
        {
            if (add && replace) 
            { 
                throw new ArgumentException("add and replace cannot both be true."); 
            }

            string endpoint = string.Format("{0}/{1}/items/{2}", CacheCore, cache, HttpUtility.UrlPathEncode(key));
            var item = new Item<T>() { Body = value, Add = add, Replace = replace, ExpiresIn = expiresIn };
            var body = JsonConvert.SerializeObject(item);
            var response = this.client.Put(endpoint, body);
        }

        /// <summary>
        /// Removes <paramref name="key"/> from <paramref name="cache"/>
        /// </summary>
        /// <param name="cache">Name of the cache</param>
        /// <param name="key">Key to be removed</param>
        public void Remove(string cache, string key)
        {
            string endpoint = string.Format("{0}/{1}/items/{2}", CacheCore, cache, HttpUtility.UrlPathEncode(key));
            try
            {
                var response = this.client.Delete(endpoint);
            }
            catch (System.Web.HttpException e)
            {
                if (e.GetHttpCode() == 404)
                {
                    return; // throw new KeyNotFoundException(e.Message);
                }

                if (e.GetHttpCode() == 400)
                {
                    throw new InvalidOperationException(e.Message);
                }

                throw;
            }
        }

        /// <summary>
        /// Only set the item if the item is already in the cache. If the item is not in the cache, do not create it.
        /// </summary>
        /// <typeparam name="T">Type of the object to be replaced</typeparam>
        /// <param name="cache">The cache name</param>
        /// <param name="key">The key to store the item under in the cache</param>
        /// <param name="value">Item to be stored</param>
        /// <param name="expiresIn"> How long in seconds to keep the item in the cache before it is deleted. Default is 604,800 seconds (7 days). Maximum is 2,592,000 seconds (30 days).</param>
        public void Replace<T>(string cache, string key, T value, int expiresIn = 0)
        {
            this.Put<T>(cache, key, value, false, true, expiresIn);
        }

        /// <summary>
        /// Sets the value at the key in the cache
        /// </summary>
        /// <typeparam name="T">Type of the object to be set</typeparam>
        /// <param name="cache">Name of the cache</param>
        /// <param name="key">The key to store the item under in the cache</param>
        /// <param name="value">Item to be stored</param>
        /// <param name="expiresIn"> How long in seconds to keep the item in the cache before it is deleted. Default is 604,800 seconds (7 days). Maximum is 2,592,000 seconds (30 days).</param>
        public void Set<T>(string cache, string key, T value, int expiresIn = 0)
        {
            this.Put<T>(cache, key, value, false, false, expiresIn);
        }

        #endregion Methods
    }
}