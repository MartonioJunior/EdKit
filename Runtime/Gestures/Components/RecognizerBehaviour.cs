using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    using GestureEvent = Event<IGesture, List<Event<IPose, Placement>>>;

    public partial class RecognizerBehaviour
    {
        // MARK: Variables
        [SerializeField] int bufferSize = 10;
        [SerializeField, Range(0,1)] float poseThreshold = 0.8f;
        [SerializeField, Range(0,1)] float gestureThreshold = 0.8f;
        [SerializeField] GestureEvaluator evaluator = new();
        List<Event<IPose, Placement>> buffer = new();

        // MARK: Properties
        public List<Event<IPose, Placement>> Buffer => buffer;
        public GestureEvaluator Evaluator => evaluator;
        
        // MARK: Events
        [SerializeField] UnityEvent<GestureEvent> onGestureRecognized = new();
        [SerializeField] UnityEvent<Event<IPose, Placement>> onPoseRecognized = new();

        // MARK: Methods
        public void EvaluateBuffer()
        {
            if (Peek() is not GestureEvent ge) return;

            onGestureRecognized.Invoke(ge);
            buffer.Clear();
        }

        public void Register(Placement placement, float? time = null)
        {
            if (buffer.Count >= bufferSize) {
                buffer.RemoveAt(0);
            }

            var baseTime = time ?? Time.time;

            if (evaluator.PoseEventFor(placement, baseTime, poseThreshold) is not Event<IPose, Placement> pe) return;

            buffer.Add(pe);
            onPoseRecognized.Invoke(pe);
        }

        public GestureEvent? Peek() => evaluator.GestureEventFor(buffer, gestureThreshold);
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Gesture Recognizer")]
    public partial class RecognizerBehaviour: MonoBehaviour
    {
        void FixedUpdate()
        {
            EvaluateBuffer();
        }
    }
    #endregion

    #region UnityEvent Integration
    public partial class RecognizerBehaviour
    {
        public void Register(Placement placement) => Register(placement, null);
        public void RegisterGesture(GestureData gesture) => evaluator.RegisterGesture(gesture);
        public void UnregisterGesture(GestureData gesture) => evaluator.UnregisterGesture(gesture);
    }
    #endregion
}