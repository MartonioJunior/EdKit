using System;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial struct Affordance
    {
        // MARK: Variables
        Action customAction;
        Func<AudioClip> audioFunction;
        Func<Color> colorFunction;
        Func<Material> materialFunction;
        Func<string> textFunction;

        // MARK: Initalizers
        public Affordance(Action customAction, Func<AudioClip> audioFunction, Func<Color> colorFunction, Func<Material> materialFunction, Func<string> textFunction)
        {
            this.customAction = customAction;
            this.audioFunction = audioFunction;
            this.colorFunction = colorFunction;
            this.materialFunction = materialFunction;
            this.textFunction = textFunction;
        }
    }

    #region IAffordance Implementation
    public partial struct Affordance : IAffordance
    {
        // MARK: Properties
        public AudioClip Audio => audioFunction?.Invoke();
        public Color Color => colorFunction?.Invoke() ?? Color.white;
        public Material Material => materialFunction?.Invoke();
        public string Text => textFunction?.Invoke() ?? string.Empty;

        // MARK: Methods
        public void Run() => customAction?.Invoke();
    }
    #endregion
}