using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    /**
    <summary>
    Component responsible for interpreting scene <c>Transform</c>s into a <c>Placement</c> instance. <br/>
    Based on a Virtual Reality setup, the component requires 4 references to compose the placement: <br/>
    - <c>leftHandTransform</c>, which represents the left hand of the actor (or equivalent). <br/>
    - <c>rightHandTransform</c>, which represents the right hand of the actor (or equivalent). <br/>
    - <c>headTransform</c>, which represents the head of the actor (or equivalent). <br/>
    - <c>xrTransform</c>, which represents the coordinate space used as reference for the actor's placement.
    </summary>
    */
    public partial class TrackerBehaviour
    {
        // MARK: Variables
        [Header("References")]
        /**
        <summary><c>Transform</c> that represents the actor's left hand (or equivalent).</summary>
        */
        [SerializeField] Transform leftHandTransform;
        /**
        <summary><c>Transform</c> that represents the actor's right hand (or equivalent).</summary>
        */
        [SerializeField] Transform rightHandTransform;
        /**
        <summary><c>Transform</c> that represents the actor's head (or equivalent).</summary>
        <remarks>Used together with <c>xrTransform</c> to define the local coordinate space of the created <c>Placement</c> instance.</remarks>
        */
        [SerializeField] Transform headTransform;
        /**
        <summary><c>Transform</c> that represents the `global` coordinate space used as reference for the actor's placement.</summary>
        */
        [SerializeField] Transform xrTransform;
        
        // MARK: Events
        /**
        <summary>Event fired when the component is sampled for a new <c>Placement</c> instance.</summary>
        */
        [SerializeField] UnityEvent<Placement> onUpdatePlacement = new();
        
        // MARK: Methods
        /**
        <summary>Returns the current placement for the actor.</summary>
        <remarks>All the <c>Orientation</c> structures for <c>Placement</c> are calculated based on <c>xrTransform</c>'s position
        and a new Unit axis created by <c>Vector3.up</c> and <c>headTransform</c>'s forward axis flattened to the XZ plane.</remarks>
        */
        public Placement GetPlacement()
        {
            return xrTransform.EdKitPlacement(leftHandTransform, rightHandTransform, headTransform);
        }
        /**
        <summary>Captures the current <c>Placement</c> for the actor in the scene and fires <c>onUpdatePlacement</c> to notify listeners.</summary>
        <remarks>Called by the component's <c>Update</c> method.</remarks>
        */
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