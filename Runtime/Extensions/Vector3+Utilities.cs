using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class Vector3Extensions
    {
        /**
        <summary>Dot product between vectors.</summary>
        <param name="lhs">The left-hand side vector.</param>
        <param name="rhs">The right-hand side vector.</param>
        <returns>The dot product between the vectors.</returns>
        <remarks>Same as <c>Vector3.Dot</c>.</remarks>
        */
        public static float Dot(this Vector3 lhs, Vector3 rhs) => Vector3.Dot(lhs, rhs);
        /**
        <summary>Returns the inverse relative vector, result of a dot product between the vector and reference axis..</summary>
        <param name="self">The vector to calculate it's inverse relative.</param>
        <param name="up">The up axis.</param>
        <param name="forward">The forward axis.</param>
        <returns>The inverse relative vector.</returns>
        */
        public static Vector3 InverseRelativeTo(this Vector3 self, Vector3 up, Vector3 forward)
        {
            var right = Vector3.Cross(forward, up);
            return new Vector3(self.Dot(right), self.Dot(up), self.Dot(forward));
        }
    }
}