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
    }
}