//-----------------------------------------------------------------------
// <copyright file="IronWorkerTest.cs" company="Oscar Deits">
//     Usage of the works is permitted provided that this instrument is retained with the works, so that any enity that uses the works is notified of this instrument. DISCLAIMER: THE WORKS ARE WITHOUT WARRANTY.
// </copyright>
//-----------------------------------------------------------------------

namespace TestIronWorker
{
    using System;
    using System.Collections.Generic;

    using IronIO;
    using IronIO.Data;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for IronWorkerTest and is intended
    /// to contain all IronWorkerTest Unit Tests
    /// </summary>
    [TestClass]
    public class IronWorkerTest
    {
        #region Fields

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
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// A test for Log
        /// </summary>
        [TestMethod]
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
        /// A test for Tasks
        /// </summary>
        [TestMethod]
        public void TasksTest()
        {
            IronWorker target = new IronWorker();
            int page = 0; // TODO: Initialize to an appropriate value
            int per_page = 10; // TODO: Initialize to an appropriate value
            StatusEnum statusFilter = StatusEnum.All; // TODO: Initialize to an appropriate value
            Nullable<DateTime> from_time = null; // TODO: Initialize to an appropriate value
            Nullable<DateTime> to_time = null; // TODO: Initialize to an appropriate value
            IList<Task> actual;
            actual = target.Tasks(page, per_page, statusFilter, from_time, to_time);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// A test for Task
        /// </summary>
        [TestMethod]
        public void TaskTest()
        {
            IronWorker target = new IronWorker();
            var tasks = target.Tasks(per_page: 1);
            Assert.AreEqual(1, tasks.Count);

            string id = tasks[0].Id;
            Task actual;
            actual = target.Task(id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(id, actual.Id);
        }

        #endregion Methods

        #region Other

        #endregion Other
    }
}