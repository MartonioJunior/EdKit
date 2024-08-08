using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    public partial class AffordanceBehaviour
    {
        // MARK: Variables
        AffordanceEffect currentEffect;
        [SerializeField] AffordanceEffect defaultEffect;
        [SerializeField] float timer;

        // MARK: Events
        [Header("Events")]
        [SerializeField] UnityEvent<AudioClip> onAudio;
        [SerializeField] UnityEvent<Material> onVisual;
        [SerializeField] UnityEvent<string> onText;
        [SerializeField] UnityEvent<AffordanceEffect> onEffect;

        // MARK: Methods
        public void Apply(AffordanceData affordance)
        {
            Apply(affordance, defaultEffect);
        }

        public void Apply(AffordanceData affordance, AffordanceEffect effect)
        {
            onAudio?.Invoke(affordance.AudioFeedback);
            onVisual?.Invoke(affordance.VisualFeedback);
            onText?.Invoke(affordance.TextFeedback);

            currentEffect = effect;
            timer = effect.Duration;
        }

        public void SetDefaultEffect(AffordanceEffect effect)
        {
            defaultEffect = effect;
        }

        public void Stop()
        {
            timer = 0;
        }

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