using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Extensions for the <c>Axis</c> type.</summary>
    */
    public static partial class AxisExtensions
    {
        /**
        <summary>Creates an affordance effect with the alignment property based on the axis' signal value.</summary>
        <param name="self">Axis to be used.</param>
        <param name="duration">Duration of the effect.</param>
        <param name="scale">Scale of the effect.</param>
        <returns>A new affordance effect.</returns>
        */
        public static AffordanceEffect Effect(this Axis.X self, float duration, float scale = 0.5f)
        {
            return new(self.Sign(), scale, duration);
        }
        /**
        <inheritdoc cref="Effect(Axis.X, float, float)"/>
        */
        public static AffordanceEffect Effect(this Axis.Y self, float duration, float scale = 0.5f)
        {
            return new(self.Sign(), scale, duration);
        }
        /**
        <inheritdoc cref="Effect(Axis.X, float, float)"/>
        */
        public static AffordanceEffect Effect(this Axis.Z self, float duration, float scale = 0.5f)
        {
            return new(self.Sign(), scale, duration);
        }
    }
}