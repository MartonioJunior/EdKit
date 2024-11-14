using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure used to store events that are associated with an object.</summary>
    <typeparam name="T">Type of the object associated with the event.</typeparam>
    <typeparam name="D">Type of the data associated with the event.</typeparam>
    */
    [Serializable]
    public partial struct Event<T,D> where T: IEventObject<D>
    {
        // MARK: Variables
        /**
        <summary>Information about the event that happened with the object.</summary>
        */
        [SerializeField] D data;
        /**
        <summary>Object associated with this event.</summary>
        */
        [SerializeReference] T obj;
        /**
        <summary>Timestamp for when the event happened.</summary>
        <remarks>We recommend that values stored here are in seconds since the `zero` timestamp.</remarks>
        */
        [SerializeField] float timestamp;
        /**
        <summary>Score for this event, calculated based on evaluation of object and associated data.</summary>
        */
        [SerializeField] float score;

        // MARK: Properties
        /**
        <inheritdoc cref="data"/>
        */
        public D Data => data;
        /**
        <inheritdoc cref="obj"/>
        */
        public T Object => obj;
        /**
        <inheritdoc cref="timestamp"/>
        */
        public float Timestamp => timestamp;
        /**
        <inheritdoc cref="score"/>
        */
        public float Score => score;

        // MARK: Initializers
        /**
        <summary>Initializes the event with an associated object, some data and a timestamp.</summary>
        <param name="obj">Object associated with the event.</param>
        <param name="data">Data associated with the event.</param>
        <param name="timestamp">Timestamp for when the event happened.</param>
        */
        private Event(T obj, D data, float timestamp)
        {
            this.obj = obj;
            this.data = data;
            this.timestamp = timestamp;
            this.score = obj.Score(data);
        }

        // MARK: Methods
        /**
        <summary>Creates a new event only if the values for it are valid.</summary>
        <param name="obj">Object associated with the event.</param>
        <param name="data">Data associated with the event.</param>
        <param name="timestamp">Timestamp for when the event happened.</param>
        <returns>A new event if the values are valid, otherwise `null`.</returns>
        */
        public static Event<T,D>? New(T obj, D data, float timestamp)
        {
            if (timestamp < 0.0f) return null;

            return new(obj, data, timestamp);
        }
        /**
        <summary>Validates the event based on the object associated and it's score.</summary>
        <inheritdoc cref="ValidateObject"/>
        <inheritdoc cref="ValidateScore"/>
        */
        public bool Validate(T obj, float threshold = 0.8f) => ValidateObject(obj) && ValidateScore(threshold);
        /**
        <summary>Validates the event based on the object associated with it.</summary>
        <param name="obj">Object to be validated.</param>
        */
        public bool ValidateObject(T obj) => obj.Equals(this.obj);
        /**
        <summary>Validates the event based on it's score.</summary>
        <param name="threshold">Threshold for the score to be considered valid.</param>
        */
        public bool ValidateScore(float threshold = 0.8f) => Score >= Mathf.Max(threshold, 0);
    }

    #region Initialization Utilities
    public static partial class Event
    {
        /**
        <summary>Creates a new event based on the object with the highest valid score.</summary>
        <typeparam name="T">Type of the object associated with the event.</typeparam>
        <typeparam name="D">Type of the data associated with the event.</typeparam>
        <param name="data">Data associated with the event.</param>
        <param name="objects">Objects to be evaluated for the event.</param>
        <param name="timestamp">Timestamp for when the event happened.</param>
        <param name="scoreThreshold">Threshold for the score to be considered valid.</param>
        <returns>A new event if the values are valid, otherwise `null`.</returns>
        */
        public static Event<T,D>? From<T,D>(D data, IEnumerable<T> objects, float timestamp, float scoreThreshold = 0) where T: IEventObject<D>
        {
            var results = objects
                    .Select(obj => New(obj, data, timestamp))
                    .OfType<Event<T,D>>()
                    .Where(e => e.ValidateScore(scoreThreshold))
                    .OrderByDescending(e => e.Score)
                    .ToArray();

            return results.Length > 0 ? results[0] : null;
        }
        /**
        <inheritdoc cref="Event{T,D}.New(T, D, float)"/>
        <inheritdoc cref="From{T, D}(D, IEnumerable{T}, float, float)"/>
        */
        public static Event<T,D>? New<T,D>(T obj, D data, float timestamp) where T: IEventObject<D>
        {
            return Event<T,D>.New(obj, data, timestamp);
        }
    }
    #endregion

    #region IEquatable Implementation
    public partial struct Event<T, D>: IEquatable<Event<T,D>>
    {
        public bool Equals(Event<T, D> other)
        {
            return Object.Equals(other.Object) && Data.Equals(other.Data) && Timestamp.Equals(other.Timestamp);
        }
    }
    #endregion

    #region IProvenanceData Implementation
    public partial struct Event<T,D>: IProvenanceData
    {
        /**
        <inheritdoc cref="IProvenanceData.ID"/>
        */
        public string ID => obj.Name;
        /**
        <inheritdoc cref="IProvenanceData.Type"/>
        */
        public string Type => $"{typeof(T).Name}Event";
        /**
        <inheritdoc cref="IProvenanceData.Attributes"/>
        */
        public IDictionary<string, object> Attributes {
            get {
                var dictionary = new Dictionary<string, object> {
                    { "timestamp", Timestamp },
                    { "score", Score },
                    { "eventData", data }
                };

                obj.Apply(dictionary, data);
                return dictionary;
            }
        }
        /**
        <inheritdoc cref="IProvenanceData.RegisterTo(IProvenanceModel)"/>
        */
        public void RegisterTo(IProvenanceModel provenance)
        {
            var associatedTag = obj.AssociatedWith(data);
            var activity = !string.IsNullOrEmpty(associatedTag) ? this.AsActivity(wasAssociatedWith: associatedTag) : this.AsActivity();
            provenance.Register(activity);
        }
    }
    #endregion

    #region ToString
    public partial struct Event<T, D>
    {
        /**
        <summary>Tag for fast displaying of an event.</summary>
        */
        public string MinimalDebugInfo => $"[{Type}: {timestamp}s] {obj.Name} - Score: {score}";

        public override string ToString()
        {
            return $"{MinimalDebugInfo}\n{data}";
        }
    }
    #endregion

    #region IList
    public static partial class IListExtensions
    {
        /**
        <summary>Removes redundant events associated the same object, keeping only the highest-scoring events.</summary>
        <typeparam name="T">Type of the object associated with the event.</typeparam>
        <typeparam name="D">Type of the data associated with the event.</typeparam>
        <param name="self">List of events to be squashed.</param>
        */
        public static void Squash<T,D>(this IList<Event<T,D>> self) where T: IEventObject<D>
        {
            for(int i = 1; i < self.Count; i++) {
                var lastEvent = self[^i];
                var currentEvent = self[^(i-1)];

                if (currentEvent.Object.Equals(lastEvent.Object)) continue;

                if (currentEvent.Score < lastEvent.Score) {
                    self.RemoveAt(self.Count - i - 1);
                } else {
                    self.RemoveAt(self.Count - i);
                }
            }
        }
    }
    #endregion

    #region UnityEditor Support
    #if UNITY_EDITOR
    public partial struct Event<T,D>
    {
        public Event(T obj, D data, float timestamp, float score)
        {
            this.obj = obj;
            this.data = data;
            this.timestamp = timestamp;
            this.score = score;
        }
    }
    #endif
    #endregion
}