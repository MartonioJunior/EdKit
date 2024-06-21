using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class AxisExtensions
    {
        public static AffordanceEffect Effect(this Axis.X self, float duration, float scale = 0.5f)
        {
            return new(self.Sign(), scale, duration);
        }

        public static AffordanceEffect Effect(this Axis.Y self, float duration, float scale = 0.5f)
        {
            return new(self.Sign(), scale, duration);
        }

        public static AffordanceEffect Effect(this Axis.Z self, float duration, float scale = 0.5f)
        {
            return new(self.Sign(), scale, duration);
        }
    }
}