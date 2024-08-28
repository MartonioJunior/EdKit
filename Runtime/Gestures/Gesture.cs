using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure to implement a gesture via a scoring function.</summary>
    */
    public partial struct Gesture
    {
        // MARK: Variables
        /**
        <summary>Name of the gesture.</summary>
        */
        string name;
        /**
        <summary>Function used for scoring the gesture.</summary>
        */
        Func<IList<Event<IPose, Placement>>, float> scoreFunction;

        // MARK: Delegates
        /**
        <summary>Delegate used to score a gesture.</summary>
        <param name="poseEvent">The <c>Event</c> instance to be scored.</param>
        <returns>The score of the gesture.</returns>
        */
        public delegate float ScoreFunction(Event<IPose, Placement> poseEvent);

        // MARK: Initializers
        /**
        <summary>Initializes a new gesture with a scoring function based on a single scoring function.</summary>
        <param name="name">The name of the gesture.</param>
        <param name="scoreFunction">The function used to score the gesture.</param>
        */
        public Gesture(string name, Func<IList<Event<IPose, Placement>>, float> scoreFunction)
        {
            this.name = name;
            this.scoreFunction = scoreFunction;
        }
        /**
        <summary>Initializes a new gesture with a scoring function based on multiple scoring functions.</summary>
        <param name="name">The name of the gesture.</param>
        <param name="scoreFunctions">The functions used to score each pose event, in reverse order.</param>
        */
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
        /**
        <summary>Creates a new gesture from a sequence of scoring functions.</summary>
        <param name="name">The name of the gesture.</param>
        <param name="evaluations">The functions used to score each pose event, in reverse order.</param>
        <returns>The gesture created from the sequence of scoring functions.</returns>
        */
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
        /**
        <inheritdoc cref="IGesture.Name"/>
        */
        public string Name => name;
        /**
        <inheritdoc cref="IGesture.Evaluate(IList{Event{IPose, Placement}})"/>
        */
        public float Evaluate(IList<Event<IPose, Placement>> events)
        {
            return scoreFunction(events);
        }
    }
    #endregion
}