using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public static partial class IEnumerableExtensions
    {
        /*
        <summary>Finds the element that evaluates to the greatest value.</summary>
        <param name="evaluator">The function that rates an element.</param>
        <returns>First element that evaluates to the greatest value with the <see cref="evaluator"/> </returns>
        */
        public static T Max<T>(this IEnumerable<T> self, Func<T, float> evaluator)
        {
            if (evaluator is null) return default;

            float currentMaxValue = float.MinValue;

            return self.Reduce(default(T), (current, next) => {
                var value = evaluator(next);

                if (value > currentMaxValue) {
                    currentMaxValue = value;
                    return next;
                }
                
                return current;
            });
        }
        /**
        <summary>Encapsulates a collection into a single value.</summary>
        <param name="initial">The initial value.</param>
        <param name="reduce">The function that reduces the value.</param>
        <returns>The reduced value.</returns>
        */
        public static N Reduce<T, N>(this IEnumerable<T> self, N initial, Func<N, T, N> reduce)
        {
            if (reduce is null) return initial;

            foreach (var item in self) {
                initial = reduce(initial, item);
            }

            return initial;
        }
    }
}