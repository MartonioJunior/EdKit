using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    public partial struct Gesture
    {
        // MARK: Variables
        string name;
        Func<IList<Event<IPose, Placement>>, float> scoreFunction;

        // MARK: Delegates
        public delegate float ScoreFunction(Event<IPose, Placement> poseEvent);

        // MARK: Initializers
        public Gesture(string name, Func<IList<Event<IPose, Placement>>, float> scoreFunction)
        {
            this.name = name;
            this.scoreFunction = scoreFunction;
        }

        public Gesture(string name, IList<Func<Event<IPose, Placement>, float>> scoreFunctions)
        {
            this.name = name;
            scoreFunction = (events) => {
                int numberOfChecks = scoreFunctions.Count;
                float score = 0;

                if (numberOfChecks == 0) return 0;

                for (int i = 0; i < numberOfChecks; i++) {
                    score += scoreFunctions[^i](events[^i]);
                }

                return score / numberOfChecks;
            };
        }

        // MARK: Methods
        public static Gesture FromSequence(string name, params Func<Event<IPose, Placement>,float>[] evaluations)
        {
            return new Gesture(name, (events) => {
                int numberOfChecks = evaluations.Length;
                float score = 0;

                if (numberOfChecks == 0) return 0;

                for (int i = 0; i < numberOfChecks; i++) {
                    score += evaluations[^i](events[^i]);
                }

                return score / numberOfChecks;
            });
        }
    }

    #region IGesture Implementation
    public partial struct Gesture: IGesture
    {
        public string Name => name;

        public float Evaluate(IList<Event<IPose, Placement>> events)
        {
            return scoreFunction(events);
        }
    }
    #endregion
}