using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;
using Pose = MartonioJunior.EdKit.Pose;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class RecognizerBehaviour_Tests
    {
        GameObject gameObject;
        RecognizerBehaviour recognizerBehaviour;
        [SetUp]
        public void CreateTestContext()
        {
            gameObject = new GameObject();
            recognizerBehaviour = gameObject.AddComponent<RecognizerBehaviour>();
        }

        [TearDown]
        public void DestroyTestContext()
        {
            Object.DestroyImmediate(gameObject);
            gameObject = null;
            recognizerBehaviour = null;
        }
    }
    #endregion

    #region Test Methods (Base Type)
    public partial class RecognizerBehaviour_Tests
    {
        public static IEnumerable Register_Placement_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Register_Placement_UseCases))]
        public void Register_Placement_AddsPlacementToBuffer(Placement placement, PoseEvent poseEvent)
        {
            recognizerBehaviour.Register(placement);

            if (poseEvent is PoseEvent pe) CollectionAssert.Contains(recognizerBehaviour.Buffer, pe);
        }

        public static IEnumerable Register_PlacementTimePoses_UseCases()
        {
            yield return new object[] { new Placement(), 0.0f, new List<IPose>(), null };
            yield return new object[] { new Placement(), 0.0f, new List<IPose> { new Pose("Pose 1", p => 1.0f), new Pose("Pose 2", p => 0.2f) }, new PoseEvent(new Placement(), new Pose("Pose 1", p => 1.0f), 0.0f) };
        }
        [TestCaseSource(nameof(Register_PlacementTimePoses_UseCases))]
        public void Register_PlacementTimePoses_CreatesPoseEventAndAddsToBuffer(Placement placement, float time, IList<IPose> poses, PoseEvent? poseEvent)
        {
            recognizerBehaviour.Register(placement, time, poses);

            if (poseEvent is PoseEvent pe) CollectionAssert.Contains(recognizerBehaviour.Buffer, pe);
        }

        public static IEnumerable Sample_NoParameters_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Sample_NoParameters_UseCases))]
        public void Sample_NoParameters_SamplesUsingComponentStructures()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Sample_BufferGestures_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Sample_BufferGestures_UseCases))]
        public void Sample_BufferGestures_ReturnsGestureEventFromEvaluationOfPoseBuffer()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable SetGestures_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(SetGestures_UseCases))]
        public void SetGestures_DefinesGesturesThatComponentCanRecognize()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable SetPoses_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(SetPoses_UseCases))]
        public void SetPoses_DefinesPosesThatComponentCanRecognize()
        {
            Assert.Ignore("Not Implemented");
        }

        public static IEnumerable Squash_UseCases()
        {
            yield return null;
        }
        [TestCaseSource(nameof(Squash_UseCases))]
        public void Squash_CompressesBufferToLeastAmountOfPoses()
        {
            Assert.Ignore("Not Implemented");
        }
    }
    #endregion
}