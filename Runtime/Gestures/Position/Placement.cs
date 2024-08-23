using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure used to define an actor's positioning, which is composed by two hands and one head, in relation to a spatial reference.<br/></summary>
    <remarks>The structure assumes that the actor has a head and two hands (or equivalent parts). <br/></remarks>
    */
    [System.Serializable]
    public partial struct Placement
    {
        // MARK: Variables
        /**
        <summary>Orientation for the left hand (or equivalent) in relation to the spatial reference.</summary>
        */
        [SerializeField] Orientation leftHand;
        /**
        <summary>Orientation for the right hand (or equivalent) in relation to the spatial reference.</summary>
        */
        [SerializeField] Orientation rightHand;
        /**
        <summary>Orientation for the head (or equivalent) in relation to the spatial reference.</summary>
        */
        [SerializeField] Orientation head;

        // MARK: Properties
        /**
        <inheritdoc cref="Placement.leftHand"/>
        */
        public Orientation LeftHand => leftHand;
        /**
        <inheritdoc cref="Placement.rightHand"/>
        */
        public Orientation RightHand => rightHand;
        /**
        <inheritdoc cref="Placement.head"/>
        */
        public Orientation Head => head;

        // MARK: Initializers
        /**
        <summary>Initializer that creates a new <c>Placement</c> with the given orientation values.</summary>
        <param name="leftHand"><c>Orientation</c> values for the left hand (or equivalent).</param>
        <param name="rightHand"><c>Orientation</c> values for the right hand (or equivalent).</param>
        <param name="head"><c>Orientation</c> values for the head (or equivalent).</param>
        */
        public Placement(Orientation leftHand, Orientation rightHand, Orientation head)
        {
            this.leftHand = leftHand;
            this.rightHand = rightHand;
            this.head = head;
        }
    }

    #region ToString
    public partial struct Placement
    {
        /**
        <summary>Creates a textual representation for <c>Placement</c></summary>
        */
        public override string ToString()
        {
            return $"| Left Hand: {leftHand}\n| Right Hand: {rightHand}\n| Head: {head}";
        }
    }
    #endregion

    #region Transform
    public static partial class TransformExtensions
    {
        /**
        <summary>Composes a new <c>Placement</c> instance, placing three reference <c>Transform</c>s in relation to it's coordinate space.</summary>
        <param name="leftHand">The transform that corresponds to the left hand (or equivalent).</param>
        <param name="rightHand">The transform that corresponds to the right hand (or equivalent).</param>
        <param name="head">The transform that corresponds to the head (or equivalent).</param>
        <returns>The new <c>Placement</c> instance composed of values in relation to this <c>Transform</c>.</returns>
        <remarks>While the <c>Transform</c> is used to define the origin's position, the forward axis is defined by the head's forward vector.</remarks>
        */
        public static Placement EdKitPlacement(this Transform self, Transform leftHand, Transform rightHand, Transform head)
        {
            Vector3 forward = new Vector3(head.forward.x, 0.0f, head.forward.z).normalized;

            return self.OrientationFrom(forward: forward).PlacementFrom(leftHand, rightHand, head);
        }
    }
    #endregion
}