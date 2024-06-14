using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    public partial class RecognizerBehaviour
    {
        // MARK: Variables
        [SerializeField] List<PoseEvent> buffer;
        [SerializeField] int bufferSize = 10;
        [SerializeField] GestureEvaluator evaluator;

        // MARK: Properties
        public List<PoseEvent> Buffer => buffer;
        
        // MARK: Events
        [SerializeField] UnityEvent<GestureEvent> onGestureRecognized = new();
        [SerializeField] UnityEvent<PoseEvent> onPoseRecognized = new();

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

            if (evaluator.PoseEventFor(placement, baseTime) is not PoseEvent pe) return;

            buffer.Add(pe);
            onPoseRecognized.Invoke(pe);
        }

        public void RegisterGesture(GestureData gesture) => evaluator.RegisterGesture(gesture);
        public GestureEvent? Peek() => evaluator.GestureEventFor(buffer);
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