using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial interface IPose
    {
        // MARK: Properties
        string Name { get; }

        // MARK: Methods
        float Evaluate(Placement placement);
    }
}