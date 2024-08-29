namespace MartonioJunior.EdKit
{
    public static partial class DoubleExtensions
    {
        /**
        <summary>Division that returns a fallback value when a division by 0 happens.</summary>
        <param name="value">The value to divide by.</param>
        <param name="fallback">The value to return when a division by 0 happens.</param>
        <returns>The division result or the fallback value.</returns>
        */
        public static float SafeDivideBy(this float self, float value, float fallback = 0.0f)
        {
            return value == 0.0f ? fallback : self / value;
        }
    }
}