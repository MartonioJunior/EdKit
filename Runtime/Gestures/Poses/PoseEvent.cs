using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    [System.Serializable]
    public partial struct PoseEvent
    {
        // MARK: Variables
        [SerializeField] Placement actorPlacement;
        [SerializeReference] IPose pose;
        [SerializeField] float timestamp;
        [SerializeField] float score;

        // MARK: Properties
        public Placement Placement => actorPlacement;
        public IPose Pose => pose;
        public float Timestamp => timestamp;
        public float Score => score;

        // MARK: Initializers
        private PoseEvent(Placement actorPlacement, IPose pose, float timestamp)
        {
            this.actorPlacement = actorPlacement;
            this.pose = pose;
            this.timestamp = timestamp;
            this.score = pose.Evaluate(actorPlacement);
        }

        // MARK: Methods
        public static PoseEvent? From<T>(Placement placement, float timestamp, IEnumerable<T> posesToCheck, float threshold = 0.0001f) where T: IPose
        {
            if (timestamp <= 0.0f) return null;

            var results = posesToCheck
                    .Select(pose => new PoseEvent(placement, pose, timestamp))
                    .Where(e => e.Pose != null && e.Score > threshold)
                    .OrderByDescending(poseEvent => poseEvent.Score)
                    .ToArray();

            return results.Length > 0 ? results[0]: null;
        }

        public static PoseEvent? New(Placement actorPlacement, IPose pose, float timestamp)
        {
            return new PoseEvent(actorPlacement, pose, timestamp);
        }
    }

    #region ToString
    public partial struct PoseEvent
    {
        public override string ToString()
        {
            return $"PoseEvent: {pose}({Placement}) - {Score} - {timestamp}";
        }
    }
    #endregion
}