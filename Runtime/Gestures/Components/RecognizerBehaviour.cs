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
        IList<IGesture> gestures;
        IList<IPose> poses;

        // MARK: Properties
        public List<PoseEvent> Buffer => buffer;
        
        // MARK: Events
        [SerializeField] UnityEvent<GestureEvent> onGestureRecognized;
        [SerializeField] UnityEvent<PoseEvent> onPoseRecognized;

        // MARK: Methods
        public void Register(Placement placement)
        {
            Register(placement, Time.time, poses);
        }

        public void Register(Placement placement, float time, IList<IPose> poses)
        {
            if (buffer.Count >= bufferSize) {
                buffer.RemoveAt(0);
            }

            PoseEvent? poseEvent = PoseEvent.From(placement, time, poses);
            if (poseEvent is not PoseEvent pe) return;

            buffer.Add(pe);
        }

        public GestureEvent? Sample()
        {
            return Sample(buffer, gestures);
        }

        public GestureEvent? Sample(List<PoseEvent> buffer, IList<IGesture> gestures)
        {
            var gesture = gestures.Max(gesture => gesture.Evaluate(buffer));
            return new GestureEvent(buffer.ToArray(), gesture, buffer[^1].Timestamp);
        }

        public void SetGestures(IList<IGesture> gestures)
        {
            this.gestures = gestures;
        }

        public void SetPoses(IList<IPose> poses)
        {
            this.poses = poses;
        }

        public void Squash()
        {
            for(int i = 1; i < bufferSize; i++) {
                if (buffer[^(i-1)].Pose.Name != buffer[^i].Pose.Name) continue;

                buffer.RemoveAt(buffer.Count - i);
            }
        }
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Gesture Recognizer")]
    public partial class RecognizerBehaviour: MonoBehaviour
    {
        
    }
    #endregion
}