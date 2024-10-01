using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    #region Alias Classes
    /**
    <summary><c>EventEvaluator</c> that interprets a list of pose events to identify gestures.</summary>
    */
    [Serializable]
    public sealed class GestureEvaluator: EventEvaluator<IGesture, List<Event<IPose, Placement>>>
    {
        /**
        <inheritdoc cref="EventEvaluator{IGesture, List{Event{IPose, Placement}}}.EventEvaluator()"/>
        */
        public GestureEvaluator(): base() {}
        /**
        <inheritdoc cref="EventEvaluator{IGesture, List{Event{IPose, Placement}}}.EventEvaluator(IEnumerable{IGesture}, float)"/>
        */
        public GestureEvaluator(IEnumerable<IGesture> gestures, float scoreThreshold = 0.8f): base(gestures, scoreThreshold) {}
    }

    /**
    <summary><c>EventEvaluator</c> that interprets a <c>Placement</c> to identify poses.</summary>
    */
    [Serializable]
    public sealed class PoseEvaluator: EventEvaluator<IPose, Placement>
    {
        /**
        <inheritdoc cref="EventEvaluator{IPose, Placement}.EventEvaluator()"/>
        */
        public PoseEvaluator() {}
        /**
        <inheritdoc cref="EventEvaluator{IPose, Placement}.EventEvaluator(IEnumerable{IPose}, float)"/>
        */
        public PoseEvaluator(IEnumerable<IPose> poses, float scoreThreshold = 0.8f): base(poses, scoreThreshold) {}
    }
    #endregion

    /**
    <summary>Score-based evaluator for events.</summary>
    <typeparam name="T">Type of the event object.</typeparam>
    <typeparam name="D">Type of the data object.</typeparam>
    */
    [Serializable]
    public partial class EventEvaluator<T,D> where T: IEventObject<D>
    {
        // MARK: Variables
        /**
        <summary>Minimum score required to consider an event as valid.</summary>
        */
        [Tooltip("Minimum score to consider an event as valid.")]
        [SerializeField, Range(0,1)] float scoreThreshold = 0.8f;
        /**
        <summary>Collection containing all objects that can be used to identify the date.</summary>
        */
        ISet<T> allPossibleElements = new HashSet<T>();

        // MARK: Initializers
        /**
        <summary>Creates an empty evaluator.</summary>
        */
        public EventEvaluator() {}
        /**
        <summary>Creates an evaluator with a collection of elements.</summary>
        <param name="elements">Collection of elements to be used in the evaluation.</param>
        <param name="scoreThreshold">Minimum score required to consider an event as valid.</param>
        */
        public EventEvaluator(IEnumerable<T> elements, float scoreThreshold = 0.8f)
        {
            this.scoreThreshold = scoreThreshold;

            foreach (T element in elements) {
                Register(element);
            }
        }

        // MARK: Methods
        /**
        <summary>Evaluates the data and returns an event if the score for any element is above the threshold.</summary>
        <param name="data">Data to be evaluated.</param>
        <param name="timestamp">Timestamp for the event.</param>
        <returns><c>Event</c> if the score is above the threshold, null otherwise.</returns>
        */
        public Event<T, D>? Evaluate(D data, float timestamp) => Event.From(data, allPossibleElements, timestamp, scoreThreshold);
        /**
        <summary>Registers an element to be identified in the evaluation.</summary>
        <param name="element">Element to be registered.</param>
        */
        public void Register(T element) => allPossibleElements.Add(element);
        /**
        <summary>Removes an element from the evaluation process.</summary>
        <param name="element">Element to be removed.</param>
        */
        public void Unregister(T element) => allPossibleElements.Remove(element);
    }

    #region UnityEditor Support
    #if UNITY_EDITOR
    public partial class EventEvaluator<T,D>
    {
        public ISet<T> AllPossibleElements => allPossibleElements;
    }
    #endif
    #endregion
}