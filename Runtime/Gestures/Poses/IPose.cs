using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    #region Aliases
    using PoseEvent = Event<IPose, Placement>;
    #endregion
    /**
    <summary>Interface used to describe a pose in EdKit.</summary>
    */
    public partial interface IPose
    {
        // MARK: Methods
        /**
        <summary>Scores a <c>Placement</c> instance based on how accurate the pose was performed.</summary>
        <param name="placement">The <c>Placement</c> instance to be scored.</param>
        <returns>The score of the pose.</returns>
        <remarks>
        While any floating value can be passed as a score, it is recommended to use a value between 0 and 1.<br/>
        Some methods may take into account other values, such as negative values to mark a pose as invalid,
        but it is recommend to follow the guideline above.
        </remarks>
        */
        float Evaluate(Placement placement);
    }

    #region Default Implementation
    public static partial class IPoseExtensions
    {
        public static bool CompareByName(this IPose self, IPose other)
        {
            return self.Name == other.Name;
        }

        public static float ScoringByObjectName(this IPose self, PoseEvent poseEvent)
        {
            return poseEvent.ScoreByObjectName(self);
        }
    }
    #endregion

    #region IEventObject Implementation
    public partial interface IPose: IEventObject<Placement>
    {
        /**
        <inheritdoc cref="IEventObject{Placement}.AssociatedWith(Placement)"/>
        */
        string IEventObject<Placement>.AssociatedWith(Placement data) => null;
        /**
        <inheritdoc cref="IEventObject{Placement}.Apply(IDictionary{string, object}, Placement)"/>
        */
        void IEventObject<Placement>.Apply(IDictionary<string, object> attributes, Placement data)
        {
            attributes.Add("Name", Name);
        }
        /**
        <inheritdoc cref="IEventObject{Placement}.Score(Placement)"/>
        */
        float IEventObject<Placement>.Score(Placement data)
        {
            return Evaluate(data);
        }
    }
    #endregion

    #region Event Extensions
    public static partial class EventExtensions
    {
        public static float ScoreByObjectName(this PoseEvent self, IPose other)
        {
            return self.Object.CompareByName(other) ? self.Score : 0;
        }
    }
    #endregion
}