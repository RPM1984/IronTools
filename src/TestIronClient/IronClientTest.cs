//-----------------------------------------------------------------------
// <copyright file="IronClientTest.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace TestIronClient
{
    using IronIO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for IronClientTest and is intended
    /// to contain all IronClientTest Unit Tests
    /// </summary>
    [TestClass]
    public class IronClientTest
    {
        #region Fields

        /// <summary>
        /// Current test context
        /// </summary>
        private TestContext testContextInstance;

        #endregion Fields

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
        /// A test for IronClient Constructor
        /// </summary>
        [TestMethod]
        [DeploymentItem("iron.json")]
        public void IronClientConstructorTest()
        {
            string name = "Iron Tools Client"; // TODO: Initialize to an appropriate value
            string version = "0.1"; // TODO: Initialize to an appropriate value
            string product = string.Empty; // TODO: Initialize to an appropriate value
            string host = string.Empty; // TODO: Initialize to an appropriate value
            int port = 0; // TODO: Initialize to an appropriate value
            string projectId = string.Empty; // TODO: Initialize to an appropriate value
            string token = string.Empty; // TODO: Initialize to an appropriate value
            string protocol = string.Empty; // TODO: Initialize to an appropriate value
            int apiVersion = 0; // TODO: Initialize to an appropriate value
            string configFile = string.Empty; // TODO: Initialize to an appropriate value
            IronClient target = new IronClient(name, version, product, host, port, projectId, token, protocol, apiVersion, configFile);
            Assert.IsNotNull(target);
        }

        #endregion Methods
    }
}