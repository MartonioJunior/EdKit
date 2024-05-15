using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class SessionBehaviour_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods (Base Type)
    public partial class SessionBehaviour_Tests
    {
        public static IEnumerable OpenSession_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(OpenSession_UseCases))]
        public void OpenSession_CreatesNewSessionWithUserAndSceneIDs()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable ClearSession_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(ClearSession_UseCases))]
        public void ClearSession_RemovesSessionObject()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable CloseSession_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(CloseSession_UseCases))]
        public void CloseSession_EndsSessionWithSpecifiedOutcome()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion

    #region Test Methods (IAnalyticsModel)
    public partial class SessionBehaviour_Tests
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
        public void RegisterAny_AddsAnyObjectToSession()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion
}