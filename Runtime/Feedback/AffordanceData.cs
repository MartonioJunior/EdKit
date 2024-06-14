using UnityEngine;
using UnityEngine.Events;

namespace MartonioJunior.EdKit
{
    public partial class AffordanceData
    {
        // MARK: Variables
        [SerializeField] string description;
        [SerializeField] AudioClip clip;
        [SerializeField] Material material;
        [SerializeField] UnityEvent<AffordanceEffect> customAction;
    }

    #region ScriptableObject Implementation
    [CreateAssetMenu(fileName = "New Affordance", menuName = "EdKit/Affordance")]
    public partial class AffordanceData: ScriptableObject {}
    #endregion

    #region IAffordance Implementation
    public partial class AffordanceData: IAffordance<string, AudioClip, Material>
    {
        public string TextFeedback => description;
        public AudioClip AudioFeedback => clip;
        public Material VisualFeedback => material;

        public void Update(AffordanceEffect effect) => customAction?.Invoke(effect);
    }
    #endregion
}