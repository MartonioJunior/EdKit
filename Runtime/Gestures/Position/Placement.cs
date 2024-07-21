using UnityEngine;

namespace MartonioJunior.EdKit
{
    [System.Serializable]
    public partial struct Placement
    {
        // MARK: Variables
        [SerializeField] Orientation leftHand;
        [SerializeField] Orientation rightHand;
        [SerializeField] Orientation head;

        // MARK: Properties
        public Orientation LeftHand => leftHand;
        public Orientation RightHand => rightHand;
        public Orientation Head => head;

        // MARK: Initializers
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
        public override string ToString()
        {
            return $"| Left Hand: {leftHand}\n| Right Hand: {rightHand}\n| Head: {head}";
        }
    }
    #endregion

    #region Transform
    public static partial class TransformExtensions
    {
        public static Placement EdKitPlacement(this Transform self, Transform leftHand, Transform rightHand, Transform head)
        {
            Vector3 forward = new Vector3(head.forward.x, 0.0f, head.forward.z).normalized;

            return self.OrientationFrom(forward: forward).PlacementFrom(leftHand, rightHand, head);
        }
    }
    #endregion
}