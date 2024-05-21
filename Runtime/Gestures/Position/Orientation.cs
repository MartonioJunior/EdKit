using System;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    [Serializable]
    public partial struct Orientation
    {
        // MARK: Variables
        [SerializeField] Vector3 position;
        [SerializeField] Quaternion rotation;
        public static readonly Orientation identity = new Orientation(Vector3.zero, Quaternion.identity);

        // MARK: Properties
        public Vector3 Position => position;
        public Quaternion Rotation => rotation;

        // MARK: Initializers
        public Orientation(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        // MARK: Methods
        public Vector3 NormalizedPositionIn(Bounds positionBounds)
        {
            return positionBounds.Sample(position);
        }

        public Vector3 NormalizedRotationIn(Bounds rotationBounds)
        {
            return rotationBounds.Sample(rotation.eulerAngles);
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
            var delta = transform.position - position + offsetPosition;

            var relativePosition = Quaternion.Inverse(rotation) * delta;
            var relativeRotation = Quaternion.Inverse(rotation) * transform.rotation * offsetRotation;

            return new Orientation(relativePosition, relativeRotation);
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
}