using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Gesture_Tests
    {
        [SetUp]
        public void CreateTestContext()
        {
            
        }

        [TearDown]
        public void DestroyTestContext()
        {
            
        }
    }
    #endregion

    #region Test Methods (Base Type)
    public partial class Gesture_Tests
    {
        public static IEnumerable Initialize_Func_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Initialize_Func_UseCases))]
        public void Initialize_Func_InstancesGestureFromScoringFunction()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Initializer_List_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Initializer_List_UseCases))]
        public void Initializer_List_InstancesGestureFromListOfPoseEventScorers()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion

    #region Test Methods (IGesture)
    public partial class Gesture_Tests
    {
        public static IEnumerable Name_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Name_UseCases))]
        public void Name_ReturnsNameOfGesture()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Evaluate_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Evaluate_UseCases))]
        public void Evaluate_ReturnsScoreFromListOfPoseEvents()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion
}