using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class Vector3Extensions
    {
        public static float Dot(this Vector3 lhs, Vector3 rhs) => Vector3.Dot(lhs, rhs);

        public static Vector3 InverseRelativeTo(this Vector3 vector, Vector3 up, Vector3 forward)
        {
            var right = Vector3.Cross(forward, up);
            return new Vector3(vector.Dot(right), vector.Dot(up), vector.Dot(forward));
        }
    }
}