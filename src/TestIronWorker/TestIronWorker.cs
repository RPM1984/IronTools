//-----------------------------------------------------------------------
// <copyright file="TestIronWorker.cs" company="Oscar Deits">
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
    /// Tests IronWorker
    /// </summary>
    [TestClass]
    public class TestIronWorker
    {
        #region Fields

        private TestContext testContextInstance;

        #endregion Fields

        #region Constructors

        public TestIronWorker()
        {
            // TODO: Add constructor logic here
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
        /// A test for CancelSchedule
        /// </summary>
        [TestMethod]
        public void TestCancelSchedule()
        {
            string projectId = string.Empty; // TODO: Initialize to an appropriate value
            string token = string.Empty; // TODO: Initialize to an appropriate value
            IronWorker target = new IronWorker(projectId, token); // TODO: Initialize to an appropriate value
            string id = string.Empty; // TODO: Initialize to an appropriate value
            target.CancelSchedule(id);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // TODO: Add test logic here
        }

        /// <summary>
        /// A test for Schedule
        /// </summary>
        [TestMethod]
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
        /// A test for ScheduleWorker
        /// </summary>
        [TestMethod]
        public void TestScheduleWorker()
        {
            IronWorker target = new IronWorker(); // TODO: Initialize to an appropriate value
            ScheduleTask schedules = new ScheduleTask()
            {
                CodeName = "IronToolsTest",
                RunTimes = 10,
                RunEvery = 60 * 60
            }; // TODO: Initialize to an appropriate value
            int expected = 1; // TODO: Initialize to an appropriate value
            IList<string> actual;
            actual = target.ScheduleWorker(schedules);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count);
        }

        #endregion Methods

        #region Other

        #endregion Other
    }
}