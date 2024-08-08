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

        // MARK: Properties
        public string Description {
            get => description;
            set => description = value;
        }

        public AudioClip Clip {
            get => clip;
            set => clip = value;
        }

        public Material Material {
            get => material;
            set => material = value;
        }
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

        public void UpdateAffordance(AffordanceEffect effect) => customAction?.Invoke(effect);
    }
    #endregion
}