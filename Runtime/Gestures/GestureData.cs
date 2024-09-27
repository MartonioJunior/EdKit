using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Gesture that is defined by an ordered sequence of poses.</summary>
    <remarks>All poses contribute equally to the final score.</remarks>
    */
    public partial class GestureData
    {
        // MARK: Variables
        /**
        <summary>Sequence of poses to be analyzed.</summary>
        */
        [SerializeField] PoseData[] poses;

        // MARK: Properties
        /**
        <inheritdoc cref="poses"/>
        */
        public PoseData[] Poses => poses;
    }

    #region ScriptableObject Implementation
    [CreateAssetMenu(fileName = "GestureData", menuName = "EdKit/Gesture", order = 0)]
    public partial class GestureData: ScriptableObject {}
    #endregion

    #region IGesture Implementation
    public partial class GestureData: IGesture
    {
        /**
        <inheritdoc cref="IGesture.Name"/>
        */
        public string Name => name;
        /**
        <remarks>The evaluation tries to match the list of pose events with the sequence of poses in reverse order,
        accumulating the score against the number of poses analyzed.</remarks>
        <inheritdoc cref="IGesture.Evaluate(IList{Event{IPose, Placement}})"/>
        */
        public float Evaluate(IList<Event<IPose, Placement>> events)
        {
            if (poses == null) return 0;

            float numberOfPoses = poses.Length + 1;
            var poseScore = 0f;
            var poseIndex = 1;

            for (int i = 1; i < numberOfPoses; i++) {
                if (events.Count <= i) return 0;

                if (!events[^i].Object.Equals(poses[^poseIndex])) continue;

                poseScore = Mathf.Min(poseScore, events[^i].Score);
                poseIndex++;
            }

            return poseScore;
        }
    }
    #endregion
}