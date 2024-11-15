using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Pose that has it's accuracy scored based on bounding boxes.</summary>
    */
    [System.Obsolete("PoseData is currently deprecated. Please use Pose or a type inheriting from IPose instead.")]
    public partial class PoseData
    {
        // MARK: Variables
        /**
        <summary><c>Bounds</c> range for the left hand's position</summary>
        */
        [Header("Left Hand")]
        [SerializeField] Bounds positionBoundsLeftHand = new(Vector3.zero, Vector3.one);
        /**
        <summary><c>Bounds</c> range for the left hand's rotation</summary>
        */
        [SerializeField] Bounds rotationBoundsLeftHand = new(Vector3.zero, new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity));
        /**
        <summary><c>Bounds</c> range for the right hand's position</summary>
        */
        [Header("Right Hand")]
        [SerializeField] Bounds positionBoundsRightHand = new(Vector3.zero, Vector3.one);
        /**
        <summary><c>Bounds</c> range for the right hand's rotation</summary>
        */
        [SerializeField] Bounds rotationBoundsRightHand = new(Vector3.zero, new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity));
        /**
        <summary><c>Bounds</c> range for the head's position</summary>
        */
        [Header("Head")]
        [SerializeField] Bounds positionBoundsHead = new(new Vector3(0,1,0), Vector3.one);
        /**
        <summary><c>Bounds</c> range for the head's rotation</summary>
        */
        [SerializeField] Bounds rotationBoundsHead = new(Vector3.zero, new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity));

        // MARK: Properties
        /**
        <summary><c>Pose</c> generated by the <c>PoseData</c> component.</summary>
        */
        public Pose Pose => new(
            name: name,
            p => Precision(p)
        );

        // MARK: Methods
        /**
        <summary>Calculates a precision score based on a <c>Placement</c> instance.</summary>
        <param name="placement">The <c>Placement</c> instance to be evaluated.</param>
        <returns>The score for the <c>Placement</c>, in the range between 0 and 1.</returns>
        */
        public float Precision(Placement placement)
        {
            var leftHandPrecision = Precision(placement.LeftHand, positionBoundsLeftHand, rotationBoundsLeftHand);
            var rightHandPrecision = Precision(placement.RightHand, positionBoundsRightHand, rotationBoundsRightHand);
            var headPrecision = Precision(placement.Head, positionBoundsHead, rotationBoundsHead);

            return (leftHandPrecision + rightHandPrecision + headPrecision) / 3;
        }
        /**
        <summary>Calculates a precision score for an <c>Orientation</c> instance.</summary>
        <param name="orientation">The <c>Orientation</c> instance to be evaluated.</param>
        <param name="positionBounds">The <c>Bounds</c> range for the position.</param>
        <param name="rotationBounds">The <c>Bounds</c> range for the rotation.</param>
        <returns>The score for the <c>Orientation</c>, in the range between 0 and 1.</returns>
        */
        public float Precision(Orientation orientation, Bounds positionBounds, Bounds rotationBounds)
        {
            var positionSample = positionBounds.Sample(orientation.Position);
            var rotationSample = rotationBounds.Sample(orientation.Rotation.eulerAngles);

            var positionScore = Mathf.Abs(positionSample.x) + Mathf.Abs(positionSample.y) + Mathf.Abs(positionSample.z);
            var rotationScore = Mathf.Abs(rotationSample.x) + Mathf.Abs(rotationSample.y) + Mathf.Abs(rotationSample.z);

            return 1-((positionScore + rotationScore) / 6);
        }
    }

    #region ScriptableObject Implementation
    // [CreateAssetMenu(fileName="New Pose", menuName="EdKit/Pose")]
    public partial class PoseData: ScriptableObject {}
    #endregion

    #region IPose Implementation
    public partial class PoseData: IPose
    {
        /**
        <inheritdoc cref="IEventObject{Placement}.Name"/>
        */
        public string Name => name;
        /**
        <inheritdoc cref="IPose.Evaluate"/>
        */
        public float Evaluate(Placement placement)
        {
            return Pose.Evaluate(placement);
        }
    }
    #endregion
}