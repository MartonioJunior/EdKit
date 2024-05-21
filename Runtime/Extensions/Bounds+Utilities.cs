using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class BoundsExtensions
    {
        public static Vector3 Sample(this Bounds self, Vector3 point)
        {
            var delta = point - self.center;

            return new Vector3(
                Mathf.Abs(delta.x / self.extents.x),
                Mathf.Abs(delta.y / self.extents.y),
                Mathf.Abs(delta.z / self.extents.z)
            );
        }
    }
}