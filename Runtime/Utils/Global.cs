using System;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Static class that serves as a Global type that can be reused in other parts of the code.</summary>
    <remarks>It's recommended to be imported as a helper class: <c>using static MartonioJunior.EdKit.Global;</c>.</remarks>
    */
    public static partial class Global
    {
        /**
        <summary>Transforms a sequence of boolean comparisons into a score.</summary>
        <param name="comparisons">A sequence of boolean comparisons.</param>
        <returns>The average score for the comparisons.</returns>
        <remarks>Comparisons that are false have a score of 0, while comparisons that are true have a score of 1.</remarks>
        */
        public static float ScoreComparisons(params bool[] comparisons) {
            return comparisons.Reduce(0, (a, b) => {
                return a + Convert.ToInt32(b);
            }) / comparisons.Length;
        }
    }
}