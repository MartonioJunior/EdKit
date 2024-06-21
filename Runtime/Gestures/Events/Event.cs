using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    [Serializable]
    public partial struct Event<T,D> where T: IEventObject<D>
    {
        // MARK: Variables
        [SerializeField] D data;
        [SerializeReference] T obj;
        [SerializeField] float timestamp;
        [SerializeField] float score;

        // MARK: Properties
        public D Data => data;
        public T Object => obj;
        public float Timestamp => timestamp;
        public float Score => score;

        // MARK: Initializers
        private Event(T obj, D data, float timestamp)
        {
            this.obj = obj;
            this.data = data;
            this.timestamp = timestamp;
            this.score = obj.Score(data);
        }

        // MARK: Methods
        public static Event<T,D>? New(T obj, D data, float timestamp)
        {
            if (timestamp < 0.0f) return null;

            return new(obj, data, timestamp);
        }

        public bool Validate(T obj, float threshold = 0.8f)
        {
            return obj.Equals(this.obj) && Score >= Mathf.Clamp01(threshold);
        }
    }

    #region Initialization Utilities
    public static partial class Event
    {
        public static Event<T,D>? From<T,D>(D data, IEnumerable<T> objects, float timestamp) where T: IEventObject<D>
        {
            var results = objects
                    .Select(obj => New(obj, data, timestamp))
                    .OfType<Event<T,D>>()
                    .Where(e => e.Score > 0)
                    .OrderByDescending(e => e.Score)
                    .ToArray();

            return results.Length > 0 ? results[0]: null;
        }

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
        public string ID => obj.Name;
        public string Type => $"{typeof(T)}Event";
        public IDictionary<string, object> Attributes {
            get {
                var dictionary = new Dictionary<string, object> {
                    { "timestamp", Timestamp },
                    { "score", Score }
                };
                obj.Apply(dictionary, data);
                return dictionary;
            }
        }

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
        public override string ToString()
        {
            return $"{Type}: {obj}({data}) - {score} - {timestamp}";
        }
    }
    #endregion

    #region IList
    public static partial class IListExtensions
    {
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
}