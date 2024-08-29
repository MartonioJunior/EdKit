using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Interface that describes a provenance data structure containing information about the game.</summary>
    <remarks>
    The structure can't be registered directly into a provenance model. <br/>
    To do so, you must decompose and register the type as one or more activities, agents and/or entities. <br/>
    </remarks>
    */
    public partial interface IProvenanceData
    {
        // MARK: Variables
        /**
        <summary>Identifier for the provenance data.</summary>
        */
        string ID { get; }
        /**
        <summary>Type of the provenance data.</summary>
        */
        string Type { get; }
        /**
        <summary>Attributes of the provenance data.</summary>
        */
        IDictionary<string, object> Attributes { get; }

        // MARK: Methods
        /**
        <summary>Registers the data into a provenance model.</summary>
        <param name="provenance">Provenance model to register to.</param>
        */
        void RegisterTo(IProvenanceModel provenance);
    }

    #region Default Implementation
    public static partial class IProvenanceDataExtensions
    {
        /**
        <summary>Transforms the provenance data into an activity.</summary>
        */
        public static Activity AsActivity(this IProvenanceData self, string used = "", string wasAssociatedWith = "")
        {
            return new Activity(self.ID, self.Type, self.Attributes, used, wasAssociatedWith);
        }
        /**
        <summary>Transforms the provenance data into an agent.</summary>
        */
        public static Agent AsAgent(this IProvenanceData self)
        {
            return new Agent(self.ID, self.Type, self.Attributes);
        }
        /**
        <summary>Transforms the provenance data into an entity.</summary>
        */
        public static Entity AsEntity(this IProvenanceData self)
        {
            return new Entity(self.ID, self.Type, self.Attributes);
        }
    }
    #endregion
}