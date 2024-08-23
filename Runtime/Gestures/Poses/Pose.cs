using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial struct Pose
    {
        // MARK: Variables
        [SerializeField] string name;
        [SerializeField] ScoreFunction scoreFunction;

        // MARK: Delegates
        public delegate float ScoreFunction(Placement placement);

        // MARK: Initializers
        public Pose(string name, ScoreFunction scoreFunction)
        {
            this.name = name;
            this.scoreFunction = scoreFunction;
        }
    }

    #region ToString
    public partial struct Pose
    {
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    #endregion

    #region IPose Implementation
    public partial struct Pose: IPose
    {
        public string Name => name;

        public float Evaluate(Placement placement)
        {
            return scoreFunction(placement);
        }
    }
    #endregion

    #region IEnumerable Extensions
    public static partial class IEnumerableExtensions
    {
        public static IPose Evaluate(this IEnumerable<IPose> self, Placement placement)
        {
            return self.Max(p => p.Evaluate(placement));
        }
    }
    #endregion
}