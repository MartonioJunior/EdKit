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
        /**
        <summary>Action to be executed when an effect is applied.</summary>
        */
        Action<AffordanceEffect> onAffordance;
        /**
        <summary>Function to retrieve an audio feedback to be applied.</summary>
        */
        Func<Audio> audioFunction;
        /**
        <summary>Function to retrieve a visual feedback to be applied.</summary>
        */
        Func<Visual> visualFunction;
        /**
        <summary>Function to retrieve a textual feedback to be applied.</summary>
        */
        Func<Text> textFunction;

        // MARK: Initalizers
        /**
        <summary>Initializes a new affordance.</summary>
        <param name="updateAction">Action to be executed when an effect is applied.</param>
        <param name="audioFunction">Function to retrieve an audio feedback to be applied.</param>
        <param name="visualFunction">Function to retrieve a visual feedback to be applied.</param>
        <param name="textFunction">Function to retrieve a textual feedback to be applied.</param>
        */
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