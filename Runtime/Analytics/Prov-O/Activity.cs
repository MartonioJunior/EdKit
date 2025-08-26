using System;
using System.Collections.Generic;
using System.Linq;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure that defines a provenance activity.</summary>
    */
    [Serializable]
    public partial struct Activity
    {
        // MARK: Variables
        /**
        <summary>Identifier for the activity.</summary>
        */
        public string activityID;
        /**
        <summary>Type of the activity.</summary>
        */
        public string type;
        /**
        <summary>Attributes for the activity.</summary>
        */
        public SerializedDictionary<string, object> attributes;
        /**
        <summary>Entity ID used by the activity.</summary>
        */
        public string used;
        /**
        <summary>Agent ID associated with the activity.</summary>
        */
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
        /**
        <summary>Associates an agent with this activity.</summary>
        <returns>The activity itself.</returns>
        */
        public Activity AssociatedWith(params Agent[] agent)
        {
            wasAssociatedWith = string.Join(",", agent.Select(a => a.agentID));
            return this;
        }
        /**
        <summary>Associates an entity with this activity.</summary>
        <returns>The activity itself.</returns>
        */
        public Activity UsedBy(params Entity[] entity)
        {
            used = string.Join(",", entity.Select(e => e.entityID));
            return this;
        }
    }
}