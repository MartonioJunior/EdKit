using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    public partial class TrackerComponent
    {
        // MARK: Variables
        [SerializeField] Vector3 offsetCenter = Vector3.zero;
        [Header("References")]
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform headTransform;
        [SerializeField] Transform xrTransform;

        // MARK: Properties
        public Placement Placement {
            get {
                return GetPlacement();
            }
        }
        
        // MARK: Events
        [SerializeField] UnityEvent<Placement> onUpdatePlacement;
        
        // MARK: Methods
        public Placement GetPlacement()
        {
            var referenceForward = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;
            var referenceUp = xrTransform.up;

            var leftHandPosition = GetPositionForTransform(leftHandTransform, referenceForward, referenceUp);
            var rightHandPosition = GetPositionForTransform(rightHandTransform, referenceForward, referenceUp);
            var headPosition = GetPositionForTransform(headTransform, referenceForward, referenceUp);

            var leftHandRotation = GetRotationForTransform(leftHandTransform, referenceForward, referenceUp);
            var rightHandRotation = GetRotationForTransform(rightHandTransform, referenceForward, referenceUp);
            var headRotation = GetRotationForTransform(headTransform, referenceForward, referenceUp);

            return new Placement(
                new Orientation(leftHandPosition, leftHandRotation),
                new Orientation(rightHandPosition, rightHandRotation),
                new Orientation(headPosition, headRotation)    
            );
        }

        public Position GetPositionForTransform(Transform transform, Vector3 forward, Vector3 up)
        {
            var xrPosition = xrTransform.position;

            var delta = transform.position - xrPosition;

            var relativePosition = new Vector3(
                -delta.x,
                delta.y-headTransform.position.y,
                delta.z
            )+offsetCenter;

            var discretePosition = relativePosition.InverseRelativeTo(up, forward);

            return new Position(discretePosition);
        }

        public Rotation GetRotationForTransform(Transform transform, Vector3 forward, Vector3 up)
        {
            var referenceQuaternion = xrTransform.rotation;

            var delta = transform.rotation * Quaternion.Inverse(referenceQuaternion);

            var relativeRotation = delta.eulerAngles;

            var discreteRotation = relativeRotation.InverseRelativeTo(up, forward);

            return new Rotation(Quaternion.Euler(discreteRotation));
        }

        public void Sample()
        {
            onUpdatePlacement.Invoke(GetPlacement());
        }
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("Leftie Wrightie/Gestures/Gesture Tracker")]
    public partial class TrackerComponent: MonoBehaviour
    {
        void Update()
        {
            Sample();
        }
    }
    #endregion
}