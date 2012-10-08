using IronIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestIronClient
{
    /// <summary>
    ///This is a test class for IronClientTest and is intended
    ///to contain all IronClientTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IronClientTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion Additional test attributes

        /// <summary>
        ///A test for IronClient Constructor
        ///</summary>
        [TestMethod()]
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
    }
}