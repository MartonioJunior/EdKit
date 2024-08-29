using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure that defines a provenance entity.</summary>
    */
    [Serializable]
    public partial struct Entity
    {
        // MARK: Variables
        /**
        <summary>Identifier for the entity.</summary>
        */
        public string entityID;
        /**
        <summary>Type of the entity</summary>
        */
        public string type;
        /**
        <summary>Attributes of the entity.</summary>
        */
        public SerializedDictionary<string, object> attributes;

        // MARK: Initializers
        /**
        <summary>Initializes a new entity.</summary>
        <param name="id">Identifier for the entity.</param>
        <param name="type">Type of the entity.</param>
        <param name="attributes">Attributes for the entity.</param>
        */
        public Entity(string id, string type, IDictionary<string, object> attributes)
        {
            entityID = id;
            this.type = type;
            this.attributes = new SerializedDictionary<string, object>(attributes);
        }
    }
}