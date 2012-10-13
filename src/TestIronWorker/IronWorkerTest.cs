using System;
using System.Collections.Generic;
using IronIO;
using IronIO.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestIronWorker
{
    /// <summary>
    ///This is a test class for IronWorkerTest and is intended
    ///to contain all IronWorkerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IronWorkerTest
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
        ///A test for Tasks
        ///</summary>
        [TestMethod()]
        public void TasksTest()
        {
            IronWorker target = new IronWorker();
            int page = 0; // TODO: Initialize to an appropriate value
            int per_page = 0; // TODO: Initialize to an appropriate value
            StatusEnum statusFilter = StatusEnum.All; // TODO: Initialize to an appropriate value
            Nullable<DateTime> from_time = null; // TODO: Initialize to an appropriate value
            Nullable<DateTime> to_time = null; // TODO: Initialize to an appropriate value
            IList<Task> actual;
            actual = target.Tasks(page, per_page, statusFilter, from_time, to_time);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Log
        ///</summary>
        [TestMethod()]
        public void LogTest()
        {
            IronWorker target = new IronWorker();
            string id = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Log(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Task
        ///</summary>
        [TestMethod()]
        public void TaskTest()
        {
            IronWorker target = new IronWorker();
            var tasks = target.Tasks(per_page:1);
            Assert.AreEqual(1, tasks.Count);

            string id = tasks[0].Id;
            Task actual;
            actual = target.Task(id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(id, actual.Id);
        }
    }
}