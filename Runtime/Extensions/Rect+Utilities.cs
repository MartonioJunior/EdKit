using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class RectExtensions
    {
        public static Rect Sample(this Rect self, float xNormal, float yNormal, float widthNormal, float heightNormal)
        {
            var width = self.width;
            var height = self.height;

            return new Rect(self.x + (width * xNormal), self.y + (height * yNormal), width * widthNormal, height * heightNormal);
        }
    }
}