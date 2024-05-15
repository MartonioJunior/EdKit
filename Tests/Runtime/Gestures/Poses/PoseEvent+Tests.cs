using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;
using Pose = MartonioJunior.EdKit.Pose;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class PoseEvent_Tests
    {
        [SetUp]
        public void CreateTestContext() {}

        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods
    public partial class PoseEvent_Tests
    {
        public static IEnumerable Placement_UseCases()
        {
            yield return new object[] { new Placement() };
        }
        [TestCaseSource(nameof(Placement_UseCases))]
        public void Placement_ReturnsActorPlacement(Placement placement)
        {
            var poseEvent = new PoseEvent(placement, null, 0.0f);

            Assert.AreEqual(placement, poseEvent.Placement);
        }

        public static IEnumerable Pose_UseCases()
        {
            yield return new object[] { new Pose("Pose 1", p => 1.0f) };
        }
        [TestCaseSource(nameof(Pose_UseCases))]
        public void Pose_ReturnsPoseIdentified(IPose pose)
        {
            var poseEvent = new PoseEvent(new Placement(), pose, 0.0f);

            Assert.AreEqual(pose, poseEvent.Pose);
        }

        public static IEnumerable Timestamp_UseCases()
        {
            yield return new object[] { 4.3f };
        }
        [TestCaseSource(nameof(Timestamp_UseCases))]
        public void Timestamp_ReturnsLevelTimeWhenEventHappened(float timestamp)
        {
            var poseEvent = new PoseEvent(new Placement(), null, timestamp);

            Assert.AreEqual(timestamp, poseEvent.Timestamp);
        }

        public static IEnumerable Score_UseCases()
        {
            yield return new object[] { new Pose("Pose 1", p => 1.0f), new Placement() };
        }
        [TestCaseSource(nameof(Score_UseCases))]
        public void Score_ReturnsScoreObtainedFromPoseAtPlacement(IPose pose, Placement placement)
        {
            var poseEvent = new PoseEvent(placement, pose, 0.0f);

            Assert.AreEqual(pose.Evaluate(placement), poseEvent.Score);
        }

        public static IEnumerable Initializer_UseCases()
        {
            yield return new object[] { new Placement(), new Pose("Pose 1", p => 1.0f), 4.3f };
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_ReturnsNewPoseEvent(Placement placement, IPose pose, float timestamp)
        {
            var poseEvent = new PoseEvent(placement, pose, timestamp);

            Assert.AreEqual(placement, poseEvent.Placement);
            Assert.AreEqual(pose, poseEvent.Pose);
            Assert.AreEqual(timestamp, poseEvent.Timestamp);
        }

        public static IEnumerable From_UseCases()
        {
            yield return new object[] { new Placement(), 4.3f, new IPose[] { new Pose("Pose 1", p => 1.0f), new Pose("Pose 2", p => 0.5f) }, new PoseEvent(new Placement(), new Pose("Pose 1", p => 1.0f), 4.3f) };
            yield return new object[] { new Placement(), 4.3f, new IPose[] { new Pose("Pose 1", p => 0.25f), new Pose("Pose 2", p => 2.0f) }, new PoseEvent(new Placement(), new Pose("Pose 2", p => 2.0f), 6.7f) };
            yield return new object[] { new Placement(), 4.3f, new IPose[] { new Pose("Pose 1", p => -1.25f), new Pose("Pose 2", p => -2.2f) }, null };
        }
        [TestCaseSource(nameof(From_UseCases))]
        public void From_EvaluatesPosesToCreatesNewPoseEventWithHighestScoringPose(Placement placement, float timestamp, IList<IPose> posesToCheck, PoseEvent? expected)
        {
            var poseEvent = PoseEvent.From(placement, timestamp, posesToCheck);

            Assert.AreEqual(expected, poseEvent);
        }
    }
    #endregion
}