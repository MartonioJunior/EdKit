using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class AffordanceBehaviour_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods
    public partial class AffordanceBehaviour_Tests
    {
        public static IEnumerable Apply_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Apply_UseCases))]
        public void Apply_InvokesEventsInAffordanceBehaviour()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion
}