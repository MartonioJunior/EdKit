using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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
            yield return new TestCaseData(new Placement());
        }
        [TestCaseSource(nameof(Placement_UseCases))]
        public void Placement_ReturnsActorPlacement(Placement placement)
        {
            var poseEvent = PoseEvent.New(placement, null, 0.0f);

            Assert.AreEqual(placement, poseEvent?.Placement);
        }

        public static IEnumerable Pose_UseCases()
        {
            yield return new TestCaseData(new Pose("Pose 1", p => 1.0f));
        }
        [TestCaseSource(nameof(Pose_UseCases))]
        public void Pose_ReturnsPoseIdentified(IPose pose)
        {
            var poseEvent = PoseEvent.New(new Placement(), pose, 0.0f);

            Assert.AreEqual(pose, poseEvent?.Pose);
        }

        public static IEnumerable Timestamp_UseCases()
        {
            yield return new TestCaseData(4.3f);
        }
        [TestCaseSource(nameof(Timestamp_UseCases))]
        public void Timestamp_ReturnsLevelTimeWhenEventHappened(float timestamp)
        {
            var poseEvent = PoseEvent.New(new Placement(), null, timestamp);

            Assert.AreEqual(timestamp, poseEvent?.Timestamp);
        }

        public static IEnumerable Score_UseCases()
        {
            yield return new TestCaseData(new Pose("Pose 1", p => 1.0f), new Placement(), 1.0f);
            yield return new TestCaseData(null, new Placement(), 0.0f);
        }
        [TestCaseSource(nameof(Score_UseCases))]
        public void Score_ReturnsScoreObtainedFromPoseAtPlacement(IPose pose, Placement placement, float expectedScore)
        {
            var poseEvent = PoseEvent.New(placement, pose, 0.0f);

            Assert.AreEqual(expectedScore, poseEvent?.Score);
        }

        public static IEnumerable Initializer_UseCases()
        {
            yield return new TestCaseData(new Placement(), new Pose("Pose 1", p => 1.0f), 4.3f);
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_ReturnsNewPoseEvent(Placement placement, IPose pose, float timestamp)
        {
            var poseEvent = PoseEvent.New(placement, pose, timestamp);

            Assert.AreEqual(placement, poseEvent?.Placement);
            Assert.AreEqual(pose, poseEvent?.Pose);
            Assert.AreEqual(timestamp, poseEvent?.Timestamp);
        }

        public static IEnumerable From_UseCases()
        {
            var poseA = new Pose("Pose 1", p => 1.0f);
            var poseB = new Pose("Pose 2", p => 0.5f);
            var poseC = new Pose("Pose 3", p => 0.25f);
            var poseD = new Pose("Pose 4", p => 2.0f);
            var poseE = new Pose("Pose 5", p => -1.25f);
            var poseF = new Pose("Pose 6", p => 0.0f);

            yield return new TestCaseData(new Placement(), 4.3f, new IPose[] { poseA, poseB }, PoseEvent.New(new Placement(), poseA, 4.3f));
            yield return new TestCaseData(new Placement(), 6.7f, new IPose[] { poseC, poseD }, PoseEvent.New(new Placement(), poseD, 6.7f));
            yield return new TestCaseData(new Placement(), 9.8f, new IPose[] { poseE, poseF }, null);
            yield return new TestCaseData(new Placement(), -2.0f, new IPose[] { poseA, poseB }, null);
        }
        [TestCaseSource(nameof(From_UseCases))]
        public void From_EvaluatesPosesToCreatesNewPoseEventWithHighestScoringPose(Placement placement, float timestamp, IList<IPose> posesToCheck, PoseEvent? expected)
        {
            var poseEvent = PoseEvent.From(placement, timestamp, posesToCheck);

            if (expected is PoseEvent expectedEvent) {
                Assert.AreEqual(expectedEvent.Placement, poseEvent?.Placement);
                Assert.AreEqual(expectedEvent.Pose, poseEvent?.Pose);
                Assert.AreEqual(expectedEvent.Timestamp, poseEvent?.Timestamp);
            } else {
                Assert.IsNull(poseEvent);
            }
        }

        public static IEnumerable ToString_UseCases()
        {
            var pose = new Pose("Pose 1", p => 1.5f);
            var placement = new Placement();
            yield return new TestCaseData(PoseEvent.New(placement, pose, 4.3f), $"PoseEvent: {pose}({placement}) - 1.5 - 4.3");
        }
        [TestCaseSource(nameof(ToString_UseCases))]
        public void ToString_CreatesNewStringWithEventDetails(PoseEvent poseEvent, string expected)
        {
            var result = poseEvent.ToString();

            Assert.AreEqual(expected, result);
        }
    }
    #endregion
}