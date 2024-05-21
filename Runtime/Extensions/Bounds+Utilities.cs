using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class BoundsExtensions
    {
        public static Vector3 Sample(this Bounds self, Vector3 point)
        {
            var delta = point - self.center;

            return new Vector3(
                delta.x / self.extents.x,
                delta.y / self.extents.y,
                delta.z / self.extents.z
            );
        }
    }
}