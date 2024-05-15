using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class RecognizerBehaviour_Tests
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
    public partial class RecognizerBehaviour_Tests
    {
        [Test]
        public void Register_Placement_AddsPlacementToBuffer()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Register_PlacementTimePoses_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Register_PlacementTimePoses_UseCases))]
        public void Register_PlacementTimePoses_CreatesPoseEventAndAddsToBuffer()
        {
            Assert.Ignore(NotImplemented);
        }

        [Test]
        public void Sample_NoParameters_SamplesUsingComponentStructures()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Sample_BufferGestures_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Sample_BufferGestures_UseCases))]
        public void Sample_BufferGestures_ReturnsGestureEventFromEvaluationOfPoseBuffer()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable SetGestures_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(SetGestures_UseCases))]
        public void SetGestures_DefinesGesturesThatComponentCanRecognize()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable SetPoses_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(SetPoses_UseCases))]
        public void SetPoses_DefinesPosesThatComponentCanRecognize()
        {
            Assert.Ignore(NotImplemented);
        }

        public static IEnumerable Squash_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Squash_UseCases))]
        public void Squash_CompressesBufferToLeastAmountOfPoses()
        {
            Assert.Ignore(NotImplemented);
        }
    }
    #endregion
}