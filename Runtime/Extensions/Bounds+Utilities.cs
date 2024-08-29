using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class BoundsExtensions
    {
        /**
        <summary>Translates a point in space using a <c>Bounds</c> as an origin reference.</summary>
        <param name="point">The point to be translated.</param>
        <returns>
        The translated point, with values scaled in relation to the bounding box: <br/>
        * Positive values are to the right of the bounds origin. <br/>
        * Negative values are to the left of the bounds origin. <br/>
        * Values between -1 and 1 (not including them) are inside the bounds. <br/>
        * Values greater than 1 or lesser than -1 are outside the bounds. <br/>
        * -1 and 1 means that the point is exactly on an edge.
        </returns>
        */
        public static Vector3 Sample(this Bounds self, Vector3 point)
        {
            var delta = point - self.center;

            return new Vector3(
                delta.x.SafeDivideBy(self.extents.x, delta.x),
                delta.y.SafeDivideBy(self.extents.y, delta.y),
                delta.z.SafeDivideBy(self.extents.z, delta.z)
            );
        }
        /**
        <summary>Translates a point in space to an <c>Axis</c> data structure.</summary>
        <param name="point">The point to be translated.</param>
        <returns>The axis that represents the point in relation to the bounds.</returns>
        <remarks>Any values inside or on an edge.</remarks>
        */
        public static Axis SampleAxis(this Bounds self, Vector3 point)
        {
            var sampledVector = self.Sample(point);

            return new Axis(sampledVector);
        }
        /**
        <summary>Returns a bounds with the same center, but infinite extents.</summary>
        <returns>A bounds with infinite extents.</returns>
        */
        public static Bounds WithInfiniteExtents(this Bounds self)
        {
            return new Bounds(self.center, Vector3.one * float.PositiveInfinity);
        }
        /**
        <summary>Returns a bounds with the same center, but no extents.</summary>
        <returns>A bounds with no extents.</returns>
        */
        public static Bounds WithNoExtents(this Bounds self)
        {
            return new Bounds(self.center, Vector3.zero);
        }
    }
}