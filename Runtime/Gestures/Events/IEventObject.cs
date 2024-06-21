using System;
using System.Collections.Generic;

namespace MartonioJunior.EdKit
{
    public interface IEventObject<Data>
    {
        string Name { get; }

        void Apply(IDictionary<string, object> attributes, Data data);
        string AssociatedWith(Data data);
        float Score(Data data);
    }
}