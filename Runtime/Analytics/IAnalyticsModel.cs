using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial interface IAnalyticsModel
    {
        // MARK: Methods
        void Register(GestureEvent gestureEvent);
        void Register(PoseEvent poseEvent);
        void RegisterAny(object anyObject);
    }
}