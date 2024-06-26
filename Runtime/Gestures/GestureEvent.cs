using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial struct GestureEvent
    {
        // MARK: Variables
        [SerializeField] List<PoseEvent> poseEvents;
        [SerializeReference] IGesture gesture;
        [SerializeField] float timestamp;

        // MARK: Properties
        public IEnumerable<PoseEvent> PoseEvents => poseEvents;
        public IGesture Gesture => gesture;
        public float Timestamp => timestamp;
        public float Score => gesture.Evaluate(poseEvents);

        // MARK: Initializers
        public GestureEvent(IEnumerable<PoseEvent> poseEvents, IGesture gesture, float timestamp)
        {
            this.poseEvents = poseEvents.ToList();
            this.gesture = gesture;
            this.timestamp = timestamp;
        }

        // MARK: Methods
        public static GestureEvent? From<T>(IEnumerable<PoseEvent> poseEvents, IEnumerable<T> gestures, float threshold = 0.0001f) where T: IGesture
        {
            if (poseEvents is not IList<PoseEvent> events) return null;

            events.Squash();

            var results = gestures
                    .Where(e => e != null && e.Evaluate(events) > threshold)
                    .Select(gesture => new GestureEvent(events, gesture, events[^1].Timestamp))
                    .OrderByDescending(e => e.Score)
                    .ToArray();

            return results.Length > 0 ? results[0]: null;
        }
    }

    #region IProvenanceData Implementation
    public partial struct GestureEvent: IProvenanceData
    {
        public string ID => gesture.Name;
        public string Type => "GestureEvent";
        public IDictionary<string, object> Attributes => new Dictionary<string, object>
        {
            { "startTime", poseEvents[0].Timestamp },
            { "endTime", poseEvents[^1].Timestamp },
            { "score", Score }
        };

        public void RegisterTo(IProvenanceModel provenance)
        {
            provenance.Register(this.AsActivity(wasAssociatedWith: poseEvents.Select(p => p.ID).Reduce("", (a,b) => a + "," + b)));
        }
    }
    #endregion
}