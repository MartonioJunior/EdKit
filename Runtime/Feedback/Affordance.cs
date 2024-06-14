using System;
using UnityEngine;

namespace MartonioJunior.EdKit
{
    public partial struct Affordance<Text,Audio,Visual>
    {
        // MARK: Variables
        Action<AffordanceEffect> onAffordance;
        Func<Audio> audioFunction;
        Func<Visual> visualFunction;
        Func<Text> textFunction;

        // MARK: Initalizers
        public Affordance(Action<AffordanceEffect> updateAction, Func<Audio> audioFunction, Func<Visual> visualFunction, Func<Text> textFunction)
        {
            this.onAffordance = updateAction;
            this.audioFunction = audioFunction;
            this.visualFunction = visualFunction;
            this.textFunction = textFunction;
        }
    }

    #region IAffordance Implementation
    public partial struct Affordance<Text,Audio,Visual>: IAffordance<Text,Audio,Visual>
    {
        public Text TextFeedback => textFunction.Invoke();
        public Visual VisualFeedback => visualFunction.Invoke();
        public Audio AudioFeedback => audioFunction.Invoke();

        public void Update(AffordanceEffect effect) => onAffordance?.Invoke(effect);
    }
    #endregion
}