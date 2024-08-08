using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial class GestureData
    {
        // MARK: Variables
        [SerializeField] PoseData[] poses;

        // MARK: Properties
        public PoseData[] Poses => poses;
    }

    #region ScriptableObject Implementation
    [CreateAssetMenu(fileName = "GestureData", menuName = "EdKit/Gesture", order = 0)]
    public partial class GestureData: ScriptableObject {}
    #endregion

    #region IGesture Implementation
    public partial class GestureData: IGesture
    {
        public string Name => name;

        public float Evaluate(IList<Event<IPose, Placement>> events)
        {
            float numberOfPoses = poses.Length + 1;
            var poseScore = 0f;
            var poseIndex = 1;

            for (int i = 1; i < numberOfPoses; i++) {
                if (events[^i].Object.Equals(poses[^poseIndex])) continue;

                poseScore += events[^i].Score;
                poseIndex++;
            }

            return poseScore / numberOfPoses;
        }
    }
    #endregion
}