using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class PoseEvent_Tests
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
    public partial class PoseEvent_Tests
    {
        public static IEnumerable Placement_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Placement_UseCases))]
        public void Placement_ReturnsActorPlacement()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Pose_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Pose_UseCases))]
        public void Pose_ReturnsPoseIdentified()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Timestamp_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Timestamp_UseCases))]
        public void Timestamp_ReturnsLevelTimeWhenEventHappened()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Score_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Score_UseCases))]
        public void Score_ReturnsScoreObtainedFromPoseAtPlacement()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Initializer_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_ReturnsNewPoseEvent()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable From_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(From_UseCases))]
        public void From_EvaluatesPosesToCreatesNewPoseEventWithHighestScoringPose()
        {
            Assert.Ignore(NotImplemented);
        }
    }
    #endregion
}