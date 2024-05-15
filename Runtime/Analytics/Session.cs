using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    #region Aliases
    using UID = String;
    #endregion

    [System.Serializable]
    public partial struct Session
    {
        // MARK: Constants
        public const uint VERSION = 1;

        // MARK: Variables
        [SerializeField] DateTime date;
        [SerializeField] UID userID;
        [SerializeField] UID sceneID;
        [SerializeField] object outcome;
        [SerializeField] List<GestureEvent> gestureEvents;
        [SerializeField] List<PoseEvent> poseEvents;
        [SerializeField] List<object> customEvents;

        // MARK: Properties
        public string Tag {
            get {
                return $"{date:yyyy-MM-dd_HH-mm-ss}_{userID}_{sceneID}";
            }
        }

        // MARK: Initializers
        public Session(UID userID, UID sceneID)
        {
            this.date = DateTime.Now;
            this.userID = userID;
            this.sceneID = sceneID;
            this.outcome = null;
            this.gestureEvents = new List<GestureEvent>();
            this.poseEvents = new List<PoseEvent>();
            this.customEvents = new List<object>();
        }

        // MARK: Methods
        public async Task SaveToLog(string basePath)
        {
            string LogsFullPath = basePath+"/Logs";

            string path = LogsFullPath+$"/{Tag}_Log.json";
            string contents = JsonUtility.ToJson(this);

            if (!Directory.Exists(LogsFullPath)) {
                Directory.CreateDirectory(LogsFullPath);
            }

            using var fileWriter = new StreamWriter(path, true);
            await fileWriter.WriteAsync(contents);

            Debug.Log($"Saved Log to Path: {path}");
        }

        public void SetOutcome(object outcome)
        {
            this.outcome = outcome;
        }
    }

    #region IAnalyticsModel Implementation
    public partial struct Session: IAnalyticsModel
    {
        public void Register(GestureEvent gestureEvent)
        {
            gestureEvents.Add(gestureEvent);
        }

        public void Register(PoseEvent poseEvent)
        {
            poseEvents.Add(poseEvent);
        }

        public void RegisterAny(object anyObject)
        {
            customEvents.Add(anyObject);
        }
    }
    #endregion
}