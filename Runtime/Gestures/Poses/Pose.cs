using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial struct Pose
    {
        // MARK: Variables
        [SerializeField] string name;
        [SerializeField] Func<Placement,float> scoreFunction;

        // MARK: Delegates
        public delegate float ScoreFunction(Placement placement);

        // MARK: Initializers
        public Pose(string name, Func<Placement,float> scoreFunction)
        {
            this.name = name;
            this.scoreFunction = scoreFunction;
        }

        // MARK: Static Methods
        public static IPose Evaluate(Placement placement, IList<IPose> poses)
        {
            return poses.Max(p => p.Evaluate(placement));
        }
    }

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
}