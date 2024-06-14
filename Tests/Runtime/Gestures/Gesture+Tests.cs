using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MartonioJunior.EdKit;
using System;
using System.Collections.Generic;
using Pose = MartonioJunior.EdKit.Pose;

namespace Tests.MartonioJunior.EdKit
{
    #region Test Setup
    public partial class Gesture_Tests
    {
        [SetUp]
        public void CreateTestContext() {}

        [TearDown]
        public void DestroyTestContext() {}
    }
    #endregion

    #region Test Methods (Base Type)
    public partial class Gesture_Tests
    {
        public static IEnumerable Initialize_ScoreList_UseCases()
        {
            Func<IList<PoseEvent>, float> scoreFunction = (events) => 4.9f;
            yield return new TestCaseData("Gesture", scoreFunction, 4.9f);
        }
        [TestCaseSource(nameof(Initialize_ScoreList_UseCases))]
        public void Initialize_ScoreList_InstancesGestureFromScoringFunction(string name, Func<IList<PoseEvent>, float> scoreFunction, float score)
        {
            var gesture = new Gesture(name, scoreFunction);
            var poseEvents = new List<PoseEvent>();

            Assert.AreEqual(name, gesture.Name);
            Assert.AreEqual(score, gesture.Evaluate(poseEvents));
        }

        public static IEnumerable Initializer_ScoreElement_UseCases()
        {
            yield return new TestCaseData("Gesture", new List<Func<PoseEvent, float>> { (poseEvent) => 4.9f }, 4.9f);
        }
        [TestCaseSource(nameof(Initializer_ScoreElement_UseCases))]
        public void Initializer_ScoreElement_InstancesGestureFromListOfPoseEventScorers(string name, IList<Func<PoseEvent, float>> scoreFunctions, float score)
        {
            var gesture = new Gesture(name, scoreFunctions);
            var poseEvents = new List<PoseEvent>();

            Assert.AreEqual(name, gesture.Name);
            Assert.AreEqual(score, gesture.Evaluate(poseEvents));
        }
    }
    #endregion

    #region Test Methods (IGesture)
    public partial class Gesture_Tests
    {
        public static IEnumerable Name_UseCases()
        {
            yield return new TestCaseData("Fly");
        }
        [TestCaseSource(nameof(Name_UseCases))]
        public void Name_ReturnsNameOfGesture(string name)
        {
            var gesture = new Gesture(name, (events) => 0f);
            Assert.AreEqual(name, gesture.Name);
        }

        public static IEnumerable Evaluate_UseCases()
        {
            var placement = new Placement();
            var pose = new Pose("Fly", (e) => 2.5f);
            var poseEvent = PoseEvent.New(placement, pose, 0.7f);
            yield return new TestCaseData(new List<PoseEvent?> { poseEvent }, 2.5f);
        }
        [TestCaseSource(nameof(Evaluate_UseCases))]
        public void Evaluate_ReturnsScoreFromListOfPoseEvents(IList<PoseEvent?> events, float expectedScore)
        {
            if (events is not IList<PoseEvent> poseEvents) return;

            var gesture = new Gesture("Fly", _ => expectedScore);
            var score = gesture.Evaluate(poseEvents);

            Assert.AreEqual(expectedScore, score);
        }
    }
    #endregion
}