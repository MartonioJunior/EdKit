using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    using GestureEvent = Event<IGesture, List<Event<IPose, Placement>>>;

    [Serializable]
    public partial class GestureEvaluator
    {
        // MARK: Variables
#if UNITY_EDITOR
        [SerializeField] List<GestureData> gestures = new();
#endif
        ISet<IGesture> allGestures = new HashSet<IGesture>();
        ISet<IPose> poses = new HashSet<IPose>();

        // MARK: Initializers
        public GestureEvaluator() {}

        public GestureEvaluator(IEnumerable<IGesture> gestures)
        {
            allGestures = new HashSet<IGesture>(gestures);
#if UNITY_EDITOR
            this.gestures = new List<GestureData>();
#endif

            foreach (var gesture in allGestures) {
                if (gesture is not GestureData gd) continue;
#if UNITY_EDITOR
                this.gestures.Add(gd);
#endif
                foreach (IPose pose in gd.Poses) {
                    poses.Add(pose);
                }
            }
        }

        // MARK: Methods
        public GestureEvent? GestureEventFor(List<Event<IPose, Placement>> poseEvents, float threshold = 0)
        {
            if (Event.From(poseEvents, allGestures, 0.0f) is GestureEvent ge && ge.Score >= threshold) {
                return ge;
            } else {
                return null;
            }
        }

        public Event<IPose, Placement>? PoseEventFor(Placement placement, float time, float threshold = 0)
        {
            if (Event.From(placement, poses, time) is Event<IPose, Placement> pe && pe.Score >= threshold) {
                return pe;
            } else {
                return null;
            }
        }

        public void RegisterGesture(IGesture gesture)
        {
            if (!allGestures.Contains(gesture)) {
                allGestures.Add(gesture);
            }

            if (gesture is GestureData gestureData) {
#if UNITY_EDITOR
                gestures.Add(gestureData);
#endif
                foreach (IPose pose in gestureData.Poses) {
                    poses.Add(pose);
                }
            }
        }

        public void RegisterPose(IPose pose) => poses.Add(pose);
        public void UnregisterGesture(IGesture gesture)
        {
            allGestures.Remove(gesture);

            if (gesture is GestureData gd) {
#if UNITY_EDITOR
                gestures.Remove(gd);
#endif
            }
        }

        public void UnregisterPose(IPose pose) => poses.Remove(pose);
    }
}