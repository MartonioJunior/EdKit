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
        public Axis ComparePosition(Orientation? other = null, Bounds? bounds = null)
        {
            var boundsOrDefault = bounds ?? new Bounds(Vector3.zero, Vector3.one);
            var otherOrDefault = other ?? identity;

            var delta = otherOrDefault.NormalizedPositionIn(boundsOrDefault) - NormalizedPositionIn(boundsOrDefault);
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
        /**
        <summary>Places a transform in relation to another Transform.</summary>
        */
        public Orientation Place(Transform transform, Vector3? offsetPosition = null, Quaternion? offsetRotation = null)
        {
            var offsetPositionOrDefault = offsetPosition ?? Vector3.zero;
            var offsetRotationOrDefault = offsetRotation ?? Quaternion.identity;

            var delta = transform.position - this.relativePosition + offsetPositionOrDefault;

            var relativePosition = Quaternion.Inverse(this.relativeRotation) * delta;
            var relativeRotation = Quaternion.Inverse(this.relativeRotation) * transform.rotation * offsetRotationOrDefault;

            return new Orientation(relativePosition, relativeRotation);
        }

        public Placement PlacementFrom(Transform leftHand, Transform rightHand, Transform head)
        {
            return new Placement(Place(leftHand), Place(rightHand), Place(head));
        }
    }

    #region ToString
    public partial struct Orientation
    {
        public override string ToString()
        {
            return $"Pos. {relativePosition}, Rot. {relativeRotation}";
        }
    }
    #endregion

    #region Transform
    public static partial class TransformExtensions
    {
        public static Orientation OrientationFrom(this Transform self, Vector3? forward = null, Vector3? up = null)
        {
            Vector3 expectedUp = up ?? self.up;
            Vector3 expectedForward = forward ?? self.forward;

            return new Orientation(self.position, Quaternion.LookRotation(expectedForward, expectedUp));
        }
    }
    #endregion
}