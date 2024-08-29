using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class RectExtensions
    {
        /**
        <summary>Samples an rectangle based on multipliers.</summary>
        <param name="xMultiplier">The multiplier for the x position.</param>
        <param name="yMultiplier">The multiplier for the y position.</param>
        <param name="widthMuliplier">The multiplier for the width.</param>
        <param name="heightMultiplier">The multiplier for the height.</param>
        <returns>The sampled rectangle.</returns>
        */
        public static Rect Sample(this Rect self, float xMultiplier, float yMultiplier, float widthMuliplier, float heightMultiplier)
        {
            var width = self.width;
            var height = self.height;

            return new Rect(self.x + (width * xMultiplier), self.y + (height * yMultiplier), width * widthMuliplier, height * heightMultiplier);
        }
    }
}