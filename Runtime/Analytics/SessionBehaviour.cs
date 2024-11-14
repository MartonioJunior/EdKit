using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    #region Alias
    using UID = System.String;
    #endregion

    /**
    <summary>Component that manages the lifecycle for a session in a scene.</summary>
    */
    public partial class SessionBehaviour
    {
        // MARK: Variables
        /**
        <summary>Session currently managed by the component.</summary>
        */
        Session? session = null;
#if UNITY_EDITOR
        /**
        <summary>When set to <c>true</c>, saves the log when leaving play mode.</summary>
        */
        [Header("Editor Settings")]
        [SerializeField, Tooltip("Saves log when leaving Play Mode")] bool saveLogInEditor = true;
#endif

        // MARK: Events
        /**
        <summary>Event invoked when a session is opened.</summary>
        */
        [SerializeField] UnityEvent<Session> onSessionOpened = new();
        /**
        <summary>Event invoked when a session is closed.</summary>
        */
        [SerializeField] UnityEvent<Session> onSessionClosed = new();

        // MARK: Methods
        /**
        <summary>Initializes a new session to be managed by the component.</summary>
        <param name="userID">User ID associated with the session.</param>
        <param name="sceneID">Scene ID associated with the session.</param>
        */
        public void OpenSession(UID userID, UID sceneID)
        {
            var s = new Session(userID, sceneID);
            session = s;
            onSessionOpened.Invoke(s);
        }
        /**
        <summary>Disposes of the current running session.</summary>
        */
        public void ClearSession()
        {
            session = null;
        }
        /**
        <param name="outcome">Description of the outcome for the session.</param>
        <inheritdoc cref="CloseSession(IDictionary{string,object})"/>
        */
        public void CloseSession(string outcome)
        {
            CloseSession(new Dictionary<string, object> { { "description", outcome } });
        }
        /**
        <summary>Closes the current session with a given outcome.</summary>
        <param name="outcome">Dictionary with data about the outcome for the session.</param>
        */
        public void CloseSession(IDictionary<string, object> outcome)
        {
            if (session is not Session s) return;

            s.RegisterOutcome(outcome);
            SaveToLog(s);
            onSessionClosed.Invoke(s);
            ClearSession();
        }
        /**
        <summary>Saves a session into a log file.</summary>
        <param name="session">Session to be saved.</param>
        <remarks>In the Unity Editor, the log will not be saved when <c>saveLogInEditor</c> property is set to <c>false</c>.</remarks>
        */
        private void SaveToLog(Session session)
        {
#if UNITY_EDITOR
            if (!saveLogInEditor) return;
#endif
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
        /**
        <inheritdoc cref="IProvenanceModel.Register(Activity)"/>
        */
        public void Register(Activity activity) => session?.Register(activity);
        /**
        <inheritdoc cref="IProvenanceModel.Register(Agent)"/>
        */
        public void Register(Agent agent) => session?.Register(agent);
        /**
        <inheritdoc cref="IProvenanceModel.Register(Entity)"/>
        */
        public void Register(Entity entity) => session?.Register(entity);
    }
    #endregion

    #region UnityEvent Bindings
    public partial class SessionBehaviour
    {
        public void RegisterGesture(Event<IGesture, List<Event<IPose, Placement>>> gestureEvent)
        {
            if (session is null) return;

            gestureEvent.RegisterTo(this);
        }

        public void RegisterPose(Event<IPose, Placement> poseEvent)
        {
            if (session is null) return;

            poseEvent.RegisterTo(this);
        }
    }
    #endregion
}