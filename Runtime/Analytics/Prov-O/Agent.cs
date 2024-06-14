using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    [Serializable]
    public partial struct Agent
    {
        // MARK: Variables
        public string agentID;
        public string type;
        public SerializedDictionary<string, object> attributes;

        public Agent(string id, string type, IDictionary<string, object> attributes)
        {
            this.agentID = id;
            this.type = type;
            this.attributes = new SerializedDictionary<string, object>(attributes);
        }
    }
}