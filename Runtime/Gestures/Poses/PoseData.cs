using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial class PoseData
    {
        // MARK: Variables
        [SerializeField] Placement placement;

        // MARK: Properties
        public Placement Placement
        {
            get => placement;
            set => placement = value;
        }

        public Pose Pose => new(
            name: name,
            p => {
                return (
                    placement.Head.Position.PrecisionFor(p.Head.Position.Value)
                    + placement.LeftHand.Position.PrecisionFor(p.LeftHand.Position.Value)
                    + placement.RightHand.Position.PrecisionFor(p.RightHand.Position.Value)
                ) / 3;
            }
        );
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