using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure that represents a spatial reference in space.</summary>
    */
    [Serializable]
    public partial struct Orientation
    {
        // MARK: Variables
        /**
        <summary>Position of this reference in relation to it's origin space.</summary>
        */
        [SerializeField] Vector3 relativePosition;
        /**
        <summary>Rotation of this reference in relation to it's origin space.</summary>
        */
        [SerializeField] Quaternion relativeRotation;
        /**
        <summary><c>Orientation</c> that is aligned with the origin.</summary>
        */
        public static readonly Orientation identity = new Orientation(Vector3.zero, Quaternion.identity);

        // MARK: Properties
        /**
        <inheritdoc cref="Orientation.relativePosition"/>
        */
        public Vector3 Position => relativePosition;
        /**
        <inheritdoc cref="Orientation.relativeRotation"/>
        */
        public Quaternion Rotation => relativeRotation;

        // MARK: Initializers
        /**
        <summary>Initializer that creates a new <c>Orientation</c> with the given position and rotation values.</summary>
        <param name="position">The position of the spatial reference.</param>
        <param name="rotation">The rotation of the spatial reference.</param>
        */
        public Orientation(Vector3 position, Quaternion rotation)
        {
            relativePosition = position;
            relativeRotation = rotation;
        }

        // MARK: Methods
        /**
        <summary>Compares the position with another <c>Orientation</c> within a defined <c>Bounds</c> range.</summary>
        <param name="other">The spatial reference to be compared against.</param>
        <param name="bounds">The bounding box defined for the comparison.</param>
        <returns>The result of the comparison in all three Axis.</returns>
        <remarks>
        When no value is supplied to the <c>other</c> parameter, the comparison is made against the origin.<br/>
        When no value is supplied to the <c>bounds</c> parameter, the comparison is made against a unitary bounding box.
        </remarks>
        */
        public Axis ComparePosition(Orientation? other = null, Bounds? bounds = null)
        {
            var boundsOrDefault = bounds ?? new Bounds(Vector3.zero, Vector3.one);
            var otherOrDefault = other ?? identity;

            var delta = otherOrDefault.NormalizedPositionIn(boundsOrDefault) - NormalizedPositionIn(boundsOrDefault);
            return new Axis(delta, Vector3.one);
        }

        public Axis ComparePositionTo(Orientation? other = null, Vector3? offset = null, Vector3? scale = null)
        {
            var offsetVector = offset ?? Vector3.zero;
            var scaleVector = scale ?? Vector3.one;
            return ComparePosition(other, new Bounds(offsetVector, scaleVector));
        }
        /**
        <summary>Compares the rotation with another <c>Orientation</c> within a defined <c>Bounds</c> range.</summary>
        <param name="other">The spatial reference to be compared against.</param>
        <param name="bounds">The bounding box defined for the comparison.</param>
        <returns>The result of the comparison in all three Axis.</returns>
        <remarks>
        When no value is supplied to the <c>other</c> parameter, the comparison is made against the origin.<br/>
        When no value is supplied to the <c>bounds</c> parameter, the comparison is made against a unitary bounding box.
        */
        public Axis CompareRotation(Orientation? other = null, Bounds? bounds = null)
        {
            var boundsOrDefault = bounds ?? new Bounds(Vector3.zero, Vector3.one);
            var otherOrDefault = other ?? identity;

            var delta = otherOrDefault.NormalizedRotationIn(boundsOrDefault) - NormalizedRotationIn(boundsOrDefault);
            return new Axis(delta, Vector3.one);
        }

        public Axis CompareRotationTo(Orientation? other = null, Vector3? offset = null, Vector3? scale = null)
        {
            var offsetVector = offset ?? Vector3.zero;
            var scaleVector = scale ?? Vector3.one;
            return CompareRotation(other, new Bounds(offsetVector, scaleVector));
        }
        /**
        <summary>Normalizes the orientation's position in relation to a <c>Bounds</c> value range.</summary>
        <param name="positionBounds">The bounding box defined for normalization.</param>
        <returns>The normalized position in relation to the bounding box.</returns>
        */
        public Vector3 NormalizedPositionIn(Bounds positionBounds)
        {
            return positionBounds.Sample(relativePosition);
        }
        /**
        <summary>Normalizes the orientation's rotation in relation to a <c>Bounds</c> value range.</summary>
        <param name="rotationBounds">The bounding box defined for normalization.</param>
        <returns>The normalized rotation in relation to the bounding box.</returns>
        */
        public Vector3 NormalizedRotationIn(Bounds rotationBounds)
        {
            return rotationBounds.Sample(relativeRotation.eulerAngles);
        }
        /**
        <summary>Places a transform in relation to this <c>Orientation</c>.</summary>
        <param name="transform">The transform to be placed.</param>
        <param name="offsetPosition">The offset position to be applied to the transform.</param>
        <param name="offsetRotation">The offset rotation to be applied to the transform.</param>
        <returns>The new <c>Orientation</c> of the transform, representing the position and rotation in relation to this spatial reference.</returns>
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
        /**
        <summary>Creates a new <c>Placement</c> for a Virtual Reality setup with hands and head.</summary>
        <param name="leftHand">The left hand transform.</param>
        <param name="rightHand">The right hand transform.</param>
        <param name="head">The head transform.</param>
        <returns>The new <c>Placement</c> instance composed of values in relation to this reference.</returns>
        */
        public Placement PlacementFrom(Transform leftHand, Transform rightHand, Transform head)
        {
            return new Placement(Place(leftHand), Place(rightHand), Place(head));
        }
    }

    #region ISerializable Reference
    public partial struct Orientation: ISerializable
    {
        public Orientation(SerializationInfo info, StreamingContext context)
        {
            relativePosition = (Vector3)info.GetValue("relativePosition", typeof(Vector3));
            relativeRotation = (Quaternion)info.GetValue("relativeRotation", typeof(Quaternion));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            object position = new {x = relativePosition.x, y = relativePosition.y, z = relativePosition.z};
            object rotation = new {x = relativeRotation.x, y = relativeRotation.y, z = relativeRotation.z, w = relativeRotation.w};
            info.AddValue("relativePosition", position);
            info.AddValue("relativeRotation", rotation);
        }
    }
    #endregion

    #region ToString
    public partial struct Orientation
    {
        /**
        <summary>Returns the position and rotation in a representable text.</summary>
        */
        public override string ToString()
        {
            return $"Pos. {relativePosition}, Rot. {relativeRotation}";
        }
    }
    #endregion

    #region Transform
    public static partial class TransformExtensions
    {
        /**
        <summary>Defines a new <c>Orientation</c> based on two reference vectors for rotation.</summary>
        <param name="forward">The forward vector to be used as reference.</param>
        <param name="up">The up vector to be used as reference.</param>
        <returns>The new <c>Orientation</c> based on the given vectors.</returns>
        <remarks>
        The position of the transform is unchanged from the origin, only the rotation is modified.<br/>
        If reference vectors aren't supplied, the function uses the forward and up vectors of the transform instead.
        </remarks>
        */
        public static Orientation OrientationFrom(this Transform self, Vector3? forward = null, Vector3? up = null)
        {
            Vector3 expectedUp = up ?? self.up;
            Vector3 expectedForward = forward ?? self.forward;

            return new Orientation(self.position, Quaternion.LookRotation(expectedForward, expectedUp));
        }
    }
    #endregion
}