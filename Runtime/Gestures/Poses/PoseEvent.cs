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

        // MARK: Properties
        public Placement Placement => actorPlacement;
        public IPose Pose => pose;
        public float Timestamp => timestamp;
        public float Score => pose.Evaluate(actorPlacement);

        // MARK: Initializers
        public PoseEvent(Placement actorPlacement, IPose pose, float timestamp)
        {
            this.actorPlacement = actorPlacement;
            this.pose = pose;
            this.timestamp = timestamp;
        }

        // MARK: Methods
        public static PoseEvent? From(Placement placement, float timestamp, IList<IPose> posesToCheck)
        {
            return posesToCheck.Select(pose => new PoseEvent(placement, pose, timestamp)).OrderByDescending(poseEvent => poseEvent.Score).FirstOrDefault();
        }
    }
}