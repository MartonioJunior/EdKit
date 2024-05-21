using System.Collections;
using NUnit.Framework;
using MartonioJunior.EdKit;

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
        public static IEnumerable Initializer_UseCases()
        {
            yield return new TestCaseData(Axis.X.Left, Axis.Y.Below, Axis.Z.Back);
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_CreatesNewAxisWithThreeSpatialRelationships(Axis.X x, Axis.Y y, Axis.Z z)
        {
            var axis = new Axis(x, y, z);

            Assert.AreEqual(x, axis.x);
            Assert.AreEqual(y, axis.y);
            Assert.AreEqual(z, axis.z);
        }
    }
    #endregion
}