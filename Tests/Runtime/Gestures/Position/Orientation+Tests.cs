using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;
using UnityEngine.TestTools.Utils;

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
            get => new Orientation(new Vector3(1,2,3), Quaternion.Euler(4,5,6));
        }
    }
    #endregion

    #region Test Methods
    public partial class Orientation_Tests
    {
        public static IEnumerable Initializer_Parameters_UseCases()
        {
            yield return new object[] { Vector3.zero, Quaternion.identity };
        }
        [TestCaseSource(nameof(Initializer_Parameters_UseCases))]
        public void Initializer_Parameters_CreatesNewOrientationWithSpecifiedPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            var orientation = new Orientation(position, rotation);

            Assert.AreEqual(position, orientation.Position);
            Assert.AreEqual(rotation, orientation.Rotation);
        }

        [Test]
        public void Identity_CreatesNewOrientationWithZeroVectorAndIdentityQuaternion()
        {
            var orientation = Orientation.identity;

            Assert.AreEqual(Vector3.zero, orientation.Position);
            Assert.AreEqual(Quaternion.identity, orientation.Rotation);
        }

        public static IEnumerable NormalizedPositionIn_UseCases()
        {
            var size = Vector3.one;
            var position = new Vector3(1,2,3);

            yield return new object[] { position, new Bounds(Vector3.zero, size), position*2};
            yield return new object[] { position, new Bounds(Vector3.zero, size*2), position};
            yield return new object[] { position, new Bounds(Vector3.zero, size*4), position/2};

            var offset = new Vector3(0.6f, 0.5f, 0.4f);
            yield return new object[] { position, new Bounds(offset, size), (position-offset)*2};
        }
        [TestCaseSource(nameof(NormalizedPositionIn_UseCases))]
        public void NormalizedPositionIn_ReturnsVector3WithNormalizedValueBasedOnBounds(Vector3 position, Bounds bounds, Vector3 expectedPosition)
        {
            var orientation = new Orientation(position, Quaternion.identity);
            var result = orientation.NormalizedPositionIn(bounds);

            Assert.That(result, Is.EqualTo(expectedPosition).Using(Vector3EqualityComparer.Instance));
        }

        public static IEnumerable NormalizedRotationIn_UseCases()
        {
            var size = Vector3.one;
            var rotation = Quaternion.Euler(30,0,0);

            yield return new object[] { rotation, new Bounds(Vector3.zero, size), rotation.eulerAngles*2};
            yield return new object[] { rotation, new Bounds(Vector3.zero, size*2), rotation.eulerAngles};
            yield return new object[] { rotation, new Bounds(Vector3.zero, size*4), rotation.eulerAngles/2};

            var offset = new Vector3(15, 10, 5);
            var estimate = (rotation.eulerAngles-offset)*2;
            yield return new object[] { rotation, new Bounds(offset, size), new Vector3(Mathf.Abs(estimate.x), Mathf.Abs(estimate.y), Mathf.Abs(estimate.z))};
        }
        [TestCaseSource(nameof(NormalizedRotationIn_UseCases))]
        public void NormalizedRotationIn_ReturnsEulerAnglesBasedOnBounds(Quaternion quaternion, Bounds bounds, Vector3 expectedRotation)
        {
            var orientation = new Orientation(Vector3.zero, quaternion);
            var rotation = orientation.NormalizedRotationIn(bounds);

            Assert.That(rotation, Is.EqualTo(expectedRotation).Using(Vector3EqualityComparer.Instance));
        }

        public static IEnumerable Place_NoOffsets_UseCases()
        {
            var position = new Vector3(3,5,0);
            var rotation = Quaternion.Euler(0,0,30);

            yield return new object[] { position, rotation, Orientation.identity, new Orientation(position, rotation)};
            yield return new object[] { position, rotation, new Orientation(position, rotation), Orientation.identity};
        }
        [TestCaseSource(nameof(Place_NoOffsets_UseCases))]
        public void Place_NoOffsets_CreatesOrientationFromTransform(Vector3 position, Quaternion rotation, Orientation reference, Orientation expected)
        {
            var gameObject = new GameObject();
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;

            var orientation = reference.Place(gameObject.transform);

            Assert.That(orientation.Position, Is.EqualTo(expected.Position).Using(Vector3EqualityComparer.Instance));
            Assert.That(orientation.Rotation, Is.EqualTo(expected.Rotation).Using(QuaternionEqualityComparer.Instance));

            Object.Destroy(gameObject);
        }

        public static IEnumerable Place_Offsets_UseCases()
        {
            var position = new Vector3(3,5,0);
            var rotation = Quaternion.Euler(0,0,30);

            yield return new object[] { position, rotation, Orientation.identity, Vector3.zero, Quaternion.identity, new Orientation(position, rotation)};
            yield return new object[] { position, rotation, new Orientation(position, rotation), Vector3.zero, Quaternion.identity, Orientation.identity};
            yield return new object[] { position, rotation, Orientation.identity, -position, Quaternion.Inverse(rotation), Orientation.identity};
        }
        [TestCaseSource(nameof(Place_Offsets_UseCases))]
        public void Place_Offsets_CreatesOrientationFromTransformAndOffsets(Vector3 position, Quaternion rotation, Orientation reference, Vector3 offsetPosition, Quaternion offsetRotation, Orientation expected)
        {
            var gameObject = new GameObject();
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;

            var orientation = reference.Place(gameObject.transform, offsetPosition, offsetRotation);

            Assert.That(orientation.Position, Is.EqualTo(expected.Position).Using(Vector3EqualityComparer.Instance));
            Assert.That(orientation.Rotation, Is.EqualTo(expected.Rotation).Using(QuaternionEqualityComparer.Instance));

            Object.Destroy(gameObject);
        }

        public static IEnumerable ToString_UseCases()
        {
            yield return new object[] { Orientation.identity, "Position: (0.00, 0.00, 0.00), Rotation: (0.00000, 0.00000, 0.00000, 1.00000)" };
        }
        [TestCaseSource(nameof(ToString_UseCases))]
        public void ToString_ReturnsDescriptionForObject(Orientation orientation, string output)
        {
            Assert.AreEqual(output, orientation.ToString());
        }
    }
    #endregion
}