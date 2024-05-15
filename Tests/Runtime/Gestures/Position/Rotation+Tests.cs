using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Rotation_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Mocking
    public partial class Rotation_Tests
    {
        public static Rotation Mock
        {
            get => new Rotation(Quaternion.identity, Vector3.one);
        }
    }
    #endregion

    #region Test Cases
    public partial class Rotation_Tests
    {
        public static IEnumerable Value_UseCases()
        {
            yield return new object[]{new Quaternion(1,2,3,4), 1, 2, 3, 4};
        }
        [TestCaseSource(nameof(Value_UseCases))]
        public void Value_ReturnsQuaternionRepresentingRotation(Quaternion quaternion, float x, float y, float z, float w)
        {
            var rotation = new Rotation(quaternion);

            Assert.AreEqual(x, rotation.Value.x);
            Assert.AreEqual(y, rotation.Value.y);
            Assert.AreEqual(z, rotation.Value.z);
            Assert.AreEqual(w, rotation.Value.w);
        }

        public static IEnumerable Deadzone_UseCases()
        {
            yield return new object[]{new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.1f, 0.1f, 0.1f)};
        }
        [TestCaseSource(nameof(Deadzone_UseCases))]
        public void Deadzone_ReturnsAngleAreaForRotation(Vector3 deadzone, Vector3 deadzoneResult)
        {
            var rotation = new Rotation(Quaternion.identity, deadzone);

            Assert.AreEqual(deadzoneResult, rotation.Deadzone);
        }

        public static IEnumerable XFor_UseCases()
        {
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(20, 15, 10), Axis.X.Center};
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(40, 15, 10), Axis.X.Right};
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(-30, 15, 10), Axis.X.Left};
        }
        [TestCaseSource(nameof(XFor_UseCases))]
        public void XFor_ReturnsRotationInXAxis(Rotation rotation, Quaternion quaternion, Axis.X expected)
        {
            Assert.AreEqual(expected, rotation.XFor(quaternion));
        }

        public static IEnumerable YFor_UseCases()
        {
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(30, 5, 10), Axis.Y.Neutral};
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(30, 25, 10), Axis.Y.Above};
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(30, -20, 10), Axis.Y.Below};
        }
        [TestCaseSource(nameof(YFor_UseCases))]
        public void YFor_ReturnsRotationInYAxis(Rotation rotation, Quaternion quaternion, Axis.Y expected)
        {
            Assert.AreEqual(expected, rotation.YFor(quaternion));
        }

        public static IEnumerable ZFor_UseCases()
        {
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(30, 15, 0), Axis.Z.Body};
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(30, 15, 20), Axis.Z.Front};
            yield return new object[]{new Rotation(Quaternion.identity, new Vector3(30,15,10)), Quaternion.Euler(30, 15, -15), Axis.Z.Back};
        }
        [TestCaseSource(nameof(ZFor_UseCases))]
        public void ZFor_ReturnsRotationInZAxis(Rotation rotation, Quaternion quaternion, Axis.Z expected)
        {
            Assert.AreEqual(expected, rotation.ZFor(quaternion));
        }
    }
    #endregion
}