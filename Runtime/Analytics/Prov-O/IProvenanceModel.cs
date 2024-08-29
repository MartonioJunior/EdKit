using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Interface that describes a provenance data model that can be register other provenance primitives.</summary>
    */
    public partial interface IProvenanceModel
    {
        // MARK: Methods
        /**
        <summary>Registers a new activity into the model.</summary>
        <param name="activity">The activity to be registered.</param>
        */
        void Register(Activity activity);
        /**
        <summary>Registers a new agent into the model.</summary>
        <param name="agent">The agent to be registered.</param>
        */
        void Register(Agent agent);
        /**
        <summary>Registers a new entity into the model.</summary>
        <param name="entity">The entity to be registered.</param>
        */
        void Register(Entity entity);
    }

    #region Default Implementation
    public static partial class IProvenanceExtensions
    {
        /**
        <summary>Registers an outcome entity for this session.</summary>
        <param name="outcomeData">Dictionary with data about the outcome for the session.</param>
        */
        public static void RegisterOutcome(this IProvenanceModel self, IDictionary<string, object> outcomeData)
        {
            self.Register(new Entity("outcome", "Outcome", outcomeData));
        }
    }
    #endregion
}