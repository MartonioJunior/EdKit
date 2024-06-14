using System.Collections.Generic;

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