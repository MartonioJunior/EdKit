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

        // MARK: Events
        [SerializeField] Event<AudioClip> audioEvent = new();
        [SerializeField] Event<string> textEvent = new();
        [SerializeField] Event<Material> visualEvent = new();

        // MARK: Methods
        private void PlayAffordance(AffordanceData affordance, AffordanceEffect effect)
        {
            affordance.UpdateAffordance(effect);
            audioEvent.OnUpdate(affordance.AudioFeedback);
            textEvent.OnUpdate(affordance.TextFeedback);
            visualEvent.OnUpdate(affordance.VisualFeedback);
        }

        public void Receive(AffordanceData affordance, AffordanceEffect effect)
        {
            if (runningAffordances.ContainsKey(affordance)) return;

            runningAffordances.Add(affordance, effect);
            audioEvent.OnTrigger(affordance.AudioFeedback);
            textEvent.OnTrigger(affordance.TextFeedback);
            visualEvent.OnTrigger(affordance.VisualFeedback);
        }

        public void Remove(AffordanceData affordance)
        {
            if (!runningAffordances.ContainsKey(affordance)) return;

            runningAffordances.Remove(affordance);
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

                debug += $"{gameObject.name} | {affordance.name} - {effect.Duration}\n";
                
                PlayAffordance(affordance, effect.WithDuration(deltaTime));

                if (effect.Duration < deltaTime) {
                    keysToRemove.Add(affordance);
                    audioEvent.OnStop(affordance.AudioFeedback);
                    visualEvent.OnStop(affordance.VisualFeedback);
                    textEvent.OnStop(affordance.TextFeedback);
                    continue;
                }

                runningAffordances[affordance] = effect.WithDuration(effect.Duration - deltaTime);
            }

            foreach (var affordance in keysToRemove) {
                runningAffordances.Remove(affordance);
            }

            if (debug != "") Debug.Log(debug);
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