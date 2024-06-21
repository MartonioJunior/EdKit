using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    [Serializable]
    public partial struct Activity
    {
        // MARK: Variables
        public string activityID;
        public string type;
        public SerializedDictionary<string, object> attributes;
        public string used;
        public string wasAssociatedWith;

        // MARK: Initializers
        public Activity(string activityID, string type, IDictionary<string, object> attributes, string used, string wasAssociatedWith)
        {
            this.activityID = activityID;
            this.type = type;
            this.attributes = new SerializedDictionary<string, object>(attributes);
            this.used = used;
            this.wasAssociatedWith = wasAssociatedWith;
        }

        // MARK: Methods
        public Activity AssociatedWith(Agent agent)
        {
            wasAssociatedWith = agent.agentID;
            return this;
        }

        public Activity UsedBy(Entity entity)
        {
            used = entity.entityID;
            return this;
        }
    }
}