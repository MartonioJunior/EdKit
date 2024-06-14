using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    [Serializable]
    public partial struct GestureEvaluator
    {
        // MARK: Variables
        [SerializeField] List<GestureData> gestures;
        ISet<IGesture> nonSerializedGestures;
        ISet<IPose> poses;

        // MARK: Initializers
        public GestureEvaluator(List<GestureData> gestures)
        {
            this.poses = new HashSet<IPose>();
            this.gestures = gestures;
            this.nonSerializedGestures = new HashSet<IGesture>();

            foreach (GestureData gesture in gestures) {
                foreach (IPose pose in gesture.Poses) {
                    poses.Add(pose);
                }
            }
        }

        // MARK: Methods
        public GestureEvent? GestureEventFor(List<PoseEvent> poseEvents) => GestureEvent.From(poseEvents, gestures);
        public PoseEvent? PoseEventFor(Placement placement, float time) => PoseEvent.From(placement, time, poses);

        public void RegisterGesture(IGesture gesture)
        {
            if (gesture is GestureData gd && !gestures.Contains(gd)) {
                gestures.Add(gd);
                foreach (IPose pose in gd.Poses) {
                    poses.Add(pose);
                }
            } else if (!nonSerializedGestures.Contains(gesture)) {
                nonSerializedGestures.Add(gesture);
            } else {
                return;
            }
        }

        public void UnregisterGesture(GestureData gesture)
        {
            gestures.Remove(gesture);
        }
    }
}