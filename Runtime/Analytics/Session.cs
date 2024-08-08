using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    #region Aliases
    using UID = String;
    #endregion

    [Serializable]
    public partial struct Session
    {
        // MARK: Constants
        public const uint VERSION = 1;

        // MARK: Variables
        [SerializeField] DateTime date;
        [SerializeField] UID userID;
        [SerializeField] UID sceneID;
        [SerializeField] List<Activity> activities;
        [SerializeField] List<Agent> agents;
        [SerializeField] List<Entity> entities;

        // MARK: Properties
        public DateTime Date => date;
        public UID UserID => userID;
        public UID SceneID => sceneID;
        public IReadOnlyList<Activity> Activities => activities;
        public IReadOnlyList<Agent> Agents => agents;
        public IReadOnlyList<Entity> Entities => entities;

        private string Tag {
            get {
                return $"{date:yyyy-MM-dd_HH-mm-ss}_{userID}_{sceneID}";
            }
        }

        // MARK: Initializers
        public Session(UID userID, UID sceneID, DateTime? date = null)
        {
            this.date = date ?? DateTime.Now;
            this.userID = userID;
            this.sceneID = sceneID;
            this.activities = new List<Activity>();
            this.agents = new List<Agent>();
            this.entities = new List<Entity>();
        }

        // MARK: Methods
        public async Task SaveToLog(string basePath, string relativeFolder = "/Logs", string fileName = null, string fileExtension = "json")
        {
            string LogsFullPath = basePath+relativeFolder;
            string FileName = fileName ?? Tag+"_Log";

            string path = LogsFullPath+$"/{FileName}."+fileExtension;
            string contents = JsonConvert.SerializeObject(this);

            if (!Directory.Exists(LogsFullPath)) {
                Directory.CreateDirectory(LogsFullPath);
            }

            using var fileWriter = new StreamWriter(path, true);
            await fileWriter.WriteAsync(contents);

            Debug.Log($"Saved Log to Path: {path}");
        }
    }

    #region IProvenance Implementation
    public partial struct Session: IProvenanceModel
    {
        public void Register(Activity activity)
        {
            activities.Add(activity);
        }

        public void Register(Agent agent)
        {
            agents.Add(agent);
        }

        public void Register(Entity entity)
        {
            entities.Add(entity);
        }
    }
    #endregion
}