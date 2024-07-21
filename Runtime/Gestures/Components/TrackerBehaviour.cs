using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    public partial class TrackerBehaviour
    {
        // MARK: Variables
        [SerializeField] Vector3 offsetCenter = Vector3.zero;
        [Header("References")]
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform headTransform;
        [SerializeField] Transform xrTransform;
        
        // MARK: Events
        [SerializeField] UnityEvent<Placement> onUpdatePlacement = new();
        
        // MARK: Methods
        public Placement GetPlacement()
        {
            return xrTransform.EdKitPlacement(leftHandTransform, rightHandTransform, headTransform);
        }

        public void Sample()
        {
            onUpdatePlacement.Invoke(GetPlacement());
        }
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Gesture Tracker")]
    public partial class TrackerBehaviour: MonoBehaviour
    {
        void Update()
        {
            Sample();
        }
    }
    #endregion
}