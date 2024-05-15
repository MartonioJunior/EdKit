using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class PoseData_Tests
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
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Pose_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Pose_UseCases))]
        public void Pose_CreatesPoseUsingPlacement(PoseData pose, string name, Placement expectedPlacement)
        {
            Assert.Ignore(NotImplemented);
        }
    }
    #endregion
}