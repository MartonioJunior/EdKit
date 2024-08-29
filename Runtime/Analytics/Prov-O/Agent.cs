using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure that defines a provenance agent.</summary>
    */
    [Serializable]
    public partial struct Agent
    {
        // MARK: Variables
        /**
        <summary>Identifier for the agent.</summary>
        */
        public string agentID;
        /**
        <summary>Type of the agent.</summary>
        */
        public string type;
        /**
        <summary>Attributes for the agent.</summary>
        */
        public SerializedDictionary<string, object> attributes;
        
        // MARK: Initializers
        /**
        <summary>Initializes a new agent.</summary>
        <param name="id">Identifier for the agent.</param>
        <param name="type">Type of the agent.</param>
        <param name="attributes">Attributes for the agent.</param>
        */
        public Agent(string id, string type, IDictionary<string, object> attributes)
        {
            this.agentID = id;
            this.type = type;
            this.attributes = new SerializedDictionary<string, object>(attributes);
        }
    }
}