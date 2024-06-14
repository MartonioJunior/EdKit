using System.Collections;
using NUnit.Framework;
using MartonioJunior.EdKit;
using Pose = MartonioJunior.EdKit.Pose;
using System;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Pose_Tests
    {
        [SetUp]
        public void CreateTestContext() {}

        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods (Base Type)
    public partial class Pose_Tests
    {
        public static IEnumerable Initializer_UseCases()
        {
            Pose.ScoreFunction function = (e) => 5.0f;
            yield return new TestCaseData("Pose", function, 5.0f);
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_CreatesNewPoseWithNameAndScoringFunction(string name, Pose.ScoreFunction scoreFunction, float score)
        {
            var pose = new Pose(name, scoreFunction);
            
            Assert.AreEqual(name, pose.Name);
            Assert.AreEqual(score, pose.Evaluate(new Placement()));
        }

        public static IEnumerable Evaluate_PoseList_UseCases()
        {
            var poses = new IPose[]
            {
                new Pose("Pose1", (e) => 1.0f),
                new Pose("Pose2", (e) => 2.0f),
                new Pose("Pose3", (e) => 3.0f),
                new Pose("Pose4", (e) => 4.0f),
                new Pose("Pose5", (e) => 5.0f),
            };

            yield return new TestCaseData(new Placement(), poses, poses[4]);
        }
        [TestCaseSource(nameof(Evaluate_PoseList_UseCases))]
        public void Evaluate_PoseList_ReturnsHighestScoringPoseFromList(Placement placement, IPose[] poses, IPose expectedPose)
        {
            var pose = Pose.Evaluate(placement, poses);

            Assert.AreEqual(expectedPose, pose);
        }
    }
    #endregion

    #region Test Methods (IPose)
    public partial class Pose_Tests
    {
        public static IEnumerable Name_UseCases()
        {
            yield return new TestCaseData(new Pose("Pose1", (e) => 1.0f), "Pose1");
        }
        [TestCaseSource(nameof(Name_UseCases))]
        public void Name_ReturnsPoseName(Pose pose, string expectedName)
        {
            Assert.AreEqual(expectedName, pose.Name);
        }

        public static IEnumerable Evaluate_UseCases()
        {
            yield return new TestCaseData(new Pose("Pose1", (e) => 1.0f), new Placement(), 1.0f);
        }
        [TestCaseSource(nameof(Evaluate_UseCases))]
        public void Evaluate_ReturnsScoreForPlacement(Pose pose, Placement placement, float expectedScore)
        {
            Assert.AreEqual(expectedScore, pose.Evaluate(placement));
        }
    }
    #endregion
}