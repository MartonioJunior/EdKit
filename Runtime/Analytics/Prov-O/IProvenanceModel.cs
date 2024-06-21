using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    public partial interface IProvenanceModel
    {
        // MARK: Methods
        void Register(Activity activity);
        void Register(Agent agent);
        void Register(Entity entity);
    }

    #region Default Implementation
    public static partial class IProvenanceExtensions
    {
        public static void Register(this IProvenanceModel self, IProvenanceData data)
        {
            data.RegisterTo(self);
        }

        public static void RegisterOutcome(this IProvenanceModel self, IDictionary<string, object> outcomeData)
        {
            self.Register(new Entity("outcome", "Outcome", outcomeData));
        }
    }
    #endregion
}