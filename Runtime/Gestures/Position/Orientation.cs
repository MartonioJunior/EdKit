using System;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    [Serializable]
    public partial struct Orientation
    {
        // MARK: Variables
        [SerializeField] Vector3 relativePosition;
        [SerializeField] Quaternion relativeRotation;
        public static readonly Orientation identity = new Orientation(Vector3.zero, Quaternion.identity);

        // MARK: Properties
        public Vector3 Position => relativePosition;
        public Quaternion Rotation => relativeRotation;

        // MARK: Initializers
        public Orientation(Vector3 position, Quaternion rotation)
        {
            relativePosition = position;
            relativeRotation = rotation;
        }

        // MARK: Methods
        public Axis ComparePositionTo(Orientation other, Bounds bounds)
        {
            var delta = other.NormalizedPositionIn(bounds) - NormalizedPositionIn(bounds);
            return new Axis(delta, Vector3.one);
        }

        public Axis CompareRotationTo(Orientation other, Bounds bounds)
        {
            var delta = other.NormalizedRotationIn(bounds) - NormalizedRotationIn(bounds);
            return new Axis(delta, Vector3.one);
        }

        public Vector3 NormalizedPositionIn(Bounds positionBounds)
        {
            return positionBounds.Sample(relativePosition);
        }

        public Vector3 NormalizedRotationIn(Bounds rotationBounds)
        {
            return rotationBounds.Sample(relativeRotation.eulerAngles);
        }

        public Orientation Place(Transform transform)
        {
            return Place(transform, Vector3.zero, Quaternion.identity);
        }
        /**
        <summary>Places a transform in relation to another Transform.</summary>
        */
        public Orientation Place(Transform transform, Vector3 offsetPosition, Quaternion offsetRotation)
        {
            var delta = transform.position - this.relativePosition + offsetPosition;

            var relativePosition = Quaternion.Inverse(this.relativeRotation) * delta;
            var relativeRotation = Quaternion.Inverse(this.relativeRotation) * transform.rotation * offsetRotation;

            return new Orientation(relativePosition, relativeRotation);
        }
    }

    #region ToString
    public partial struct Orientation
    {
        public override string ToString()
        {
            return $"Position: {relativePosition}, Rotation: {relativeRotation}";
        }
    }
    #endregion
}