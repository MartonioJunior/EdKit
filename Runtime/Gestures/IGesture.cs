using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public interface IGesture
    {
        // MARK: Properties
        string Name { get; }

        // MARK: Methods
        float Evaluate(IList<PoseEvent> poses);
    }
}