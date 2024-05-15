using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Orientation_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Mocking
    public partial class Orientation_Tests
    {
        public static Orientation Mock
        {
            get => new Orientation(Position_Tests.Mock, Rotation_Tests.Mock);
        }
    }
    #endregion

    #region Test Methods
    public partial class Orientation_Tests
    {
        public static IEnumerable Initializer_UseCases()
        {
            yield return new object[] { new Position(Vector3.zero, Vector3.zero), new Rotation(Quaternion.identity, Vector3.zero) };
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_CreatesNewOrientationWithSpecifiedPositionAndRotation(Position position, Rotation rotation)
        {
            var orientation = new Orientation(position, rotation);

            Assert.AreEqual(position, orientation.Position);
            Assert.AreEqual(rotation, orientation.Rotation);
        }

        public static IEnumerable ToString_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(ToString_UseCases))]
        public void ToString_ReturnsDescriptionForObject(Orientation leftHand, Orientation rightHand, Orientation head, string output)
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Place_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Place_UseCases))]
        public void Place_CreatesOrientationFromTransform(Transform transform, Vector3 offset, Orientation expectedOrientation)
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion
}