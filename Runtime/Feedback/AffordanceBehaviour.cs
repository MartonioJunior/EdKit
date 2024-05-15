using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    public partial class AffordanceBehaviour
    {
        // MARK: Events
        [SerializeField] UnityEvent<AudioClip> onAudio;
        [SerializeField] UnityEvent<Color> onColor;
        [SerializeField] UnityEvent<Material> onMaterial;
        [SerializeField] UnityEvent<string> onText;
        [SerializeField] UnityEvent<AffordanceEffect> onAffordance;

        // MARK: Methods
        public void Apply(IAffordance affordance, AffordanceEffect effect)
        {
            onAudio?.Invoke(affordance.Audio);
            onColor?.Invoke(affordance.Color);
            onMaterial?.Invoke(affordance.Material);
            onText?.Invoke(affordance.Text);
            onAffordance?.Invoke(effect);
        }
    }

    #region MonoBehaviour Implementation
    [AddComponentMenu("EdKit/Affordance Receiver")]
    public partial class AffordanceBehaviour: MonoBehaviour
    {
        
    }
    #endregion
}