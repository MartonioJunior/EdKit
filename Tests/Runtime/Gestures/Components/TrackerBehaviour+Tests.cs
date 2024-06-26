using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class TrackerBehaviour_Tests
    {
        GameObject gameObject;
        TrackerBehaviour trackerBehaviour;

        [SetUp]
        public void CreateTestContext()
        {
            gameObject = new GameObject();
            trackerBehaviour = gameObject.AddComponent<TrackerBehaviour>();
        }

        [TearDown]
        public void DestroyTestContext()
        {
            GameObject.DestroyImmediate(gameObject);
            gameObject = null;
            trackerBehaviour = null;
        }
    }
    #endregion

    #region Test Methods (Base Type)
    public partial class TrackerBehaviour_Tests
    {
        public static IEnumerable GetPlacement_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(GetPlacement_UseCases))]
        public void GetPlacement_ReturnsPlacementForTransforms()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable GetPositionForTransform_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(GetPositionForTransform_UseCases))]
        public void GetPositionForTransform_ReturnsPositionForTransformBasedOnUnitVectors()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable GetRotationForTransform_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(GetRotationForTransform_UseCases))]
        public void GetRotationForTransform_ReturnsRotationForTransformBasedOnUnitVectors()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Sample_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Sample_UseCases))]
        public void Sample_InvokesOnUpdatePlacementWithCurrentTrackedPlacement()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion
}