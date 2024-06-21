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
        List<Event<IPose, Placement>> buffer;
        GestureEvaluator evaluator;

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

        public void RegisterGesture(GestureData gesture) => evaluator.RegisterGesture(gesture);
        public GestureEvent? Peek() => evaluator.GestureEventFor(buffer, gestureThreshold);
        public void UnregisterGesture(GestureData gesture) => evaluator.UnregisterGesture(gesture);
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Gesture Recognizer")]
    public partial class RecognizerBehaviour: MonoBehaviour
    {
        void Update()
        {
            EvaluateBuffer();
        }
    }
    #endregion
}