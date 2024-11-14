using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure to implement a pose via scripting using a scoring function.</summary>
    */
    public partial struct Pose
    {
        // MARK: Variables
        /**
        <summary>Name of the pose.</summary>
        */
        [SerializeField] string name;
        /**
        <summary>Function used for scoring the pose.</summary>
        */
        [SerializeField] ScoreFunction scoreFunction;

        // MARK: Delegates
        /**
        <summary>Delegate used to score a pose.</summary>
        <param name="placement">The <c>Placement</c> instance to be scored.</param>
        <returns>The score of the pose.</returns>
        */
        public delegate float ScoreFunction(Placement placement);

        // MARK: Initializers
        /**
        <summary>Initializes a new <c>Pose</c> with a name and score function.</summary>
        <param name="name">The name of the pose.</param>
        <param name="scoreFunction">The function used to score the pose.</param>
        */
        public Pose(string name, ScoreFunction scoreFunction)
        {
            this.name = name;
            this.scoreFunction = scoreFunction;
        }
    }

    #region ToString
    public partial struct Pose
    {
        /**
        <summary>Converts the pose to a textual representation.</summary>
        <returns>The name of the pose.</returns>
        */
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    #endregion

    #region IPose Implementation
    public partial struct Pose: IPose
    {
        /**
        <inheritdoc cref="IEventObject{Placement}.Name"/>
        */
        public string Name => name;
        /**
        <inheritdoc cref="IPose.Evaluate(Placement)"/>
        */
        public float Evaluate(Placement placement)
        {
            return scoreFunction(placement);
        }
    }
    #endregion

    #region IEnumerable<IPose>
    public static partial class IEnumerableExtensions
    {
        /**
        <summary>Identifies which pose most closely resembles a given <c>Placement</c> instance.</summary>
        <param name="placement">The <c>Placement</c> instance to be evaluated.</param>
        <returns>The highest-scoring pose for <c>Placement</c>.</returns>
        */
        public static IPose Evaluate(this IEnumerable<IPose> self, Placement placement)
        {
            return self.Max(p => p.Evaluate(placement));
        }
    }
    #endregion
}