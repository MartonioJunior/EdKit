using System.Collections.Generic;
using System.Linq;

namespace MartonioJunior.EdKit
{
    #region Aliases
    using PoseEvent = Event<IPose, Placement>;
    #endregion

    /**
    <summary>Interface used to describe a gesture in EdKit.</summary>
    */
    public partial interface IGesture
    {
        // MARK: Methods
        /**
        <summary>Evaluates a ordered sequence of pose events.</summary>
        <param name="poses">The pose events to be evaluated.</param>
        <returns>The score for the sequence of poses.</returns>
        <remarks>
        While any floating value can be passed as a score, it is recommended to use a value between 0 and 1.<br/>
        Some methods may take into account other values, such as negative values to mark a gesture as invalid,
        but it is recommend to follow the guideline above.
        </remarks>
        */
        float Evaluate(IList<PoseEvent> poses);
    }

    #region IEventObject Implementation
    public partial interface IGesture: IEventObject<List<PoseEvent>>
    {
        /**
        <inheritdoc cref="IEventObject{List{PoseEvent}}.AssociatedWith(List{PoseEvent})"/>
        */
        string IEventObject<List<PoseEvent>>.AssociatedWith(List<PoseEvent> data)
        {
            return data.Select(p => p.Object.Name).Reduce("", (a, b) => a + "," + b);
        }
        /**
        <inheritdoc cref="IEventObject{List{PoseEvent}}.Apply(IDictionary{string, object}, List{PoseEvent})"/>
        */
        void IEventObject<List<PoseEvent>>.Apply(IDictionary<string, object> attributes, List<PoseEvent> data)
        {
            attributes.Remove("timestamp");
            attributes.Add("startTime", data[0].Timestamp);
            attributes.Add("endTime", data[^1].Timestamp);
        }
        /**
        <inheritdoc cref="IEventObject{List{PoseEvent}}.Score(List{PoseEvent})"/>
        */
        float IEventObject<List<PoseEvent>>.Score(List<PoseEvent> data)
        {
            return Evaluate(data);
        }
    }
    #endregion
}