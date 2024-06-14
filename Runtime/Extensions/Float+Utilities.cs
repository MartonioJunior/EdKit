using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class DoubleExtensions
    {
        public static float SafeDivideBy(this float self, float value, float fallback = 0.0f)
        {
            return value == 0.0f ? fallback : self / value;
        }
    }
}