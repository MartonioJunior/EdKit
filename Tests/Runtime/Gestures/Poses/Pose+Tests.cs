using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Pose_Tests
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
    public partial class Pose_Tests
    {
        [Test]
        public void Initializer_CreatesNewPoseWithNameAndScoringFunction()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Evaluate_ReturnsHighestScoringPoseFromList()
        {
            Assert.Ignore(NotImplemented);
        }
    }
    #endregion

    #region Test Methods (IPose)
    public partial class Pose_Tests
    {
        [Test]
        public void Name_ReturnsPoseName()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Evaluate_ReturnsScoreForPlacement()
        {
            Assert.Ignore(NotImplemented);
        }
    }
    #endregion
}