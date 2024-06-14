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
        ISet<IPose> poses;

        // MARK: Initializers
        public GestureEvaluator(List<GestureData> gestures)
        {
            this.poses = new HashSet<IPose>();
            this.gestures = gestures;

            foreach (GestureData gesture in gestures) {
                foreach (IPose pose in gesture.Poses) {
                    poses.Add(pose);
                }
            }
        }

        // MARK: Methods
        public GestureEvent? GestureEventFor(List<PoseEvent> poseEvents) => GestureEvent.From(poseEvents, gestures);
        public PoseEvent? PoseEventFor(Placement placement, float time) => PoseEvent.From(placement, time, poses);

        public void RegisterGesture(GestureData gesture)
        {
            gestures.Add(gesture);
            foreach (IPose pose in gesture.Poses) {
                poses.Add(pose);
            }
        }

        public void UnregisterGesture(GestureData gesture)
        {
            gestures.Remove(gesture);
        }
    }
}