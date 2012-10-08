using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronIO;
using System;
using IronIO.Data;
using System.Collections.Generic;

namespace TestIronWorker
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestIronWorker
    {
        public TestIronWorker()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion Additional test attributes

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }

        /// <summary>
        ///A test for ScheduleWorker
        ///</summary>
        [TestMethod()]
        public void TestScheduleWorker()
        {
            IronWorker target = new IronWorker(); // TODO: Initialize to an appropriate value
            ScheduleTask schedules = new ScheduleTask()
            {
                code_name="IronToolsTest",
                run_times = 10,
                run_every = 60 * 60
            }; // TODO: Initialize to an appropriate value
            int expected = 1; // TODO: Initialize to an appropriate value
            IList<string> actual;
            actual = target.ScheduleWorker(schedules);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count);
        }

        /// <summary>
        ///A test for Schedule
        ///</summary>
        [TestMethod()]
        public void TestSchedule()
        {
            string projectId = string.Empty; // TODO: Initialize to an appropriate value
            string token = string.Empty; // TODO: Initialize to an appropriate value
            IronWorker target = new IronWorker(projectId, token); // TODO: Initialize to an appropriate value
            string id = string.Empty; // TODO: Initialize to an appropriate value
            ScheduleTask expected = null; // TODO: Initialize to an appropriate value
            ScheduleTask actual;
            actual = target.Schedule(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CancelSchedule
        ///</summary>
        [TestMethod()]
        public void TestCancelSchedule()
        {
            string projectId = string.Empty; // TODO: Initialize to an appropriate value
            string token = string.Empty; // TODO: Initialize to an appropriate value
            IronWorker target = new IronWorker(projectId, token); // TODO: Initialize to an appropriate value
            string id = string.Empty; // TODO: Initialize to an appropriate value
            target.CancelSchedule(id);
        }
    }
}