using UnityEngine;

namespace MartonioJunior.EdKit
{
    [System.Serializable]
    public partial struct Orientation
    {
        // MARK: Variables
        [SerializeField] Position position;
        [SerializeField] Rotation rotation;

        // MARK: Properties
        public Position Position => position;
        public Rotation Rotation => rotation;

        // MARK: Initializers
        public Orientation(Position position, Rotation rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }

    #region ToString
    public partial struct Orientation
    {
        public override string ToString()
        {
            return $"Position: {position}, Rotation: {rotation}";
        }
    }
    #endregion

    #region Gizmos Support
    public partial struct Orientation
    {
        public void DrawGizmos(Transform origin)
        {
            position.DrawGizmos(origin);
            rotation.DrawGizmos(origin);
        }
    }
    #endregion

    #region Transform
    public partial struct Orientation
    {
        public Orientation Place(Transform transform, Vector3 offset)
        {
            var delta = transform.position - position.Value + offset;
            delta.x *= -1.0f;

            var relativePosition = new Position(delta.InverseRelativeTo(rotation.Value * Vector3.up, rotation.Value * Vector3.forward));
            var relativeRotation = new Rotation(Quaternion.Inverse(rotation.Value) * transform.rotation);

            return new Orientation(relativePosition, relativeRotation);
        }
    }
    #endregion
}