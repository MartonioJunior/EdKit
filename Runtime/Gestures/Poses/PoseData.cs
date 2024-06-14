using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial class PoseData
    {
        // MARK: Variables
        [Header("Left Hand")]
        [SerializeField] Bounds positionBoundsLeftHand;
        [SerializeField] Bounds rotationBoundsLeftHand;
        [Header("Right Hand")]
        [SerializeField] Bounds positionBoundsRightHand;
        [SerializeField] Bounds rotationBoundsRightHand;
        [Header("Head")]
        [SerializeField] Bounds positionBoundsHead;
        [SerializeField] Bounds rotationBoundsHead;

        // MARK: Properties
        public Pose Pose => new(
            name: name,
            p => Precision(p)
        );

        // MARK: Methods
        public float Precision(Placement placement)
        {
            var leftHandPrecision = Precision(placement.LeftHand, positionBoundsLeftHand, rotationBoundsLeftHand);
            var rightHandPrecision = Precision(placement.RightHand, positionBoundsRightHand, rotationBoundsRightHand);
            var headPrecision = Precision(placement.Head, positionBoundsHead, rotationBoundsHead);

            return (leftHandPrecision + rightHandPrecision + headPrecision) / 3;
        }

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
    [CreateAssetMenu(fileName="New Pose", menuName="EdKit/Pose")]
    public partial class PoseData: ScriptableObject {}
    #endregion

    #region IPose Implementation
    public partial class PoseData: IPose
    {
        public string Name => name;

        public float Evaluate(Placement placement)
        {
            return Pose.Evaluate(placement);
        }
    }
    #endregion
}