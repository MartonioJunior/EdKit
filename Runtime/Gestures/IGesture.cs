using System.Collections.Generic;
using System.Linq;

namespace MartonioJunior.EdKit
{
    using PoseEvent = Event<IPose, Placement>;

    public partial interface IGesture
    {
        // MARK: Methods
        float Evaluate(IList<PoseEvent> poses);
    }

    #region IEventObject Implementation
    public partial interface IGesture: IEventObject<List<PoseEvent>>
    {
        string IEventObject<List<PoseEvent>>.AssociatedWith(List<PoseEvent> data)
        {
            return data.Select(p => p.Object.Name).Reduce("", (a, b) => a + "," + b);
        }

        void IEventObject<List<PoseEvent>>.Apply(IDictionary<string, object> attributes, List<PoseEvent> data)
        {
            attributes.Remove("timestamp");
            attributes.Add("startTime", data[0].Timestamp);
            attributes.Add("endTime", data[^1].Timestamp);
        }

        float IEventObject<List<PoseEvent>>.Score(List<PoseEvent> data)
        {
            return Evaluate(data);
        }
    }
    #endregion
}