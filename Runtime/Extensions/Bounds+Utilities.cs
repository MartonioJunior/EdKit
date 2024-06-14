using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class BoundsExtensions
    {
        public static Vector3 Sample(this Bounds self, Vector3 point)
        {
            var delta = point - self.center;

            return new Vector3(
                delta.x.SafeDivideBy(self.extents.x, delta.x),
                delta.y.SafeDivideBy(self.extents.y, delta.y),
                delta.z.SafeDivideBy(self.extents.z, delta.z)
            );
        }

        public static Axis SampleAxis(this Bounds self, Vector3 point)
        {
            var sampledVector = self.Sample(point);

            return new Axis(sampledVector);
        }

        public static Bounds WithInfiniteExtents(this Bounds self)
        {
            return new Bounds(self.center, Vector3.one * float.PositiveInfinity);
        }

        public static Bounds WithNoExtents(this Bounds self)
        {
            return new Bounds(self.center, Vector3.zero);
        }
    }
}