using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    #region Aliases
    using PoseEvent = Event<IPose, Placement>;
    using GestureEvent = Event<IGesture, List<Event<IPose, Placement>>>;
    #endregion

    /**
    <summary>
    Component responsible for identifying poses and gestures out of information in <c>Placement</c> data.
    <br/>
    The component is composed of two evaluators: <br/>
    - <c>GestureEvaluator</c>, which is responsible for identifying gestures out of a list of poses. <br/>
    - <c>PoseEvaluator</c>, which are responsible for identifying poses out of a <c>Placement</c> instance.
    </summary>
    <remarks>
    To identify poses and gestures from tracked <c>Transform</c>s in a scene, wire the <c>Register</c> method to the <c>onUpdatePlacement</c>
    event in the <c>TrackerBehaviour</c> component.
    </remarks>
    */
    public partial class RecognizerBehaviour
    {
        // MARK: Variables
        /**
        <summary>Buffer used to store <c>PoseEvent</c> instances for the evaluation of gestures.</summary>
        */
        List<PoseEvent> buffer = new();
        /**
        <summary>Size of the buffer used to evaluate gestures.</summary>
        */
        [SerializeField] int bufferSize = 10;
        [Header("Evaluation")]
        /**
        <summary>List of <c>ScriptableObject</c>-based gestures to be added to evaluation.</summary>
        <remarks>Poses stored in the <c>Poses</c> property are also registered to the pose evaluator.</remarks>
        */
        [SerializeField] List<GestureData> gestures = new();
        /**
        <summary>Evaluator for gesture events.</summary>
        */
        [SerializeField] GestureEvaluator gestureEvaluator = new();
        /**
        <summary>Evaluator for pose events.</summary>
        */
        [SerializeField] PoseEvaluator poseEvaluator = new();

        // MARK: Properties
        /**
        <inheritdoc cref="buffer"/>
        */
        public List<PoseEvent> Buffer => buffer;
        /**
        <inheritdoc cref="gestureEvaluator"/>
        */
        public GestureEvaluator GestureEvaluator => gestureEvaluator;
        /**
        <inheritdoc cref="poseEvaluator"/>
        */
        public PoseEvaluator PoseEvaluator => poseEvaluator;
        
        // MARK: Events
        /**
        <summary>Event fired when the component recognizes a new gesture was executed.</summary>
        */
        [SerializeField] UnityEvent<GestureEvent> onGestureRecognized = new();
        /**
        <summary>Event fired when the component recognizes a new pose was executed.</summary>
        */
        [SerializeField] UnityEvent<PoseEvent> onPoseRecognized = new();

        // MARK: Methods
        /**
        <summary>
        Evaluates the pose buffer to identify a gesture and, when identified, fires <c>onGestureRecognized</c>.
        <br/>
        When a gesture is recognized, the buffer is cleared.
        </summary>
        <remarks>Called by this component's <c>FixedUpdate</c> method</remarks>
        */
        public void EvaluateBuffer()
        {
            if (Peek() is not GestureEvent ge) return;

            onGestureRecognized.Invoke(ge);
            buffer.Clear();
        }
        /**
        <summary>Utility function for determining timestamps for events.</summary>
        <param name="time">Timestamp to mark the event with. If <c>null</c>, the current time is used.</param>
        <returns>Resolved timestamp.</returns>
        */
        private float ResolveTime(float? time) => time ?? Time.time;
        /**
        <summary>Registers a new placement instance identified at a moment in time, firing <c>onPoseRecognized</c> when a pose is successfully detected.</summary>
        <param name="placement">Placement instance to be registered.</param>
        <param name="time">Timestamp for the event. If <c>null</c>, the current time is used.</param>
        <remarks>If the buffer is already full, the operation removes the oldest event registered in the buffer.</remarks>
        */
        public void Register(Placement placement, float? time = null)
        {
            if (buffer.Count >= bufferSize) {
                buffer.RemoveAt(0);
            }

            if (poseEvaluator.Evaluate(placement, ResolveTime(time)) is not PoseEvent pe) return;

            buffer.Add(pe);
            onPoseRecognized.Invoke(pe);
        }
        /**
        <summary>Configures the pose and gesture evaluators for the component.</summary>
        <remarks>Called by the component's <c>Awake</c> method</remarks>
        */
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
        /**
        <summary>Returns the gesture event identified by the gesture evaluator.</summary>
        <param name="time">Timestamp for the event. If <c>null</c>, the current time is used.</param>
        <returns>Gesture event identified by the gesture evaluator.</returns>
        */
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

    #region UnityEvent Bindings
    public partial class RecognizerBehaviour
    {
        /**
        <inheritdoc cref="Register"/>
        */
        public void Register(Placement placement) => Register(placement, null);
        /**
        <summary>Registers a new gesture with the gesture evaluator.</summary>
        <param name="gesture">Gesture to be registered.</param>
        <remarks>Poses stored in the <c>Poses</c> property are also registered to the pose evaluator.</remarks>
        */
        public void RegisterGesture(GestureData gesture)
        {
            gestureEvaluator.Register(gesture);

            foreach (var pose in gesture.Poses) {
                poseEvaluator.Register(pose);
            }
        }
        /**
        <summary>Unregisters a gesture from the gesture evaluator.</summary>
        <param name="gesture">Gesture to be removed.</param>
        <remarks>
        This only de-registers the gesture from the evaluation process and not the poses that compose it, as they can be shared between gestures.<br/>
        To also remove the poses associated with this gesture, use the <c>Unregister</c> method in the <c>PoseEvaluator</c> property.
        </remarks>
        */
        public void UnregisterGesture(GestureData gesture)
        {
            gestureEvaluator.Unregister(gesture);
        }
    }
    #endregion

    #region UnityEditor Support
    #if UNITY_EDITOR
    public partial class RecognizerBehaviour
    {
        public void FeedPoseEvent(PoseEvent poseEvent)
        {
            buffer.Add(poseEvent);
            onPoseRecognized.Invoke(poseEvent);
        }

        public void FeedGestureEvent(GestureEvent gestureEvent)
        {
            onGestureRecognized.Invoke(gestureEvent);
            buffer.Clear();
        }
    }
    #endif
    #endregion
}