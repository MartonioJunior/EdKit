using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    public partial class AffordanceReceiver
    {
        // MARK: Variables
        [SerializeField] AffordanceEffect defaultEffect;
        Dictionary<AffordanceData, AffordanceEffect> runningAffordances = new();

        // MARK: Properties
        public AffordanceEffect DefaultEffect
        {
            get => defaultEffect;
            set => defaultEffect = value;
        }

        // MARK: Events
        [SerializeField] Event<AudioClip> audioEvent = new();
        [SerializeField] Event<string> textEvent = new();
        [SerializeField] Event<Material> visualEvent = new();
        [SerializeField] Event<float> alignmentEvent = new();
        [SerializeField] Event<float> scaleEvent = new();

        // MARK: Methods
        private void PlayAffordance(AffordanceData affordance, AffordanceEffect effect)
        {
            affordance.UpdateAffordance(effect);
            alignmentEvent.OnUpdate(effect.Alignment);
            scaleEvent.OnUpdate(effect.Scale);
            audioEvent.OnUpdate(affordance.AudioFeedback);
            textEvent.OnUpdate(affordance.TextFeedback);
            visualEvent.OnUpdate(affordance.VisualFeedback);
        }

        public void Receive(AffordanceData affordance, AffordanceEffect effect)
        {
            if (affordance == null || runningAffordances.ContainsKey(affordance)) return;

            runningAffordances.Add(affordance, effect);
            alignmentEvent.OnTrigger(effect.Alignment);
            scaleEvent.OnTrigger(effect.Scale);
            audioEvent.OnTrigger(affordance.AudioFeedback);
            textEvent.OnTrigger(affordance.TextFeedback);
            visualEvent.OnTrigger(affordance.VisualFeedback);
        }

        public void Remove(AffordanceData affordance)
        {
            if (affordance == null || !runningAffordances.ContainsKey(affordance)) return;

            runningAffordances.Remove(affordance);
            alignmentEvent.OnStop(defaultEffect.Alignment);
            scaleEvent.OnStop(defaultEffect.Scale);
            audioEvent.OnStop(affordance.AudioFeedback);
            visualEvent.OnStop(affordance.VisualFeedback);
            textEvent.OnStop(affordance.TextFeedback);
        }

        public void UpdateQueue(float deltaTime)
        {
            var keysToRemove = new List<AffordanceData>();
            var keysToAnalyze = new List<AffordanceData>(runningAffordances.Keys);
            string debug = "";

            foreach (var affordance in keysToAnalyze) {
                var effect = runningAffordances[affordance];

                // debug += $"{gameObject.name} | {affordance.name} - {effect.Duration}\n";
                
                PlayAffordance(affordance, effect.WithDuration(deltaTime));

                if (effect.Duration < deltaTime) {
                    keysToRemove.Add(affordance);
                    continue;
                }

                runningAffordances[affordance] = effect.WithDuration(effect.Duration - deltaTime);
            }

            foreach (var affordance in keysToRemove) {
                runningAffordances.Remove(affordance);
            }

            // if (debug != "") Debug.Log(debug);
        }
    }

    #region AffordanceReceiver.Event
    public partial class AffordanceReceiver
    {
        [System.Serializable]
        public class Event<T>
        {
            [SerializeField] UnityEvent<T> onTrigger = new();
            [SerializeField] UnityEvent<T> onUpdate = new();
            [SerializeField] UnityEvent<T> onStop = new();

            public void OnTrigger(T value) => onTrigger.Invoke(value);
            public void OnUpdate(T value) => onUpdate.Invoke(value);
            public void OnStop(T value) => onStop.Invoke(value);
        }
    }
    #endregion

    #region MonoBehaviour Implementation
    public partial class AffordanceReceiver: MonoBehaviour
    {
        void Update()
        {
            UpdateQueue(Time.deltaTime);
        }
    }
    #endregion

    #region UnityEvent Bindings
    public partial class AffordanceReceiver
    {
        public void Receive(AffordanceData affordance) => Receive(affordance, defaultEffect);
    }
    #endregion
}