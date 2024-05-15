using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Session_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods (Base Type)
    public partial class Session_Tests
    {
        public static IEnumerable Initializer_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_CreatesNewSessionWithUserAndSceneIDs()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable SetOutcome_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(SetOutcome_UseCases))]
        public void SetOutcome_DefinesOutcomeForSession()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion

    #region Test Methods (IAnalyticsModel)
    public partial class Session_Tests
    {
        public static IEnumerable Register_GestureEvent_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Register_GestureEvent_UseCases))]
        public void Register_GestureEvent_AddsNewGestureEvent()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Register_PoseEvent_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Register_PoseEvent_UseCases))]
        public void Register_PoseEvent_AddsNewPoseEvent()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable RegisterAny_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(RegisterAny_UseCases))]
        public void RegisterAny_AddsNewEventOfAnyType()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion
}