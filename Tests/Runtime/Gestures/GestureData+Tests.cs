using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class GestureData_Tests
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
    public partial class GestureData_Tests
    {
        public static IEnumerable Poses_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Poses_UseCases))]
        public void Poses_ReturnsSequenceOfPoseDataObjects()
        {
            Assert.Ignore(NotImplemented);
        }
    }
    #endregion

    #region Test Methods (IGesture)
    public partial class GestureData_Tests
    {
        public static IEnumerable Name_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Name_UseCases))]
        public void Name_ReturnsNameForGesture()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Evaluate_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Evaluate_UseCases))]
        public void Evaluate_ReturnsScoreFromListOfPoseEvents()
        {
            Assert.Ignore(NotImplemented);
        }
    }
    #endregion
}