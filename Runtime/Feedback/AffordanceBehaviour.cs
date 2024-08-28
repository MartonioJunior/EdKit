using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    /**
    <summary>Component used to execute <c>AffordanceData</c> elements.</summary>
    */
    public partial class AffordanceBehaviour
    {
        // MARK: Variables
        /**
        <summary>Current effect being executed.</summary>
        */
        AffordanceEffect currentEffect;
        /**
        <summary>Default effect to be executed if no effect is supplied alongside <c>AffordanceData</c> object.</summary>
        */
        [SerializeField] AffordanceEffect defaultEffect;
        /**
        <summary>Timer for the current effect.</summary>
        */
        [SerializeField] float timer;

        // MARK: Events
        /**
        <summary>Event to notify about an audio clip to be applied.</summary>
        */
        [Header("Events")]
        [SerializeField] UnityEvent<AudioClip> onAudio;
        /**
        <summary>Event to notify about a material to be applied.</summary>
        */
        [SerializeField] UnityEvent<Material> onVisual;
        /**
        <summary>Event to notify about a text to be applied.</summary>
        */
        [SerializeField] UnityEvent<string> onText;
        /**
        <summary>Event to notify about an effect to be applied.</summary>
        */
        [SerializeField] UnityEvent<AffordanceEffect> onEffect;

        // MARK: Methods
        /**
        <summary>Applies an affordance to the component.</summary>
        <param name="affordance">Affordance to be applied.</param>
        */
        public void Apply(AffordanceData affordance)
        {
            Apply(affordance, defaultEffect);
        }
        /**
        <inheritdoc cref="Apply(AffordanceData)"/>
        <param name="effect">Effect to be applied.</param>
        */
        public void Apply(AffordanceData affordance, AffordanceEffect effect)
        {
            onAudio?.Invoke(affordance.AudioFeedback);
            onVisual?.Invoke(affordance.VisualFeedback);
            onText?.Invoke(affordance.TextFeedback);

            currentEffect = effect;
            timer = effect.Duration;
        }
        /**
        <summary>Sets the default effect to be executed with an affordance.</summary>
        <param name="effect">Default effect to be executed.</param>
        */
        public void SetDefaultEffect(AffordanceEffect effect)
        {
            defaultEffect = effect;
        }
        /**
        <summary>Stops the current affordance in execution.</summary>
        */
        public void Stop()
        {
            timer = 0;
        }
        /**
        <summary>Updates the component with a given effect.</summary>
        <param name="deltaTime">Time elapsed since the last update.</param>
        */
        public void Tick(float deltaTime)
        {
            if (timer <= 0) return;

            deltaTime = Mathf.Min(deltaTime, timer);
            onEffect?.Invoke(currentEffect.WithDuration(deltaTime));
            timer -= deltaTime;
        }
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Affordance Receiver")]
    public partial class AffordanceBehaviour: MonoBehaviour
    {
        void Update()
        {
            Tick(Time.deltaTime);
        }
    }
    #endregion
}