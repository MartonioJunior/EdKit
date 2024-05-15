using System.Collections;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial struct GestureEvent
    {
        // MARK: Variables
        [SerializeField] PoseEvent[] poseEvents;
        [SerializeReference] IGesture gesture;
        [SerializeField] float timestamp;

        // MARK: Properties
        public PoseEvent[] PoseEvents => poseEvents;
        public IGesture Gesture => gesture;
        public float Timestamp => timestamp;

        // MARK: Initializers
        public GestureEvent(PoseEvent[] poseEvents, IGesture gesture, float timestamp)
        {
            this.poseEvents = poseEvents;
            this.gesture = gesture;
            this.timestamp = timestamp;
        }
    }
}