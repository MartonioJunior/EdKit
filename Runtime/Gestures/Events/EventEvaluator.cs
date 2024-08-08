using System;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    #region Alias Classes
    [Serializable]
    public sealed class GestureEvaluator: EventEvaluator<IGesture, List<Event<IPose, Placement>>>
    {
        public GestureEvaluator() {}
        public GestureEvaluator(IEnumerable<IGesture> gestures, float scoreThreshold = 0.8f): base(gestures, scoreThreshold) {}
    }

    [Serializable]
    public sealed class PoseEvaluator: EventEvaluator<IPose, Placement>
    {
        public PoseEvaluator() {}
        public PoseEvaluator(IEnumerable<IPose> poses, float scoreThreshold = 0.8f): base(poses, scoreThreshold) {}
    }
    #endregion

    [Serializable]
    public partial class EventEvaluator<T,D> where T: IEventObject<D>
    {
        // MARK: Variables
        [SerializeField, Range(0,1)] float scoreThreshold = 0.8f;
        ISet<T> allPossibleElements = new HashSet<T>();

        // MARK: Initializers
        public EventEvaluator() {}

        public EventEvaluator(IEnumerable<T> elements, float scoreThreshold = 0.8f)
        {
            this.scoreThreshold = scoreThreshold;

            foreach (T element in elements) {
                Register(element);
            }
        }

        // MARK: Methods
        public Event<T, D>? Evaluate(D data, float timestamp) => Event.From(data, allPossibleElements, timestamp, scoreThreshold);
        public void Register(T element) => allPossibleElements.Add(element);
        public void Unregister(T gesture) => allPossibleElements.Remove(gesture);
    }
}