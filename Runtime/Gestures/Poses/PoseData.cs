using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial class PoseData
    {
        // MARK: Variables
        [SerializeField] Bounds positionBoundsLeftHand;
        [SerializeField] Bounds rotationBoundsLeftHand;
        [SerializeField] Bounds positionBoundsRightHand;
        [SerializeField] Bounds rotationBoundsRightHand;
        [SerializeField] Bounds positionBoundsHead;
        [SerializeField] Bounds rotationBoundsHead;

        // MARK: Properties
        public Placement Placement
        {
            get => new Placement(
                new Orientation(positionBoundsLeftHand.center, Quaternion.Euler(rotationBoundsLeftHand.center)),
                new Orientation(positionBoundsRightHand.center, Quaternion.Euler(rotationBoundsRightHand.center)),
                new Orientation(positionBoundsHead.center, Quaternion.Euler(rotationBoundsHead.center))
            );
            set {
                positionBoundsLeftHand.center = value.LeftHand.Position;
                rotationBoundsLeftHand.center = value.LeftHand.Rotation.eulerAngles;
                positionBoundsRightHand.center = value.RightHand.Position;
                rotationBoundsRightHand.center = value.RightHand.Rotation.eulerAngles;
                positionBoundsHead.center = value.Head.Position;
                rotationBoundsHead.center = value.Head.Rotation.eulerAngles;
            }
        }

        public Pose Pose => new(
            name: name,
            p => {
                return Precision(p.LeftHand, positionBoundsLeftHand, rotationBoundsLeftHand)
                    + Precision(p.RightHand, positionBoundsRightHand, rotationBoundsRightHand)
                    + Precision(p.Head, positionBoundsHead, rotationBoundsHead);
            }
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

            return (positionScore + rotationScore) / 6;
        }
    }

    #region ScriptableObject Implementation
    [CreateAssetMenu(fileName="New Pose", menuName="EdKit/Pose")]
    public partial class PoseData: ScriptableObject
    {
        
    }
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