//-----------------------------------------------------------------------
// <copyright file="TestIronCache.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------
namespace IronCacheTests
{
    using System.Collections.Generic;
    using System.Configuration;

    using IronIO;
    using IronIO.Data;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for IronCacheTest and is intended
    /// to contain all IronCacheTest Unit Tests
    /// </summary>
    [TestClass]
    public class TestIronCache
    {
        #region Fields

        /// <summary>
        /// Reference to the test context
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Project identifier and token
        /// </summary>
        private string projectId, token;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestIronCache" /> class.    
        /// </summary>
        public TestIronCache()
        {
            this.projectId = ConfigurationManager.AppSettings["IRONIO_PROJECT_ID"];
            this.token = ConfigurationManager.AppSettings["IRONIO_TOKEN"];
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Test Add / Get integer
        /// </summary>
        [TestMethod]
        public void AddGetIntTest()
        {
            string projectId = this.projectId;
            string token = this.token;
            IronCache target = new IronCache(projectId, token);

            int value = 10;
            string key = "this is an arbitrary key";
            string cache = "test_cache";

            target.Put(cache, key, value);
            var actual = target.Get<int?>(cache, key);

            Assert.IsNotNull(actual);
            Assert.AreEqual(value, actual);
        }

        /// <summary>
        /// Test Add / Get string
        /// </summary>
        [TestMethod]
        public void AddGetTest()
        {
            string projectId = this.projectId;
            string token = this.token;
            IronCache target = new IronCache(projectId, token);

            string value = "this is some arbitrary text";
            string key = "this is an arbitrary key";
            string cache = "test_cache";

            target.Put(cache, key, value);
            var actual = target.Get<string>(cache, key);

            Assert.IsNotNull(actual);
            Assert.AreEqual(value, actual);
        }

        /// <summary>
        /// A test for Caches
        /// </summary>
        [TestMethod]
        public void CachesTest()
        {
            string projectId = this.projectId;
            string token = this.token;
            IronCache target = new IronCache(projectId, token);
            var expected = 1;
            IList<Cache> actual;
            actual = target.Caches();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count);
        }

        /// <summary>
        /// Test Get on missing key
        /// </summary>
        [TestMethod]
        public void GetMissingValueTest()
        {
            string projectId = this.projectId;
            string token = this.token;
            IronCache target = new IronCache(projectId, token);

            string key = "this is an arbitrary key";
            string cache = "test_cache";

            target.Remove(cache, key);
            var actual = target.Get<string>(cache, key);
            Assert.IsNull(actual);
        }

        /// <summary>
        /// Test increment on integer
        /// </summary>
        [TestMethod]
        public void IncrementExistingIntegerTest()
        {
            string projectId = this.projectId;
            string token = this.token;
            IronCache target = new IronCache(projectId, token);

            string key = "cf435dc2-7f12-4f37-94c2-26077b3cd414"; // random unique identifier
            string cache = "test_cache";

            // target.Remove(cache, key);
            var expected = 1;
            target.Put(cache, key, 0, false, false, 0);
            var actual = target.Increment(cache, key, 1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test incrementing a non existing key
        /// </summary>
        [TestMethod]
        public void IncrementNonExistingTest()
        {
            string projectId = this.projectId;
            string token = this.token;
            IronCache target = new IronCache(projectId, token);

            string key = "82de17a0-cab9-45a5-a851-bccb210a9e1f";
            string cache = "test_cache";
            target.Remove(cache, key);
            try
            {
                var actual = target.Increment(cache, key, 1);
                Assert.Fail();
            }
            catch (KeyNotFoundException)
            {
            }
        }

        /// <summary>
        /// A test for IronCache Constructor
        /// </summary>
        [TestMethod]
        public void IronCacheConstructorTest()
        {
            string projectId = this.projectId;
            string token = this.token;
            IronCache target = new IronCache(projectId, token);

            Assert.IsNotNull(target);
        }

        #endregion Methods
    }
}