using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    #region Alias
    using UID = System.String;
    #endregion

    public partial class SessionBehaviour
    {
        // MARK: Variables
        [SerializeField] Session? session = null;

        // MARK: Methods
        public void OpenSession(UID userID, UID sceneID)
        {
            session = new Session(userID, sceneID);
        }

        public void ClearSession()
        {
            session = null;
        }

        public void CloseSession(string outcome)
        {
            CloseSession(new Dictionary<string, object> { { "description", outcome } });
        }

        public void CloseSession(IDictionary<string, object> outcome)
        {
            if (session is not Session s) return;

            s.RegisterOutcome(outcome);
            SaveToLog(s);
        }

        private void SaveToLog(Session session)
        {
            _ = session.SaveToLog(Application.persistentDataPath);
        }
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Session Behaviour")]
    public partial class SessionBehaviour: MonoBehaviour {}
    #endregion

    #region IProvenance Implementation
    public partial class SessionBehaviour: IProvenanceModel
    {
        public void Register(Activity activity) => session?.Register(activity);
        public void Register(Agent agent) => session?.Register(agent);
        public void Register(Entity entity) => session?.Register(entity);
    }
    #endregion
}