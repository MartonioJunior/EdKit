using System.Collections;
using NUnit.Framework;
using MartonioJunior.EdKit;
using UnityEngine;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Axis_Tests
    {
        [SetUp]
        public void CreateTestContext() {}
        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods
    public partial class Axis_Tests
    {
        public static IEnumerable Initializer_Enums_UseCases()
        {
            yield return new TestCaseData(Axis.X.Left, Axis.Y.Below, Axis.Z.Back);
        }
        [TestCaseSource(nameof(Initializer_Enums_UseCases))]
        public void Initializer_Enums_CreatesNewAxisWithThreeSpatialRelationships(Axis.X x, Axis.Y y, Axis.Z z)
        {
            var axis = new Axis(x, y, z);

            Assert.AreEqual(x, axis.x);
            Assert.AreEqual(y, axis.y);
            Assert.AreEqual(z, axis.z);
        }

        public static IEnumerable Initializer_Vector3_UseCases()
        {
            yield return new TestCaseData(new Vector3(2,-1,0), Axis.X.Right, Axis.Y.Below, Axis.Z.Body);
            yield return new TestCaseData(new Vector3(-3,1,8), Axis.X.Left, Axis.Y.Above, Axis.Z.Front);
            yield return new TestCaseData(new Vector3(0,0,-4), Axis.X.Center, Axis.Y.Neutral, Axis.Z.Back);
            yield return new TestCaseData(new Vector3(float.NaN,float.NaN,float.NaN), Axis.X.None, Axis.Y.None, Axis.Z.None);
        }
        [TestCaseSource(nameof(Initializer_Vector3_UseCases))]
        public void Initializer_Vector3_CreatesNewAxisBasedOnVector(Vector3 vector, Axis.X x, Axis.Y y, Axis.Z z)
        {
            var axis = new Axis(vector);

            Assert.AreEqual(x, axis.x);
            Assert.AreEqual(y, axis.y);
            Assert.AreEqual(z, axis.z);
        }

        public static IEnumerable GetX_UseCases()
        {
            yield return new TestCaseData(new Vector3(2,-1,0), Axis.X.Right);
            yield return new TestCaseData(new Vector3(-3,1,8), Axis.X.Left);
            yield return new TestCaseData(new Vector3(0,0,-4), Axis.X.Center);
            yield return new TestCaseData(new Vector3(float.NaN,2,5), Axis.X.None);
        }
        [TestCaseSource(nameof(GetX_UseCases))]
        public void GetX_ReturnsAxisXForVectorX(Vector3 vector, Axis.X expected)
        {
            var actual = Axis.GetX(vector);

            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable GetY_UseCases()
        {
            yield return new TestCaseData(new Vector3(2,-1,0), Axis.Y.Below);
            yield return new TestCaseData(new Vector3(-3,1,8), Axis.Y.Above);
            yield return new TestCaseData(new Vector3(0,0,-4), Axis.Y.Neutral);
            yield return new TestCaseData(new Vector3(2,float.NaN,5), Axis.Y.None);
        }
        [TestCaseSource(nameof(GetY_UseCases))]
        public void GetY_ReturnsAxisYForVectorY(Vector3 vector, Axis.Y expected)
        {
            var actual = Axis.GetY(vector);

            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable GetZ_UseCases()
        {
            yield return new TestCaseData(new Vector3(2,-1,0), Axis.Z.Body);
            yield return new TestCaseData(new Vector3(-3,1,8), Axis.Z.Front);
            yield return new TestCaseData(new Vector3(0,0,-4), Axis.Z.Back);
            yield return new TestCaseData(new Vector3(2,5,float.NaN), Axis.Z.None);
        }
        [TestCaseSource(nameof(GetZ_UseCases))]
        public void GetZ_ReturnsAxisZForVectorZ(Vector3 vector, Axis.Z expected)
        {
            var actual = Axis.GetZ(vector);

            Assert.AreEqual(expected, actual);
        }
    }
    #endregion
}