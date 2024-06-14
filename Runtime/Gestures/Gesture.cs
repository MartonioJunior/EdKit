using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    public partial struct Gesture
    {
        // MARK: Variables
        string name;
        Func<IList<PoseEvent>, float> scoreFunction;

        // MARK: Initializers
        public Gesture(string name, Func<IList<PoseEvent>, float> scoreFunction)
        {
            this.name = name;
            this.scoreFunction = scoreFunction;
        }

        public Gesture(string name, IList<Func<PoseEvent, float>> scoreFunctions)
        {
            this.name = name;
            this.scoreFunction = (events) => {
                int numberOfChecks = scoreFunctions.Count;
                float score = 0;

                if (numberOfChecks == 0) return 0;

                for (int i = 0; i < numberOfChecks; i++) {
                    score += scoreFunctions[^i](events[^i]);
                }

                return score / numberOfChecks;
            };
        }
    }

    #region IGesture Implementation
    public partial struct Gesture: IGesture
    {
        public string Name => name;

        public float Evaluate(IList<PoseEvent> events)
        {
            return scoreFunction(events);
        }
    }
    #endregion
}