using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial interface IAffordance
    {
        // MARK: Variables
        AudioClip Audio { get; }
        Color Color { get; }
        Material Material { get; }
        string Text { get; }

        // MARK: Methods
        void Run();
    }
}