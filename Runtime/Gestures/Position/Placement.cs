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
            return $"Left Hand: {leftHand}, Right Hand: {rightHand}, Head: {head}";
        }
    }
    #endregion

    #region Transform
    public partial struct Placement
    {
        public static Placement From(Transform origin, Transform leftHand, Transform rightHand, Transform head)
        {
            Vector3 forward = new Vector3(head.forward.x, 0.0f, head.forward.z).normalized;
            Vector3 up = origin.up;

            var bodyOrientation = new Orientation(
                new Vector3(origin.position.x, origin.position.y, origin.position.z),
                Quaternion.LookRotation(forward, up)
            );

            return new Placement(
                bodyOrientation.Place(leftHand),
                bodyOrientation.Place(rightHand),
                bodyOrientation.Place(head)
            );
        }
    }
    #endregion
}