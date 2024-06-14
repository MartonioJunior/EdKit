using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    [Serializable]
    public partial struct Entity
    {
        // MARK: Variables
        public string entityID;
        public string type;
        public SerializedDictionary<string, object> attributes;

        public Entity(string id, string type, IDictionary<string, object> attributes) : this()
        {
            entityID = id;
            this.type = type;
            this.attributes = new SerializedDictionary<string, object>(attributes);
        }
    }
}