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

    /**
    <summary>
    Structure that defines a gameplay session in EdKit. <br/>
    In EdKit, each session is composed of basic general information, such as the date, the user and the scene. <br/>
    It also contains a list of activities, agents and entities that are part of the session, inspired on the PROV-O provenance model specification. <br/>
    </summary>
    <remarks></remarks>
    */
    [Serializable]
    public partial struct Session
    {
        // MARK: Constants
        /**
        <summary>Current version of the Session structure in code.</summary>
        <remarks>This value is updated when the serialization of this type changes it's format.</remarks>
        */
        public const uint VERSION = 1;

        // MARK: Variables
        /**
        <summary>Timestamp for this session.</summary>
        */
        [SerializeField] DateTime date;
        /**
        <summary>User ID associated with this session.</summary>
        */
        [SerializeField] UID userID;
        /**
        <summary>Scene ID associated with this session.</summary>
        */
        [SerializeField] UID sceneID;
        /**
        <summary>List of activities registered in this session.</summary>
        */
        [SerializeField] List<Activity> activities;
        /**
        <summary>List of agents registered in this session.</summary>
        */
        [SerializeField] List<Agent> agents;
        /**
        <summary>List of entities registered in this session.</summary>
        */
        [SerializeField] List<Entity> entities;

        // MARK: Properties
        /**
        <inheritdoc cref="date"/>
        */
        public DateTime Date => date;
        /**
        <inheritdoc cref="userID"/>
        */
        public UID UserID => userID;
        /**
        <inheritdoc cref="sceneID"/>
        */
        public UID SceneID => sceneID;
        /**
        <inheritdoc cref="activities"/>
        */
        public IReadOnlyList<Activity> Activities => activities;
        /**
        <inheritdoc cref="agents"/>
        */
        public IReadOnlyList<Agent> Agents => agents;
        /**
        <inheritdoc cref="entities"/>
        */
        public IReadOnlyList<Entity> Entities => entities;
        /**
        <summary>Unique tag for this session.</summary>
        */
        private string Tag {
            get {
                return $"{date:yyyy-MM-dd_HH-mm-ss}_{userID}_{sceneID}";
            }
        }

        // MARK: Initializers
        /**
        <summary>Initializes a new session associated with an user and scene.</summary>
        <param name="userID">User ID associated with this session.</param>
        <param name="sceneID">Scene ID associated with this session.</param>
        <param name="date">Timestamp for this session. If not provided, it will be set to the current date and time.</param>
        */
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
        /**
        <summary>Saves the information of the session in a log file.</summary>
        <param name="basePath">Path for the game's persistent folder. (e.g. <c>Application.persistentDataPath</c>)</param>
        <param name="relativeFolder">Relative folder where logs file are saved. Default is "/Logs".</param>
        <param name="fileName">Name of the file. Default is the session tag.</param>
        <param name="fileExtension">Extension of the file. Default is "json".</param>
        <returns>Task that represents the writing operation.</returns>
        */
        public async Task SaveToLog(string basePath, string relativeFolder = "/Logs", string fileName = null, string fileExtension = "json")
        {
            string LogsFullPath = basePath+relativeFolder;
            string FileName = fileName ?? Tag+"_Log";

            string path = LogsFullPath+$"/{FileName}."+fileExtension;
            var settings = new JsonSerializerSettings() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string contents = JsonConvert.SerializeObject(this, settings);

            if (!Directory.Exists(LogsFullPath)) {
                Directory.CreateDirectory(LogsFullPath);
            }

            using var fileWriter = new StreamWriter(path, false);
            await fileWriter.WriteAsync(contents);

            Debug.Log($"Saved Log to Path: {path}");
        }
    }

    #region IProvenance Implementation
    public partial struct Session: IProvenanceModel
    {
        /**
        <inheritdoc cref="IProvenanceModel.Register(Activity)"/>
        */
        public void Register(Activity activity) => activities.Add(activity);
        /**
        <inheritdoc cref="IProvenanceModel.Register(Agent)"/>
        */
        public void Register(Agent agent) => agents.Add(agent);
        /**
        <inheritdoc cref="IProvenanceModel.Register(Entity)"/>
        */
        public void Register(Entity entity) => entities.Add(entity);
    }
    #endregion
}