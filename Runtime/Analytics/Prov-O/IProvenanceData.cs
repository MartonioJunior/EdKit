using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    public partial interface IProvenanceData
    {
        // MARK: Variables
        string ID { get; }
        string Type { get; }
        IDictionary<string, object> Attributes { get; }

        // MARK: Methods
        void RegisterTo(IProvenanceModel provenance);
    }

    #region Default Implementation
    public static partial class IProvenanceDataExtensions
    {
        public static Activity AsActivity(this IProvenanceData self, string used = "", string wasAssociatedWith = "")
        {
            return new Activity(self.ID, self.Type, self.Attributes, used, wasAssociatedWith);
        }

        public static Agent AsAgent(this IProvenanceData self)
        {
            return new Agent(self.ID, self.Type, self.Attributes);
        }

        public static Entity AsEntity(this IProvenanceData self)
        {
            return new Entity(self.ID, self.Type, self.Attributes);
        }
    }
    #endregion
}