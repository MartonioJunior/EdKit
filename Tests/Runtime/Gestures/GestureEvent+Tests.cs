using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class GestureEvent_Tests
    {
        [SetUp]
        public override void CreateTestContext()
        {
            
        }

        [TearDown]
        public override void DestroyTestContext()
        {
            
        }
    }
    #endregion

    #region Test Methods
    public partial class GestureEvent_Tests
    {
        public static IEnumerable PoseEvents_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(PoseEvents_UseCases))]
        public void PoseEvents_ReturnsListOfPoseEventsInGesture()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Gesture_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Gesture_UseCases))]
        public void Gesture_ReturnsGestureIdentified()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Timestamp_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Timestamp_UseCases))]
        public void Timestamp_ReturnsLevelTimeWhereEventWasDetected()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Initializer_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_CreatesNewGestureEventFromPoseEventsGestureAndTimestamp()
        {
            Assert.Ignore(NotImplemented);
        }
    }
    #endregion
}