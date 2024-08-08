using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    #region Aliases
    using PoseEvent = Event<IPose, Placement>;
    using GestureEvent = Event<IGesture, List<Event<IPose, Placement>>>;
    #endregion

    public partial class RecognizerBehaviour
    {
        // MARK: Variables
        List<PoseEvent> buffer = new();
        [SerializeField] int bufferSize = 10;
        [Header("Evaluation")]
        [SerializeField] List<GestureData> gestures = new();
        [SerializeField] GestureEvaluator gestureEvaluator = new();
        [SerializeField] PoseEvaluator poseEvaluator = new();

        // MARK: Properties
        public List<PoseEvent> Buffer => buffer;
        public GestureEvaluator GestureEvaluator => gestureEvaluator;
        public PoseEvaluator PoseEvaluator => poseEvaluator;
        
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

        private float ResolveTime(float? time) => time ?? Time.time;

        public void Register(Placement placement, float? time = null)
        {
            if (buffer.Count >= bufferSize) {
                buffer.RemoveAt(0);
            }

            if (poseEvaluator.Evaluate(placement, ResolveTime(time)) is not PoseEvent pe) return;

            buffer.Add(pe);
            onPoseRecognized.Invoke(pe);
        }

        public void SetupEvaluators()
        {
            gestures.ForEach(gestureEvaluator.Register);
            gestures.Reduce(new List<IPose>(), InsertPoses).ForEach(poseEvaluator.Register);

            List<IPose> InsertPoses(List<IPose> poseList, GestureData gesture)
            {
                poseList.AddRange(gesture.Poses);
                return poseList;
            }
        }

        public GestureEvent? Peek(float? time = null)
        {
            return gestureEvaluator.Evaluate(buffer, ResolveTime(time));
        }
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Gesture Recognizer")]
    public partial class RecognizerBehaviour: MonoBehaviour
    {
        void Awake()
        {
            SetupEvaluators();
        }

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
        public void RegisterGesture(GestureData gesture)
        {
            gestureEvaluator.Register(gesture);

            foreach (var pose in gesture.Poses) {
                poseEvaluator.Register(pose);
            }
        }

        public void UnregisterGesture(GestureData gesture)
        {
            gestureEvaluator.Unregister(gesture);
        }
    }
    #endregion
}