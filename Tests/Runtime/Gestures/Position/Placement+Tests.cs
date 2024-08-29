using System.Collections;
using NUnit.Framework;
using UnityEngine;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Placement_Tests
    {
        [SetUp]
        public void CreateTestContext() {}

        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Mocking
    public partial class Placement_Tests
    {
        public static Placement Mock
        {
            get => new Placement(Orientation_Tests.Mock, Orientation_Tests.Mock, Orientation_Tests.Mock);
        }
    }
    #endregion

    #region Test Methods
    public partial class Placement_Tests
    {
        public static IEnumerable Initializer_UseCases()
        {
            yield return new TestCaseData(Orientation_Tests.Mock, Orientation_Tests.Mock, Orientation_Tests.Mock);
        }
        [TestCaseSource(nameof(Initializer_UseCases))]
        public void Initializer_CreatesNewPlacementWithSpecifiedOrientations(Orientation leftHand, Orientation rightHand, Orientation head)
        {
            var placement = new Placement(leftHand, rightHand, head);

            Assert.AreEqual(leftHand, placement.LeftHand);
            Assert.AreEqual(rightHand, placement.RightHand);
            Assert.AreEqual(head, placement.Head);
        }

        public static IEnumerable ToString_UseCases()
        {
            var placement = Mock;

            yield return new TestCaseData(placement, $"Left Hand: {placement.LeftHand}, Right Hand: {placement.RightHand}, Head: {placement.Head}");
        }
        [TestCaseSource(nameof(ToString_UseCases))]
        public void ToString_ReturnsObjectDescription(Placement placement, string output)
        {
            Assert.AreEqual(placement.ToString(), output);
        }
    }
    #endregion
}