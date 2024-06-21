using System.Collections.Generic;
using System.Data.Common;

namespace MartonioJunior.EdKit
{
    public partial interface IPose
    {
        // MARK: Methods
        float Evaluate(Placement placement);
    }

    #region IEventObject Implementation
    public partial interface IPose: IEventObject<Placement>
    {
        string IEventObject<Placement>.AssociatedWith(Placement data) => null;

        void IEventObject<Placement>.Apply(IDictionary<string, object> attributes, Placement data)
        {
            attributes.Add("Name", Name);
        }

        float IEventObject<Placement>.Score(Placement data)
        {
            return Evaluate(data);
        }
    }
    #endregion
}