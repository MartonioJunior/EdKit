using System;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Structure that defines an affordance.</summary>
    <typeparam name="Audio">Type of audio feedback.</typeparam>
    <typeparam name="Visual">Type of visual feedback.</typeparam>
    <typeparam name="Text">Type of textual feedback.</typeparam>
    */
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
        /**
        <inheritdoc cref="IAffordance{Text,Audio,Visual}.TextFeedback"/>
        */
        public Text TextFeedback => textFunction.Invoke();
        /**
        <inheritdoc cref="IAffordance{Text,Audio,Visual}.VisualFeedback"/>
        */
        public Visual VisualFeedback => visualFunction.Invoke();
        /**
        <inheritdoc cref="IAffordance{Text,Audio,Visual}.AudioFeedback"/>
        */
        public Audio AudioFeedback => audioFunction.Invoke();
        /**
        <inheritdoc cref="IAffordance{Text,Audio,Visual}.UpdateAffordance"/>
        */
        public void UpdateAffordance(AffordanceEffect effect) => onAffordance?.Invoke(effect);
    }
    #endregion
}