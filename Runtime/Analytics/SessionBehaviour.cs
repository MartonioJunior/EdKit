using UnityEngine;

namespace MartonioJunior.EdKit
{
    #region Alias
    using UID = System.String;
    #endregion

    public partial class SessionBehaviour
    {
        // MARK: Variables
        [SerializeField] Session? session;

        // MARK: Methods
        public void OpenSession(UID userID, UID sceneID)
        {
            session = new Session(userID, sceneID);
        }

        public void ClearSession()
        {
            session = null;
        }

        public void CloseSession(object outcome)
        {
            if (session is not Session s) return;

            s.SetOutcome(outcome);
            SaveToLog(s);
        }

        private void SaveToLog(Session session)
        {
            _ = session.SaveToLog(Application.persistentDataPath);
        }
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Session Behaviour")]
    public partial class SessionBehaviour: MonoBehaviour
    {
        
    }
    #endregion

    #region IAnalyticsModel Implementation
    public partial class SessionBehaviour: IAnalyticsModel
    {
        public void Register(GestureEvent gestureEvent)
        {
            session?.Register(gestureEvent);
        }

        public void Register(PoseEvent poseEvent)
        {
            session?.Register(poseEvent);
        }

        public void RegisterAny(object anyObject)
        {
            session?.RegisterAny(anyObject);
        }
    }
    #endregion
}