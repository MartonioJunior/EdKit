using System;

namespace MartonioJunior.EdKit
{
    public static partial class Global
    {
        public static float ScoreComparisons(params bool[] comparisons) {
            return comparisons.Reduce(0, (a, b) => {
                return a + Convert.ToInt32(b);
            }) / comparisons.Length;
        }
    }
}