using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Interface used to describe an event object in EdKit's `Gestures` module.</summary>
    <typeparam name="Data">The data type that the event object is associated with.</typeparam>
    */
    public interface IEventObject<Data>
    {
        // MARK: Properties
        /**
        <summary>Name for the event.</summary>
        */
        string Name { get; }
        // MARK: Methods
        /**
        <summary>Populates a dictionary with information about the event data.</summary>
        <param name="attributes">Dictionary to be populated.</param>
        <param name="data">Data to be used to populate the dictionary.</param>
        */
        void Apply(IDictionary<string, object> attributes, Data data);
        /**
        <summary>Creates a tag that defines the associated tag for an event.</summary>
        <param name="data">Data to be used to create the tag.</param>
        */
        string AssociatedWith(Data data);
        /**
        <summary>Returns a scoring based on the data received on an event.</summary>
        <param name="data">Data to be used to score the event.</param>
        <remarks>Allows reuse of the event object while evaluating with new data.</remarks>
        */
        float Score(Data data);
    }
}