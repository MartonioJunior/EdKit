using System.Collections;
using NUnit.Framework;
using UnityEngine;
using MartonioJunior.EdKit;
using Pose = MartonioJunior.EdKit.Pose;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class PoseData_Tests
    {
        PoseData poseData;

        [SetUp]
        public void CreateTestContext()
        {
            poseData = ScriptableObject.CreateInstance<PoseData>();
        }

        [TearDown]
        public void DestroyTestContext()
        {
            Object.DestroyImmediate(poseData);
            poseData = null;
        }
    }
    #endregion

    #region Test Methods (Base Type)
    public partial class PoseData_Tests
    {
        public static IEnumerable Placement_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Placement_UseCases))]
        public void Placement_ReturnsDefinedPlacement(Placement input, Placement expected)
        {
            poseData.Placement = input;

            Assert.AreEqual(expected, poseData.Placement);
        }

        public static IEnumerable Pose_UseCases()
        {
            yield return new TestCaseData("Pose 1", new Placement(), new Pose("Pose 1", p => 1.0f));
        }
        [TestCaseSource(nameof(Pose_UseCases))]
        public void Pose_CreatesPoseUsingPlacement(string name, Placement placement, Pose expected)
        {
            poseData.name = name;
            poseData.Placement = placement;

            var newPose = poseData.Pose;

            Assert.AreEqual(expected.Name, newPose.Name);
            Assert.AreEqual(expected.Evaluate(new Placement()), newPose.Evaluate(new Placement()));
        }
    }
    #endregion

    #region Test Methods (IPose)
    public partial class PoseData_Tests
    {
        public static IEnumerable Name_UseCases()
        {
            yield return new TestCaseData("Pose 1");
        }
        [TestCaseSource(nameof(Name_UseCases))]
        public void Name_ReturnsDefinedName(string name)
        {
            poseData.name = name;

            Assert.AreEqual(name, poseData.Name);
        }

        public static IEnumerable Evaluate_UseCases()
        {
            yield return new TestCaseData(new Placement(), 1.0f);
        }
        [TestCaseSource(nameof(Evaluate_UseCases))]
        public void Evaluate_ReturnsPoseEvaluation(Placement placement, float expected)
        {
            poseData.Placement = new Placement();

            Assert.AreEqual(expected, poseData.Evaluate(placement));
        }
    }
    #endregion
}